
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;

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

        public T ExecuteScalar<T>(string sql, params MySqlParameter[] pars) where T : IConvertible
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = sql;
            comm.Parameters.AddRange(pars);
            object o = comm.ExecuteScalar();
            comm.Dispose();
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public DataTable GetDataTable(string sql, CommandType ctype = CommandType.Text, params MySqlParameter[] pars)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataTable dt = new DataTable();
            using (MySqlCommand comm = conn.CreateCommand())
            {
                MySqlDataAdapter mad = new MySqlDataAdapter(comm);
                comm.CommandText = sql;
                comm.CommandType = ctype;
                comm.Parameters.AddRange(pars);
                mad.Fill(dt);
            }
            return dt;
        }
        public DataSet GetDataSet(string sql,CommandType ctype=CommandType.Text ,params MySqlParameter[] pars)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataSet ds = new DataSet();
            using (MySqlCommand comm = conn.CreateCommand())
            {
                MySqlDataAdapter mad = new MySqlDataAdapter(comm);
                comm.CommandText = sql;
                comm.CommandType = ctype;
                comm.Parameters.AddRange(pars);
                mad.Fill(ds);
            }
            return ds;
        }

    }

}
