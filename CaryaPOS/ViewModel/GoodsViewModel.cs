using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class GoodsViewModel : ViewModelBase
    {
        public int GoodsID { get; set; }
        public string GoodsName { get; set; }
        public string ShortName { get; set; }
        public bool IsVisible { get; set; }
    }
}
