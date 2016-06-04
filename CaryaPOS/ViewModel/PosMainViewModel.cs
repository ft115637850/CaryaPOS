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
        public DataTable Level1Categories { get; set; }
        public PosMainViewModel(DataTable level1Categories)
        {
            this.Level1Categories = level1Categories;
        }
    }
}
