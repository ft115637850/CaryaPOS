﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    class CashPayProcessor : PayProcessor
    {
        private const int payTypeID = 1;
        private const string payName = "Cash";  //TO DO: Localization

        public CashPayProcessor(string sheetID)
            : base(sheetID)
        {

        }

        public override decimal GetPayItemValue()
        {
            return this.salesDBDao.GetPayItemValue(this.sheetID, CashPayProcessor.payTypeID);
        }

        public override void UpdatePayRecord(decimal payValue)
        {
            int record = this.salesDBDao.UpdatePayItemValue(this.sheetID, CashPayProcessor.payTypeID, payValue);
            if (record < 1)
            {
                this.salesDBDao.AddPayItem(this.sheetID, 0, CashPayProcessor.payTypeID, CashPayProcessor.payName, "RMB", 1, payValue, payValue, null, 0, 0, 0, null);
            }
        }
    }
}
