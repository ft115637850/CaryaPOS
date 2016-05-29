using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Models
{
    public class DBHelper
    {
        private static readonly string dbSource = Path.Combine(System.Environment.CurrentDirectory, "LocalDB.db");
        private static SQLiteConnectionStringBuilder cnnStrBlder = new SQLiteConnectionStringBuilder()
        {
            DataSource = dbSource
        };

        private const string sqlCreateTables = @"
            create table DBVersion
            (
            VersionNO   int,
            VersionDesc varchar(50)
            );
            CREATE TABLE GoodsPrice
            (
            goodsid				int,
            barcodeid			char(20),
            goodsname			char(64),
            shortname			char(50),
            unitname			char(8),
            spec				char(16),
            deptid				int,
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
            SaleTaxRate	        hhdecimal(4,2),
            PromFlag			smallint,
            CustomNo			char(30),
            MnemonicCode		varchar(50),
            SaleStatus			int,
            primary key(goodsid),
            unique(barcodeid)
            );
            ";
        
        public static void CreateDB()
        {
            SQLiteConnection.CreateFile(dbSource);
            using (var cnn = new SQLiteConnection(cnnStrBlder.ToString()))
            {
                cnn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand(sqlCreateTables, cnn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
