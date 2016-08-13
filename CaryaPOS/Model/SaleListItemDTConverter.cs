using CaryaPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Model
{
    public class SaleListItemDTConverter
    {
        public static List<SaleListItemViewModel> GetModel(DataTable dtData)
        {
            var items = new List<SaleListItemViewModel>();

            foreach (DataRow row in dtData.Rows)
            {
                Guid sheetid;
                Guid.TryParse(Convert.ToString(row["sheetid"]), out sheetid);
                var item = new SaleListItemViewModel
                {
                    SheetID = sheetid,
                    SeqID = Convert.ToInt32(row["seqID"]),
                    GoodsID = Convert.ToInt32(row["goodsid"]),
                    GoodsName = Convert.ToString(row["GoodsName"]),
                    BarcodeID = Convert.ToString(row["barcodeid"]),
                    Quantity = Convert.ToDecimal(row["QTY"]),
                    SalePrice = Convert.ToDecimal(row["SALEPRICE"]),
                    SaleValue = Convert.ToDecimal(row["SaleValue"]),
                    Cost = Convert.ToDecimal(row["cost"])
                };
                items.Add(item);
            }
            return items;
        }
    }
}
