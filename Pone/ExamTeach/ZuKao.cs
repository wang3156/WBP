using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        Main m;
        private void ZuKao_Load(object sender, EventArgs e)
        {
            LoadKSInfo();
            m = this.MdiParent as Main;
        }

        public void LoadKSInfo()
        {
            dataGridView1.DataSource = Comm.GetKSInfo(textBox1.Text.Trim());
        }

        private void ZuKao_FormClosing(object sender, FormClosingEventArgs e)
        {
            m.DisabledMSetExam();
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
                    m.TL.StartExamByEID(new int[] { Convert.ToInt32(vr.Cells["EID"].Value) });
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
                    m.TL.EndExamByEID(new int[] { Convert.ToInt32(vr.Cells["EID"].Value) });

                    MessageBox.Show("操作成功!");
                    break;
                case "E_ExamKS":
                    E_Student es = new E_Student(Convert.ToInt32(vr.Cells["EID"].Value), vr.Cells["EStatus"].Value.ToString(),m);
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

            m.TL.StartExamByEID(edis);
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

            m.TL.EndExamByEID(edis);
        }
    }
}
