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


    }
}
