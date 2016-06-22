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
        private const string sqlCreateLocalDBTables = @"
            create table DBVersion
            (
            DBVersionID int,
            VersionNO   int,
            VersionDesc varchar(50),
            primary key(DBVersionID)
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
            : base("", "SalesDB.db")
        {
        }
    }
}
