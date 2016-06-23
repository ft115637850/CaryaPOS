using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class PosMainViewModel : ViewModelBase
    {
        public SaleListViewModel SaleList { get; set; }
        public List<SaleListItemViewModel> SaleListItems { get; set; }
        public List<CategoryViewModel> GoodsCategoriesInfo { get; set; }
        private RelayCommand addGoodsCommand;
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
        public PosMainViewModel(List<CategoryViewModel> goodsCategories, SaleListViewModel saleList, List<SaleListItemViewModel> saleListItems)
        {
            this.GoodsCategoriesInfo = goodsCategories;
            this.SaleList = saleList;
            this.SaleListItems = saleListItems;
        }

        private void AddGoods(object goods)
        {
            var s = goods;
        }
    }
}
