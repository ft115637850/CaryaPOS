using CaryaPOS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaryaPOS.Dao
{
    class BaseDao
    {
        private DBHelper dbHelper;
        public BaseDao(DBHelper myDBHelper)
        {
            this.dbHelper = myDBHelper;
        }

        protected DataTable GetData(string sqlTxt, SQLiteParameter[] parms)
        {
            using (var cnn = dbHelper.GetConnection())
            {
                try
                {
                    var cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlTxt;
                    if (parms!=null)
                    {
                        foreach (var parm in parms)
                        {
                            cmd.Parameters.Add(parm);
                        }
                    }

                    cnn.Open();
                    var rdr = cmd.ExecuteReader();
                    var data = new DataTable();
                    data.Load(rdr, LoadOption.OverwriteChanges);
                    return data;
                }
                catch (DbException ex)
                {
                    Debug.Print("Error SQL:" + sqlTxt);
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        protected object GetSingleValue(string sqlTxt, SQLiteParameter[] parms)
        {
            using (var cnn = dbHelper.GetConnection())
            {
                try
                {
                    var cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlTxt;
                    if (parms != null)
                    {
                        foreach (var parm in parms)
                        {
                            cmd.Parameters.Add(parm);
                        }
                    }

                    cnn.Open();
                    var data = cmd.ExecuteScalar();
                    if (data == null || Convert.IsDBNull(data))
                    {
                        return null;
                    }
                    else
                    {
                        return data;
                    }
                }
                catch (DbException ex)
                {
                    Debug.Print("Error SQL:" + sqlTxt);
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        protected int ExecuteNonQuery(string sqlTxt, SQLiteParameter[] parms)
        {
            using (var cnn = dbHelper.GetConnection())
            {
                try
                {
                    var cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlTxt;
                    if (parms != null)
                    {
                        foreach (var parm in parms)
                        {
                            cmd.Parameters.Add(parm);
                        }
                    }

                    cnn.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (DbException ex)
                {
                    Debug.Print("Error SQL:" + sqlTxt);
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
    }
}
