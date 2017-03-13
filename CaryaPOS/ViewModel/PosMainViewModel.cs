using CaryaPOS.Helper;
using CaryaPOS.Model;
using CaryaPOS.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaryaPOS.ViewModel
{
    public class PosMainViewModel : ViewModelBase
    {
        private SaleListItemViewModel currentItem;
        private RelayCommand addGoodsCommand;
        private RelayCommand cashPayCommand;
        private RelayCommand cancelCommand;
        private RelayCommand deleteCommand;
        private RelayCommand exitCommand;
        private RelayCommand holdCommand;
        private SalesData salesData;

        public SaleListViewModel SaleList { get; set; }
        public ObservableCollection<SaleListItemViewModel> SaleListItems { get; set; }
        public ObservableCollection<SaleListViewModel> OnHoldSaleLists { get; set; }
        public List<CategoryViewModel> GoodsCategoriesInfo { get; set; }

        public SaleListItemViewModel CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                if (currentItem != value)
                {
                    currentItem = value;
                    RaisePropertyChanged("CurrentItem");
                }
            }
        }

        public RelayCommand AddGoodsCommand
        {
            get
            {
                if (addGoodsCommand == null)
                {
                    addGoodsCommand = new RelayCommand(AddGoods);
                }
                return addGoodsCommand;
            }
        }

        public RelayCommand CashPayCommand
        {
            get
            {
                if (cashPayCommand == null)
                {
                    cashPayCommand = new RelayCommand(CashPay);
                }
                return cashPayCommand;
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                if (cancelCommand==null)
                {
                    cancelCommand = new RelayCommand(CancelSheet);
                }
                return cancelCommand;
            }            
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                if (deleteCommand==null)
                {
                    deleteCommand = new RelayCommand(DeleteGoodsItem);
                }
                return deleteCommand;
            }
        }

        public RelayCommand HoldCommand
        {
            get
            {
                if (holdCommand == null)
                {
                    holdCommand = new RelayCommand(OnHold);
                }
                return holdCommand;
            }
        }
        
        public RelayCommand ExitCommand
        {
            get
            {
                if (exitCommand==null)
                {
                    exitCommand = new RelayCommand(Exit);
                }
                return exitCommand;
            }
        }

        public PosMainViewModel(List<CategoryViewModel> goodsCategories, SaleListViewModel saleList, List<SaleListItemViewModel> saleListItems, List<SaleListViewModel> onHoldSaleLists)
        {
            this.GoodsCategoriesInfo = goodsCategories;
            this.SaleList = saleList;
            this.SaleListItems = new ObservableCollection<SaleListItemViewModel>(saleListItems);
            this.salesData = new SalesData();
            this.OnHoldSaleLists = new ObservableCollection<SaleListViewModel>(onHoldSaleLists);
        }

        private void AddGoods(object goods)
        {
            var goodsID = (int)goods;
            var newItem = this.salesData.AddGoods(goodsID, this.SaleList, this.SaleListItems);
            this.CurrentItem = newItem;
        }

        private void DeleteGoodsItem(object param)
        {
            IList collection = param as IList;
            if (collection == null || collection.Count == 0)
            {
                return;
            }
            var deleteItems = collection.Cast<SaleListItemViewModel>().ToList<SaleListItemViewModel>();
            this.salesData.DeleteSalelistItems(this.SaleList.SheetID.ToString(), deleteItems, this.SaleList, this.SaleListItems);
        }

        private void CashPay(object param)
        {
            //Process promotion and get the total value
            this.salesData.CalcTotalValue(this.SaleList, this.SaleListItems);
            
            // Get the total payed records
            PayProcessor payProcessor = new CashPayProcessor(this.SaleList.SheetID.ToString());
            decimal payIn = payProcessor.GetTotalPayValue();
            decimal inputAmount = payProcessor.GetPayItemValue();

            // Bring up the payment UI
            var cashPayVM = new CashPayViewModel(payProcessor)
            {
                Purchase = this.SaleList.SaleValue - this.SaleList.DiscValue,
                OtherPayIn = payIn - inputAmount,
                NewPayIn = payIn,
                Change = this.SaleList.PayValue - (this.SaleList.SaleValue - this.SaleList.DiscValue),
                InputAmount = inputAmount.ToString()
            };

            var cashPayWin = new CashPay(cashPayVM);
            if (cashPayWin.ShowDialog() == true)
            {
                this.SaleList.PayValue = cashPayVM.NewPayIn;
                this.salesData.UpdateSalesData(this.SaleList, this.SaleListItems);
                if (cashPayVM.Change >= 0)
                {
                    salesData.MoveSaleListToHistory(this.SaleList.SheetID.ToString());
                    var saleList = salesData.GetCurrentSaleList();
                    this.SaleList.Copy(saleList);
                    this.SaleListItems.Clear(); 
                }
            }
        }

        private void CancelSheet(object param)
        {
            //TO DO: Add custom dialog window
            var isConfirmed = MessageBox.Show(Properties.Resources.CancelConfirmation, Properties.Resources.ConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (isConfirmed == MessageBoxResult.Yes)
            {
                salesData.DeleteSheet(this.SaleList.SheetID.ToString());
                var saleList = salesData.GetCurrentSaleList();
                this.SaleList.Copy(saleList);
                this.SaleListItems.Clear();
            }
            else
            {
                return;
            }
        }

        private void OnHold(object obj)
        {
            if (this.SaleListItems.Count > 0)
            {
                //If current sheet is not empty, hold current sheet.
                salesData.HoldSaleList(this.SaleList.SheetID.ToString());
                this.OnHoldSaleLists.Add(this.SaleList);
                var saleList = salesData.GetCurrentSaleList();
                this.SaleList.Copy(saleList);
                this.SaleListItems.Clear(); 
            }
            else if (this.OnHoldSaleLists.Count == 1)
            {
                //If current sheet is empty, get the on hold sheet.
                salesData.DeleteSheet(this.SaleList.SheetID.ToString());
                salesData.UnHoldSaleList(this.OnHoldSaleLists.First().SheetID.ToString());
                this.OnHoldSaleLists.RemoveAt(0);
                var saleList = salesData.GetCurrentSaleList();
                this.SaleList.Copy(saleList);
                var saleListItems = salesData.GetSaleListItem(saleList.SheetID);
                foreach (var item in saleListItems)
                {
                    this.SaleListItems.Add(item);
                }
            }
            else if (this.OnHoldSaleLists.Count > 1)
            {
                //If there are more than 1 on hold sheet, get the on hold sheets list for selection.
            }
        }

        private void Exit(object param)
        {
            Application.Current.Shutdown();
        }
    }
}
