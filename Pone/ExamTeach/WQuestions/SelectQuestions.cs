using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamTeach.WQuestions
{
    public partial class SelectQuestions : Form
    {


        public SelectQuestions()
        {
            InitializeComponent();
        }

        private void SelectQuestions_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.MdiParent as Main).DisabledMenu_AddSelect();

        }

        private void SelectQuestions_Load(object sender, EventArgs e)
        {
            BindList();

        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (QID > 0)
            {
                if (MessageBox.Show("确认修改,该题目?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Txt_Questions.Text))
            {
                MessageBox.Show("请输入问题内容!");
                return;
            }
            DataSet ds = GetData();
            if (ds == null)
            {
                return;
            }
            TeacherB tb = new TeacherB();
            string error = tb.UpdateSelectData(ds, QID);
            if (string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show("保存成功!");
                BindList();
            }
            else
            {
                MessageBox.Show(error);
            }
            tb.Dispose();
          
        }

        void BindList()
        {

            this.e_SelectQuestionsTableAdapter.Fill(this.examSystemDataSet.E_SelectQuestions);
            listBox1.SelectedIndex = -1;
            ClearContent();
            button1_Click(null, null);
        }

        /// <summary>
        /// 获取数据 
        /// </summary>
        /// <returns>表0 为主表数据 .表1 为从表数据 (选择表)</returns>
        private DataSet GetData()
        {
            string chk = "CK_";
            string tx = "Txt_Anwser_";
            DataTable dt_main = new DataTable();
            dt_main.Columns.Add("Questions");
            dt_main.Columns.Add("Answer");

            DataTable dt = new DataTable();
            dt.Columns.Add("_option");
            dt.Columns.Add("_content");
            DataRow crow;

            List<string> ans = new List<string>();

            for (int i = 0,j=0 ; i < 5; i++)
            {
                string o = ((char)(i + 65)).ToString();
                string content = P_Answer.Controls.Find(tx + o, false).First().Text;
                if (string.IsNullOrWhiteSpace(content))
                {
                    continue;
                }

                bool isan = (P_Answer.Controls.Find(chk + o, false).First() as CheckBox).Checked;
                crow = dt.NewRow();
                dt.Rows.Add(crow);
                string sj = ((char)(j + 65)).ToString();
                crow["_option"] = sj;
                j++;
                crow["_content"] = content;
                if (isan)
                {
                    ans.Add(sj);
                }

            }
            if (ans.Count == 0)
            {
                MessageBox.Show("请至少设置一个正确答案!");
                return null;
            }
            crow = dt_main.NewRow();
            dt_main.Rows.Add(crow);
            crow["Questions"] = Txt_Questions.Text.Trim();
            crow["Answer"] = string.Join(",", ans);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt_main);
            ds.Tables.Add(dt);
            return ds;

        }

        int QID;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (sender as ListBox);
            if (lb.SelectedValue == null)
            {
                return;
            }
            ClearContent();

            DataTable dt = Comm.GetCSelectOpetion(QID = Convert.ToInt32(lb.SelectedValue));
            string[] a = (lb.SelectedItem as DataRowView)["Answer"].ToString().Split(',');
            string chk = "CK_";
            string tx = "Txt_Anwser_";
            dt.AsEnumerable().ToList().ForEach(c =>
            {
                P_Answer.Controls.Find(tx + c["_option"], false).First().Text = Convert.ToString(c["_content"]);
            });

            foreach (var aa in a)
            {
                (P_Answer.Controls.Find(chk + aa, false).First() as CheckBox).Checked = true;
            }

            Txt_Questions.Text = lb.Text;
            Lab_CanADD.Text = "否";

        }

        /// <summary>
        /// 清空内容
        /// </summary>
        public void ClearContent()
        {
            string chk = "CK_";
            string tx = "Txt_Anwser_";

            for (int i = 0; i < 5; i++)
            {
                string o = ((char)(i + 65)).ToString();
                CheckBox cb = (P_Answer.Controls.Find(chk + o, false).First() as CheckBox);
                cb.Checked = false;
                P_Answer.Controls.Find(tx + o, false).First().Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            QID = 0;
            Lab_CanADD.Text = "是";
            ClearContent();
        }
    }
}
