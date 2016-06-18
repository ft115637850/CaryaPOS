using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.ViewModel
{
    public class GoodsViewModel : ViewModelBase, ICloneable
    {
        public int GoodsID { get; set; }
        public string ShortName { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
