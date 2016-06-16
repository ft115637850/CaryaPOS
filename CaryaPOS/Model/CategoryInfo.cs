using CaryaPOS.Dao;
using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class CategoryInfo
    {
        private LocalDBDao dao;
        public CategoryInfo()
        {
            dao = new LocalDBDao();
        }

        public List<CategoryViewModel> GetGoodsCategoryInfo()
        {
            var categories = dao.GetLevel1Categories();
            var goodsCategory = dao.GetGoodsCategoryInfo();
            var categoriesList = new List<CategoryViewModel>();
            foreach (DataRow row in categories.Rows)
            {
                var goodsList = new List<GoodsViewModel>();
                DataRow[] goodsRows = goodsCategory.Select("categoryid=" + row["CategoryID"]);
                foreach (DataRow goodsRow in goodsRows)
                {
                    var goods = new GoodsViewModel()
                    {
                        GoodsID = (int)goodsRow["goodsid"],
                        ShortName = (string)goodsRow["shortname"],
                        IsVisible = "Visible"
                    };
                    goodsList.Add(goods);
                }

                var category = new CategoryViewModel()
                {
                    CategoryID = (int)row["CategoryID"],
                    CategoryName = (string)row["CategoryName"],
                    IsChecked = false,
                    GoodsList = goodsList
                };
                categoriesList.Add(category);
            }
            return categoriesList;
        }
    }
}
