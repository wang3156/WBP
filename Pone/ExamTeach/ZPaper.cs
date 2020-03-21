using Business;
using Newtonsoft.Json;
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
    public partial class ZPaper : Form
    {
        public ZPaper()
        {
            InitializeComponent();
        }
        int PID;
        int Qtype;

        FPars fp;

        private void ZPaper_Load(object sender, EventArgs e)
        {
            if (fp == null)
            {
                fp = this.Tag as FPars;
                PID = Convert.ToInt32(fp.PID);
            }


            BindPaperData();
            BindDataWithPID();
            BindUnData();
        }
        /// <summary>
        /// 绑定未选择的数据 
        /// </summary>
        private void BindUnData()
        {
            DataTable dt;
            using (TeacherB tb = new TeacherB())
            {
                dt = tb.GetUnData(PID, Qtype);
            }
            DataTable dt2 = dataGridView2.DataSource as DataTable;
            DataTable dtn = dt.Clone();
            dt.AsEnumerable().Except(dt2.AsEnumerable(), new ProductComparer()).CopyToDataTable(dtn, LoadOption.OverwriteChanges);

            dataGridView1.DataSource = dtn;
        }

        private void BindDataWithPID()
        {

            DataTable ds = Comm.GetPaperMx(PID);

            if (ds.Rows.Count > 0)
            {

                dataGridView2.DataSource = ds;
            }
            else
            {
                MessageBox.Show("数据异常");
            }


        }

        void BindPaperData()
        {
            if (PID == 0)
            {
                return;
            }
            DataTable dt = Comm.GetPaper(PID);
            if (dt != null)
            {
                textBox1.Text = Convert.ToString(dt.Rows[0]["PaperName"]);
            }

        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Qtype = 0;
            BindUnData();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Qtype = 1;
            BindUnData();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            ChangeList(dataGridView1, dataGridView2);

        }

        /// <summary>
        /// 改变题目在的列表
        /// </summary>
        /// <param name="dataGridView1">移除的源</param>
        /// <param name="dataGridView2">移入的源</param>
        void ChangeList(DataGridView dataGridView1, DataGridView dataGridView2, string checkName = "CheckRow")
        {
            List<Object[]> QIDS = new List<Object[]>();
            int s = dataGridView1.Rows.Count - 1;

            for (int i = s; i > -1; i--)
            {

                DataGridViewCheckBoxCell cell = (dataGridView1.Rows[i].Cells[checkName] as DataGridViewCheckBoxCell);
                if (Convert.ToString(cell.Value) == "1")
                {
                    DataRowView dv = dataGridView1.Rows[i].DataBoundItem as DataRowView;
                    if (checkName == "CheckRow2")
                    {
                        if (Convert.ToInt32(dv["QType"]) == Qtype)
                        {
                            //移除要检查类型
                            QIDS.Add(dv.Row.ItemArray);
                        }

                    }
                    else
                    {
                        QIDS.Add((dataGridView1.Rows[i].DataBoundItem as DataRowView).Row.ItemArray);
                    }

                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);

                }
            }


            if (QIDS.Count == 0)
            {
                MessageBox.Show("未选择需要操作的题目 !");
                return;
            }
            DataTable dt = dataGridView2.DataSource as DataTable;

            foreach (var item in QIDS)
            {
                dt.Rows.Add(item);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            ChangeList(dataGridView2, dataGridView1, "CheckRow2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text = textBox1.Text.Trim()))
            {
                MessageBox.Show("请输入试卷名称 !");
                return;
            }
            DataTable dt = dataGridView2.DataSource as DataTable;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("请先设置试题 !");
                return;
            }

            string error;
            using (TeacherB tb = new TeacherB())
            {
                error = tb.SavePaper(ref PID, textBox1.Text, dt);
            }
            if (string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show("保存成功!");
                ZPaper_Load(null, null);
            }
            else
            {
                MessageBox.Show(error);

            }

        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            DataGridViewRow dgr = dataGridView2.Rows[e.RowIndex];
            if (Convert.ToInt32((dgr.DataBoundItem as DataRowView)["QType"]) == 0)
            {
                dgr.Cells["QQType"].Value = "选择";
            }
            else
            {
                dgr.Cells["QQType"].Value = "填空";
            }
        }

        private void ZPaper_FormClosing(object sender, FormClosingEventArgs e)
        {
            fp.Zpa.button2_Click(null, null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            DataGridView g = sender as DataGridView;

            Comm.CreateControl((g.SelectedRows[0].DataBoundItem as DataRowView).Row, Txt_Questions, P_Content, false);
        }
    }

    class ProductComparer : IEqualityComparer<DataRow>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(DataRow x, DataRow y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x["QType"].Equals(y["QType"]) && x["QID"].Equals(y["QID"]);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(DataRow product)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(product, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = product["QType"] == null ? 0 : product["QType"].GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = product["QID"].GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }

    public class FPars
    {
        public int PID;
        public ZPaperM Zpa;

    }
}
