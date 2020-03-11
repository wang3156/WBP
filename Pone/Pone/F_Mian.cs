
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
            InitFormPanlControl();
        }

        private void InitFormPanlControl()
        {
            PB_ProductImg.Image = Properties.Resources.noting;
            ClearLabTxt();

            P_Imgs.Visible = true;
            LinkLabel ll;
            for (int i = 0; i < 10; i++)
            {
                ll = new LinkLabel();
                ll.Text = (i + 1).ToString();
                ll.Tag = i;
                ll.Top = 5;
                ll.Width = 15;
                ll.Left = i * ll.Width;
                ll.Click += Ll_Click;
                P_Imgs.Controls.Add(ll);
                if (i == 0)
                {
                    Ll_Click(ll, null);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Current_Row = null;
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
                    Current_Row = null;
                    GetDataAndBindToForm();
                }
            }
        }

        private void GetDataAndBindToForm()
        {
            List<MySqlParameter> pars = new List<MySqlParameter>();
            string sql = "  select a.*,b.BRID,b.VerificationCode,b.BorrowUser,b.Handler,b.BorrowTime,b.BorrowTimeLimit,b.DateOfReturn,b.ReturnHandler,  (select count(1) From TB_BorrowRecord c where c.TID = b.TID and c.DateOfReturn is null) as `BCount`  From TB_Goods a,TB_BorrowRecord b where a.TID = b.TID ";
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
            sql += ";";
            DataTable dt = new DataTable();
            using (MySqlDBHelper mydb = new MySqlDBHelper())
            {
                dt = mydb.GetDataSet(sql, pars: pars.ToArray()).Tables[0];
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("验证信息有误!");
            }
            else
            {
                Current_Row = dt.Rows[0];
                BindDateToForm();
            }
        }

        string[] imgs = new string[] { @"D:\a\img\a.jpg", @"D:\a\img\b.jpg" };
        DataRow Current_Row;
        List<Control> FormCot = new List<Control>(30);

        void BindDateToForm()
        {

            Current_Row.Table.Columns.Add(new DataColumn("StrIsActive", typeof(string)) { Expression = "IIF(IsActive,'有效','无效')" });



            #region 处理预览图
            P_Imgs.Visible = false;
            P_Imgs.Controls.Clear();
            imgs = Convert.ToString(Current_Row["ProductImg"]).Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //BRID, VerificationCode, BorrowUser, Handler, BorrowTime, BorrowTimeLimit, DateOfReturn, ReturnHandler
            if (imgs.Length > 0)
            {
                //PB_ProductImg.ImageLocation = imgs[0];
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
                        ll.Width = 20;
                        ll.Left = i * ll.Width;
                        ll.Click += Ll_Click;
                        P_Imgs.Controls.Add(ll);
                        if (i==0)
                        {
                            Ll_Click(ll, null);
                        }
                    }
                }
            }
            else
            {//无预览的图
                PB_ProductImg.Image = Properties.Resources.noting;

            }
            #endregion

            #region 绑定其它固有数据的值 
            FormCot.ForEach(c =>
            {
                string cname = c.Name.Replace("Lab_", "");
                if (Current_Row.Table.Columns.Contains(cname))
                    c.Text = Convert.ToString(Current_Row[cname]);
            });

            #endregion

            //绑定其它信息的值 
            Label lother = (this.panel2.Controls.Find("Lab_Other", true).FirstOrDefault() as Label);
            if (Current_Row["DateOfReturn"] == DBNull.Value)
            {
                DateTime now = DateTime.Now;
                lother.Text = "尚未归还";
                DateTime DueTime = Convert.ToDateTime(Current_Row["BorrowTime"]).AddDays(Convert.ToInt32(Current_Row["BorrowTimeLimit"]));
                lother.Text += $",应归还日期为：{ DueTime.ToString("yyyy年MM月dd日")}。";
                if (DueTime < now)
                {
                    double dbnow = (now - DueTime).TotalMinutes;
                    double hour = Math.Floor((dbnow / 60) % 24);
                    lother.Text += $" 逾期：{ Math.Floor(dbnow / 60 / 24)}天{hour}小时{Math.Ceiling((dbnow / 60))}分 \r\n 逾期金额（5元/小时）：{Math.Round((dbnow / 60) * 5, 2, MidpointRounding.AwayFromZero)}";
                    lother.ForeColor = Color.Red;
                }
            }

        }

        void ClearLabTxt()
        {

            foreach (Control item in this.panel2.Controls)
            {
                foreach (Control lab in item.Controls)
                {
                    if (lab.Name.StartsWith("Lab_"))
                    {
                        lab.Text = "";
                        FormCot.Add(lab);
                    }
                }
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
