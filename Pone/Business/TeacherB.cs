using CommLibrary.DBHelper;
using CommLibrary.DBHelper.BaseClass;
using CommLibrary.Secrecy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business
{
    public class TeacherB : IDisposable
    {
        /// <summary>
        /// 填空填答案的分隔符
        /// </summary>
        public const string TKASp = "╀";
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

        public void Dispose()
        {
            db.Dispose();
        }

        public string UpdateSelectData(DataSet ds, int qID)
        {
            DataRow row = ds.Tables[0].Rows[0];
            string sql;
            string error = "";
            db.BeginTransaction();
            try
            {

                if (qID > 0)
                {
                    sql = "update [E_SelectQuestions] set Questions=@Q,Answer=@A where QID=" + qID + "  ; select " + qID;

                }
                else
                {
                    sql = "insert into E_SelectQuestions values(@Q,@A) ;select SCOPE_IDENTITY(); ";
                }
                qID = db.ExecuteScalar<int>(sql, pars: new SqlParameter[] { new SqlParameter("@Q", row["Questions"]), new SqlParameter("@A", row["Answer"]) });
                db.ExecuteNonQuery("delete [E_CSelectQuestions] where QID=" + qID);
                ds.Tables[1].Columns.Add(new DataColumn("QID", typeof(int)) { DefaultValue = qID });
                db.BulkCopyToDB(ds.Tables[1], "E_CSelectQuestions");
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                return ex.Message;
            }
            return "";
        }

        /// <summary>
        ///保存填空题信息
        /// </summary>
        /// <param name="Questions">问题</param>
        /// <param name="Answer">以╀分隔的答案</param>
        public string UpdateTKData(string Questions, string Answer, int QID)
        {
            string sql = "";
            if (QID > 0)
            {
                sql = "update [E_TKQuestions] set [Questions]=@Q ,[Answer]=@A where TKID=" + QID;
            }
            else
            {
                sql = "insert into E_TKQuestions Values(@Q,@A)";
            }
            try
            {
                db.ExecuteNonQuery(sql, pars: new SqlParameter[] { new SqlParameter("@Q", Questions), new SqlParameter("@A", Answer) });

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }
    }
}
