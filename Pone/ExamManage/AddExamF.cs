using Business;
using CommLibrary.OfficeHelper.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamTeach
{
    public partial class AddExamF : Form
    {
        public AddExamF()
        {
            InitializeComponent();
        }

        DataRow row;
        ZuKao zk;

        private void AddExamF_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“paperInfo.E_Paper”中。您可以根据需要移动或删除它。
            this.e_PaperTableAdapter.Fill(this.paperInfo.E_Paper);
            if (this.Tag != null)
            {
                dynamic dyn = this.Tag as dynamic;
                row = dyn.R as DataRow;
                zk = dyn.P as ZuKao;
                if (row != null)
                {
                    Txt_ExamName.Text = Convert.ToString(row["ExamName"]);
                    date_Start.Text = Convert.ToDateTime(row["EStart"]).ToString("yyyy-MM-dd HH:mm");
                    date_End.Text = Convert.ToDateTime(row["EEnd"]).ToString("yyyy-MM-dd HH:mm");
                    Cb_Paper.SelectedValue = Convert.ToString(row["PID"]);
                    rich_Remark.Text = Convert.ToString(row["ExamRemark"]);
                }

            }
            CheckZt();

        }

        void CheckZt()
        {
            if (row == null)
            {
                return;
            }
            if (Convert.ToString(row["EStatus"]) != "未开考")
            {
                Txt_List.Enabled = rich_Remark.Enabled = button2.Enabled = Txt_ExamName.Enabled = date_Start.Enabled = date_End.Enabled = Cb_Paper.Enabled = false;

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt_ExamName.Text = Txt_ExamName.Text.Trim()))
            {
                MessageBox.Show("请输入考试名称");
                return;
            }
            if (string.IsNullOrWhiteSpace(date_Start.Text = date_Start.Text.Trim()))
            {
                MessageBox.Show("请输入开始时间");
                return;
            }
            if (string.IsNullOrWhiteSpace(date_End.Text = date_End.Text.Trim()))
            {
                MessageBox.Show("请输入结束时间");
                return;
            }

            if (Convert.ToDateTime(date_End.Text) <= Convert.ToDateTime(date_Start.Text))
            {
                MessageBox.Show("结束时间应该大于开始时间");
                return;

            }

            using (TeacherB tb = new TeacherB())
            {
                tb.BeginTransaction();
                try
                {
                    if (row != null)
                    {
                        row = tb.UpdateExam(Txt_ExamName.Text, date_Start.Text, date_End.Text, Cb_Paper.SelectedValue, rich_Remark.Text.Trim(), Convert.ToInt32(row["EID"])).Rows[0];
                    }
                    else
                    {
                        row = tb.AddExam(Txt_ExamName.Text, date_Start.Text, date_End.Text, Cb_Paper.SelectedValue, rich_Remark.Text.Trim()).Rows[0];
                    }

                    if (Txt_List.Text != "") //有上传学生数据
                    {
                        DataTable dt = NPOIHelper.GetDataTableFromExcel(Txt_List.Text);
                        if (dt.Rows.Count > 0)
                        {
                            tb.UpdateKSList(dt, Convert.ToInt32(row["EID"]));
                        }
                    }
                    tb.Commit();
                }
                catch (Exception ex)
                {
                    tb.Rollback();
                }

            }
            MessageBox.Show("保存成功!");
        }

        private void AddExamF_FormClosing(object sender, FormClosingEventArgs e)
        {
            zk.LoadKSInfo();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog pd = new OpenFileDialog();
            pd.Filter = "2007+excel (*.xlsx)|*.xlsx| 2003excel (*.xls)|*.xls";
            pd.Multiselect = false;
            if (pd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Txt_List.Text = pd.FileName;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "2007+excel (*.xlsx)|*.xlsx| 2003excel (*.xls)|*.xls";
            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files\\Students.xlsx"), sf.FileName, true);
            MessageBox.Show("保存成功!");
        }
    }
}
