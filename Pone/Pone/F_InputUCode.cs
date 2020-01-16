using P_Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pone
{
    public partial class F_InputUCode : Form
    {
        public F_InputUCode()
        {
            InitializeComponent();
        }

        public string UCode;

        private void button1_Click(object sender, EventArgs e)
        {
            UCode = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(UCode))
            {
                MessageBox.Show("请输入账号!", "提示");
            }
            else
            {
                TB_VailUser Vuser = new TB_VailUser().StudentDb.GetSingle(c => c.UCode == UCode);
                if (Vuser != null)
                {
                    F_Mian f = (this.Tag as F_Mian);
                    f.Text = $"信息验证({Vuser.UCode} {Vuser.UName} )";
                    f.Vuser = Vuser;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("输入的账号有误!");
                }

            }
        }
    }
}
