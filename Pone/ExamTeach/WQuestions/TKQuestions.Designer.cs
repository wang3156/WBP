namespace ExamTeach.WQuestions
{
    partial class TKQuestions
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.eTKQuestionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.examSystemDataSet1 = new ExamTeach.ExamSystemDataSet1();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Txt_Copy = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Add_Question = new System.Windows.Forms.Button();
            this.Lab_CanADD = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.e_TKQuestionsTableAdapter = new ExamTeach.ExamSystemDataSet1TableAdapters.E_TKQuestionsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.eTKQuestionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examSystemDataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.eTKQuestionsBindingSource;
            this.listBox1.DisplayMember = "Questions";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(254, 388);
            this.listBox1.TabIndex = 0;
            this.listBox1.ValueMember = "QID";
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // eTKQuestionsBindingSource
            // 
            this.eTKQuestionsBindingSource.DataMember = "E_TKQuestions";
            this.eTKQuestionsBindingSource.DataSource = this.examSystemDataSet1;
            // 
            // examSystemDataSet1
            // 
            this.examSystemDataSet1.DataSetName = "ExamSystemDataSet1";
            this.examSystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(282, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(605, 133);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(277, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(466, 25);
            this.label7.TabIndex = 10;
            this.label7.Text = "题面：(输入题面时空格以四个连续的_表示,如:A____C)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Txt_Copy);
            this.panel1.Location = new System.Drawing.Point(282, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 207);
            this.panel1.TabIndex = 11;
            // 
            // Txt_Copy
            // 
            this.Txt_Copy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Copy.Location = new System.Drawing.Point(17, 13);
            this.Txt_Copy.Name = "Txt_Copy";
            this.Txt_Copy.Size = new System.Drawing.Size(573, 21);
            this.Txt_Copy.TabIndex = 0;
            this.Txt_Copy.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(812, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "保存题目";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Add_Question
            // 
            this.Add_Question.Location = new System.Drawing.Point(731, 389);
            this.Add_Question.Name = "Add_Question";
            this.Add_Question.Size = new System.Drawing.Size(75, 23);
            this.Add_Question.TabIndex = 13;
            this.Add_Question.Text = "添加模式";
            this.Add_Question.UseVisualStyleBackColor = true;
            this.Add_Question.Click += new System.EventHandler(this.button2_Click);
            // 
            // Lab_CanADD
            // 
            this.Lab_CanADD.AutoSize = true;
            this.Lab_CanADD.Location = new System.Drawing.Point(675, 394);
            this.Lab_CanADD.Name = "Lab_CanADD";
            this.Lab_CanADD.Size = new System.Drawing.Size(17, 12);
            this.Lab_CanADD.TabIndex = 14;
            this.Lab_CanADD.Text = "是";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "可添加：";
            // 
            // e_TKQuestionsTableAdapter
            // 
            this.e_TKQuestionsTableAdapter.ClearBeforeFill = true;
            // 
            // TKQuestions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 420);
            this.Controls.Add(this.Lab_CanADD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Add_Question);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TKQuestions";
            this.Text = "填空题";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TKQuestions_FormClosing);
            this.Load += new System.EventHandler(this.TKQuestions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eTKQuestionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examSystemDataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Add_Question;
        private System.Windows.Forms.TextBox Txt_Copy;
        private System.Windows.Forms.Label Lab_CanADD;
        private System.Windows.Forms.Label label1;
        private ExamSystemDataSet1 examSystemDataSet1;
        private System.Windows.Forms.BindingSource eTKQuestionsBindingSource;
        private ExamSystemDataSet1TableAdapters.E_TKQuestionsTableAdapter e_TKQuestionsTableAdapter;
    }
}