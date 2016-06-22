using CaryaPOS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class SaleListData
    {
        private SalesDBDao dao;
        public SaleListData()
        {
            dao = new SalesDBDao();
        }
    }
}
