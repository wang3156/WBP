﻿using Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamTeach
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Main());
            }
            catch (Exception ex)
            {
                using (TeacherB tb = new TeacherB())
                {
                    tb.EmptyServerInfo();

                }

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                File.AppendAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), new string[] { ex.Message, ex.StackTrace });
            }
        }
    }
}
