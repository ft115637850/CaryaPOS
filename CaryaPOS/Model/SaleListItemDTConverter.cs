using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class SaleListItemDTConverter
    {
        public static List<SaleListItemViewModel> GetModel(DataTable dtData)
        {
            return new List<SaleListItemViewModel>();
        }
    }
}
