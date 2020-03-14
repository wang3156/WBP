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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        TeacherB tb = new TeacherB();
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt_User.Text = Txt_User.Text.Trim()) || string.IsNullOrWhiteSpace(Txt_PassWord.Text = Txt_PassWord.Text.Trim()))
            {
                MessageBox.Show("请输入用户名密码!");
                return;
            }

            if (!tb.CheckLogin(Txt_User.Text, Txt_PassWord.Text))
            {
                MessageBox.Show("账号或密码错误!");
            }
            else
            {

                p.Text = $"教师端（{(p.UName = Txt_User.Text)}）";
                this.Close();
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            p.Close();
        }

        Main p;
        private void Login_Load(object sender, EventArgs e)
        {
            p = (this.Tag as Main);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            tb.Dispose();
        }
    }
}
