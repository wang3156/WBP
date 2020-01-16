using CommLibrary.DBHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pone
{
    public partial class F_InputUCode : Form
    {
        public F_InputUCode()
        {
            InitializeComponent();
        }

 

        private void button1_Click(object sender, EventArgs e)
        {
            string UCode = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(UCode))
            {
                MessageBox.Show("请输入账号!", "提示");
            }
            else
            {
                DataTable dt = new DataTable();
                using (MySqlDBHelper db=new MySqlDBHelper())
                {
                    dt = db.GetDataTable("select * From TB_VailUser where UCode=@code",pars: new MySqlParameter("@code", SqlDbType.VarChar) { Value = UCode });
                }
                 
                if (dt.Rows.Count==0)
                {
                    DataRow dr = dt.Rows[0];
                    F_Mian f = (this.Tag as F_Mian);
                    f.Text = $"信息验证({UCode} {dr["UName"]} )";                    
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
