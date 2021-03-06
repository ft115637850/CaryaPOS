﻿using CaryaPOS.Helper;
using CaryaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class CashPayViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private decimal newPayIn;
        private decimal change;
        private decimal inputValue;
        private string inputAmount;
        private RelayCommand cancelCommand;
        private RelayCommand confirmCommand;
        private PayProcessor payProcessor;
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        internal event EventHandler CancelCloseCashPayWindow;
        internal event EventHandler ConfirmCloseCashPayWindow;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                // Indicate whether the entire Product object is error-free.
                return (errors.Count > 0);
            }
        }

        public decimal Purchase { get; set; }
        public decimal OtherPayIn
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

        public string InputAmount
        {
            get
            {
                return this.inputAmount;
            }
            set
            {
                if (this.inputAmount != value)
                {
                    this.inputAmount = value;
                    bool result = decimal.TryParse(value, out this.inputValue);
                    if (result)
                    {
                        ClearErrors("InputAmount");
                        RaisePropertyChanged("InputAmount");
                        this.NewPayIn = this.OtherPayIn + this.inputValue;
                        this.Change = this.newPayIn - this.Purchase;
                    }
                    else
                    {
                        List<string> errors = new List<string>();
                        errors.Add("The InputAmount should be number.");
                        SetErrors("InputAmount", errors);
                    }
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

        public RelayCommand ConfirmCommand
        {
            get
            {
                if (confirmCommand==null)
                {
                    confirmCommand = new RelayCommand(Confirm, (obj) => { return !this.HasErrors; });
                }

                return confirmCommand;
            }
        }

        internal CashPayViewModel(PayProcessor processor)
        {
            this.payProcessor = processor;
        }

        private void Cancel(object param)
        {
            this.CancelCloseCashPayWindow(this, null);
        }

        private void Confirm(object param)
        {
            this.payProcessor.UpdatePayRecord(this.inputValue);
            this.ConfirmCloseCashPayWindow(this, null);
        }

        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            // Clear any errors that already exist for this property.
            errors.Remove(propertyName);
            // Add the list collection for the specified property.
            errors.Add(propertyName, propertyErrors);
            // Raise the error-notification event.
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            // Remove the error list for this property.
            errors.Remove(propertyName);
            // Raise the error-notification event.
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // Provide all the error collections.
                return (errors.Values);
            }
            else
            {
                // Provice the error collection for the requested property
                // (if it has errors).
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }        
    }
}
