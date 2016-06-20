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
    public class GoodsSelectPane : Control
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

        static GoodsSelectPane()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GoodsSelectPane), new FrameworkPropertyMetadata(typeof(GoodsSelectPane)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ScrollViewer sv = GetTemplateChild("PART_btnsScrollViewer") as ScrollViewer;
            sv.ManipulationBoundaryFeedback += ScrollViewer_ManipulationBoundaryFeedback;
            for (int i = 1; i <= 100; i++)
            {
                Button btn = GetTemplateChild("PART_btn" + i) as Button;
                this.goodsBtns.Add(btn);
            }
        }

        //TO DO:
        //public void Dispose()
        //{
        //    ScrollViewer sv = GetTemplateChild("PART_btnsScrollViewer") as ScrollViewer;
        //    sv.ManipulationBoundaryFeedback -= ScrollViewer_ManipulationBoundaryFeedback;
        //}
        
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
