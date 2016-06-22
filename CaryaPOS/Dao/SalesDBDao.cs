using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class SalesDBDao
    {
        private SalesDBHelper dbHelper;
        public SalesDBDao()
        {
            dbHelper = SalesDBHelper.GetInstance();
        }
    }
}
