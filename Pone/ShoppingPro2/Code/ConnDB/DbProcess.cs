using CommLibrary.DALBase;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingPro2.Code.ConnDB
{
    using ShoppingPro2.Code.Entity;
    public class DbProcess : BaseDAL, IDisposable
    {
        public DbProcess(string connStr = "") : base(connStr)
        {
        }

        public T GetData<T>(string sql, params SqlParameter[] pars) where T : class
        {
            DataSet ds = db.GetDataSet(sql, pars);
            DataTable dt = ds.Tables[0];

            string tname = typeof(T).Name;
            if (tname == typeof(List<>).Name)
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dt));
            }
            else if (tname == typeof(DataTable).Name)
            {
                return dt as T;
            }
            else if (tname == typeof(DataSet).Name) {
                return ds as T;
            }
            else if (tname == typeof(DataRow).Name)
            {
                return dt.Rows[0] as T;
            }
            else if (dt.Rows.Count > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                DataRow row = dt.Rows[0];
                foreach (DataColumn item in dt.Columns)
                {
                    dic.Add(item.ColumnName, row[item.ColumnName].ToString());
                }

                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dic));
            }
            else
            {
                return default(T);
            }

        }

        public void ExecSql(string sql, params SqlParameter[] pars)
        {
            db.ExecuteNonQuery(sql, pars: pars);
        }

        public T ExecuteScalarSql<T>(string sql, params SqlParameter[] pars) where T : IConvertible
        {
            return db.ExecuteScalar<T>(sql, pars: pars);
        }

        public void BulkCopyToDB(DataTable dt, string tbname)
        {
            db.BulkCopyToDB(dt, tbname);
        }
    }
}