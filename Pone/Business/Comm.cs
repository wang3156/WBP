using CommLibrary.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business
{
    public class Comm
    {

        /// <summary>
        /// 获取选择题选项
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCSelectOpetion(int QID)
        {
            using (SqlServerDBHelper db = new SqlServerDBHelper())
            {
                return db.GetDataSet("select * From [dbo].[E_CSelectQuestions] where QID=" + QID).Tables[0];
            }
        }

        /// <summary>
        /// 获取选择题选项
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCTK(int QID)
        {
            using (SqlServerDBHelper db = new SqlServerDBHelper())
            {
                return db.GetDataSet("select * From [dbo].[E_TKQuestions] where QID=" + QID).Tables[0];
            }
        }


        /// <summary>
        /// 根据传入的行数据生成控件
        /// </summary>
        /// <param name="row"></param>
        public static void CreateControl(DataRow dataRow, System.Windows.Forms.RichTextBox txt_Questions, System.Windows.Forms.Panel p_Content, bool teacher)
        {
            string q = Convert.ToString(dataRow["Questions"]);
            int Qtype = Convert.ToInt32(dataRow["QType"]);
            int QID = Convert.ToInt32(dataRow["QID"]);

            txt_Questions.Text = q;
            p_Content.Controls.Clear();

            DataRow row;
            List<Control> cots = new List<Control>();
            string[] ans;
            if (Qtype == 0)
            {
                DataTable dt = GetCSelectOpetion(QID);
                string code;
                ans = Convert.ToString(dataRow["Answer"]).Split(',');

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    row = dt.Rows[i];
                    code = Convert.ToString(row["_option"]);
                    Label le = new Label();
                    le.AutoSize = false;
                    le.Height = 20;
                    le.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    le.Text = $"{code} . {Convert.ToString(row["_content"])}";
                    CheckBox cb = new CheckBox();
                    cb.Name = "CK_" + code;
                    le.Top = (i * le.Height + 5 * i);
                    cb.Height = 20;
                    cb.Top = (i * cb.Height + 5 * i);


                    cb.Left = 5;
                    cb.Width = 20;
                    le.Left = 27;

                    if (teacher)
                    {
                        cb.Enabled = false;
                        cb.Checked = ans.Contains(code);
                    }

                    cots.Add(le);
                    cots.Add(cb);
                }
            }
            else
            {
                DataTable dt = GetCTK(QID);

                row = dt.Rows[0];
                ans = Convert.ToString(row["Answer"]).Split(new string[] { TeacherB.TKASp }, StringSplitOptions.None);
                for (int i = 0; i < ans.Length; i++)
                {
                    if (teacher)
                    {//显示label给老师看
                        Label le = new Label();
                        le.Top = (i * le.Height + 5 * i);
                        le.Left = 5;
                        le.AutoSize = false;
                        le.Height = 25;
                        le.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                        le.Text = $"第{((i + 1))}空 : {ans[i]}";
                        cots.Add(le);

                    }
                    else
                    { //显示文本框给用户填写
                        TextBox tb = new TextBox();
                        tb.Top = (i * tb.Height + 5 * i);
                        tb.Left = 5;
                        tb.Width = 540;
                        cots.Add(tb);
                    }
                }
            }
            p_Content.Controls.AddRange(cots.ToArray());

        }

        /// <summary>
        /// 获取考试信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetKSInfo(string examName)
        {
            DataSet dt = null;
            using (SqlServerDBHelper db = new SqlServerDBHelper())
            {
                dt = db.GetDataSet(@"select[EID],[ExamName],[ExamRemark],[EStart],[EEnd],[EStatus] = (case [EStatus] when 0 then N'未开考' when 1 then '正在考试' else '考试结束' end ),b.*
 From E_ExamInfo a
left join E_Paper b on b.PID = a.PID  where ExamName like @e", pars: new SqlParameter[] { new SqlParameter("@e", $"%{examName}%") });
            }
            if (dt.Tables.Count > 0)
            {
                return dt.Tables[0];
            }
            return null;
        }
    }
}
