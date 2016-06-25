using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class BaseDao
    {
        private DBHelper dbHelper;
        public BaseDao(DBHelper myDBHelper)
        {
            this.dbHelper = myDBHelper;
        }
        protected DataTable GetData(string sqlTxt)
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
                    return data;
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        protected T GetSingleValue<T>(string sqlTxt)
        {
            using (var cnn = dbHelper.GetConnection())
            {
                try
                {
                    var cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlTxt;
                    cnn.Open();
                    var data = (T)cmd.ExecuteScalar();
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
