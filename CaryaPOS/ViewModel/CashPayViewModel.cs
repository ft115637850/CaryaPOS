using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class CashPayViewModel : ViewModelBase
    {
        private decimal payIn;
        private decimal change;
        private decimal inputAmount;
        public decimal Purchase { get; set; }
        public decimal PayIn
        {
            get
            {
                return payIn;
            }
            set
            {
                if (payIn != value)
                {
                    payIn = value;
                    RaisePropertyChanged("PayIn");
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

        public decimal InputAmount
        {
            get
            {
                return inputAmount;
            }
            set
            {
                if (inputAmount != value)
                {
                    inputAmount = value;
                    RaisePropertyChanged("InputAmount");
                }
            }
        }
    }
}
