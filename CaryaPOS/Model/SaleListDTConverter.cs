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
        public static List<SaleListViewModel> GetModel(DataTable dtData)
        {            
            var items = new List<SaleListViewModel>();
            foreach (DataRow row in dtData.Rows)
            {
                Guid sheetid;
                Guid.TryParse(Convert.ToString(row["SheetID"]), out sheetid);
                var item = new SaleListViewModel
                {
                    SheetID = sheetid,
                    CashierID = Convert.ToString(row["CashierID"]),
                    PayValue = Convert.ToDecimal(row["PayValue"]),
                    SaleValue = Convert.ToDecimal(row["SaleValue"]),
                    DiscValue = Convert.ToDecimal(row["DiscValue"]),
                    Change = Convert.ToDecimal(row["PayValue"]) - (Convert.ToDecimal(row["SaleValue"]) - Convert.ToDecimal(row["DiscValue"]))
                };
                items.Add(item);
            }

            return items;
        }
    }
}
