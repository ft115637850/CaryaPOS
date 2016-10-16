using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaryaPOS.View
{
    /// <summary>
    /// Interaction logic for CashPay.xaml
    /// </summary>
    public partial class CashPay : Window
    {
        public CashPay(CashPayViewModel vm)
        {
            this.DataContext = vm;
            vm.CloseCashPayWindow += (_, __) => { this.Close(); };
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtInp.SelectAll();
        }
    }
}
