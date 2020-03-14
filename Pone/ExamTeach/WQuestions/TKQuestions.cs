using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamTeach.WQuestions
{
    public partial class TKQuestions : Form
    {
        public TKQuestions()
        {
            InitializeComponent();
        }

        private void TKQuestions_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“examSystemDataSet1.E_TKQuestions”中。您可以根据需要移动或删除它。
            this.e_TKQuestionsTableAdapter.Fill(this.examSystemDataSet1.E_TKQuestions);
            BindList();

        }

        void BindList()
        {
            // TODO: 这行代码将数据加载到表“examSystemDataSet1.E_TKQuestions”中。您可以根据需要移动或删除它。
            this.e_TKQuestionsTableAdapter.Fill(this.examSystemDataSet1.E_TKQuestions);
            listBox1.SelectedIndex = -1;
            ClearContent();
            button2_Click(null, null);
        }

        /// <summary>
        /// 记录生成了多少控件 (算坐标时要用)
        /// </summary>
        List<TextBox> li_t = new List<TextBox>();
        //题目ID
        int QID;
        /// <summary>
        /// 添加试题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            QID = 0;
            Lab_CanADD.Text = "是";
            ClearContent();

        }

        void ClearContent()
        {
            richTextBox1.Text = "";
            panel1.Controls.Clear();
            li_t.Clear();
        }

        /// <summary>
        /// 保存试题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("请输入题面!");
                return;
            }

            if (li_t.Count == 0)
            {
                MessageBox.Show("没有需要填的空!");
                return;

            }
            if (QID > 0)
            {
                if (MessageBox.Show("确认修改,该题目?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }
            if (li_t.Where(c => string.IsNullOrWhiteSpace(c.Text)).Count() > 0)
            {
                MessageBox.Show("请给所有填空补充上正确答案!");
                return;
            }
            string error;
            using (TeacherB tb = new TeacherB())
            {
                error = tb.UpdateTKData(richTextBox1.Text.Trim(), string.Join(TeacherB.TKASp, li_t.Select(c => c.Text.Trim())), QID);
            }
            if (string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show("保存成功!");
                BindList();
            }
            else
            {
                MessageBox.Show(error);
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string[] ss = richTextBox1.Text.Trim().Split(new string[] { "____" }, StringSplitOptions.None);
            if (ss.Length < 2)
            {
                li_t.Clear();
                panel1.Controls.Clear();
                return;
            }
            int ks = ss.Length - 1;
            if (ks == li_t.Count)
            {
                return;
            }
            if (li_t.Count == 5 && ks > 5)
            {
                richTextBox1.Text = string.Join("____", ss, 0, ss.Length - 1);
                MessageBox.Show("单题填空数请在5个之内!");
                return;
            }

            if (li_t.Count > 0 && ks > li_t.Count)
            {
                for (int i = 0; i < ks - li_t.Count; i++)
                {
                    TextBox t = new TextBox();
                    t.Size = Txt_Copy.Size;
                    t.Left = Txt_Copy.Left;
                    t.Visible = true;
                    t.Top = (li_t.Count * t.Height + li_t.Count * 5);
                    panel1.Controls.Add(t);
                    li_t.Add(t);
                }

            }
            else
            {
                li_t.Clear();
                panel1.Controls.Clear();
                for (int i = 0; i < (ss.Length - 1); i++)
                {
                    TextBox t = new TextBox();
                    t.Size = Txt_Copy.Size;
                    t.Left = Txt_Copy.Left;
                    t.Visible = true;
                    t.Top = (li_t.Count * t.Height + li_t.Count * 5);
                    panel1.Controls.Add(t);
                    li_t.Add(t);
                }
            }



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedValue == null)
            {
                return;
            }
            QID = Convert.ToInt32(listBox1.SelectedValue);
            Lab_CanADD.Text = "否";
            ClearContent();
            DataRowView dv = listBox1.SelectedItem as DataRowView;
            richTextBox1.Text = dv["Questions"].ToString();
            string[] ans = Convert.ToString(dv["Answer"]).Split(new string[] { TeacherB.TKASp }, StringSplitOptions.None);
            int i = 0;
            foreach (Control item in panel1.Controls)
            {
                if (item.Visible)
                {
                    item.Text = ans[i];
                    i++;

                }
            }

        }

        private void TKQuestions_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.MdiParent as Main).DisabledMenu_AddTK();
        }
    }
}
