using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Helper
{
    class SalesDBHelper : DBHelper
    {
        private SalesDBHelper()
            : base("", "SalesDB.db")
        {
        }
    }
}
