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

namespace ExamStudent
{
    public partial class StuLogin : Form
    {
        Main M;
        public StuLogin(Main m)
        {
            InitializeComponent();
            M = m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("请输入准考证号!");
                return;
            }
            DataTable dt = Comm.GetUserInfoByZKZH(textBox1.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("你输入的准考证号不存在或被禁止考试!");
                return;
            }
            M.UserRow = dt.Rows[0];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
