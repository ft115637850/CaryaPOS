using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class CashPayViewModel : ViewModelBase
    {
        private decimal newPayIn;
        private decimal change;
        private decimal inputAmount;
        private RelayCommand cancelCommand;
        internal event EventHandler CloseCashPayWindow;

        public decimal Purchase { get; set; }
        public decimal OldPayIn
        {
            get;
            set;  
        }

        public decimal NewPayIn
        {
            get
            {
                return newPayIn;
            }
            set
            {
                if (newPayIn != value)
                {
                    newPayIn = value;
                    RaisePropertyChanged("NewPayIn");
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
                    this.NewPayIn = this.OldPayIn + this.inputAmount;
                    this.Change = this.Purchase - this.newPayIn;
                }
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(Cancel);
                }
                return cancelCommand;
            }
        }

        private void Cancel(object param)
        {
            this.CloseCashPayWindow(this, null);
        }
    }
}
