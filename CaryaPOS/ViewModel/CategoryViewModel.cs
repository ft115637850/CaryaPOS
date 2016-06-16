using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsChecked { get; set; }
        public List<GoodsViewModel> GoodsList { get; set; }
    }
}
