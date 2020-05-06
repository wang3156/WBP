using ExamManage;

namespace ExamTeach
{
    partial class SetPaperByExam
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
            this.Cb_Paper = new System.Windows.Forms.ComboBox();
            this.ePaperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.paperInfo = new PaperInfo();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.e_PaperTableAdapter = new ExamManage.PaperInfoTableAdapters.E_PaperTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ePaperBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paperInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // Cb_Paper
            // 
            this.Cb_Paper.DataSource = this.ePaperBindingSource;
            this.Cb_Paper.DisplayMember = "PaperName";
            this.Cb_Paper.FormattingEnabled = true;
            this.Cb_Paper.Location = new System.Drawing.Point(74, 22);
            this.Cb_Paper.Name = "Cb_Paper";
            this.Cb_Paper.Size = new System.Drawing.Size(304, 21);
            this.Cb_Paper.TabIndex = 6;
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
            this.label4.Location = new System.Drawing.Point(2, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "设置试卷";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(220, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 7;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // e_PaperTableAdapter
            // 
            this.e_PaperTableAdapter.ClearBeforeFill = true;
            // 
            // SetPaperByExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 100);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Cb_Paper);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetPaperByExam";
            this.Text = "批量设置试卷";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetPaperByExam_FormClosing);
            this.Load += new System.EventHandler(this.SetPaperByExam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ePaperBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paperInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cb_Paper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private PaperInfo paperInfo;
        private System.Windows.Forms.BindingSource ePaperBindingSource;
        private ExamManage.PaperInfoTableAdapters.E_PaperTableAdapter e_PaperTableAdapter;
    }
}