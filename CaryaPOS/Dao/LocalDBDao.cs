using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class LocalDBDao : BaseDao
    {
        public LocalDBDao()
            : base(LocalDBHelper.GetInstance())
        {
        }

        public DataTable GetLevel1Categories()
        {
            return this.GetData("select CategoryID,CategoryName from Category where levelid=1", null);
        }

        public DataTable GetGoodsCategoryInfo()
        {
            return this.GetData("select goodsid,shortname,categoryid/10000 categoryid from GoodsPrice", null);
        }

        public DataTable GetGoods(int goodsid)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@goodsid", DbType.Int32)
            };

            parms[0].Value = goodsid;
            return this.GetData("select shortname,price,barcodeid,cost from GoodsPrice where goodsid=@goodsid", parms);
        }
    }
}
