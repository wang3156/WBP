using Business;
using SmartKernel.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamTeach
{
    public partial class E_Student : Form
    {
        int EID;
        string status;
        Main M;
        public E_Student(int EID, string status, Main m)
        {
            this.EID = EID;
            this.status = status;
            M = m;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            textBox1.Text = textBox1.Text.Trim();
            using (TeacherB tb = new TeacherB())
            {
                dataGridView1.DataSource = tb.GetKsData(EID, comboBox1.Text, textBox1.Text);
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            DataRowView vr = dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView;
            string zkzh = vr["ZKZH"].ToString();

            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "删除")
            {
                int id = Convert.ToInt32(vr["STID"]);
                if (MessageBox.Show("确认删除?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    using (TeacherB tb = new TeacherB())
                    {
                        tb.DeleteStudent(id);
                    }
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    MessageBox.Show("删除成功!");
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "禁考")
            {


                if (Convert.ToBoolean(vr["JZKS"]))
                {
                    MessageBox.Show("该考生已被禁止考试!");
                    return;
                }
                using (TeacherB tb = new TeacherB())
                {
                    tb.DisabledStu(EID, zkzh);
                }
                vr["JZKS"] = true;
                M.TL?.SendTOClient(new ConnectCheck() { RCode = ResponseCode.DisabledExam }, zkzh);

                MessageBox.Show("该考生已被禁止考试!");

            }
            else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "查看")
            {

                string s = M.GetHostName(zkzh, EID);
                if (string.IsNullOrWhiteSpace(s))
                {
                    MessageBox.Show("用户未上线!");
                    return;
                }
                //MonitorClient mc = new MonitorClient(s);
                //mc.Show();
                string ptah = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"viewer\Viewer.exe ");
                System.Diagnostics.Process.Start(ptah, s);

            }
        }

        private void E_Student_Load(object sender, EventArgs e)
        {
            if (status == "正在考试")
            {
                ((DataGridViewLinkColumn)dataGridView1.Columns["SDelete"]).Text = dataGridView1.Columns["SDelete"].HeaderText = "禁考";
            }
            else if (status == "考试结束")
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
            }
            LoadData();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            DataRowView dv = dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (Convert.ToBoolean(dv["JZKS"]))
            {

                foreach (DataGridViewCell item in dataGridView1.Rows[e.RowIndex].Cells)
                {
                    item.Style.SelectionForeColor = item.Style.ForeColor = Color.Red;

                }

            }
        }
    }
}
