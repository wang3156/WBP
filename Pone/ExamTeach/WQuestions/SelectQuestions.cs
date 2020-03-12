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
            // TODO: 这行代码将数据加载到表“examSystemDataSet.E_SelectQuestions”中。您可以根据需要移动或删除它。
            this.e_SelectQuestionsTableAdapter.Fill(this.examSystemDataSet.E_SelectQuestions);

        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {

        }
        int QID;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (sender as ListBox);
            if (lb.SelectedValue==null)
            {
                return;
            }

            DataTable dt = Comm.GetCSelectOpetion(QID = Convert.ToInt32(lb.SelectedValue));
            string a = (lb.SelectedItem as DataRowView)["Answer"].ToString();
            string chk = "CK_";
            string tx = "Txt_Anwser_";
            dt.AsEnumerable().ToList().ForEach(c =>
            {
                P_Answer.Controls.Find(tx + c["_option"], false).First().Text = Convert.ToString(c["_content"]);
            });
            for (int i = 0; i < 5; i++)
            {
                string o = ((char)(i + 65)).ToString();
                CheckBox cb = (P_Answer.Controls.Find(chk + o, false).First() as CheckBox);
                cb.Checked = o == a;
            }
            Txt_Questions.Text = lb.Text;
            Lab_CanADD.Text = "否";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            QID = 0;
            Lab_CanADD.Text = "是";
        }
    }
}
