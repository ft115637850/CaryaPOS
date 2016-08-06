using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class SaleListViewModel : ViewModelBase
    {
        public Guid SheetID { get; set; }
        public decimal SaleValue { get; set; }
        public decimal DiscValue { get; set; }
        public decimal PayValue { get; set; }
        public decimal Change { get; set; }
        public string CashierID { get; set; }
        public string ShopID { get; set; }
    }
}
