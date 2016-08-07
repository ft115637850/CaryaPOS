using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
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
            return this.GetData("select CategoryID,CategoryName from Category where levelid=1");
        }

        public DataTable GetGoodsCategoryInfo()
        {
            return this.GetData("select goodsid,shortname,categoryid/10000 categoryid from GoodsPrice");
        }

        public DataTable GetGoods(int goodsid)
        {
            return this.GetData("select goodsname,price,barcodeid,cost from GoodsPrice where goodsid=" + goodsid);
        }
    }
}
