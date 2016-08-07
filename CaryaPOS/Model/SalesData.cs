using CaryaPOS.Dao;
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
            var sales = salesDBDao.GetOngoingSaleList();
            var currentSaleList = SaleListDTConverter.GetModel(sales);
            if (sales.Rows.Count == 0)
            {
                salesDBDao.NewSaleList(
                    currentSaleList.SheetID.ToString(),
                    currentSaleList.CashierID,
                    currentSaleList.ShopID,
                    currentSaleList.PayValue,
                    currentSaleList.SaleValue,
                    currentSaleList.DiscValue);
            }

            return currentSaleList;
        }

        public List<SaleListItemViewModel> GetSaleListItem(Guid sheetID)
        {
            return new List<SaleListItemViewModel>();
        }

        public SaleListItemViewModel AddGoods(int goodsID, SaleListViewModel saleList, ObservableCollection<SaleListItemViewModel> salelistItems)
        {
            var goods = localDBDao.GetGoods(goodsID);
            var newItem = new SaleListItemViewModel
            {
                GoodsID = goodsID,
                GoodsName = goods.Rows[0]["GoodsName"].ToString(),
                BarcodeID = goods.Rows[0]["barcodeid"].ToString(),
                Quantity = 1,
                Price = Convert.ToDecimal(goods.Rows[0]["Price"]),
                SaleValue = Convert.ToDecimal(goods.Rows[0]["Price"]) * 1,
                Cost = Convert.ToDecimal(goods.Rows[0]["cost"]),
            };

            salelistItems.Add(newItem);
            this.UpdateSalesData(saleList, salelistItems);
            salesDBDao.AddSaleListItem(newItem.SheetID.ToString(), 
                newItem.GoodsID,
                newItem.BarcodeID, 
                newItem.Quantity,
                newItem.Cost, 
                newItem.Price, 
                newItem.SaleValue, 
                newItem.DiscValue);
            return newItem;
        }

        public void UpdateSalesData(SaleListViewModel saleList, ObservableCollection<SaleListItemViewModel> salelistItems)
        {
            //TO DO: Process sales promotion
            saleList.SaleValue = salelistItems.Sum(x => x.SaleValue);
            saleList.DiscValue = salelistItems.Sum(x => x.DiscValue);
            //TO DO: Process Payment
            saleList.PayValue = 0;
            this.salesDBDao.UpdateSaleList(saleList.SheetID.ToString(), saleList.PayValue, saleList.SaleValue, saleList.DiscValue);
        }
    }
}
