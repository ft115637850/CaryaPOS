using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class CategoryDao
    {
        private LocalDBHelper dbHelper;
        public CategoryDao()
        {
            dbHelper = new LocalDBHelper();
        }

        public DataTable GetLevel1Categories()
        {
            using (var cnn = dbHelper.GetConnection())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select CategoryID,CategoryName from Category where levelid=1";
                cnn.Open();
                var rdr = cmd.ExecuteReader();
                var level1Categories = new DataTable();
                level1Categories.Load(rdr, LoadOption.OverwriteChanges);
                cnn.Close();
                return level1Categories;
            }
        }
    }
}
