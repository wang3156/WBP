using ExamTeach.WQuestions;
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
    public partial class Main : Form
    {
        public string UName;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(UName))
            //{
            //    Login l = new Login();
            //    l.Tag = this;
            //    l.ShowDialog();
            //}
        }

        private void AddSelect_Click(object sender, EventArgs e)
        {
            AddSelect.Enabled = false;
            SelectQuestions sq = new SelectQuestions();
            sq.MdiParent = this;
            sq.Show();
        }

        public void DisabledMenu_AddSelect()
        {
            AddSelect.Enabled = true;
        }

        public void DisabledMenu_AddTK()
        {
            AddTK.Enabled = true;
        }

        private void AddTK_Click(object sender, EventArgs e)
        {
            AddTK.Enabled = false;
            TKQuestions tk = new TKQuestions();
            tk.MdiParent = this;
            tk.Show();

        }
    }


}
