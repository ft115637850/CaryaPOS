using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class SaleListDTConverter
    {
        public static SaleListViewModel GetModel(DataTable dtData)
        {
            var model = new SaleListViewModel();
            Guid sheetID;
            if (dtData.Rows.Count > 0)
            {
                //Ongoing sheet
                var firstSale = dtData.Rows[0];

                Guid.TryParse(Convert.ToString(firstSale["SheetID"]), out sheetID);
                model.SheetID = sheetID;
                model.CashierID = Convert.ToString(firstSale["CashierID"]);
                model.PayValue = Convert.ToDecimal(firstSale["PayValue"]);
                model.SaleValue = Convert.ToDecimal(firstSale["SaleValue"]);
                model.DiscValue = Convert.ToDecimal(firstSale["DiscValue"]);
                model.Change = model.PayValue - (model.SaleValue - model.DiscValue);
            }
            else
            {
                //New sheet
                sheetID = Guid.NewGuid();
                model.SheetID = sheetID;
                
                model.Change = 0;
                model.PayValue = 0;
                model.SaleValue = 0;
                model.DiscValue = 0;
                //TO DO: update to login user
                model.CashierID = "Tester";
                model.ShopID = "A001";
            }

            return model;
        }
    }
}
