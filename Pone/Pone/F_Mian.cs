
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
    public partial class F_Mian : Form
    {
        public TB_VailUser Vuser = null;
        public F_Mian()
        {
            InitializeComponent();
        }

        private void F_Mian_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vuser = null;
            this.Text = "信息验证 (未登录)";
            MessageBox.Show("注销成功!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Vuser == null)
            {
                F_InputUCode fu = new F_InputUCode();
                fu.Tag = this;
                fu.ShowDialog();
            }
        }
    }


}
