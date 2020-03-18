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

        private void ZuKao_Load(object sender, EventArgs e)
        {
            LoadKSInfo();
        }

        public void LoadKSInfo()
        {
            dataGridView1.DataSource = Comm.GetKSInfo(textBox1.Text.Trim());
        }

        private void ZuKao_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.MdiParent as Main).DisabledMSetExam();
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
            string a = Convert.ToString(vr.Cells["EStatus"]);

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
                    break;
                case "EEndExam":
                    if (a != "正在考试")
                    {
                        MessageBox.Show(a);
                        return;
                    }
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

            SetPaperByExam se = new SetPaperByExam(li_Rows.Select(c => Convert.ToInt32(c["EID"])), this);
            se.ShowDialog();




        }
    }
}
