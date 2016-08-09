namespace proteinDetect
{
    partial class ChildForm
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
            //这是新打开一个图片判断的依据
            flag = false;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.分析AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.目标定位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbPanel = new System.Windows.Forms.Panel();
            this.childPb = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.pbPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.childPb)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分析AToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(818, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 分析AToolStripMenuItem
            // 
            this.分析AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.目标定位ToolStripMenuItem,
            this.testToolStripMenuItem});
            this.分析AToolStripMenuItem.Name = "分析AToolStripMenuItem";
            this.分析AToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.分析AToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.分析AToolStripMenuItem.Text = "分析(&A)";
            this.分析AToolStripMenuItem.Click += new System.EventHandler(this.分析AToolStripMenuItem_Click);
            // 
            // 目标定位ToolStripMenuItem
            // 
            this.目标定位ToolStripMenuItem.Enabled = false;
            this.目标定位ToolStripMenuItem.Name = "目标定位ToolStripMenuItem";
            this.目标定位ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.目标定位ToolStripMenuItem.Text = "目标定位";
            this.目标定位ToolStripMenuItem.Visible = false;
            this.目标定位ToolStripMenuItem.Click += new System.EventHandler(this.目标定位ToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Enabled = false;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.testToolStripMenuItem.Text = "分析结果";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // pbPanel
            // 
            this.pbPanel.AutoScroll = true;
            this.pbPanel.BackColor = System.Drawing.SystemColors.Control;
            this.pbPanel.Controls.Add(this.childPb);
            this.pbPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPanel.Location = new System.Drawing.Point(0, 25);
            this.pbPanel.Name = "pbPanel";
            this.pbPanel.Size = new System.Drawing.Size(818, 612);
            this.pbPanel.TabIndex = 3;
            // 
            // childPb
            // 
            this.childPb.BackColor = System.Drawing.SystemColors.Control;
            this.childPb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.childPb.Location = new System.Drawing.Point(0, 0);
            this.childPb.Name = "childPb";
            this.childPb.Size = new System.Drawing.Size(448, 552);
            this.childPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.childPb.TabIndex = 0;
            this.childPb.TabStop = false;
            this.childPb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChildPbMouseDown);
            this.childPb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChildPbMouseMove);
            this.childPb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildPbMouseUp);
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(818, 637);
            this.Controls.Add(this.pbPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ChildForm";
            this.Text = "ChildForm";
            this.Load += new System.EventHandler(this.ChildForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pbPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.childPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 分析AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 目标定位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.PictureBox childPb;
        private System.Windows.Forms.Panel pbPanel;
    }
}