using CaryaPOS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class CategoryInfo
    {
        public DataTable GetLevel1Categories()
        {
            using (var cnn = DBHelper.GetConnection(LocalDBType.LocalDB))
            {
                cnn.Open();
            }
            return null;
        }
    }
}
