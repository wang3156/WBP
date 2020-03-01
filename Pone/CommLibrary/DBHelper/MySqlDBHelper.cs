
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
using CommLibrary.DBHelper.BaseClass;

namespace CommLibrary.DBHelper
{
    public class MySqlDBHelper : BaseDBHelper<MySqlConnection>, IDisposable
    {
        //使用父类的
        //MySqlConnection conn;
        //MySqlTransaction tran;
        //public MySqlDBHelper(string connStr = "")
        //{
        //    if (string.IsNullOrWhiteSpace(connStr))
        //    {
        //        connStr = ConfigurationManager.AppSettings["ConStr"];
        //    }
        //    conn = new MySqlConnection(connStr);

        //}

        //public void Dispose()
        //{
        //    tran?.Commit();
        //    conn?.Dispose();
        //}

        //public void BeginTransaction()
        //{
        //    if (tran == null)
        //    {
        //        tran = conn.BeginTransaction();
        //    }
        //}

        //public void Rollback()
        //{
        //    tran?.Rollback();
        //}

        //public void Commit()
        //{
        //    tran?.Commit();
        //}

        //public void ExecuteNonQuery(string sql, params MySqlParameter[] pars)
        //{
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    MySqlCommand comm = conn.CreateCommand();
        //    comm.Transaction = tran;
        //    comm.CommandText = sql;
        //    comm.Parameters.AddRange(pars);
        //    comm.ExecuteNonQuery();
        //    comm.Dispose();

        //}
        //public T ExecuteScalar<T>(string sql, params MySqlParameter[] pars) where T : IConvertible
        //{
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    MySqlCommand comm = conn.CreateCommand();
        //    comm.Transaction = tran;
        //    comm.CommandText = sql;
        //    comm.Parameters.AddRange(pars);
        //    object o = comm.ExecuteScalar();
        //    comm.Dispose();
        //    return (T)Convert.ChangeType(o, typeof(T));
        //}

        //public DataTable GetDataTable(string sql, CommandType ctype = CommandType.Text, params MySqlParameter[] pars)
        //{
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    DataTable dt = new DataTable();
        //    using (MySqlCommand comm = conn.CreateCommand())
        //    {
        //        MySqlDataAdapter mad = new MySqlDataAdapter(comm);
        //        comm.Transaction = tran;
        //        comm.CommandText = sql;
        //        comm.CommandType = ctype;
        //        comm.Parameters.AddRange(pars);
        //        mad.Fill(dt);
        //    }
        //    return dt;
        //}
        //public DataSet GetDataSet(string sql, CommandType ctype = CommandType.Text, params MySqlParameter[] pars)
        //{
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    DataSet ds = new DataSet();
        //    using (MySqlCommand comm = conn.CreateCommand())
        //    {
        //        MySqlDataAdapter mad = new MySqlDataAdapter(comm);
        //        comm.Transaction = tran;
        //        comm.CommandText = sql;
        //        comm.CommandType = ctype;
        //        comm.Parameters.AddRange(pars);
        //        mad.Fill(ds);
        //    }
        //    return ds;
        //}

    }

}
