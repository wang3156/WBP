namespace ExamTeach.WQuestions
{
    partial class SelectQuestions
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
            this.Btn_Add = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.eSelectQuestionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.examSystemDataSet = new ExamTeach.ExamSystemDataSet();
            this.e_SelectQuestionsTableAdapter = new ExamTeach.ExamSystemDataSetTableAdapters.E_SelectQuestionsTableAdapter();
            this.P_Answer = new System.Windows.Forms.Panel();
            this.Txt_Anwser_E = new System.Windows.Forms.TextBox();
            this.CK_E = new System.Windows.Forms.CheckBox();
            this.Txt_Anwser_D = new System.Windows.Forms.TextBox();
            this.CK_D = new System.Windows.Forms.CheckBox();
            this.Txt_Anwser_C = new System.Windows.Forms.TextBox();
            this.CK_C = new System.Windows.Forms.CheckBox();
            this.Txt_Anwser_B = new System.Windows.Forms.TextBox();
            this.CK_B = new System.Windows.Forms.CheckBox();
            this.Txt_Anwser_A = new System.Windows.Forms.TextBox();
            this.CK_A = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Txt_Questions = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Lab_CanADD = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eSelectQuestionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examSystemDataSet)).BeginInit();
            this.P_Answer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Add
            // 
            this.Btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Add.Location = new System.Drawing.Point(822, 377);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(65, 23);
            this.Btn_Add.TabIndex = 2;
            this.Btn_Add.Text = "保存题目";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.eSelectQuestionsBindingSource;
            this.listBox1.DisplayMember = "Questions";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(254, 388);
            this.listBox1.TabIndex = 3;
            this.listBox1.ValueMember = "QID";
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // eSelectQuestionsBindingSource
            // 
            this.eSelectQuestionsBindingSource.DataMember = "E_SelectQuestions";
            this.eSelectQuestionsBindingSource.DataSource = this.examSystemDataSet;
            // 
            // examSystemDataSet
            // 
            this.examSystemDataSet.DataSetName = "ExamSystemDataSet";
            this.examSystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // e_SelectQuestionsTableAdapter
            // 
            this.e_SelectQuestionsTableAdapter.ClearBeforeFill = true;
            // 
            // P_Answer
            // 
            this.P_Answer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.P_Answer.Controls.Add(this.Txt_Anwser_E);
            this.P_Answer.Controls.Add(this.CK_E);
            this.P_Answer.Controls.Add(this.Txt_Anwser_D);
            this.P_Answer.Controls.Add(this.CK_D);
            this.P_Answer.Controls.Add(this.Txt_Anwser_C);
            this.P_Answer.Controls.Add(this.CK_C);
            this.P_Answer.Controls.Add(this.Txt_Anwser_B);
            this.P_Answer.Controls.Add(this.CK_B);
            this.P_Answer.Controls.Add(this.Txt_Anwser_A);
            this.P_Answer.Controls.Add(this.CK_A);
            this.P_Answer.Location = new System.Drawing.Point(295, 180);
            this.P_Answer.Name = "P_Answer";
            this.P_Answer.Size = new System.Drawing.Size(592, 164);
            this.P_Answer.TabIndex = 10;
            // 
            // Txt_Anwser_E
            // 
            this.Txt_Anwser_E.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Anwser_E.Location = new System.Drawing.Point(67, 124);
            this.Txt_Anwser_E.Name = "Txt_Anwser_E";
            this.Txt_Anwser_E.Size = new System.Drawing.Size(496, 21);
            this.Txt_Anwser_E.TabIndex = 9;
            // 
            // CK_E
            // 
            this.CK_E.AutoSize = true;
            this.CK_E.Location = new System.Drawing.Point(25, 127);
            this.CK_E.Name = "CK_E";
            this.CK_E.Size = new System.Drawing.Size(15, 14);
            this.CK_E.TabIndex = 8;
            this.CK_E.UseVisualStyleBackColor = true;
            // 
            // Txt_Anwser_D
            // 
            this.Txt_Anwser_D.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Anwser_D.Location = new System.Drawing.Point(67, 97);
            this.Txt_Anwser_D.Name = "Txt_Anwser_D";
            this.Txt_Anwser_D.Size = new System.Drawing.Size(496, 21);
            this.Txt_Anwser_D.TabIndex = 7;
            // 
            // CK_D
            // 
            this.CK_D.AutoSize = true;
            this.CK_D.Location = new System.Drawing.Point(25, 100);
            this.CK_D.Name = "CK_D";
            this.CK_D.Size = new System.Drawing.Size(15, 14);
            this.CK_D.TabIndex = 6;
            this.CK_D.UseVisualStyleBackColor = true;
            // 
            // Txt_Anwser_C
            // 
            this.Txt_Anwser_C.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Anwser_C.Location = new System.Drawing.Point(67, 70);
            this.Txt_Anwser_C.Name = "Txt_Anwser_C";
            this.Txt_Anwser_C.Size = new System.Drawing.Size(496, 21);
            this.Txt_Anwser_C.TabIndex = 5;
            // 
            // CK_C
            // 
            this.CK_C.AutoSize = true;
            this.CK_C.Location = new System.Drawing.Point(25, 73);
            this.CK_C.Name = "CK_C";
            this.CK_C.Size = new System.Drawing.Size(15, 14);
            this.CK_C.TabIndex = 4;
            this.CK_C.UseVisualStyleBackColor = true;
            // 
            // Txt_Anwser_B
            // 
            this.Txt_Anwser_B.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Anwser_B.Location = new System.Drawing.Point(67, 43);
            this.Txt_Anwser_B.Name = "Txt_Anwser_B";
            this.Txt_Anwser_B.Size = new System.Drawing.Size(496, 21);
            this.Txt_Anwser_B.TabIndex = 3;
            // 
            // CK_B
            // 
            this.CK_B.AutoSize = true;
            this.CK_B.Location = new System.Drawing.Point(25, 46);
            this.CK_B.Name = "CK_B";
            this.CK_B.Size = new System.Drawing.Size(15, 14);
            this.CK_B.TabIndex = 2;
            this.CK_B.UseVisualStyleBackColor = true;
            // 
            // Txt_Anwser_A
            // 
            this.Txt_Anwser_A.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Anwser_A.Location = new System.Drawing.Point(67, 16);
            this.Txt_Anwser_A.Name = "Txt_Anwser_A";
            this.Txt_Anwser_A.Size = new System.Drawing.Size(496, 21);
            this.Txt_Anwser_A.TabIndex = 1;
            // 
            // CK_A
            // 
            this.CK_A.AutoSize = true;
            this.CK_A.Location = new System.Drawing.Point(25, 19);
            this.CK_A.Name = "CK_A";
            this.CK_A.Size = new System.Drawing.Size(15, 14);
            this.CK_A.TabIndex = 0;
            this.CK_A.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(290, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(328, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "题面：(请在下面复选框选择正确答案)";
            // 
            // Txt_Questions
            // 
            this.Txt_Questions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Questions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_Questions.Location = new System.Drawing.Point(295, 40);
            this.Txt_Questions.Name = "Txt_Questions";
            this.Txt_Questions.Size = new System.Drawing.Size(592, 134);
            this.Txt_Questions.TabIndex = 8;
            this.Txt_Questions.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(741, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "添加模式";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(615, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "可添加：";
            // 
            // Lab_CanADD
            // 
            this.Lab_CanADD.AutoSize = true;
            this.Lab_CanADD.Location = new System.Drawing.Point(674, 382);
            this.Lab_CanADD.Name = "Lab_CanADD";
            this.Lab_CanADD.Size = new System.Drawing.Size(17, 12);
            this.Lab_CanADD.TabIndex = 12;
            this.Lab_CanADD.Text = "是";
            // 
            // SelectQuestions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 420);
            this.Controls.Add(this.Lab_CanADD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.P_Answer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Txt_Questions);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Btn_Add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectQuestions";
            this.Text = "选择题";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectQuestions_FormClosing);
            this.Load += new System.EventHandler(this.SelectQuestions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eSelectQuestionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examSystemDataSet)).EndInit();
            this.P_Answer.ResumeLayout(false);
            this.P_Answer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.ListBox listBox1;
        private ExamSystemDataSet examSystemDataSet;
        private System.Windows.Forms.BindingSource eSelectQuestionsBindingSource;
        private ExamSystemDataSetTableAdapters.E_SelectQuestionsTableAdapter e_SelectQuestionsTableAdapter;
        private System.Windows.Forms.Panel P_Answer;
        private System.Windows.Forms.TextBox Txt_Anwser_E;
        private System.Windows.Forms.CheckBox CK_E;
        private System.Windows.Forms.TextBox Txt_Anwser_D;
        private System.Windows.Forms.CheckBox CK_D;
        private System.Windows.Forms.TextBox Txt_Anwser_C;
        private System.Windows.Forms.CheckBox CK_C;
        private System.Windows.Forms.TextBox Txt_Anwser_B;
        private System.Windows.Forms.CheckBox CK_B;
        private System.Windows.Forms.TextBox Txt_Anwser_A;
        private System.Windows.Forms.CheckBox CK_A;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox Txt_Questions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lab_CanADD;
    }
}