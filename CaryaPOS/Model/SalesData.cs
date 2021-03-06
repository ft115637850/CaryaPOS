﻿using CaryaPOS.Dao;
using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class SalesData
    {
        private SalesDBDao salesDBDao;
        private LocalDBDao localDBDao;
        public SalesData()
        {
            salesDBDao = new SalesDBDao();
            localDBDao = new LocalDBDao();
        }

        public SaleListViewModel GetCurrentSaleList()
        {
            SaleListViewModel currentSaleList = null;
            var sales = salesDBDao.GetOnGoingSaleList();
            if (sales.Rows.Count == 0)
            {
                currentSaleList = new SaleListViewModel()
                {
                    SheetID = Guid.NewGuid(),
                    Change = 0,
                    PayValue = 0,
                    SaleValue = 0,
                    DiscValue = 0,
                    //TO DO: update to login user
                    CashierID = "Tester",
                    ShopID = "A001"
                };
                salesDBDao.NewSaleList(
                    currentSaleList.SheetID.ToString(),
                    currentSaleList.CashierID,
                    currentSaleList.ShopID,
                    currentSaleList.PayValue,
                    currentSaleList.SaleValue,
                    currentSaleList.DiscValue);
            }
            else
            {
                currentSaleList = SaleListDTConverter.GetModel(sales).First();
            }
            return currentSaleList;
        }

        public List<SaleListViewModel> GetOnHoldSaleList()
        {
            var salesOnHold = salesDBDao.GetOnHoldSaleList();
            return SaleListDTConverter.GetModel(salesOnHold);
        }

        public void HoldSaleList(string sheetID)
        {
            salesDBDao.HoldSaleList(sheetID);
        }

        public void UnHoldSaleList(string sheetID)
        {
            salesDBDao.UnHoldSaleList(sheetID);
        }

        public List<SaleListItemViewModel> GetSaleListItem(Guid sheetID)
        {
            return SaleListItemDTConverter.GetModel(salesDBDao.GetSaleListItems(sheetID.ToString())); ;
        }

        public SaleListItemViewModel AddGoods(int goodsID, SaleListViewModel saleList, ObservableCollection<SaleListItemViewModel> salelistItems)
        {
            var goods = localDBDao.GetGoods(goodsID);
            int newSeqID = 1;
            if (salelistItems.Count > 0)
            {
                newSeqID = salelistItems.Max(x => x.SeqID) + 1;
            }

            var newItem = new SaleListItemViewModel
            {
                SheetID = saleList.SheetID,
                SeqID = newSeqID,
                GoodsID = goodsID,
                GoodsName = goods.Rows[0]["shortname"].ToString(),
                BarcodeID = goods.Rows[0]["barcodeid"].ToString(),
                Quantity = 1,
                SalePrice = Convert.ToDecimal(goods.Rows[0]["Price"]),
                SaleValue = Convert.ToDecimal(goods.Rows[0]["Price"]) * 1,
                Cost = Convert.ToDecimal(goods.Rows[0]["cost"])
            };

            salelistItems.Add(newItem);
            this.UpdateSalesData(saleList, salelistItems);
            salesDBDao.AddSaleListItem(newItem.SheetID.ToString(), 
                newItem.SeqID,
                newItem.GoodsID,
                newItem.GoodsName,
                newItem.BarcodeID, 
                newItem.Quantity,
                newItem.Cost, 
                newItem.SalePrice, 
                newItem.SaleValue, 
                newItem.DiscValue);
            return newItem;
        }

        public void UpdateSalesData(SaleListViewModel saleList, ObservableCollection<SaleListItemViewModel> salelistItems)
        {
            this.CalcTotalValue(saleList, salelistItems);
            saleList.Change = saleList.PayValue - saleList.SaleValue + saleList.DiscValue;
            this.salesDBDao.UpdateSaleList(saleList.SheetID.ToString(), saleList.PayValue, saleList.SaleValue, saleList.DiscValue);
        }

        public void CalcTotalValue(SaleListViewModel saleList, ObservableCollection<SaleListItemViewModel> salelistItems)
        {
            //TO DO: Process sales promotion
            saleList.SaleValue = salelistItems.Sum(x => x.SaleValue);
            saleList.DiscValue = salelistItems.Sum(x => x.DiscValue);
        }

        public void MoveSaleListToHistory(string sheetID)
        {
            this.salesDBDao.MoveSaleListToHistory(sheetID);
        }

        public void DeleteSheet(string sheetID)
        {
            this.salesDBDao.DeleteSaleSheetData(sheetID);
        }

        public void DeleteSalelistItems(string sheetID, List<SaleListItemViewModel> deleteItems, SaleListViewModel saleList, ObservableCollection<SaleListItemViewModel> salelistItems)
        {
            List<int> seqs = new List<int>();
            foreach (var deleteItem in deleteItems)
            {
                seqs.Add(deleteItem.SeqID);
                salelistItems.Remove(deleteItem);
            }

            this.salesDBDao.DeleteSalelistItems(sheetID, seqs);
            this.UpdateSalesData(saleList, salelistItems);
        }
    }
}
