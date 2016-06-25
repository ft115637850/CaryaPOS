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
