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
        public PosMainViewModel(List<CategoryViewModel> goodsCategories)
        {
            this.GoodsCategoriesInfo = goodsCategories;
        }

        private void AddGoods(object goods)
        {
            var s = goods;
        }
    }
}
