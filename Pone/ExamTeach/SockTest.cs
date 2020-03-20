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
    public partial class SockTest : Form
    {
        public SockTest()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        TListener TL;
        private void button1_Click(object sender, EventArgs e)
        {
            //启动监听服务
            try
            {
                TL = new TListener();
                TL.AddList = c =>
                {
                    listBox1.Items.Add(c.ZKZH);

                };
                TL.RemoveList = c =>
                {
                    listBox1.Items.Remove(c.ZKZH);
                };
                TL.BeginListen();
                textBox1.Text = TL.server_ip;
                textBox2.Text = TL.server_port.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("服务开启失败!" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TL.Dispose();
            TL = null;
            textBox1.Text = textBox2.Text = "";

            using (TeacherB tb = new TeacherB())
            {
                tb.EmptyServerInfo();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(listBox1.Text))
            {
                return;
            }
            TL.SendTOClient(new ConnectCheck() { RCode = (ResponseCode)Enum.Parse(typeof(ResponseCode), comboBox1.Text.Trim()) }, listBox1.Text);
            MessageBox.Show("发送成功");

        }
    }
}
