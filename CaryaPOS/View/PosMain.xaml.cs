using CaryaPOS.Model;
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
    /// Interaction logic for PosMain.xaml
    /// </summary>
    public partial class PosMain : Window
    {
        public PosMain()
        {
            InitializeComponent();
            var category = new CategoryInfo();
            var salesData = new SalesData();
            var saleList = salesData.GetCurrentSaleList();
            var saleListItems = salesData.GetSaleListItem(saleList.SheetID);
            var saleListOnHold = salesData.GetOnHoldSaleList();
            //TO DO: get on hold sheet list from salesData
            var vm = new PosMainViewModel(category.GetGoodsCategoryInfo(), saleList, saleListItems, saleListOnHold);
            DataContext = vm;
        }

        private void Detail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Detail.Items.Count > 0)
            {
                if (this.Detail.SelectedItem == null)
                {
                    //The old last item was just deleted, then select the new last item.
                    this.Detail.SelectedItem = this.Detail.Items[this.Detail.Items.Count - 1];
                }
                this.Detail.ScrollIntoView(this.Detail.SelectedItem);
            }
        }

        private void Detail_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
