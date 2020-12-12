using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingPro2.Code.Entity
{
    public class UserInfo
    {
        public int UID { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// 0 管理员  1 用户
        /// </summary>
        public int URole { get; set; }
    }

    public class ResultEntity
    {
        public bool Success { get; set; } = true;

        public Object Data { get; set; }

        public string Msg { get; set; }

    }

}