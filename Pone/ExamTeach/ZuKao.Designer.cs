namespace ExamTeach
{
    partial class ZuKao
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.EID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EStartExam = new System.Windows.Forms.DataGridViewLinkColumn();
            this.EEndExam = new System.Windows.Forms.DataGridViewLinkColumn();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1169, 606);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1163, 44);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1166, 553);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EID,
            this.ExamName,
            this.ExamRemark,
            this.EStart,
            this.EEnd,
            this.EStatus,
            this.EStartExam,
            this.EEndExam});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1160, 547);
            this.dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(319, 21);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "考试名称";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(407, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // EID
            // 
            this.EID.DataPropertyName = "EID";
            this.EID.Frozen = true;
            this.EID.HeaderText = "考试编号";
            this.EID.Name = "EID";
            this.EID.ReadOnly = true;
            // 
            // ExamName
            // 
            this.ExamName.DataPropertyName = "ExamName";
            this.ExamName.Frozen = true;
            this.ExamName.HeaderText = "考试名称";
            this.ExamName.Name = "ExamName";
            this.ExamName.ReadOnly = true;
            // 
            // ExamRemark
            // 
            this.ExamRemark.DataPropertyName = "ExamRemark";
            this.ExamRemark.Frozen = true;
            this.ExamRemark.HeaderText = "考试说明";
            this.ExamRemark.Name = "ExamRemark";
            this.ExamRemark.ReadOnly = true;
            // 
            // EStart
            // 
            this.EStart.DataPropertyName = "EStart";
            this.EStart.Frozen = true;
            this.EStart.HeaderText = "开始时间";
            this.EStart.Name = "EStart";
            this.EStart.ReadOnly = true;
            // 
            // EEnd
            // 
            this.EEnd.DataPropertyName = "EEnd";
            this.EEnd.Frozen = true;
            this.EEnd.HeaderText = "结束时间";
            this.EEnd.Name = "EEnd";
            this.EEnd.ReadOnly = true;
            // 
            // EStatus
            // 
            this.EStatus.DataPropertyName = "EStatus";
            this.EStatus.Frozen = true;
            this.EStatus.HeaderText = "状态";
            this.EStatus.Name = "EStatus";
            this.EStatus.ReadOnly = true;
            // 
            // EStartExam
            // 
            this.EStartExam.Frozen = true;
            this.EStartExam.HeaderText = "开始考试";
            this.EStartExam.Name = "EStartExam";
            this.EStartExam.ReadOnly = true;
            // 
            // EEndExam
            // 
            this.EEndExam.Frozen = true;
            this.EEndExam.HeaderText = "结束考试";
            this.EEndExam.Name = "EEndExam";
            this.EEndExam.ReadOnly = true;
            // 
            // ZuKao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 606);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZuKao";
            this.Text = "组考管理";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn EStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn EEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn EStatus;
        private System.Windows.Forms.DataGridViewLinkColumn EStartExam;
        private System.Windows.Forms.DataGridViewLinkColumn EEndExam;
    }
}