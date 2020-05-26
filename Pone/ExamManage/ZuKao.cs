using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamTeach
{
    public partial class ZuKao : Form
    {
        public ZuKao()
        {
            InitializeComponent();
        }
        public TListener TL;
        public string UName;

        public void checkend()
        {

            //检查考试状态
            Thread ttt = new Thread(() =>
            {
                DataTable dt = null;
                while (true)
                {
                    using (TeacherB tb = new TeacherB())
                    {
                        dt = tb.CheckEndExam();
                    }
                    if (dt.Rows.Count > 0 && TL != null)
                    {
                        TL.EndExamByEID(dt.AsEnumerable().Select(c => Convert.ToInt32(c["EID"])));
                    }
                    Thread.Sleep(30 * 1000);
                }
            });
            ttt.IsBackground = true;
            ttt.Start();
        }
        private void ZuKao_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UName))
            {
                Login l = new Login();
                l.Tag = this;
                if (l.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;

                }
            }
            M_EndListener.Enabled = false;
            LoadKSInfo();
            checkend();
        }

        public void LoadKSInfo()
        {
            dataGridView1.DataSource = Comm.GetKSInfo(textBox1.Text.Trim());
        }

        private void ZuKao_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (TeacherB tb = new TeacherB())
            {
                tb.EmptyServerInfo();
            }
            TL?.Dispose();

            //杀掉所有监控进程
            killProcess();
        }
        void killProcess()
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程

            pro.Where(c => c.ProcessName.Equals("viewer", StringComparison.CurrentCultureIgnoreCase)).ToList().ForEach(c =>
            {
                c.Kill();
            });
        }


        private void button5_Click(object sender, EventArgs e)
        {
            AddExamF af = new AddExamF();
            af.Tag = new { R = "", P = this };
            af.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }
            DataGridViewRow vr = dataGridView1.Rows[e.RowIndex];
            string a = Convert.ToString(vr.Cells["EStatus"].Value);

            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "E_Paper":
                    AddExamF af = new AddExamF();
                    af.Tag = new { R = (dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView).Row, P = this };
                    af.ShowDialog();
                    break;
                case "EStartExam":
                    if (a != "未开考")
                    {
                        MessageBox.Show(a);
                        return;
                    }
                    using (TeacherB tb = new TeacherB())
                    {
                        tb.SetStatusWithExam(new int[] { Convert.ToInt32(vr.Cells["EID"].Value) }, 1);
                    }
                    vr.Cells["EStatus"].Value = "正在考试";
                    if (TL != null)
                        TL.StartExamByEID(new int[] { Convert.ToInt32(vr.Cells["EID"].Value) });
                    MessageBox.Show("操作成功!");
                    break;
                case "EEndExam":
                    if (a != "正在考试")
                    {
                        MessageBox.Show(a);
                        return;
                    }

                    using (TeacherB tb = new TeacherB())
                    {
                        tb.SetStatusWithExam(new int[] { Convert.ToInt32(vr.Cells["EID"].Value) }, 2);
                    }
                    vr.Cells["EStatus"].Value = "考试结束";
                    if (TL != null)
                    {
                        TL.EndExamByEID(new int[] { Convert.ToInt32(vr.Cells["EID"].Value) });
                    }


                    MessageBox.Show("操作成功!");
                    break;
                case "E_ExamKS":
                    E_Student es = new E_Student(Convert.ToInt32(vr.Cells["EID"].Value), vr.Cells["EStatus"].Value.ToString(), this);
                    es.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadKSInfo();
        }

        /// <summary>
        /// 批量设置试卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            List<DataRowView> li_Rows = new List<DataRowView>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells["CheckRow"].Value) == "1")
                {
                    if (row.Cells["EStatus"].Value.ToString() != "未开考")
                    {
                        MessageBox.Show("选择中有考试正在进行或已结束无法进行此操作!");
                        return;
                    }
                    li_Rows.Add(row.DataBoundItem as DataRowView);
                }
            }
            if (li_Rows.Count == 0)
            {
                MessageBox.Show("没有选择需要操作的数据 !");
                return;
            }
            SetPaperByExam se = new SetPaperByExam(li_Rows.Select(c => Convert.ToInt32(c["EID"])), this);
            se.ShowDialog();




        }
        //批量开始
        private void button2_Click(object sender, EventArgs e)
        {
            List<DataRowView> li_Rows = new List<DataRowView>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells["CheckRow"].Value) == "1")
                {
                    if (row.Cells["EStatus"].Value.ToString() != "未开考")
                    {
                        MessageBox.Show("选择中有考试正在进行或已结束无法进行此操作!");
                        return;
                    }
                    li_Rows.Add(row.DataBoundItem as DataRowView);
                }
            }
            if (li_Rows.Count == 0)
            {
                MessageBox.Show("没有选择需要操作的数据 !");
                return;
            }
            var edis = li_Rows.Select(c => Convert.ToInt32(c["EID"]));
            using (TeacherB tb = new TeacherB())
            {
                tb.SetStatusWithExam(edis, 1);
            }
            li_Rows.ForEach(c =>
            {
                c["EStatus"] = "正在考试";
            });
            if (TL != null)
                TL.StartExamByEID(edis);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<DataRowView> li_Rows = new List<DataRowView>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells["CheckRow"].Value) == "1")
                {
                    if (row.Cells["EStatus"].Value.ToString() != "正在考试")
                    {
                        MessageBox.Show("选择中有考试不在进行中无法进行此操作!");
                        return;
                    }
                    li_Rows.Add(row.DataBoundItem as DataRowView);
                }
            }
            if (li_Rows.Count == 0)
            {
                MessageBox.Show("没有选择需要操作的数据 !");
                return;
            }

            var edis = li_Rows.Select(c => Convert.ToInt32(c["EID"]));
            using (TeacherB tb = new TeacherB())
            {
                tb.SetStatusWithExam(edis, 2);
            }
            li_Rows.ForEach(c =>
            {
                c["EStatus"] = "考试结束";
            });
            if (TL != null)
                TL.EndExamByEID(edis);
        }


        /////////////////////

        internal string GetHostName(string zkzh, int EID)
        {

            return TL?.GetHostName(zkzh, EID);
        }

        private void M_StartListener_Click(object sender, EventArgs e)
        {   //启动监听服务
            try
            {
                TL = new TListener();
                TL.BeginListen();
                this.Text += "(服务已开启)";
                M_StartListener.Enabled = !(M_EndListener.Enabled = true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务开启失败!" + ex.Message);
            }

        }

        private void M_EndListener_Click(object sender, EventArgs e)
        {
            TL.Dispose();
            TL = null;
            this.Text = this.Text.Replace("(服务已开启)", "");
            M_StartListener.Enabled = !(M_EndListener.Enabled = false);

        }
    }
}
