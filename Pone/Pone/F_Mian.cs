
using CommLibrary.DBHelper;
using MySql.Data.MySqlClient;
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
            PB_ProductImg.Image = Properties.Resources.noting;
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
            if (Vuser != null)
            {
                if (string.IsNullOrWhiteSpace(Txt_BRID.Text) && string.IsNullOrWhiteSpace(Txt_VerificationCode.Text))
                {
                    MessageBox.Show("请输入验证条件!");
                    return;
                }
                else
                {
                    List<MySqlParameter> pars = new List<MySqlParameter>();
                    string sql = "select a.*,b.BRID,b.VerificationCode,b.BorrowUser,b.Handler,b.BorrowTime,b.BorrowTimeLimit,b.DateOfReturn,b.ReturnHandler From TB_Goods a,TB_BorrowRecord b where a.TID=b.TID ";
                    if (!string.IsNullOrWhiteSpace(Txt_BRID.Text))
                    {
                        sql += " and b.BRID =@brid ";
                        pars.Add(new MySqlParameter("@brid", Txt_BRID.Text.Trim()));
                    }
                    if (!string.IsNullOrWhiteSpace(Txt_VerificationCode.Text))
                    {
                        sql += " and b.VerificationCode =@vcode ";
                        pars.Add(new MySqlParameter("@vcode", Txt_VerificationCode.Text.Trim()));
                    }
                    DataTable dt = new DataTable();
                    using (MySqlDBHelper mydb = new MySqlDBHelper())
                    {
                        dt = mydb.GetDataTable(sql, pars: pars.ToArray());
                    }
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("验证信息有误!");
                    }
                    else
                    {
                        BindDateToForm(dt.Rows[0]);
                    }
                }
            }
        }

        string[] imgs = new string[] { @"D:\a\img\a.jpg", @"D:\a\img\b.jpg" };

        void BindDateToForm(DataRow row)
        {
            P_Imgs.Visible = false;
            P_Imgs.Controls.Clear();

            //BRID, VerificationCode, BorrowUser, Handler, BorrowTime, BorrowTimeLimit, DateOfReturn, ReturnHandler
            if (imgs.Length > 0)
            {
                PB_ProductImg.ImageLocation = imgs[0];
                if (imgs.Length > 1)
                {
                    P_Imgs.Visible = true;
                    LinkLabel ll;
                    for (int i = 0; i < imgs.Length; i++)
                    {
                        ll = new LinkLabel();
                        ll.Text = (i + 1).ToString();
                        ll.Tag = i;
                        ll.Top = 5;
                        ll.Width = 15;
                        ll.Left = i * ll.Width;
                        ll.Click += Ll_Click;
                        P_Imgs.Controls.Add(ll);
                    }
                }
            }
            else
            {//无预览的图
                PB_ProductImg.Image = Properties.Resources.noting;

            }

        }

        private void Ll_Click(object sender, EventArgs e)
        {


            LinkLabel ll = (sender as LinkLabel);
            PB_ProductImg.ImageLocation = imgs[Convert.ToInt32(ll.Tag)];
            ll.LinkBehavior = LinkBehavior.NeverUnderline;
            ll.Cursor = Cursors.No;
            foreach (LinkLabel item in P_Imgs.Controls)
            {
                LinkLabel lb = item as LinkLabel;
                if (lb == ll)
                {
                    continue;
                }
                else
                {
                    lb.LinkBehavior = LinkBehavior.SystemDefault;
                    lb.Cursor = Cursors.Default;
                }
            }
        }
 
    }


}
