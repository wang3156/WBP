using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamStudent
{
    public partial class Mian : Form
    {
        public Mian()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        CListener cl;
        private void Mian_Load(object sender, EventArgs e)
        {
            ShowStatus();
            ThreadPool.QueueUserWorkItem((a) =>
            {
               
                cl = new CListener();
                cl.ServerP = c =>
                {
                    switch (c.RCode)
                    {
                        case ResponseCode.StartExam:
                            label1.Text = ("====考试开始!");
                            break;
                        case ResponseCode.EndExam:
                            label1.Text = ("====考试结束!");
                            break;
                        case ResponseCode.DisabledExam:
                            label1.Text = ("禁止考试!");
                            break;
                        case ResponseCode.ServerColseConnected:
                          
                            cl.Dispose();                      
                            label1.Text = ("服务器已断开!");
                            ShowStatus();
                            cl.BeginConnction("zkz0004");
                            break;
                        default:
                            break;
                    }

                };
                cl.BeginConnction("zkz0004");
                ShowStatus(false);
            });
        }

        void ShowStatus(bool lj = true)
        {
            if (lj)
            {
                this.Text = "学生端(连接中.....)";

            }
            else
            {
                this.Text = "学生端(已连接服务器)";
            }

        }
    }
}
