using CommLibrary.DBHelper.BaseClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace CommLibrary.DBHelper
{
    /// <summary>
    /// SqlServer数据库帮助类
    /// </summary>
    public class SqlServerDBHelper : BaseDBHelper, IDisposable
    {
        ///<summary>
        /// 初始化一个MySql操作对象
        ///</summary>
        /// <param name="connStr">连接字符串,不传则使用config中配置的</param>
        public SqlServerDBHelper(string connStr = "")
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                connStr = ConfigurationManager.AppSettings["ConStr"];
            }
            BaseDBHelper.connStr = connStr;
            conn = new SqlConnection(BaseDBHelper.connStr);
        }


        /// <summary>
        /// 批量插入数据到
        /// </summary>
        /// <param name="data">需要插入的数据</param>
        /// <param name="tbName">数据库表名称默认使用数据源Table的Name</param>
        /// <param name="mapping">key(数据源列名)和value(表列名)映射关系.默认使用数据源Table的列名</param>
        /// <returns>如果 有值则是错误信息</returns>
        public override void BulkCopyToDB(DataTable data, string tbName = "", Dictionary<string, string> mapping = null)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (string.IsNullOrWhiteSpace(tbName))
            {
                tbName = data.TableName;
            }

            using (SqlBulkCopy sb = new SqlBulkCopy(conn as SqlConnection, SqlBulkCopyOptions.Default, (tran as SqlTransaction)))
            {
                sb.DestinationTableName = tbName;
                if (mapping == null)
                {
                    foreach (DataColumn col in data.Columns)
                    {
                        sb.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }
                }
                else
                {
                    foreach (var item in mapping)
                    {
                        sb.ColumnMappings.Add(item.Key, item.Value);
                    }
                }
                sb.WriteToServer(data);
            }

 ;
        }
    }
}
