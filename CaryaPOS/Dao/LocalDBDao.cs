using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class LocalDBDao
    {
        private LocalDBHelper dbHelper;
        public LocalDBDao()
        {
            dbHelper = LocalDBHelper.GetInstance();
        }

        public DataTable GetLevel1Categories()
        {
            return this.GetData("select CategoryID,CategoryName from Category where levelid=1");
        }

        public DataTable GetGoodsCategoryInfo()
        {
            return this.GetData("select goodsid,shortname,categoryid/10000 categoryid from GoodsPrice");
        }

        private DataTable GetData(string sqlTxt)
        {
            using (var cnn = dbHelper.GetConnection())
            {
                try
                {
                    var cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlTxt;
                    cnn.Open();
                    var rdr = cmd.ExecuteReader();
                    var data = new DataTable();
                    data.Load(rdr, LoadOption.OverwriteChanges);
                    cnn.Close();
                    return data;
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        
    }
}
