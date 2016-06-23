using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Helper
{
    class LocalDBHelper : DBHelper
    {
        private static LocalDBHelper dbHelper;
        private const string sqlCreateLocalDBTables = @"
            create table DBVersion
            (
            DBVersionID int,
            VersionNO   int,
            VersionDesc varchar(50),
            primary key(DBVersionID)
            );

            CREATE TABLE GoodsPrice
            (
            goodsid				int,
            barcodeid			char(20),
            goodsname			char(64),
            shortname			char(50),
            unitname			char(8),
            spec				char(16),
            CategoryID			int,
            brandid				int,
            cost				decimal(16,8),
            price				decimal(10,2),
            wholesaleprice		decimal(10,2),
            memprice			decimal(10,2),
            SillerPrice			decimal(10,2),
            goldprice			decimal(10,2),
            VenderID			int,
            PackQty				int,
            PackPrice			decimal(10,2),
            BarcodeType			int,
            PriceRate			int,
            GoodsType			int,
            SaleTaxRate	        decimal(4,2),
            PromFlag			smallint,
            CustomNo			char(30),
            MnemonicCode		varchar(50),
            SaleStatus			int,
            primary key(goodsid),
            unique(barcodeid)
            );

            Create Table Category
            (
            CategoryID     int,
            CategoryName   varchar(32),
            LevelID        int,
            primary key(CategoryID)
            );
            ";

        public static LocalDBHelper GetInstance()
        {
            if (dbHelper==null)
            {
                dbHelper = new LocalDBHelper();
            }
            return dbHelper;
        }

        private LocalDBHelper()
            : base(sqlCreateLocalDBTables, "LocalDB.db")
        {
        }

    }
}
