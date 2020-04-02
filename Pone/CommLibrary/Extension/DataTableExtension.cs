using CommLibrary.OfficeHelper.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.Extension
{
    /// <summary>
    /// DataTable 扩展的相关方法
    /// </summary>
    public static class DataTableExtension
    {
        /// <summary>
        /// 从行类取出指定列的值 ,如果列不存在或值为空则返回defautValue
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defalutValue">如果列没有值则返回的默认值</param>
        /// <returns></returns>
        public static T GetField<T>(this DataRow row, string colName, T defalutValue=default(T))
        {
            if (!row.Table.Columns.Contains(colName))
            {
                return defalutValue;
            }
            object v = row[colName];
            if (Convert.ToString(v) == "")
            {
                return defalutValue;
            }
            Type dis = typeof(T);
            if (dis == typeof(bool))
            {
                string[] trueStr = new string[] { "1", "Y", "TRUE", "是" };
                if (trueStr.Contains(Convert.ToString(v), true))
                    defalutValue = (T)Convert.ChangeType(true, dis);
            }
            return (T)Convert.ChangeType(v, dis);

        }
    }
}
