using CommLibrary.DBHelper.BaseClass;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.DBHelper
{
    /// <summary>
    /// 使用数据库结构检查数据的合法性
    /// </summary>
    public class DataVerification
    {

        /// <summary>
        /// 获取表定义的Sql
        /// </summary>
        string DfSql = @"--获取SqlServer中表结构
                SELECT syscolumns.name as ColName,systypes.name as DBType,syscolumns.isnullable,
                syscolumns.length
                FROM syscolumns, systypes
                WHERE syscolumns.xusertype = systypes.xusertype
                AND syscolumns.id = object_id(@tb) ; ";
        /// <summary>
        /// 获取tbName的结构检查表中的数据类型是否正确
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName">数据库表的名字</param>
        /// <param name="ent">要检查的对象</param>
        /// <returns></returns>
        public static VerifyResultEntity VerfiyEntity<T>(string tbName, T ent) where T : class
        {
            if (string.IsNullOrWhiteSpace(tbName))
            {
                throw new Exception("方法 VerfiyEntity 的参数tbName是必须的!");
            }


            EnumerableRowCollection<DataRow> TableDef = null;
            using (BaseDBHelper bh = BaseDBHelper.GetDBHelper())
            {
                TableDef = bh.GetDataTable(DfSql, default, new SqlParameter("@tb", tbName)).AsEnumerable();
            }
            if (TableDef.Count() == 0)
            {
                throw new Exception($"方法 VerfiyEntity 中输入的表名={tbName},无法从数据库中获取到表信息!");
            }

            //将表结构数据重组成字典.
            var dic_Def = TableDef.ToDictionary(c => Convert.ToString(c["ColName"]), d =>
            {
                string DBType = "";
                string old = Convert.ToString(d["DBType"]).ToUpper();
                DBType = GetCSharpType(old);
                return new { ColName = Convert.ToString(d["ColName"]), DBType = DBType, SqlType = old, IsNullble = Convert.ToBoolean(d["isnullable"]), Length = Convert.ToInt32(d["length"]) };
            });

            Type T_ent = ent.GetType();
            var propers = T_ent.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            VerifyResultEntity ve = new VerifyResultEntity() { Succeeded = true };
            List<Dictionary<string, string>> lidic = new List<Dictionary<string, string>>();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            lidic.Add(dic);
            ve.Error = lidic;

            foreach (var item in propers)
            {
                if (!dic_Def.ContainsKey(item.Name))
                {
                    ve.NoDefCol.Add(item.Name);
                    continue;
                }
                var def = dic_Def[item.Name];
                //由于是类所以只检查string 类型的长度
                if (def.DBType == "String")
                {
                    if (def.Length < item.GetValue(ent).ToString().Length)
                    {
                        ve.Succeeded = false;
                        dic.Add(item.Name, "字符串长度超过数据库限制!");
                    }
                }
            }

            return ve;
        }

        /// <summary>
        /// wm 2018年11月28日13:37
        ///  将数据库常用类型转为C# 中的类名(.Net的类型名)
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        private static string GetCSharpType(string old)
        {
            string DBType = "";
            switch (old)
            {
                case "INT":
                case "BIGINT":
                case "SMALLINT":
                    DBType = "Int32";
                    break;
                case "DECIMAL":
                case "FLOAT":
                case "NUMERIC":
                    DBType = "Decimal";
                    break;
                case "BIT":
                    DBType = "Boolean";
                    break;
                case "TEXT":
                case "CHAR":
                case "NCHAR":
                case "VARCHAR":
                case "NVARCHAR":
                case "TIME":
                    DBType = "String";
                    break;
                case "DATE":
                case "DATETIME":
                    DBType = "DateTime";
                    break;
                default:
                    throw new Exception("GetCSharpType数据类型" + DBType + "无法识别!");

            }

            return DBType;
        }
    }

    /// <summary>
    /// 验证结果返回类
    /// </summary>
    public class VerifyResultEntity
    {
        /// <summary>
        /// 标识定义中存在的数据校验是否成功!
        /// </summary>
        public bool Succeeded;

        /// <summary>
        /// 记录表定义中不存在的列信息
        /// </summary>
        public List<string> NoDefCol = new List<string>();
        /// <summary>
        /// 记录每行数据各列检验的失败结果
        /// </summary>
        public List<Dictionary<string, string>> Error = new List<Dictionary<string, string>>();
    }
}
