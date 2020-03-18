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

namespace ExamTeach
{
    public partial class SetPaperByExam : Form
    {
        IEnumerable<int> EIDs;
        ZuKao zk;
        public SetPaperByExam(IEnumerable<int> EIDs, ZuKao zk)
        {
            this.EIDs = EIDs;
            this.zk = zk;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int r = 0;
            if (Cb_Paper.SelectedValue == null || !int.TryParse(Cb_Paper.SelectedValue.ToString(), out r) || r <= 0)
            {
                MessageBox.Show("设置的试卷不正确!");
                return;
            }
            using (TeacherB tb = new TeacherB())
            {
                tb.SetPaperWithExam(EIDs, r);
            }

            MessageBox.Show("设置成功!");
            this.Close();
        }

        private void SetPaperByExam_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“paperInfo.E_Paper”中。您可以根据需要移动或删除它。
            this.e_PaperTableAdapter.Fill(this.paperInfo.E_Paper);

        }

        private void SetPaperByExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            zk.LoadKSInfo();
        }
    }
}
