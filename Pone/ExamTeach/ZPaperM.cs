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
    public partial class ZPaperM : Form
    {
        public ZPaperM()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            using (TeacherB tb = new TeacherB())
            {
                dataGridView1.DataSource = tb.GetPaper(textBox1.Text);
            }
        }

        private void ZPaperM_Load(object sender, EventArgs e)
        {
            button2_Click(null, null);

        }

        public void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 新增试卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ZPaper zp = new ZPaper();
            zp.Tag = new FPars { PID = 0, Zpa = this };
            zp.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText != "操作")
            {
                return;
            }
            ZPaper zp = new ZPaper();
            zp.Tag = new FPars { PID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["PID"].Value), Zpa = this };
            zp.ShowDialog();
        }

        private void ZPaperM_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.MdiParent as Main).DisabledMenu_MZJ();
        }
    }
}
