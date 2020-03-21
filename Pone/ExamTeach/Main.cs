using Business;
using ExamTeach.WQuestions;
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

namespace ExamTeach
{
    public partial class Main : Form
    {
        public string UName;
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public TListener TL;
        private void Main_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(UName))
            //{
            //    Login l = new Login();
            //    l.Tag = this;
            //    l.ShowDialog();
            //}
            M_EndListener.Enabled = false;

        }

        private void AddSelect_Click(object sender, EventArgs e)
        {
            AddSelect.Enabled = false;
            SelectQuestions sq = new SelectQuestions();
            sq.MdiParent = this;
            sq.Show();
        }

        public void DisabledMenu_AddSelect()
        {
            AddSelect.Enabled = true;
        }

        public void DisabledMenu_AddTK()
        {
            AddTK.Enabled = true;
        }

        private void AddTK_Click(object sender, EventArgs e)
        {
            AddTK.Enabled = false;
            TKQuestions tk = new TKQuestions();
            tk.MdiParent = this;
            tk.Show();

        }

        private void MZJ_Click(object sender, EventArgs e)
        {
            MZJ.Enabled = false;
            ZPaperM zm = new ZPaperM();
            zm.MdiParent = this;
            zm.Show();
        }

        public void DisabledMenu_MZJ()
        {
            MZJ.Enabled = true;
        }

        private void MSetExam_Click(object sender, EventArgs e)
        {
            MSetExam.Enabled = false;
            ZuKao zk = new ZuKao();
            zk.MdiParent = this;
            zk.Show();
        }
        public void DisabledMSetExam()
        {
            MSetExam.Enabled = true;
        }

        private void M_StartListener_Click(object sender, EventArgs e)
        {
            //启动监听服务
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

        internal string GetHostName(string zkzh,int EID)
        {
           return TL.GetHostName(zkzh, EID);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (TeacherB tb=new TeacherB())
            {
                tb.EmptyServerInfo();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SockTest st = new SockTest();
            st.Show();
        }
    }


}
