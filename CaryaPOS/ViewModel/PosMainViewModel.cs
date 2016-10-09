using CaryaPOS.Helper;
using CaryaPOS.Model;
using CaryaPOS.View;
using System;
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
        private RelayCommand exitCommand;
        private SalesData salesData;

        public SaleListViewModel SaleList { get; set; }
        public ObservableCollection<SaleListItemViewModel> SaleListItems { get; set; }
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

        public PosMainViewModel(List<CategoryViewModel> goodsCategories, SaleListViewModel saleList, List<SaleListItemViewModel> saleListItems)
        {
            this.GoodsCategoriesInfo = goodsCategories;
            this.SaleList = saleList;
            this.SaleListItems = new ObservableCollection<SaleListItemViewModel>(saleListItems);
            this.salesData = new SalesData();
        }

        private void AddGoods(object goods)
        {
            var goodsID = (int)goods;
            var newItem = this.salesData.AddGoods(goodsID, this.SaleList, this.SaleListItems);
            this.CurrentItem = newItem;
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
            var cashPayVM = new CashPayViewModel()
            {
                Purchase = this.SaleList.SaleValue - this.SaleList.DiscValue,
                PayIn = payIn,
                Change = this.SaleList.SaleValue - this.SaleList.DiscValue - this.SaleList.PayValue,
                InputAmount = inputAmount
            };

            var cashPayWin = new CashPay() { DataContext = cashPayVM };
            if (cashPayWin.ShowDialog() == true)
            {
                this.SaleList.PayValue = cashPayVM.PayIn;
                //TO DO: update the database
            }
        }

        private void Exit(object param)
        {
            Application.Current.Shutdown();
        }
    }
}
