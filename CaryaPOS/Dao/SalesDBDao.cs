using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class SalesDBDao : BaseDao
    {
        public SalesDBDao()
            : base(SalesDBHelper.GetInstance())
        {
        }

        public DataTable GetOngoingSaleList()
        {
            return this.GetData("select * from SALELIST where STATUSFLAG=0");
        }

        public void NewSaleList(string sheetID, string cashierID, string shopID, decimal payValue, decimal saleValue, decimal discValue)
        {
            this.ExecuteNonQuery("insert into SALELIST (SHEETID,SALETIME,CASHIERID,SHOPID,PAYVALUE,SALEVALUE,DISCVALUE,STATUSFLAG) values ('"
                + sheetID + "','"
                + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','"
                + cashierID + "','"
                + shopID + "',"
                + payValue + ","
                + saleValue + ","
                + discValue + ",0)");
        }
    }
}
