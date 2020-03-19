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
    public partial class E_Student : Form
    {
        int EID;
        string status;
        public E_Student(int EID, string status)
        {
            this.EID = EID;
            this.status = status;
            InitializeComponent();
          
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

            if (dataGridView1.Columns[e.ColumnIndex].Name == "SDelete")
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["STID"].Value);
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
        }

        private void E_Student_Load(object sender, EventArgs e)
        {
            if (status != "未开考")
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
            }
            LoadData();
        }
    }
}
