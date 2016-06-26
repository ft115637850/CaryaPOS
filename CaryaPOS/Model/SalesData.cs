using CaryaPOS.Dao;
using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
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
            var currentSaleList = new SaleListViewModel();
            if (sales.Rows.Count > 0)
            {
                //Ongoing sheet
                var firstSale = sales.Rows[0];
                Guid sheetID;
                Guid.TryParse(Convert.ToString(firstSale["SheetID"]), out sheetID);
                currentSaleList.SheetID =sheetID;
                currentSaleList.Cashier = Convert.ToString(firstSale["Cashier"]);
                currentSaleList.PayValue = Convert.ToDecimal(firstSale["PayValue"]);
                currentSaleList.SaleValue = Convert.ToDecimal(firstSale["SaleValue"]);
                currentSaleList.DiscValue = Convert.ToDecimal(firstSale["DiscValue"]);
                currentSaleList.Change = currentSaleList.PayValue - (currentSaleList.SaleValue - currentSaleList.DiscValue);
            }
            else
            {
                //New sheet

            }
            return new SaleListViewModel { SheetID = Guid.NewGuid(), Cashier = "", Change = 1, PayValue = 2, SaleValue = 3 };
        }

        public List<SaleListItemViewModel> GetSaleListItem(Guid sheetID)
        {
            return new List<SaleListItemViewModel>();
        }

        public SaleListItemViewModel AddGoods(int goodsID)
        {
            var goods = localDBDao.GetGoods(goodsID);
            return new SaleListItemViewModel
            {
                GoodsID = goodsID,
                GoodsName = goods.Rows[0]["GoodsName"].ToString(),
                Quantity = 1,
                SaleValue = Convert.ToDecimal(goods.Rows[0]["Price"])
            };           
        }
    }
}
