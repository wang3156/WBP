
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.DBHelper.BaseClass
{
    using CommLibrary.Extension;
    using MySql.Data.MySqlClient;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Text.RegularExpressions;


    /// <summary>
    /// 使用时请先在config的AppSettings里配置数据库类型 可选类型:sqlserver,mysql
    /// 连接字符串由AppSettings中的ConStr配置 或 实例化对象时传入
    /// </summary>
    public abstract class BaseDBHelper : IDisposable
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        protected DbConnection conn;
        /// <summary>
        /// 事务对象
        /// </summary>
        protected DbTransaction tran;

        static string _connStr;
        /// <summary>
        /// 连接字符串
        /// </summary>
        protected static string connStr
        {
            get
            {
                return _connStr;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("未检测到连接字符串!");
                }
                _connStr = value;
            }
        }

        static string DBType = "sqlserver";
        static readonly string[] Supported_DB = new string[] { "sqlserver", "mysql" };

        /// <summary>
        /// 实例化数据库操作对象 
        /// </summary>       
        public BaseDBHelper()
        {


        }
        /// <summary>
        /// 根据配置的DBType获取一个数据库操作对象
        /// </summary>
        /// <returns></returns>
        public static BaseDBHelper GetDBHelper(string connstr = "")
        {

            #region 检查参数是否正确
            string d = ConfigurationManager.AppSettings["DBType"];
            if (!string.IsNullOrWhiteSpace(d))
            {
                DBType = d;
            }

            if (string.IsNullOrWhiteSpace(DBType) || !Supported_DB.Contains(DBType, true))
            {
                throw new Exception("AppSetting中未配置DBType节点指定DB类型或类型不被支持.可选类型:sqlserver,mysql. ");
            }

            if (string.IsNullOrWhiteSpace(connstr))
            {
                connStr = ConfigurationManager.AppSettings["ConStr"];
            }
            else
            {
                connStr = connstr;
            }


            #endregion

            switch (DBType.ToLower())
            {
                case "sqlserver":
                    return new SqlServerDBHelper();
                case "mysql":
                    return new MySqlDBHelper();
                default:
                    return null;

            }
        }


        /// <summary>
        /// 释放使用的实例 ,连接释放时会先尝试提交事务(未开事务不会抛出异常)
        /// </summary>
        public void Dispose()
        {
            //如果有事务就尝试回滚一下再释放连接,防止用户开了连接池,事务没有提交.而连接被放回连接池影响后续使用的问题.
            try
            {
                tran?.Rollback();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            finally
            {
                //conn?.Close();
                conn.Dispose();
            }


        }
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="level">事务级别,默认为ReadCommitted </param>
        public void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (tran == null)
            {
                tran = conn.BeginTransaction();
            }
        }
        /// <summary>
        /// 回滚事务,如果未开不会抛出异常
        /// </summary>
        public void Rollback()
        {
            tran?.Rollback();
        }
        /// <summary>
        /// 提交事务,如果未开不会抛出异常
        /// </summary>
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
        /// <returns>如果不能转成指定类型则返回指定类型的默认值 </returns>
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

            try
            {
                return (N)Convert.ChangeType(o, typeof(N));
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return default(N);
            }

        }

        /// <summary>
        /// 执行一个sql并返回一个DataTable
        /// </summary>
        /// <param name="sql">如果 以 - 开头表示不允许脏读</param>
        /// <param name="ctype">执行Sql的类型,默认为Sql语句</param>
        /// <param name="pars">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, IDataParameter[] pars, PageInfo pginfo = null, CommandType ctype = CommandType.Text)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataTable dt = new DataTable();
            if (sql.StartsWith("-"))
            {
                sql = sql.Substring(1);
            }
            else
            {
                sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;" + sql;
            }
             
            if (pginfo != null)
            {
                string[] sqpls = Regex.Split(sql, "From", RegexOptions.IgnoreCase);
                sqpls[0] += " into #k ";
                sql = string.Join(" From ", sqpls);
                sql += $" select count(1) From #k ; select * From #k order by {pginfo.SortExpression} offset {(pginfo.PageNumber - 1) * pginfo.PageSize} rows fetch next  {pginfo.PageSize} rows only";
                DataSet ds = GetDataSet("-" + sql, pars);
                pginfo.TotalRows = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                return ds.Tables[1];

            }


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
        public DataSet GetDataSet(string sql, IDataParameter[] pars, CommandType ctype = CommandType.Text)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DataSet ds = new DataSet();
            if (sql.StartsWith("-"))
            {
                sql = sql.Substring(1);
            }
            else
            {
                sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;" + sql;
            }
            using (DbCommand comm = conn.CreateCommand())
            {
                using (DbDataAdapter mad = GetDbDataAdapter())
                {
                    comm.Transaction = tran;
                    comm.CommandText = sql;
                    comm.CommandType = ctype;
                    comm.Parameters.AddRange(pars);
                    mad.SelectCommand = comm;
                    mad.Fill(ds);

                }

            }

            return ds;
        }
        /// <summary>
        /// 将数据批量插入到指定表(该连接上如果存在事务则会被使用)
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="tbName">数据库表名称</param>
        /// <param name="mapping">列映射,不传则默认为数据源中的所有列</param>      
        /// <returns></returns>
        public abstract void BulkCopyToDB(DataTable data, string tbName = "", Dictionary<string, string> mapping = null);
        //{
        //    string error = "";

        //    switch (DBType.ToLower())
        //    {
        //        case "sqlserver":
        //            return new MySqlBatch();
        //        case "mysql":
        //            return new SqlBulkCopy();
        //        default:
        //            return null;
        //    }

        //    if (mapping==null)
        //    {

        //    }

        //}

        /// <summary>
        /// 按连接对象的类型返回一个适配器类型
        /// </summary>
        /// <returns></returns>
        DbDataAdapter GetDbDataAdapter()
        {
            switch (DBType.ToLower())
            {
                case "sqlserver":
                    return new MySqlDataAdapter();
                case "mysql":
                    return new SqlDataAdapter();
                default:
                    return null;
            }

        }

    }
}
