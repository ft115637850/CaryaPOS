using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    class PosMainViewModel : ViewModelBase
    {
        public List<CategoryViewModel> GoodsCategoriesInfo { get; set; }
        public PosMainViewModel(List<CategoryViewModel> goodsCategories)
        {
            this.GoodsCategoriesInfo = goodsCategories;
        }
    }
}
