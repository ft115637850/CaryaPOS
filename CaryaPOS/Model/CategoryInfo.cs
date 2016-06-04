using CaryaPOS.Dao;
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
        private CategoryDao dao;
        public CategoryInfo()
        {
            dao = new CategoryDao();
        }

        public DataTable GetLevel1Categories()
        {
            return dao.GetLevel1Categories();
        }
    }
}
