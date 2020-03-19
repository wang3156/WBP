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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CheckRow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_ExamKS = new System.Windows.Forms.DataGridViewLinkColumn();
            this.E_Paper = new System.Windows.Forms.DataGridViewLinkColumn();
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1407, 612);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1404, 44);
            this.panel1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1009, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 21);
            this.button5.TabIndex = 4;
            this.button5.Text = "添加";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1310, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 21);
            this.button3.TabIndex = 3;
            this.button3.Text = "结束选择";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1109, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 21);
            this.button4.TabIndex = 3;
            this.button4.Text = "批量设置试卷";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1219, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 3;
            this.button2.Text = "开始选择";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(407, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(319, 21);
            this.textBox1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1404, 559);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckRow,
            this.EID,
            this.ExamName,
            this.ExamRemark,
            this.EStart,
            this.EEnd,
            this.EStatus,
            this.PaperName,
            this.E_ExamKS,
            this.E_Paper,
            this.EStartExam,
            this.EEndExam});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1401, 553);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // CheckRow
            // 
            this.CheckRow.FalseValue = "0";
            this.CheckRow.FillWeight = 52F;
            this.CheckRow.Frozen = true;
            this.CheckRow.HeaderText = "选择";
            this.CheckRow.IndeterminateValue = "0";
            this.CheckRow.Name = "CheckRow";
            this.CheckRow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CheckRow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CheckRow.TrueValue = "1";
            this.CheckRow.Width = 52;
            // 
            // EID
            // 
            this.EID.DataPropertyName = "EID";
            this.EID.FillWeight = 52F;
            this.EID.Frozen = true;
            this.EID.HeaderText = "编号";
            this.EID.Name = "EID";
            this.EID.ReadOnly = true;
            this.EID.Width = 52;
            // 
            // ExamName
            // 
            this.ExamName.DataPropertyName = "ExamName";
            this.ExamName.FillWeight = 250F;
            this.ExamName.Frozen = true;
            this.ExamName.HeaderText = "考试名称";
            this.ExamName.Name = "ExamName";
            this.ExamName.ReadOnly = true;
            this.ExamName.Width = 225;
            // 
            // ExamRemark
            // 
            this.ExamRemark.DataPropertyName = "ExamRemark";
            this.ExamRemark.FillWeight = 354F;
            this.ExamRemark.Frozen = true;
            this.ExamRemark.HeaderText = "考试说明";
            this.ExamRemark.Name = "ExamRemark";
            this.ExamRemark.ReadOnly = true;
            this.ExamRemark.Width = 328;
            // 
            // EStart
            // 
            this.EStart.DataPropertyName = "EStart";
            this.EStart.FillWeight = 150F;
            this.EStart.Frozen = true;
            this.EStart.HeaderText = "开始时间";
            this.EStart.Name = "EStart";
            this.EStart.ReadOnly = true;
            this.EStart.Width = 150;
            // 
            // EEnd
            // 
            this.EEnd.DataPropertyName = "EEnd";
            this.EEnd.FillWeight = 150F;
            this.EEnd.Frozen = true;
            this.EEnd.HeaderText = "结束时间";
            this.EEnd.Name = "EEnd";
            this.EEnd.ReadOnly = true;
            this.EEnd.Width = 150;
            // 
            // EStatus
            // 
            this.EStatus.DataPropertyName = "EStatus";
            this.EStatus.Frozen = true;
            this.EStatus.HeaderText = "状态";
            this.EStatus.Name = "EStatus";
            this.EStatus.ReadOnly = true;
            // 
            // PaperName
            // 
            this.PaperName.DataPropertyName = "PaperName";
            this.PaperName.FillWeight = 115F;
            this.PaperName.Frozen = true;
            this.PaperName.HeaderText = "试卷名";
            this.PaperName.Name = "PaperName";
            this.PaperName.ReadOnly = true;
            this.PaperName.Width = 115;
            // 
            // E_ExamKS
            // 
            this.E_ExamKS.FillWeight = 80F;
            this.E_ExamKS.Frozen = true;
            this.E_ExamKS.HeaderText = "考生信息";
            this.E_ExamKS.Name = "E_ExamKS";
            this.E_ExamKS.ReadOnly = true;
            this.E_ExamKS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.E_ExamKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.E_ExamKS.Text = "信息";
            this.E_ExamKS.UseColumnTextForLinkValue = true;
            this.E_ExamKS.Width = 80;
            // 
            // E_Paper
            // 
            this.E_Paper.FillWeight = 52F;
            this.E_Paper.Frozen = true;
            this.E_Paper.HeaderText = "设置";
            this.E_Paper.Name = "E_Paper";
            this.E_Paper.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.E_Paper.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.E_Paper.Text = "设置";
            this.E_Paper.UseColumnTextForLinkValue = true;
            this.E_Paper.Width = 52;
            // 
            // EStartExam
            // 
            this.EStartExam.FillWeight = 50F;
            this.EStartExam.Frozen = true;
            this.EStartExam.HeaderText = "开始";
            this.EStartExam.Name = "EStartExam";
            this.EStartExam.Text = "开始";
            this.EStartExam.UseColumnTextForLinkValue = true;
            this.EStartExam.Width = 50;
            // 
            // EEndExam
            // 
            this.EEndExam.FillWeight = 50F;
            this.EEndExam.Frozen = true;
            this.EEndExam.HeaderText = "结束";
            this.EEndExam.Name = "EEndExam";
            this.EEndExam.Text = "结束";
            this.EEndExam.UseColumnTextForLinkValue = true;
            this.EEndExam.Width = 50;
            // 
            // ZuKao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 612);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZuKao";
            this.Text = "组考管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZuKao_FormClosing);
            this.Load += new System.EventHandler(this.ZuKao_Load);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn EID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn EStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn EEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn EStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaperName;
        private System.Windows.Forms.DataGridViewLinkColumn E_ExamKS;
        private System.Windows.Forms.DataGridViewLinkColumn E_Paper;
        private System.Windows.Forms.DataGridViewLinkColumn EStartExam;
        private System.Windows.Forms.DataGridViewLinkColumn EEndExam;
    }
}