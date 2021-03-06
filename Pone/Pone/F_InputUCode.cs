﻿using CommLibrary.DBHelper;
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
using Newtonsoft.Json;
using P_Entity;

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
                using (MySqlDBHelper db = new MySqlDBHelper())
                {
                    dt = db.GetDataSet("select * From db_admin where name=@code", pars: new MySqlParameter[] { new MySqlParameter("@code", SqlDbType.VarChar) { Value = UCode } }).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    F_Main1 f = (this.Tag as F_Main1);
                    f.Vuser = JsonConvert.DeserializeObject<TB_VailUser>(JsonConvert.SerializeObject(dr));
                    f.Text = $"信息验证({UCode})";

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("输入的账号有误!");
                }

            }
        }

        private void F_InputUCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this.button1, null);
            }
        }
    }
}
