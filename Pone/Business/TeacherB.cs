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

        SqlServerDBHelper db = new SqlServerDBHelper();

        public void BeginTransaction()
        {
            db.BeginTransaction();
        }

        public void Commit()
        {
            db.Commit();
        }
        public void Rollback()
        {
            db.Rollback();
        }







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




        #region 试题操作
        /// <summary>
        /// 填空填答案的分隔符
        /// </summary>
        public const string TKASp = "╀";
        /// <summary>
        /// 保存选择题 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="qID"></param>
        /// <returns></returns>
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
                sql = "update [E_TKQuestions] set [Questions]=@Q ,[Answer]=@A where QID=" + QID;
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

        public void EmptyServerInfo()
        {
            db.ExecuteNonQuery("truncate table E_ServerInfo");
        }


        #endregion


        #region 试卷操作
        public DataTable GetPaper(string where)
        {
            DataSet dt = db.GetDataSet("select * From [dbo].[E_Paper] where [PaperName] like @pn ", pars: new SqlParameter[] { new SqlParameter("@pn", $"%{where}%") });
            if (dt.Tables.Count > 0)
            {
                return dt.Tables[0];
            }
            return null;
        }

        public DataTable GetPaper(int PID)
        {
            DataSet dt = db.GetDataSet("select * From [dbo].[E_Paper] where PID= " + PID);
            if (dt.Tables.Count > 0)
            {
                return dt.Tables[0];
            }
            return null;
        }

        public string SavePaper(ref int pID, string pName, DataTable dataTable)
        {
            db.BeginTransaction();
            SqlParameter[] pars = new SqlParameter[] { new SqlParameter("@n", pName) };
            try
            {
                if (pID > 0)
                {
                    db.ExecuteNonQuery("update E_Paper set PaperName=@n where PID=" + pID, pars: pars);
                    db.ExecuteNonQuery("Delete [dbo].[E_CPaper] where PID=" + pID);
                }
                else
                {
                    pID = db.ExecuteScalar<int>("insert into E_Paper values(@n) ; select SCOPE_IDENTITY(); ", pars: pars);
                }
                dataTable.Columns.Add(new DataColumn("PID", typeof(int)) { DefaultValue = pID });
                db.BulkCopyToDB(dataTable, "E_CPaper", new Dictionary<string, string>() { { "PID", "PID" }, { "QID", "QID" }, { "QType", "QType" } });
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
        /// 获取试卷数据 
        /// </summary>
        /// <param name="pID">试卷ID</param>
        /// <returns>表1 试卷信息  表2 试卷明细</returns>
        public DataSet GetPaperMx(int pID)
        {
            string sql = @"
select a.QType,b.* From [dbo].[E_CPaper] a,[dbo].[E_SelectQuestions] b where PID=@p and a.[QType]=0 and b.QID=a.QID
union all
select a.QType,b.* From [dbo].[E_CPaper] a,[dbo].[E_TKQuestions] b where PID=@p and a.[QType]=1 and b.QID=a.QID
 ";
            return db.GetDataSet(sql, pars: new SqlParameter[] { new SqlParameter("@p", pID) });
        }

        /// <summary>
        /// 获取试卷中没有选择的数据 
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="QType">0 选择填 1 填空题</param>
        /// <returns></returns>
        public DataTable GetUnData(int pID, int QType)
        {
            string[] sql = new string[] { "select QType=0,c.* From [E_SelectQuestions] c where not exists( select 1 From [dbo].[E_CPaper] a where PID=@p and a.[QType]=0  and c.QID=a.QID)", "select QType=1,c.* From [E_TKQuestions] c where not exists( select 1 From [dbo].[E_CPaper] a where PID=@p and a.[QType]=1  and c.QID=a.QID) " };
            DataSet ds = db.GetDataSet(sql[QType], pars: new SqlParameter[] { new SqlParameter("@p", pID) });
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region 考试操作
        /// <summary>
        /// 新增一场考试
        /// </summary>
        /// <param name="Name">考试名称</param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        /// <param name="selectedValue">试卷信息</param>
        /// <param name="remark">说明 </param>

        /// <returns></returns>
        public DataTable AddExam(string Name, string STime, string ETime, object selectedValue, string remark)
        {
            return db.GetDataSet(@"INSERT INTO [dbo].[E_ExamInfo]
           ([ExamName]
           ,[ExamRemark]
           ,[EStart]
           ,[EEnd]
           ,[EStatus]
           ,[PID])
     VALUES
           (@n
           ,@r
           ,@es
           ,@en
           ,0
           ,@p)  ;  select * From E_ExamInfo where [EID]=SCOPE_IDENTITY() ", pars: new SqlParameter[] { new SqlParameter("@n", Name), new SqlParameter("@r", remark), new SqlParameter("@es", STime), new SqlParameter("@en", ETime), new SqlParameter("@p", selectedValue) }).Tables[0];
        }

        public DataTable UpdateExam(string Name, string STime, string ETime, object selectedValue, string remark, int EID)
        {
            return db.GetDataSet(@"UPDATE [dbo].[E_ExamInfo]
   SET [ExamName] = @ExamName
      ,[ExamRemark] = @ExamRemark
      ,[EStart] = @EStart
      ,[EEnd] = @EEnd    
      ,[PID] = @PID
 WHERE EID=" + EID + " ; select * From E_ExamInfo where [EID]=" + EID, pars: new SqlParameter[] { new SqlParameter("@ExamName", Name), new SqlParameter("@ExamRemark", remark), new SqlParameter("@EStart", STime), new SqlParameter("@EEnd", ETime), new SqlParameter("@PID", selectedValue) }).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">考生信息</param>
        /// <param name="EID">考试号</param>
        public void UpdateKSList(DataTable dt, int EID)
        {
            dt.Columns.Add(new DataColumn("EID", typeof(int)) { DefaultValue = EID });

            dt.Columns["学号"].ColumnName = "XH";
            dt.Columns["准考证号"].ColumnName = "ZKZH";
            dt.Columns["姓名"].ColumnName = "UName";
            db.ExecuteNonQuery("Delete E_StudentKs where EID=" + EID);

            db.BulkCopyToDB(dt, "E_StudentKs");

        }


        /// <summary>
        /// 设置一批考试的试卷
        /// </summary>
        /// <param name="eIDs">一批考试号</param>
        /// <param name="r">试卷号</param>
        public void SetPaperWithExam(IEnumerable<int> eIDs, int r)
        {
            db.ExecuteNonQuery($"update E_ExamInfo set PID={r} where EID in({string.Join(",", eIDs)})");
        }

        /// <summary>
        /// 设置一批考试的状态
        /// </summary>
        /// <param name="eIDs">一批考试号</param>
        /// <param name="r">状态 1 开始考试  2 结束考试</param>
        public void SetStatusWithExam(IEnumerable<int> eIDs, int r)
        {
            db.ExecuteNonQuery($"update E_ExamInfo set EStatus={r} where EID in({string.Join(",", eIDs)})");
        }


        public DataTable GetKsData(int EID, string lx, string where)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "准考证号", "ZKZH" }, { "姓名", "UName" }, { "学号", "XH" } };
            return db.GetDataSet($"select * From [dbo].[E_StudentKs] where EID={EID}  {(string.IsNullOrWhiteSpace(where) ? "" : " and " + dic[lx] + "=@n")}", pars: new SqlParameter[] { new SqlParameter("@n", where) }).Tables[0];
        }

        public void DeleteStudent(int id)
        {
            db.ExecuteNonQuery("delete E_StudentKs where stid=" + id);
        }

        #endregion
    }
}
