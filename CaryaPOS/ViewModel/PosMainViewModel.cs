using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class PosMainViewModel : ViewModelBase
    {
        public CategoryViewModel SelectedCategory { get; set; }
        //TO DO: update to ObservableCollection
        public List<GoodsViewModel> GoodsShown { get; set; }
        public List<CategoryViewModel> GoodsCategoriesInfo { get; set; }
        public PosMainViewModel(List<CategoryViewModel> goodsCategories)
        {
            this.GoodsCategoriesInfo = goodsCategories;
            
            if (goodsCategories.Count > 0)
            {
                SelectedCategory = goodsCategories[0];
                //Find the longest list to initialize enough buttons
                foreach (var vm in goodsCategories)
                {
                    //Set the reference to PosMainViewModel
                    vm.MainViewModel = this;
                    if (vm.GoodsList.Count > this.SelectedCategory.GoodsList.Count)
                    {
                        SelectedCategory = vm;
                    }
                }
                SelectedCategory.IsChecked = true;
                GoodsShown = SelectedCategory.GoodsList.Select(item => (GoodsViewModel)item.Clone()).ToList();
            }
        }
    }
}
