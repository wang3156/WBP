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
using System.Threading;
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
        Thread t;
        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Txt_VerificationCode.Text))
            {
                MessageBox.Show("请输入查询条件!");
                return;
            }
            else
            {
                Current_Row = null;
                GetDataAndBindToForm();
            }

        }
        private void GetDataAndBindToForm()
        {
            List<MySqlParameter> pars = new List<MySqlParameter>();
            string sql = @" select a.id,a.duration,a.eid,a.deposit,a.create_time,b.`name`,b.eno,b.price as eprice,a.start_time,a.end_time,a.ono,cast(b.`status` as char) as estatus,b.pic,a.total,c.`name` as uname,CAST(a.`status` as char) as ostatus,a.price as oprice,a.vcode,b.details  From db_order a 
  inner join db_equipment b on a.eid=b.id
	inner join db_user c on c.id=a.uid
	where  1=1  ";

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
                ClearLabTxt();
                Current_Row = null;
                MessageBox.Show("验证信息有误!");
            }
            else
            {
                Current_Row = dt.Rows[0];

            }
            BindDateToForm();


        }
        List<Control> FormCot = new List<Control>(30);

        object ConvertMySqlDateTime(string timeStamp)
        {
            if (string.IsNullOrWhiteSpace(timeStamp))
            {
                return DBNull.Value;
            }
            long lll = 0;
            if (!long.TryParse(timeStamp, out lll))
            {
                return timeStamp;
            }
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        void BindDateToForm()
        {
            SetButtonStatus();
            if (Current_Row == null)
            {
                return;
            }
            t?.Abort();
            t = null;
            CheckOutTime();

            string[] estatus = new string[] { "删除状态", "空闲", "租借" };
            string[] ostatus = new string[] { "管理员关闭_Red", "进行中_Orange", "已完成_Green", "待激活_SkyBlue" };

            string os = ostatus[Convert.ToInt32(Current_Row["ostatus"])];

            //Current_Row["estatus"] = estatus[Convert.ToInt32(Current_Row["estatus"])];
            //Current_Row["ostatus"] = os.Split('_')[0];

            Current_Row["start_time"] = ConvertMySqlDateTime(Convert.ToString(Current_Row["start_time"]));
            Current_Row["end_time"] = ConvertMySqlDateTime(Convert.ToString(Current_Row["end_time"]));
            Current_Row["create_time"] = ConvertMySqlDateTime(Convert.ToString(Current_Row["create_time"]));
            #region 处理预览图
            P_Imgs.Visible = false;
            P_Imgs.Controls.Clear();
            // imgs = Convert.ToString(Current_Row["pic"]).Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //BRID, VerificationCode, BorrowUser, Handler, BorrowTime, BorrowTimeLimit, DateOfReturn, ReturnHandler
            string pic = Regex.Replace(Convert.ToString(Current_Row["pic"]), "data:.+;base64,", "").Trim();
            if (!string.IsNullOrWhiteSpace(pic))
            {
                if (pic.Length % 4 != 0)
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
                    c.Text = os.Split('_')[0];
                }
                else if (cname == "estatus")
                {
                    c.Text = estatus[Convert.ToInt32(Current_Row["estatus"])];

                }
                else
                {
                    if (Current_Row.Table.Columns.Contains(cname))
                        c.Text = Convert.ToString(Current_Row[cname]);
                }

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

        private void CheckOutTime()
        {
            if (t != null && t.ThreadState == ThreadState.Background)
            {
                return;
            }

            t = new Thread(() =>
            {
                while (true)
                {
                    string ostatus = Convert.ToString(Current_Row["ostatus"]);
                    switch (ostatus)
                    {
                        //进行中
                        case "1":
                        case "3":
                            if (string.IsNullOrWhiteSpace(Convert.ToString(Current_Row["start_time"])))
                            {
                                break;
                            }
                            int duration = Convert.ToInt32(Current_Row["duration"]);
                            DateTime zore = new DateTime(1970, 1, 1);
                            DateTime activeTime = Convert.ToDateTime(ConvertMySqlDateTime(Convert.ToString(Current_Row["start_time"])));

                            if (activeTime.AddMinutes(duration) <= DateTime.Now)
                            {

                                Btn_Return.Invoke((Action)delegate
                                {
                                    istip = false;
                                    ReturnPC();
                                    MessageBox.Show("订单已经超时.自动完成!");

                                });                               
                                return;
                            }
                            break;
                        default:
                            return;
                    }
                    //5秒检查一次
                    Thread.Sleep(5 * 1000);
                }
            });
            t.IsBackground = true;
            t.Start();

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

        bool istip = true;
        void SetButtonStatus()
        {
            Btn_Active.Enabled = false;
            Btn_Return.Enabled = false;
            if (Current_Row == null)
            {
                return;
            }
            string ostatus = Convert.ToString(Current_Row["ostatus"]);

            switch (ostatus)
            {
                //进行中
                case "1":
                    Btn_Return.Enabled = true;
                    break;
                case "3":
                    Btn_Active.Enabled = true;
                    break;
                default:
                    break;
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


            int duration = Convert.ToInt32(Current_Row["duration"]);

            DateTime now = DateTime.Now;
            DateTime zore = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            Current_Row["start_time"] = (int)(now - zore).TotalSeconds;
            Current_Row["end_time"] = (int)(now.AddMinutes(duration) - zore).TotalSeconds;

            List<MySqlParameter> pars = new List<MySqlParameter>();
            string sql = @"  update db_order set `status`=1,start_time=@st,end_time=@et where id=@id ;";
            pars.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = Current_Row["id"] });
            pars.Add(new MySqlParameter("@st", MySqlDbType.Int32) { Value = Current_Row["start_time"] });
            pars.Add(new MySqlParameter("@et", MySqlDbType.Int32) { Value = Current_Row["end_time"] });


            using (MySqlDBHelper mydb = new MySqlDBHelper())
            {
                mydb.ExecuteNonQuery(sql, pars: pars.ToArray());
            }

            Current_Row["ostatus"] = "1";
            BindDateToForm();
            MessageBox.Show("激活成功!");
        }

        private void Btn_Return_Click(object sender, EventArgs e)
        {
            istip = true;
            ReturnPC();

        }

        private void ReturnPC()
        {
            int duration = Convert.ToInt32(Current_Row["duration"]);
            DateTime now = DateTime.Now;
            DateTime zore = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            Current_Row["end_time"] = (int)(now - zore).TotalSeconds;

            List<MySqlParameter> pars = new List<MySqlParameter>();
            string sql = @"  update db_order set `status`=2,end_time=@et where id=@id ;";
            pars.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = Current_Row["id"] });
            pars.Add(new MySqlParameter("@et", MySqlDbType.Int32) { Value = Current_Row["end_time"] });

            sql += " update db_equipment set `status`=1 where id=@eid  ;";
            pars.Add(new MySqlParameter("@eid", MySqlDbType.Int32) { Value = Current_Row["eid"] });

            using (MySqlDBHelper mydb = new MySqlDBHelper())
            {
                mydb.ExecuteNonQuery(sql, pars: pars.ToArray());
            }

            Current_Row["ostatus"] = "2";
            Current_Row["estatus"] = "1";
            BindDateToForm();
            if (istip)
            {
                MessageBox.Show("已归还!");
            }
        }
    }
}
