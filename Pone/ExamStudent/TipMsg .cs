using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamStudent
{
    public partial class TipMsg : Form
    {
        string msg;
        public TipMsg(string msg)
        {
            this.msg = msg;
            InitializeComponent();
        }

        private void TipMsg_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = this.msg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
