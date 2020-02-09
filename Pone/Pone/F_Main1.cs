using CommLibrary.DBHelper;
using MySql.Data.MySqlClient;
using P_Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pone
{
    public partial class F_Main1 : Form
    {

        public TB_VailUser Vuser = null;
        DataRow Current_Row;
        string[] imgs = new string[0];// new string[] { @"D:\a\img\a.jpg", @"D:\a\img\b.jpg" };

        public F_Main1()
        {
            InitializeComponent();
            ClearLabTxt();
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
            string sql = @" select b.`name`,b.eno,b.price as eprice,a.start_time,a.end_time,a.ono,cast(b.`status` as char) as estatus,b.pic,a.total,c.`name` as uname,CAST(a.`status` as char) as ostatus,a.price as oprice,a.vcode,b.details  From db_order a 
  inner join db_equipment b on a.eid=b.id
	inner join db_user c on c.id=a.uid
	where  1=1  ";
            if (!string.IsNullOrWhiteSpace(Txt_BRID.Text))
            {
                sql += " and a.ono =@brid ";
                pars.Add(new MySqlParameter("@brid", Txt_BRID.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(Txt_VerificationCode.Text))
            {
                sql += " and a.vcode =@vcode ";
                pars.Add(new MySqlParameter("@vcode", Txt_VerificationCode.Text.Trim()));
            }
            sql += ";";
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
                Current_Row = dt.Rows[0];
                BindDateToForm();
            }
        }
        List<Control> FormCot = new List<Control>(30);

        DateTime ConvertMySqlDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        void BindDateToForm()
        {
            string[] estatus = new string[] { "删除状态", "空闲", "租借" };
            string[] ostatus = new string[] { "管理员关闭_Red", "进行中_Orange", "已完成_Green" };

            string os = ostatus[Convert.ToInt32(Current_Row["ostatus"])];

            Current_Row["estatus"] = estatus[Convert.ToInt32(Current_Row["estatus"])];
            Current_Row["ostatus"] = os.Split('_')[0];

            Current_Row["start_time"] = ConvertMySqlDateTime(Convert.ToString(Current_Row["start_time"]));
            Current_Row["end_time"] = ConvertMySqlDateTime(Convert.ToString(Current_Row["end_time"]));

            #region 处理预览图
            P_Imgs.Visible = false;
            P_Imgs.Controls.Clear();
            // imgs = Convert.ToString(Current_Row["pic"]).Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //BRID, VerificationCode, BorrowUser, Handler, BorrowTime, BorrowTimeLimit, DateOfReturn, ReturnHandler
            string pic = Regex.Replace(Convert.ToString(Current_Row["pic"]), "data:.+;base64,", "");
            if (!string.IsNullOrWhiteSpace(pic))
            {
                if (pic.Length % 4!=0)
                {
                    pic += "=";

                }
               
                byte[] bys = Convert.FromBase64String(pic);
                PB_ProductImg.Image = Image.FromStream(new MemoryStream(bys));


            }
            else
            {
                PB_ProductImg.Image = Properties.Resources.noting;
            }


            #endregion

            #region 绑定其它固有数据的值 
            FormCot.ForEach(c =>
            {
                string cname = c.Name.Replace("Lab_", "");
                if (cname == "ostatus")
                {


                    c.ForeColor = Color.FromName(os.Split('_')[1]);
                }
                if (Current_Row.Table.Columns.Contains(cname))
                    c.Text = Convert.ToString(Current_Row[cname]);
            });

            #endregion

            //绑定其它信息的值 
            if (webBrowser1.Document == null)
            {
                webBrowser1.Navigate("");
                webBrowser1.Document.Write(Convert.ToString(Current_Row["details"]));
            }
            else
            {
                webBrowser1.Document.Body.InnerHtml = Convert.ToString(Current_Row["details"]);
            }




        }

        void ClearLabTxt()
        {

            foreach (Control item in this.groupBox3.Controls)
            {
                if (item.Name.StartsWith("Lab_"))
                {
                    item.Text = "";
                    FormCot.Add(item);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Current_Row = null;
            Vuser = null;
            this.Text = "信息验证 (未登录)";

            MessageBox.Show("注销成功!");
        }
    }
}
