using CaryaPOS.Helper;
using CaryaPOS.Model;
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
        private RelayCommand payCommand;
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

        public RelayCommand PayCommand
        {
            get
            {
                if (payCommand == null)
                {
                    payCommand = new RelayCommand(Pay);
                }
                return payCommand;
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

        private void Pay(object param)
        {
            // Get the total value
            this.salesData.CalcTotalValue(this.SaleList, this.SaleListItems);

            // Get the total payed records


            // Bring up the payment UI
        }

        private void Exit(object param)
        {
            Application.Current.Shutdown();
        }
    }
}
