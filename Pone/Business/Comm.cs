using CommLibrary.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business
{
    public class Comm
    {

        /// <summary>
        /// socket包的结束标志
        /// </summary>
        public const string EndMark = "⒂";

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
        ///通过准考证号获取学生信息
        /// </summary>
        /// <param name="zkzh"></param>
        /// <returns></returns>
        internal static DataTable GetUserInfoByZKZH(string zkzh)
        {
            DataTable dt;
            using (SqlServerDBHelper db = new SqlServerDBHelper())
            {
                dt = db.GetDataSet($"	   select  * From [E_StudentKs] where [ZKZH]='{zkzh}'").Tables[0];
            }
            return dt;
        }

        internal static void UpdateServerInfo(string server_ip, int server_port)
        {
            using (SqlServerDBHelper db = new SqlServerDBHelper())
            {

                db.ExecuteNonQuery($@"truncate table E_ServerInfo
                          insert into E_ServerInfo values('{server_ip}',{server_port})");
            }
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

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetServerSocketInfo()
        {
            using (SqlServerDBHelper db = new SqlServerDBHelper())
            {
                return db.GetDataSet("select * From E_ServerInfo").Tables[0];
            }

        }


        #region IP相关
        /// <summary>  
        /// 获取当前使用的IP  
        /// </summary>  
        /// <returns></returns>  
        public static string GetLocalIP()
        {
            string result = RunApp("route", "print", true);
            Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
            if (m.Success)
            {
                return m.Groups[2].Value;
            }
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                    c.Connect("www.baidu.com", 80);
                    string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                    c.Close();
                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>  
        /// 获取本机主DNS  
        /// </summary>  
        /// <returns></returns>  
        public static string GetPrimaryDNS()
        {
            string result = RunApp("nslookup", "", true);
            Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>  
        /// 运行一个控制台程序并返回其输出参数。  
        /// </summary>  
        /// <param name="filename">程序名</param>  
        /// <param name="arguments">输入参数</param>  
        /// <returns></returns>  
        static string RunApp(string filename, string arguments, bool recordLog)
        {
            try
            {

                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {

                    //上面标记的是原文，下面是我自己调试错误后自行修改的  
                    Thread.Sleep(100);           //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行  
                                                 //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应  
                    if (!proc.HasExited)         //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行  
                    {                            //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行  
                        proc.Kill();
                    }
                    string txt = sr.ReadToEnd();
                    sr.Close();

                    return txt;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }
        #endregion



    }
}
