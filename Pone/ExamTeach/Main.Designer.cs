namespace ExamTeach
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MQuestion = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTK = new System.Windows.Forms.ToolStripMenuItem();
            this.MZJ = new System.Windows.Forms.ToolStripMenuItem();
            this.MSetExam = new System.Windows.Forms.ToolStripMenuItem();
            this.监听服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.M_StartListener = new System.Windows.Forms.ToolStripMenuItem();
            this.M_EndListener = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MQuestion,
            this.MZJ,
            this.MSetExam,
            this.监听服务ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1344, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MQuestion
            // 
            this.MQuestion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSelect,
            this.AddTK});
            this.MQuestion.Name = "MQuestion";
            this.MQuestion.Size = new System.Drawing.Size(68, 21);
            this.MQuestion.Text = "添加试题";
            // 
            // AddSelect
            // 
            this.AddSelect.Name = "AddSelect";
            this.AddSelect.Size = new System.Drawing.Size(112, 22);
            this.AddSelect.Text = "选择题";
            this.AddSelect.Click += new System.EventHandler(this.AddSelect_Click);
            // 
            // AddTK
            // 
            this.AddTK.Name = "AddTK";
            this.AddTK.Size = new System.Drawing.Size(112, 22);
            this.AddTK.Text = "填空题";
            this.AddTK.Click += new System.EventHandler(this.AddTK_Click);
            // 
            // MZJ
            // 
            this.MZJ.Name = "MZJ";
            this.MZJ.Size = new System.Drawing.Size(44, 21);
            this.MZJ.Text = "组卷";
            this.MZJ.Click += new System.EventHandler(this.MZJ_Click);
            // 
            // MSetExam
            // 
            this.MSetExam.Name = "MSetExam";
            this.MSetExam.Size = new System.Drawing.Size(68, 21);
            this.MSetExam.Text = "设置考试";
            this.MSetExam.Click += new System.EventHandler(this.MSetExam_Click);
            // 
            // 监听服务ToolStripMenuItem
            // 
            this.监听服务ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M_StartListener,
            this.M_EndListener});
            this.监听服务ToolStripMenuItem.Name = "监听服务ToolStripMenuItem";
            this.监听服务ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.监听服务ToolStripMenuItem.Text = "监听服务";
            // 
            // M_StartListener
            // 
            this.M_StartListener.Name = "M_StartListener";
            this.M_StartListener.Size = new System.Drawing.Size(124, 22);
            this.M_StartListener.Text = "开启监听";
            this.M_StartListener.Click += new System.EventHandler(this.M_StartListener_Click);
            // 
            // M_EndListener
            // 
            this.M_EndListener.Name = "M_EndListener";
            this.M_EndListener.Size = new System.Drawing.Size(124, 22);
            this.M_EndListener.Text = "停止监听";
            this.M_EndListener.Click += new System.EventHandler(this.M_EndListener_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 795);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "教师端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MQuestion;
        private System.Windows.Forms.ToolStripMenuItem MZJ;
        private System.Windows.Forms.ToolStripMenuItem MSetExam;
        private System.Windows.Forms.ToolStripMenuItem AddSelect;
        private System.Windows.Forms.ToolStripMenuItem AddTK;
        private System.Windows.Forms.ToolStripMenuItem 监听服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem M_StartListener;
        private System.Windows.Forms.ToolStripMenuItem M_EndListener;
    }
}

