using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Helper
{
    class SalesDBHelper : DBHelper
    {
        private static SalesDBHelper dbHelper;
        private const string sqlCreateSalesDBTables = @"
            create table DBVersion
            (
            DBVersionID int,
            VersionNO   int,
            VersionDesc varchar(50),
            primary key(DBVersionID)
            );
            CREATE TABLE SALELIST
            (
            SHEETID			VARCHAR(50),
            SALETIME		DATETIME,
            PAYTIME			DATETIME,
            SHOPID			VARCHAR(10),
            LISTNO			INT,
            SUBLISTNO		INT,
            POSID			INT,
            SHIFTID			INT,
            CASHIERID		VARCHAR(10),
            COUNTERID		INT,
            TRAINFLAG		INT,
            SALETYPE		INT,
            MEMBERFLAG		INT,
            MEMBERID		VARCHAR(50),
            MEMBERCARDNO	VARCHAR(50),
            SALEVALUE		DECIMAL(10,2),
            DISCVALUE		DECIMAL(10,2),
            PAYVALUE		DECIMAL(10,2),
            POINTS			INT,
            CHECKINFLAG		INT,
            STATUSFLAG		INT,        --0.Normal;1.Hold
            NOTES			VARCHAR(255),
            PRIMARY KEY(SHEETID)
            );
            CREATE TABLE SALELISTITEM
            (
            SHEETID			VARCHAR(50),
            SEQID			INT,
            REQTIME			DATETIME,
            GOODSID			INT,
            GOODSNAME		char(50),
            BARCODEID		VARCHAR(20),
            DEPTID			INT,
            QTY				DECIMAL(10,3),
            NORMALPRICE		DECIMAL(10,2),
            SALEPRICE		DECIMAL(10,2),
            COST			DECIMAL(10,2),
            SALEVALUE		DECIMAL(10,2),
            DISCVALUE		DECIMAL(10,2),
            DISCRATE		DECIMAL(10,2),
            DISCTYPE		DECIMAL(10,2),
            PROMTYPEID		INT,
            AUTHORIZERID	INT,
            POINTS			INT,
            COUPONNO		VARCHAR(50),
            PRIMARY KEY (SHEETID,SEQID)
            );
            CREATE TABLE SALELISTHIST
            (
            SHEETID			VARCHAR(50),
            SALETIME		DATETIME,
            PAYTIME			DATETIME,
            SHOPID			VARCHAR(10),
            LISTNO			INT,
            SUBLISTNO		INT,
            POSID			INT,
            SHIFTID			INT,
            CASHIERID		VARCHAR(10),
            COUNTERID		INT,
            TRAINFLAG		INT,
            SALETYPE		INT,
            MEMBERFLAG		INT,
            MEMBERID		VARCHAR(50),
            MEMBERCARDNO	VARCHAR(50),
            SALEVALUE		DECIMAL(10,2),
            DISCVALUE		DECIMAL(10,2),
            PAYVALUE		DECIMAL(10,2),
            POINTS			INT,
            CHECKINFLAG		INT,
            STATUSFLAG		INT,
            NOTES			VARCHAR(255),
            PRIMARY KEY(SHEETID)
            );
            CREATE TABLE SALELISTITEMHIST
            (
            SHEETID			VARCHAR(50),
            SEQID			INT,
            REQTIME			DATETIME,
            GOODSID			INT,
            GOODSNAME		char(50),
            BARCODEID		VARCHAR(20),
            DEPTID			INT,
            QTY				DECIMAL(10,3),
            NORMALPRICE		DECIMAL(10,2),
            SALEPRICE		DECIMAL(10,2),
            COST			DECIMAL(10,2),
            SALEVALUE		DECIMAL(10,2),
            DISCVALUE		DECIMAL(10,2),
            DISCRATE		DECIMAL(10,2),
            DISCTYPE		DECIMAL(10,2),
            PROMTYPEID		INT,
            AUTHORIZERID	INT,
            POINTS			INT,
            COUPONNO		VARCHAR(50),
            PRIMARY KEY (SHEETID,SEQID)
            );
            ";

        public static SalesDBHelper GetInstance()
        {
            if (dbHelper == null)
            {
                dbHelper = new SalesDBHelper();
            }
            return dbHelper;
        }

        private SalesDBHelper()
            : base(sqlCreateSalesDBTables, "SalesDB.db")
        {
        }
    }
}
