using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamStudent
{
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Tcp;

    public partial class Main : Form
    {

        public DataRow UserRow;
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }


        CListener cl;
        private void Mian_Load(object sender, EventArgs e)
        {
            if (UserRow == null)
            {
                StuLogin sl = new StuLogin(this);
                if (sl.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
            }


            DisabledMian(false);

            string zkzh = Lab_ZKZH.Text = Convert.ToString(UserRow["ZKZH"]);
            Lab_UName.Text = Convert.ToString(UserRow["UName"]);
            Lab_XH.Text = Convert.ToString(UserRow["XH"]);
            Lab_Name.Text = "等待考试!";

            //StuAnswer.Columns.AddRange(new DataColumn[] { new DataColumn("EID", typeof(int)) { AllowDBNull = false }, new DataColumn("PID", typeof(int)) { AllowDBNull = false }, new DataColumn("QID", typeof(int)), new DataColumn("Answers", typeof(string)), new DataColumn("ZKZH", typeof(string)) { AllowDBNull = false } });
            Re();

            Action<ConnectCheck> atin = c =>
             {
                 switch (c.RCode)
                 {
                     case ResponseCode.ClientConnected:
                         if (papers.Rows.Count > 0)
                         {
                             DisabledMian(true);
                         }
                         break;
                     case ResponseCode.CheckOnLine:
                         ShowStatus(false);
                         break;
                     case ResponseCode.StartExam:
                         SetPaper();
                         break;
                     case ResponseCode.EndExam:
                         SaveData(true);
                         break;
                     case ResponseCode.DisabledExam:
                         DisabledExam();
                         break;
                     case ResponseCode.ServerColseConnected:
                         DisabledMian(false);
                         SaveData(false);
                         ShowStatus();
                         cl.Dispose();
                         this.Invoke(new Action(() =>
                         {
                             CShowDialog("服务器已断开!");

                         }));
                         cl.BeginConnction(zkzh);
                         break;
                     default:
                         break;
                 }

             };

            ShowStatus();
            ThreadPool.QueueUserWorkItem((a) =>
            {

                cl = new CListener();
                cl.ServerP = atin;
                cl.BeginConnction(zkzh);
                ShowStatus(false);
                if (Convert.ToInt32(UserRow["EStatus"]) == 1)  //考试中要直接加入
                {
                    this.Invoke(new Action(() => { SetPaper(); }));
                }
            });
        }



        public void CreateCotronl(DataRow dataRow, string Answer)
        {
            this.Invoke(new Action(() =>
            {
                Comm.CreateControl(dataRow, richTextBox1, panel3, false, Answer);
            }));
        }

        private void SaveData(bool isEnd)
        {
            using (StudentB sb = new StudentB())
            {
                sb.SaveData(StuAnswer);
            }
            if (isEnd)
            {
                this.Invoke(new Action(() =>
                {
                    CShowDialog("考试结束,系统将自动关闭!");
                    this.Close();
                }));
            }
        }

        DataTable papers;
        DataTable E_Test;
        int RowIndex = -1;

        DataTable StuAnswer;


        void DisabledMian(bool disabled)
        {
            panel1.Enabled = groupBox1.Enabled = disabled;
        }

        private void SetPaper()
        {
            DisabledMian(true);
            papers = Comm.GetPaperByEID(Convert.ToInt32(UserRow["EID"]));
            Lab_Name.Text = Convert.ToString(papers.Rows[0]["PaperName"]);
            E_Test = Comm.GetPaperMx(Convert.ToInt32(papers.Rows[0]["PID"]));
            //获取学生答案
            using (StudentB sb = new StudentB())
            {
                StuAnswer = sb.GetKSAnswer(Convert.ToInt32(UserRow["EID"]), Convert.ToInt32(papers.Rows[0]["PID"]), Lab_ZKZH.Text);
            }

            Btn_Next_Click(null, null);
            Btu_Submit.Enabled = true;
            StuAnswer.Columns["EID"].DefaultValue = Convert.ToInt32(UserRow["EID"]);
            StuAnswer.Columns["PID"].DefaultValue = Convert.ToInt32(papers.Rows[0]["PID"]);
            StuAnswer.Columns["ZKZH"].DefaultValue = Lab_ZKZH.Text;
            this.Invoke(new Action(() =>
            {
                notifyIcon1_MouseDoubleClick(null, null);
            }));
        }

        private void DisabledExam()
        {
            SaveData(false);
            this.Invoke(new Action(() =>
            {
                CShowDialog("你已被禁止参考,系统将自动退!");
                this.Close();
            }));


        }
        System.Windows.Forms.Timer t;
        void ShowStatus(bool lj = true)
        {
            if (lj)
            {
                if (t == null)
                {
                    t = new System.Windows.Forms.Timer();
                    t.Tick += T_Tick;
                    t.Interval = 1 * 1000;
                    t.Start();
                }
                else
                {
                    t.Start();
                }
                Lab_Connection.Text = "服务器连接中.....";

            }
            else
            {
                t.Stop();
                Lab_Connection.Text = "已连接服务器";
            }

        }

        private void T_Tick(object sender, EventArgs e)
        {
            int maxLength = 11;
            string gd = "服务器连接中";
            if (Lab_Connection.Text.Length == maxLength)
            {
                Lab_Connection.Text = gd;

            }
            else
            {
                gd = Lab_Connection.Text;
            }
            if (Lab_Connection.Text.Length < maxLength)
            {
                gd += ".";
            }
            Lab_Connection.Text = gd;
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            Btn_Next.Enabled = false;
            RecodAnswer();
            if ((RowIndex + 1) == E_Test.Rows.Count)
            {
                RowIndex = -1;
            }
            RowIndex++;
            DataRow dr = E_Test.Rows[RowIndex];

            CreateCotronl(dr, StuAnswer.AsEnumerable().Where(c => Convert.ToInt32(c["QID"]) == Convert.ToInt32(dr["QID"])).FirstOrDefault()?["Answers"].ToString());

            Lab_Count.Text = $"共 {E_Test.Rows.Count} 题,第 {RowIndex + 1} 题 ";


            Btn_Next.Enabled = true;
        }

        void RecodAnswer()
        {
            if (RowIndex < 0)
            {
                return;
            }
            //记录当前的答案
            DataRow row = E_Test.Rows[RowIndex];
            int qid = Convert.ToInt32(row["QID"]);
            DataRow arow = StuAnswer.AsEnumerable().Where(c => Convert.ToInt32(c["QID"]) == qid).FirstOrDefault();
            if (arow == null)
            {
                arow = StuAnswer.NewRow();
                arow["QID"] = qid;
                StuAnswer.Rows.Add(arow);
            }
            List<string> ans = new List<string>();

            foreach (Control cot in panel3.Controls)
            {
                if (Convert.ToInt32(row["QType"]) == 0)  //选择题
                {
                    CheckBox cb = cot as CheckBox;
                    if (cb == null || !cb.Checked)
                    {
                        continue;
                    }
                    ans.Add(cb.Name.Replace("CK_", ""));
                }
                else
                {
                    TextBox tb = cot as TextBox;
                    ans.Add(tb.Text.Trim());
                }
            }
            if (Convert.ToInt32(row["QType"]) == 0)  //选择题
            {
                arow["Answers"] = string.Join(",", ans);

            }
            else
            {
                arow["Answers"] = string.Join(TeacherB.TKASp, ans);
            }
        }

        private void Btn_Perv_Click(object sender, EventArgs e)
        {
            Btn_Perv.Enabled = false;
            RecodAnswer();
            if (RowIndex == 0)
            {
                RowIndex = E_Test.Rows.Count;
            }

            RowIndex--;

            DataRow dr = E_Test.Rows[RowIndex];
            CreateCotronl(dr, StuAnswer.AsEnumerable().Where(c => Convert.ToInt32(c["QID"]) == Convert.ToInt32(dr["QID"])).FirstOrDefault()?["Answers"].ToString());
            Lab_Count.Text = $"共 {E_Test.Rows.Count} 题,第 {RowIndex + 1} 题 ";

            Btn_Perv.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecodAnswer();
            SaveData(false);
            CShowDialog("保存成功!");
        }

        void CShowDialog(string msg)
        {
            TipMsg t = new TipMsg(msg);
            t.ShowDialog();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;

            }
        }

        private void Btu_Submit_Click(object sender, EventArgs e)
        {
            RecodAnswer();
            SaveData(false);
            MessageBox.Show("试卷已提交,确定后将自动退出");
        }

        public void Re()
        {
            TcpServerChannel channel = new TcpServerChannel(24563);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SmartKernel.Net.Monitor), "MonitorServerUrl", WellKnownObjectMode.SingleCall);
        }
    }
}
