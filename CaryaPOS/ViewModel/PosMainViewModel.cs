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
        public List<GoodsViewModel> Goods { get; set; }
        public PosMainViewModel(List<CategoryViewModel> goodsCategories)
        {
            this.GoodsCategoriesInfo = goodsCategories;
            if (goodsCategories.Count > 0)
            {
                this.Goods = goodsCategories[0].GoodsList;
            }
        }
    }
}
