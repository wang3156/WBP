﻿namespace ExamStudent
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btu_Submit = new System.Windows.Forms.Button();
            this.Lab_UName = new System.Windows.Forms.Label();
            this.Lab_XH = new System.Windows.Forms.Label();
            this.Lab_ZKZH = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Lab_Connection = new System.Windows.Forms.Label();
            this.Lab_Name = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Btn_Perv = new System.Windows.Forms.Button();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.Lab_Count = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Lab_Name);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Btu_Submit);
            this.panel1.Controls.Add(this.Lab_UName);
            this.panel1.Controls.Add(this.Lab_XH);
            this.panel1.Controls.Add(this.Lab_ZKZH);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 41);
            this.panel1.TabIndex = 2;
            // 
            // Btu_Submit
            // 
            this.Btu_Submit.Location = new System.Drawing.Point(925, 12);
            this.Btu_Submit.Name = "Btu_Submit";
            this.Btu_Submit.Size = new System.Drawing.Size(75, 23);
            this.Btu_Submit.TabIndex = 2;
            this.Btu_Submit.Text = "交卷";
            this.Btu_Submit.UseVisualStyleBackColor = true;
            this.Btu_Submit.Click += new System.EventHandler(this.Btu_Submit_Click);
            // 
            // Lab_UName
            // 
            this.Lab_UName.AutoSize = true;
            this.Lab_UName.Location = new System.Drawing.Point(533, 16);
            this.Lab_UName.Name = "Lab_UName";
            this.Lab_UName.Size = new System.Drawing.Size(41, 12);
            this.Lab_UName.TabIndex = 1;
            this.Lab_UName.Text = "label5";
            // 
            // Lab_XH
            // 
            this.Lab_XH.AutoSize = true;
            this.Lab_XH.Location = new System.Drawing.Point(320, 16);
            this.Lab_XH.Name = "Lab_XH";
            this.Lab_XH.Size = new System.Drawing.Size(41, 12);
            this.Lab_XH.TabIndex = 1;
            this.Lab_XH.Text = "label5";
            // 
            // Lab_ZKZH
            // 
            this.Lab_ZKZH.AutoSize = true;
            this.Lab_ZKZH.Location = new System.Drawing.Point(81, 16);
            this.Lab_ZKZH.Name = "Lab_ZKZH";
            this.Lab_ZKZH.Size = new System.Drawing.Size(41, 12);
            this.Lab_ZKZH.TabIndex = 1;
            this.Lab_ZKZH.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(486, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "姓名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "学号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "准考证号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lab_Count);
            this.groupBox1.Controls.Add(this.Btn_Next);
            this.groupBox1.Controls.Add(this.Btn_Perv);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(0, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1012, 613);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "试卷";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Lab_Connection);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 666);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1012, 25);
            this.panel2.TabIndex = 4;
            // 
            // Lab_Connection
            // 
            this.Lab_Connection.AutoSize = true;
            this.Lab_Connection.Location = new System.Drawing.Point(849, 6);
            this.Lab_Connection.Name = "Lab_Connection";
            this.Lab_Connection.Size = new System.Drawing.Size(41, 12);
            this.Lab_Connection.TabIndex = 0;
            this.Lab_Connection.Text = "label1";
            // 
            // Lab_Name
            // 
            this.Lab_Name.AutoSize = true;
            this.Lab_Name.Location = new System.Drawing.Point(714, 16);
            this.Lab_Name.Name = "Lab_Name";
            this.Lab_Name.Size = new System.Drawing.Size(41, 12);
            this.Lab_Name.TabIndex = 3;
            this.Lab_Name.Text = "label1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(988, 183);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(12, 227);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(994, 362);
            this.panel3.TabIndex = 1;
            // 
            // Btn_Perv
            // 
            this.Btn_Perv.Location = new System.Drawing.Point(815, 590);
            this.Btn_Perv.Name = "Btn_Perv";
            this.Btn_Perv.Size = new System.Drawing.Size(75, 23);
            this.Btn_Perv.TabIndex = 2;
            this.Btn_Perv.Text = "上一题";
            this.Btn_Perv.UseVisualStyleBackColor = true;
            this.Btn_Perv.Click += new System.EventHandler(this.Btn_Perv_Click);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Location = new System.Drawing.Point(910, 590);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(75, 23);
            this.Btn_Next.TabIndex = 2;
            this.Btn_Next.Text = "下一题";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // Lab_Count
            // 
            this.Lab_Count.AutoSize = true;
            this.Lab_Count.Location = new System.Drawing.Point(657, 595);
            this.Lab_Count.Name = "Lab_Count";
            this.Lab_Count.Size = new System.Drawing.Size(41, 12);
            this.Lab_Count.TabIndex = 3;
            this.Lab_Count.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(844, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "考试系统";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 691);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "学生端";
            this.Load += new System.EventHandler(this.Mian_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btu_Submit;
        private System.Windows.Forms.Label Lab_UName;
        private System.Windows.Forms.Label Lab_XH;
        private System.Windows.Forms.Label Lab_ZKZH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Lab_Connection;
        private System.Windows.Forms.Label Lab_Name;
        private System.Windows.Forms.Button Btn_Perv;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Label Lab_Count;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

