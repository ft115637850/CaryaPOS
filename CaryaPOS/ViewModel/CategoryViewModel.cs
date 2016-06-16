using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CaryaPOS.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsChecked { get; set; }
        public List<GoodsViewModel> GoodsList { get; set; }
        public PosMainViewModel MainViewModel { get; set; }

        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand<object>((o) => { UpdateShownGoods(); });
            }
        }

        private void UpdateShownGoods()
        {
            if (MainViewModel == null)
                return;
            var shownGoods = MainViewModel.GoodsShown;
            int idx = 0;
            for (; idx < GoodsList.Count; idx++)
            {
                shownGoods[idx] = GoodsList[idx];
            }

            while (idx < shownGoods.Count)
            {
                shownGoods[idx].IsVisible = "Collapsed";
                idx++;
            }
        }

    }
}
