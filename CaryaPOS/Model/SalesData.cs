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
        private SalesDBDao dao;
        public SalesData()
        {
            dao = new SalesDBDao();
        }

        public SaleListViewModel GetCurrentSaleList()
        {
            return new SaleListViewModel { Cashier = "", Change = 1, PayValue = 2, SaleValue = 3 };
        }

        public List<SaleListItemViewModel> GetSaleListItem(Guid sheetID)
        {
            return null;
        }
    }
}
