using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Helper
{
    class DBHelper
    {
        private string sqlCreateDBTables;
        protected string sqlToUpgrade;
        private string dbName;
        private string dbSource;
        private string connectStr;

        protected string ConnectStr
        {
            get { return connectStr; }
        }

        public DBHelper(string sqlToCreateDBTables, string databaseName, string sqlToUpgrade = "")
        {
            this.sqlCreateDBTables = sqlToCreateDBTables;
            this.dbName = databaseName;
            this.dbSource = Path.Combine(System.Environment.CurrentDirectory, this.dbName);
            this.connectStr = new SQLiteConnectionStringBuilder()
            {
                DataSource = dbSource
            }.ToString();
            this.sqlToUpgrade = sqlToUpgrade;

            //TO DO: CreateDB UpgradeDB
        }

        private void CreateDB()
        {
            SQLiteConnection.CreateFile(dbSource);
            var cnnStrBlder = new SQLiteConnectionStringBuilder()
            {
                DataSource = dbSource
            };

            using (var cnn = new SQLiteConnection(cnnStrBlder.ToString()))
            {
                cnn.Open();
                var cmd = new System.Data.SQLite.SQLiteCommand(this.sqlCreateDBTables, cnn);
                cmd.ExecuteNonQuery();
            }
        }

        private void UpgradeDB()
        {

        }
    }
}
