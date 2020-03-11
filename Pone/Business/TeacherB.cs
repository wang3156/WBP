using CommLibrary.DBHelper;
using CommLibrary.DBHelper.BaseClass;
using CommLibrary.Secrecy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TeacherB
    {
        SqlServerDBHelper db = new SqlServerDBHelper();
        /// <summary>
        /// 检查账号和用户名是否正确
        /// </summary>
        /// <param name="uName">用户名</param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        public bool CheckLogin(string uName, string pwd)
        {
            pwd = MD5Helper.GenerateMD5(pwd);
            return db.ExecuteScalar<int>("select  1 From A_Teacher where AUser=@u and APassword=@p", pars: new SqlParameter[] { new SqlParameter("@u", uName), new SqlParameter("@p", pwd) }) == 1;
        }
    }
}
