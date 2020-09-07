using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.DBHelper
{
    /// <summary>
    /// 用于分页的页码对象 
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 排序列 为字符串如 name desc 
        /// </summary>
        public string SortExpression { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows { get; set; }

    }
}
