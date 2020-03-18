namespace ExamTeach
{
    partial class AddExamF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_ExamName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.date_Start = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.date_End = new System.Windows.Forms.DateTimePicker();
            this.Cb_Paper = new System.Windows.Forms.ComboBox();
            this.ePaperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.paperInfo = new ExamTeach.PaperInfo();
            this.label4 = new System.Windows.Forms.Label();
            this.e_PaperTableAdapter = new ExamTeach.PaperInfoTableAdapters.E_PaperTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rich_Remark = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Txt_List = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ePaperBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paperInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "考试名称";
            // 
            // Txt_ExamName
            // 
            this.Txt_ExamName.Location = new System.Drawing.Point(80, 13);
            this.Txt_ExamName.Name = "Txt_ExamName";
            this.Txt_ExamName.Size = new System.Drawing.Size(451, 20);
            this.Txt_ExamName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间";
            // 
            // date_Start
            // 
            this.date_Start.CustomFormat = "yyyy-MM-dd HH:mm";
            this.date_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_Start.Location = new System.Drawing.Point(80, 53);
            this.date_Start.Name = "date_Start";
            this.date_Start.Size = new System.Drawing.Size(451, 20);
            this.date_Start.TabIndex = 3;
            this.date_Start.Value = new System.DateTime(2020, 3, 16, 20, 36, 15, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "结束时间";
            // 
            // date_End
            // 
            this.date_End.CustomFormat = "yyyy-MM-dd HH:mm";
            this.date_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_End.Location = new System.Drawing.Point(80, 95);
            this.date_End.Name = "date_End";
            this.date_End.Size = new System.Drawing.Size(451, 20);
            this.date_End.TabIndex = 3;
            this.date_End.Value = new System.DateTime(2020, 3, 16, 20, 36, 15, 0);
            // 
            // Cb_Paper
            // 
            this.Cb_Paper.DataSource = this.ePaperBindingSource;
            this.Cb_Paper.DisplayMember = "PaperName";
            this.Cb_Paper.FormattingEnabled = true;
            this.Cb_Paper.Location = new System.Drawing.Point(80, 141);
            this.Cb_Paper.Name = "Cb_Paper";
            this.Cb_Paper.Size = new System.Drawing.Size(451, 21);
            this.Cb_Paper.TabIndex = 4;
            this.Cb_Paper.ValueMember = "PID";
            // 
            // ePaperBindingSource
            // 
            this.ePaperBindingSource.DataMember = "E_Paper";
            this.ePaperBindingSource.DataSource = this.paperInfo;
            // 
            // paperInfo
            // 
            this.paperInfo.DataSetName = "PaperInfo";
            this.paperInfo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "设置试卷";
            // 
            // e_PaperTableAdapter
            // 
            this.e_PaperTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(461, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(380, 296);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "说明";
            // 
            // rich_Remark
            // 
            this.rich_Remark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rich_Remark.Location = new System.Drawing.Point(80, 183);
            this.rich_Remark.Name = "rich_Remark";
            this.rich_Remark.Size = new System.Drawing.Size(451, 54);
            this.rich_Remark.TabIndex = 6;
            this.rich_Remark.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "考生信息";
            // 
            // Txt_List
            // 
            this.Txt_List.Location = new System.Drawing.Point(80, 254);
            this.Txt_List.Name = "Txt_List";
            this.Txt_List.ReadOnly = true;
            this.Txt_List.Size = new System.Drawing.Size(375, 20);
            this.Txt_List.TabIndex = 8;
            this.Txt_List.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(476, 257);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "模板下载";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // AddExamF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(543, 331);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Txt_List);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rich_Remark);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Cb_Paper);
            this.Controls.Add(this.date_End);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.date_Start);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_ExamName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddExamF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考试信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddExamF_FormClosing);
            this.Load += new System.EventHandler(this.AddExamF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ePaperBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paperInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_ExamName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker date_Start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker date_End;
        private System.Windows.Forms.ComboBox Cb_Paper;
        private System.Windows.Forms.Label label4;
        private PaperInfo paperInfo;
        private System.Windows.Forms.BindingSource ePaperBindingSource;
        private PaperInfoTableAdapters.E_PaperTableAdapter e_PaperTableAdapter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rich_Remark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Txt_List;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}