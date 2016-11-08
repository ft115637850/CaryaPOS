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

        public decimal GetTotalPayValue(string sheetID)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String)
            };

            parms[0].Value = sheetID;
            //TO DO: Multi Currency support
            var data = this.GetSingleValue("select sum(PAYVALUE) from SALELISTPAY where sheetID=@sheetID", parms);
            return data == null ? 0 : Convert.ToDecimal(data);
        }

        public decimal GetPayItemValue(string sheetID, int payTypeID)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@payTypeID", DbType.Int32),
            };

            parms[0].Value = sheetID;
            parms[1].Value = payTypeID;
            //TO DO: Multi Currency support
            var data = this.GetSingleValue("select sum(PAYVALUE) from SALELISTPAY where sheetID=@sheetID and PAYTYPEID=@payTypeID", parms);
            return data == null ? 0 : Convert.ToDecimal(data);
        }

        public int UpdatePayItemValue(string sheetID, int payTypeID, decimal payValue)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@payTypeID", DbType.Int32),
                new SQLiteParameter("@payValue", DbType.Decimal),
            };

            parms[0].Value = sheetID;
            parms[1].Value = payTypeID;
            parms[2].Value = payValue;
            //TO DO: Multi-Currency support
            return this.ExecuteNonQuery("update SALELISTPAY set PAYVALUE=@payValue,REALVALUE=@payValue where sheetID=@sheetID and PAYTYPEID=@payTypeID", parms);
        }

        public void AddPayItem(string sheetID, int seqID, int payTypeID, string payTypeName, string currencyCode, decimal rate, decimal payValue, decimal realValue, string cardNO, decimal cardOldValue, decimal cardNewValue, int cardID, string cardType)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String),
                new SQLiteParameter("@seqID", DbType.Int32),
                new SQLiteParameter("@payTime", DbType.Time),
                new SQLiteParameter("@payTypeID", DbType.Int32),
                new SQLiteParameter("@payTypeName", DbType.String),
                new SQLiteParameter("@currencyCode", DbType.String),
                new SQLiteParameter("@rate", DbType.Decimal),
                new SQLiteParameter("@payValue", DbType.Decimal),
                new SQLiteParameter("@realValue", DbType.Decimal),
                new SQLiteParameter("@cardNO", DbType.String),
                new SQLiteParameter("@cardOldValue", DbType.Decimal),
                new SQLiteParameter("@cardNewValue", DbType.Decimal),
                new SQLiteParameter("@cardID", DbType.Int32),
                new SQLiteParameter("@cardType", DbType.String)
            };

            parms[0].Value = sheetID;
            parms[1].Value = seqID;
            parms[2].Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            parms[3].Value = payTypeID;
            parms[4].Value = payTypeName;
            parms[5].Value = currencyCode;
            parms[6].Value = rate;
            parms[7].Value = payValue;
            parms[8].Value = realValue;     //TO DO: Update to normalprice in promotion
            parms[9].Value = cardNO;
            parms[10].Value = cardOldValue;
            parms[11].Value = cardNewValue;
            parms[12].Value = cardID;
            parms[13].Value = cardType;
            this.ExecuteNonQuery("insert into SALELISTPAY (SHEETID,SEQID,PAYTIME,PAYTYPEID,PAYTYPENAME,CURRENCYCODE,RATE,PAYVALUE,REALVALUE,CARDNO,CARDOLDVALUE,CARDNEWVALUE,CARDID,CARDTYPE) values ("
               + "@sheetID,@seqID,@payTime,@payTypeID,@payTypeName,@currencyCode,@rate,@payValue,@realValue,@cardNO,@cardOldValue,@cardNewValue,@cardID,@cardType)", parms);
        }

        public void MoveSaleListToHistory(string sheetID)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String)
            };

            parms[0].Value = sheetID;
            this.ExecuteNonQuery("insert into SALELISTPAYHIST select * from SALELISTPAY where sheetid=@sheetID", parms);
            this.ExecuteNonQuery("insert into SALELISTHIST select * from SALELIST where sheetid=@sheetID", parms);
            this.ExecuteNonQuery("insert into SALELISTITEMHIST select * from SALELISTITEM where sheetid=@sheetID", parms);
            this.DeleteSaleList(sheetID);
        }

        public void DeleteSaleList(string sheetID)
        {
            SQLiteParameter[] parms = new SQLiteParameter[]
            {
                new SQLiteParameter("@sheetID", DbType.String)
            };

            parms[0].Value = sheetID;
            this.ExecuteNonQuery("delete from SALELISTPAY where sheetid=@sheetID", parms);
            this.ExecuteNonQuery("delete from SALELIST where sheetid=@sheetID", parms);
            this.ExecuteNonQuery("delete from SALELISTITEM where sheetid=@sheetID", parms);
        }
    }
}
