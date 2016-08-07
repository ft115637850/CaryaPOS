using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class SalesDBDao : BaseDao
    {
        public SalesDBDao()
            : base(SalesDBHelper.GetInstance())
        {
        }

        public DataTable GetOngoingSaleList()
        {
            return this.GetData("select * from SALELIST where STATUSFLAG=0", null);
        }

        public DataTable GetSaleListItems(string sheetID)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", SqlDbType.VarChar)
            };

            parms[0].Value = sheetID;
            return this.GetData("select * from SALELISTITEM where sheetID=@sheetID", parms);
        }

        public void NewSaleList(string sheetID, string cashierID, string shopID, decimal payValue, decimal saleValue, decimal discValue)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@saletime", DbType.Time),
                new SQLiteParameter("@cashierID", DbType.String),
                new SQLiteParameter("@shopID", DbType.String),
                new SQLiteParameter("@payValue", DbType.Decimal),
                new SQLiteParameter("@saleValue", DbType.Decimal),
                new SQLiteParameter("@discValue", DbType.Decimal)
            };

            parms[0].Value = sheetID;
            parms[1].Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            parms[2].Value = cashierID;
            parms[3].Value = shopID;
            parms[4].Value = payValue;
            parms[5].Value = saleValue;
            parms[6].Value = discValue;
            this.ExecuteNonQuery(@"insert into SALELIST (SHEETID,SALETIME,CASHIERID,SHOPID,PAYVALUE,SALEVALUE,DISCVALUE,STATUSFLAG) values (
                @sheetID,@saletime,@cashierID,@shopID,@payValue,@saleValue,@discValue,0)", parms);
        }

        public void UpdateSaleList(string sheetID, decimal payValue, decimal saleValue, decimal discValue)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@payValue", DbType.Decimal),
                new SQLiteParameter("@saleValue", DbType.Decimal),
                new SQLiteParameter("@discValue", DbType.Decimal)
            };

            parms[0].Value = sheetID;
            parms[1].Value = payValue;
            parms[2].Value = saleValue;
            parms[3].Value = discValue;
            this.ExecuteNonQuery("update SALELIST set PAYVALUE=@payValue,SALEVALUE=@saleValue,DISCVALUE=@discValue where sheetid=@sheetID", parms);
        }

        public void AddSaleListItem(string sheetID, int goodsID, string barcodeID, decimal quantity, decimal cost, decimal price, decimal saleValue, decimal discValue)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@reqtime", DbType.Time),
                new SQLiteParameter("@goodsID", DbType.Int32),
                new SQLiteParameter("@barcodeID", DbType.String),
                new SQLiteParameter("@quantity", DbType.Decimal),
                new SQLiteParameter("@cost", DbType.Decimal),
                new SQLiteParameter("@normalprice", DbType.Decimal),
                new SQLiteParameter("@saleValue", DbType.Decimal),
                new SQLiteParameter("@discValue", DbType.Decimal)
            };

            parms[0].Value = sheetID;
            parms[1].Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            parms[2].Value = goodsID;
            parms[3].Value = barcodeID;
            parms[4].Value = quantity;
            parms[5].Value = cost;
            parms[6].Value = price;
            parms[7].Value = saleValue;
            parms[8].Value = discValue;
            this.ExecuteNonQuery("insert into SALELISTITEM (SHEETID,REQTIME,GOODSID,BARCODEID,QTY,COST,NORMALPRICE,SALEVALUE,DISCVALUE) values ("
               + "@sheetID,@reqtime,@goodsID,@barcodeID,@quantity,@cost,@normalprice,@saleValue,@discValue)", parms);
        }
    }
}
