using CaryaPOS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    abstract class PayProcessor
    {
        protected SalesDBDao salesDBDao;
        protected string sheetID;
        

        public PayProcessor(string currentSheetID)
        {
            this.sheetID = currentSheetID;
            this.salesDBDao = new SalesDBDao();
        }

        public decimal GetTotalPayValue()
        {
            return salesDBDao.GetTotalPayValue(this.sheetID);
        }

        public abstract decimal GetPayItemValue();
        public abstract void UpdatePayRecord(decimal payValue);
    }
}
