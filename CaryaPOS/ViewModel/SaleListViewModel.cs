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
        private decimal salevalue;
        private decimal payValue;
        private decimal change;
        private decimal discValue;

        public Guid SheetID { get; set; }
        public decimal SaleValue
        {
            get
            {
                return salevalue;
            }
            set
            {
                if (salevalue != value)
                {
                    salevalue = value;
                    RaisePropertyChanged("SaleValue");
                }
            }
        }

        public decimal DiscValue
        {
            get
            {
                return discValue;
            }
            set
            {
                if (discValue != value)
                {
                    discValue = value;
                    RaisePropertyChanged("DiscValue");
                }
            }
        }

        public decimal PayValue
        {
            get
            {
                return payValue;
            }
            set
            {
                if (payValue != value)
                {
                    payValue = value;
                    RaisePropertyChanged("PayValue");
                }
            }
        }

        public decimal Change
        {
            get
            {
                return change;
            }
            set
            {
                if (change != value)
                {
                    change = value;
                    RaisePropertyChanged("Change");
                }
            }
        }

        public string CashierID { get; set; }
        public string ShopID { get; set; }
    }
}
