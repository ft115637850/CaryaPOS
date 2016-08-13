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
                new SQLiteParameter("@sheetID", DbType.String)
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

        public void AddSaleListItem(string sheetID,int seqID, int goodsID,string goodsname, string barcodeID, decimal quantity, decimal cost, decimal saleprice, decimal saleValue, decimal discValue)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@seqID", DbType.Int32),
                new SQLiteParameter("@reqtime", DbType.Time),
                new SQLiteParameter("@goodsID", DbType.Int32),
                new SQLiteParameter("@goodsname", DbType.String),
                new SQLiteParameter("@barcodeID", DbType.String),
                new SQLiteParameter("@quantity", DbType.Decimal),
                new SQLiteParameter("@cost", DbType.Decimal),
                new SQLiteParameter("@normalprice", DbType.Decimal),
                new SQLiteParameter("@saleprice", DbType.Decimal),
                new SQLiteParameter("@saleValue", DbType.Decimal),
                new SQLiteParameter("@discValue", DbType.Decimal)
            };

            parms[0].Value = sheetID;
            parms[1].Value = seqID;
            parms[2].Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            parms[3].Value = goodsID;
            parms[4].Value = goodsname;
            parms[5].Value = barcodeID;
            parms[6].Value = quantity;
            parms[7].Value = cost;
            parms[8].Value = saleprice;     //TO DO: Update to normalprice in promotion
            parms[9].Value = saleprice;
            parms[10].Value = saleValue;
            parms[11].Value = discValue;
            this.ExecuteNonQuery("insert into SALELISTITEM (SHEETID,SEQID,REQTIME,GOODSID,GOODSNAME,BARCODEID,QTY,COST,NORMALPRICE,SALEPRICE,SALEVALUE,DISCVALUE) values ("
               + "@sheetID,@seqID,@reqtime,@goodsID,@goodsname,@barcodeID,@quantity,@cost,@normalprice,@saleprice,@saleValue,@discValue)", parms);
        }
    }
}
