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
        public List<GoodsViewModel> GoodsList { get; set; }
    }
}
