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
            DataTable dt = db.GetDataTable(sql, pars);
            if (typeof(T).Name == typeof(List<>).Name)
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dt));
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dt.Rows[0]));
                }
                else
                {
                    return default(T);
                }
            }



        }
    }
}