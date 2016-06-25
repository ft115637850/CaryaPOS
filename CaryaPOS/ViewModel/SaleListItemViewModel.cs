using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class SaleListItemViewModel : ViewModelBase
    {
        public Guid SheetID { get; set; }
        public int GoodsID { get; set; }
        public string GoodsName { get; set; }
        public decimal Quantity { get; set; }
        public decimal SaleValue { get; set; }
    }
}
