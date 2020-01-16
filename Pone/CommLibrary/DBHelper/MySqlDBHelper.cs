
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CommLibrary.DBHelper
{
    public class MySqlDBHelper : IDisposable
    {
        MySqlConnection conn;
        MySqlTransaction Tran;
        public MySqlDBHelper(string connStr = "")
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                connStr = ConfigurationManager.AppSettings["ConStr"];
            }
            conn = new MySqlConnection(connStr);

        }

        public void Dispose()
        {
            Tran?.Commit();
            conn?.Dispose();
        }

        public void BeginTransaction()
        {
            if (Tran == null)
            {
                Tran = conn.BeginTransaction();
            }
        }

        public void Rollback()
        {
            Tran?.Rollback();
        }

        public void Commit()
        {
            Tran?.Commit();
        }
    }
}
