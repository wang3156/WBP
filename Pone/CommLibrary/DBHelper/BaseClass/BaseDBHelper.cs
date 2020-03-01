using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.DBHelper.BaseClass
{
    //public interface IBaseDBHelper
    //{
    //    void BeginTransaction();

    //    void Rollback();
    //    void Commit();

    //    void ExecuteNonQuery(string sql, params IDataParameter[] pars);
    //    T ExecuteScalar<T>(string sql, params IDataParameter[] pars);

    //    DataTable GetDataTable(string sql, CommandType ctype = CommandType.Text, params IDataParameter[] pars);
    //    DataSet GetDataSet(string sql, CommandType ctype = CommandType.Text, params IDataParameter[] pars);

    //}

    public class BaseDBHelper<T> where T : DbConnection, new()
    {
        DbConnection conn;
        DbTransaction tran;
        public BaseDBHelper(string connStr = "")
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                connStr = ConfigurationManager.AppSettings["ConStr"];
            }
            Type tp = typeof(T);
            conn = Activator.CreateInstance(tp, connStr) as T;

        }

        public void Dispose()
        {
            tran?.Commit();
            conn?.Dispose();
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            if (tran == null)
            {
                tran = conn.BeginTransaction();
            }
        }

        public void Rollback()
        {
            tran?.Rollback();
        }

        public void Commit()
        {
            tran?.Commit();
        }
        /// <summary>
        /// 执行无返回的sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ctp">执行Sql的类型,默认为Sql语句</param>
        /// <param name="pars">查询参数</param>
        public void ExecuteNonQuery(string sql, CommandType ctp = CommandType.Text, params IDataParameter[] pars)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DbCommand comm = conn.CreateCommand();
            comm.Transaction = tran;
            comm.CommandText = sql;
            comm.Parameters.AddRange(pars);
            comm.CommandType = ctp;
            comm.ExecuteNonQuery();
            comm.Dispose();

        }
        /// <summary>
        /// 执行一个sql并返回首行首列的值
        /// </summary>
        /// <typeparam name="N">基础类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="ctp">执行Sql的类型,默认为Sql语句</param>
        /// <param name="pars">查询参数</param>
        /// <returns></returns>
        public N ExecuteScalar<N>(string sql, CommandType ctp = CommandType.Text, params IDataParameter[] pars) where N : IConvertible
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DbCommand comm = conn.CreateCommand();
            comm.Transaction = tran;
            comm.CommandText = sql;
            comm.Parameters.AddRange(pars);
            comm.CommandType = ctp;
            object o = comm.ExecuteScalar();
            comm.Dispose();
            return (N)Convert.ChangeType(o, typeof(N));
        }

        /// <summary>
        /// 执行一个sql并返回一个DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ctype">执行Sql的类型,默认为Sql语句</param>
        /// <param name="pars">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, CommandType ctype = CommandType.Text, params IDataParameter[] pars)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataTable dt = new DataTable();
            using (DbCommand comm = conn.CreateCommand())
            {
                DbDataAdapter mad = GetDbDataAdapter();
                comm.Transaction = tran;
                comm.CommandText = sql;
                comm.CommandType = ctype;
                comm.Parameters.AddRange(pars);
                mad.SelectCommand = comm;
                mad.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 执行一个sql并返回一个DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ctype">执行Sql的类型,默认为Sql语句</param>
        /// <param name="pars">查询参数</param>
        public DataSet GetDataSet(string sql, CommandType ctype = CommandType.Text, params IDataParameter[] pars)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataSet ds = new DataSet();
            using (DbCommand comm = conn.CreateCommand())
            {
                DbDataAdapter mad = GetDbDataAdapter();
                comm.Transaction = tran;
                comm.CommandText = sql;
                comm.CommandType = ctype;
                comm.Parameters.AddRange(pars);
                mad.SelectCommand = comm;
                mad.Fill(ds);
            }
            return ds;
        }
        

        /// <summary>
        /// 按连接对象的类型返回一个适配器类型
        /// </summary>
        /// <returns></returns>
        DbDataAdapter GetDbDataAdapter()
        {
            string dbname = typeof(T).Name.Replace("Connection", "").ToUpper();
            switch (dbname)
            {
                case "MYSQL":
                    return new MySqlDataAdapter();
                case "SQL":
                    return new SqlDataAdapter();
                default:
                    return null;

            }
        }
    }
}
