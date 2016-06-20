using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CaryaPOS.ViewModel;
using CaryaPOS.Helper;

namespace CaryaPOS.View
{
    /// <summary>
    /// Interaction logic for GoodsSelectPane.xaml
    /// </summary>
    public partial class GoodsSelectPane : UserControl
    {
        public static readonly DependencyProperty SelectedCategoryProperty =
           DependencyProperty.Register("SelectedCategory", typeof(CategoryViewModel), typeof(GoodsSelectPane), new UIPropertyMetadata(OnCategorySelected));
        public static readonly DependencyProperty GoodsCategoriesDataProperty =
           DependencyProperty.Register("GoodsCategoriesData", typeof(List<CategoryViewModel>), typeof(GoodsSelectPane), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDataAssigned)));
        public static readonly DependencyProperty SelectedGoodsIDProperty =
           DependencyProperty.Register("SelectedGoodsID", typeof(int), typeof(GoodsSelectPane), new UIPropertyMetadata(null));
        public static readonly RoutedEvent OnGoodsSelectedEvent =
            EventManager.RegisterRoutedEvent("OnGoodsSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GoodsSelectPane));

        private List<Button> goodsBtns = new List<Button>();
        private RelayCommand goodsClickCommand;

        public CategoryViewModel SelectedCategory
        {
            get { return (CategoryViewModel)GetValue(SelectedCategoryProperty); }
            set { SetValue(SelectedCategoryProperty, value); }
        }
       
        public List<CategoryViewModel> GoodsCategoriesData
        {
            get { return (List<CategoryViewModel>)GetValue(GoodsCategoriesDataProperty); }
            set { SetValue(GoodsCategoriesDataProperty, value); }
        }

        public int SelectedGoodsID
        {
            get { return (int)GetValue(SelectedGoodsIDProperty); }
            set { SetValue(SelectedGoodsIDProperty, value); }
        }

        public event RoutedEventHandler OnGoodsSelected
        {
            add { AddHandler(OnGoodsSelectedEvent, value); }
            remove { RemoveHandler(OnGoodsSelectedEvent, value); }
        }

        
        public RelayCommand GoodsClickCommand
        {
            get {
                if (goodsClickCommand == null)
                {
                    goodsClickCommand = new RelayCommand(OnGoodsClick); 
                }
                return goodsClickCommand;
            }
        }

        public GoodsSelectPane()
        {
            InitializeComponent();
            this.goodsBtns.AddRange(new Button[] {this.btn1,this.btn2,this.btn3,this.btn4,this.btn5,
                                    this.btn6,this.btn7,this.btn8,this.btn9,this.btn10,this.btn11,this.btn12,this.btn13,this.btn14,this.btn15,this.btn16,this.btn17,this.btn18,this.btn19,this.btn20,this.btn21,this.btn22,this.btn23,this.btn24,
                                    this.btn25,this.btn26,this.btn27,this.btn28,this.btn29,this.btn30,this.btn31,this.btn32,this.btn33,this.btn34,this.btn35,this.btn36,this.btn37,this.btn38,this.btn39,this.btn40,this.btn41,this.btn42,this.btn43,
                                    this.btn44,this.btn45,this.btn46,this.btn47,this.btn48,this.btn49,this.btn50,this.btn51,this.btn52,this.btn53,this.btn54,this.btn55,this.btn56,this.btn57,this.btn58,this.btn59,this.btn60,this.btn61,this.btn62,
                                    this.btn63,this.btn64,this.btn65,this.btn66,this.btn67,this.btn68,this.btn69,this.btn70,this.btn71,this.btn72,this.btn73,this.btn74,this.btn75,this.btn76,this.btn77,this.btn78,this.btn79,this.btn80,this.btn81,
                                    this.btn82,this.btn83,this.btn84,this.btn85,this.btn86,this.btn87,this.btn88,this.btn89,this.btn90,this.btn91,this.btn92,this.btn93,this.btn94,this.btn95,this.btn96,this.btn97,this.btn98,this.btn99,this.btn100
                                    });
        }

        private void OnGoodsClick(object goods)
        {
            SelectedGoodsID = (int)goods;
            this.RaiseEvent(new RoutedEventArgs(GoodsSelectPane.OnGoodsSelectedEvent));
        }

        private static void OnCategorySelected(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var goodsPane = (GoodsSelectPane)d;
            goodsPane.UpdateGoodsPane();
        }

        private static void OnDataAssigned(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var goodsPane = (GoodsSelectPane)d;
            var goodsCategories = goodsPane.GoodsCategoriesData;
            if (goodsCategories.Count > 0)
            {
                goodsPane.SelectedCategory = goodsCategories[0];
            }
        }

        private void UpdateGoodsPane()
        {
            var goodsList = this.SelectedCategory.GoodsList;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                this.BeginInit();
                for (int i = 0; i < goodsBtns.Count(); i++)
                {
                    if (i < goodsList.Count())
                    {
                        goodsBtns[i].Content = this.SelectedCategory.GoodsList[i].ShortName;
                        goodsBtns[i].Visibility = System.Windows.Visibility.Visible;
                        goodsBtns[i].CommandParameter = this.SelectedCategory.GoodsList[i].GoodsID;
                    }
                    else
                    {
                        goodsBtns[i].Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
                this.EndInit();
            });
        }

        private void ScrollViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
