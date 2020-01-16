using CommLibrary.DBHelper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Entity
{
    /// <summary>
    /// 用户信息验证
    /// </summary>
    public class TB_VailUser: DbContext<TB_VailUser>
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int TUID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string UPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UEmail { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [SugarColumn(DefaultValue = "true")]
        public bool IsActive { get; set; }

    }
}
