namespace proteinDetect
{
    partial class showResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(showResult));
            this.fitCurveStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meanGray = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meanOpticalDensity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveConcenStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CalcMeanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToExcel = new System.Windows.Forms.SaveFileDialog();
            this.concenStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.求平均值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputConcenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppendToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppendToExcelOFD = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fitCurveStripMenuItem
            // 
            this.fitCurveStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fitCurveStripMenuItem.Name = "fitCurveStripMenuItem";
            this.fitCurveStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.fitCurveStripMenuItem.Text = "生成曲线";
            this.fitCurveStripMenuItem.Click += new System.EventHandler(this.fitCurveStripMenuItem_Click);
            // 
            // resultGrid
            // 
            this.resultGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.resultGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.area,
            this.meanGray,
            this.meanOpticalDensity});
            this.resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultGrid.Location = new System.Drawing.Point(0, 27);
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.RowTemplate.Height = 23;
            this.resultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.resultGrid.Size = new System.Drawing.Size(833, 755);
            this.resultGrid.TabIndex = 4;
            // 
            // num
            // 
            this.num.HeaderText = "序号";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // area
            // 
            this.area.HeaderText = "面积";
            this.area.Name = "area";
            this.area.ReadOnly = true;
            this.area.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // meanGray
            // 
            this.meanGray.HeaderText = "平均灰度";
            this.meanGray.Name = "meanGray";
            this.meanGray.ReadOnly = true;
            this.meanGray.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // meanOpticalDensity
            // 
            this.meanOpticalDensity.HeaderText = "平均光密度";
            this.meanOpticalDensity.Name = "meanOpticalDensity";
            this.meanOpticalDensity.ReadOnly = true;
            this.meanOpticalDensity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // saveConcenStripMenuItem
            // 
            this.saveConcenStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveConcenStripMenuItem.CheckOnClick = true;
            this.saveConcenStripMenuItem.Name = "saveConcenStripMenuItem";
            this.saveConcenStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.saveConcenStripMenuItem.Text = "保存浓度";
            this.saveConcenStripMenuItem.Click += new System.EventHandler(this.saveConcenStripMenuItem_Click);
            // 
            // CalcMeanToolStripMenuItem
            // 
            this.CalcMeanToolStripMenuItem.Name = "CalcMeanToolStripMenuItem";
            this.CalcMeanToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.CalcMeanToolStripMenuItem.Text = "求平均";
            this.CalcMeanToolStripMenuItem.Click += new System.EventHandler(this.CalcMeanToolStripMenuItem_Click);
            // 
            // concenStripTextBox
            // 
            this.concenStripTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.concenStripTextBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.concenStripTextBox.Name = "concenStripTextBox";
            this.concenStripTextBox.Size = new System.Drawing.Size(100, 23);
            this.concenStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConcenStripTextBoxKeyDown);
            this.concenStripTextBox.Click += new System.EventHandler(this.ConcenStripTextBoxClick);
            // 
            // 求平均值ToolStripMenuItem
            // 
            this.求平均值ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CalcMeanToolStripMenuItem});
            this.求平均值ToolStripMenuItem.Name = "求平均值ToolStripMenuItem";
            this.求平均值ToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.求平均值ToolStripMenuItem.Text = "分析";
            // 
            // inputConcenMenuItem
            // 
            this.inputConcenMenuItem.Name = "inputConcenMenuItem";
            this.inputConcenMenuItem.Size = new System.Drawing.Size(68, 23);
            this.inputConcenMenuItem.Text = "填入浓度";
            // 
            // AppendToExcelToolStripMenuItem
            // 
            this.AppendToExcelToolStripMenuItem.Name = "AppendToExcelToolStripMenuItem";
            this.AppendToExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AppendToExcelToolStripMenuItem.Text = "添加到Excel";
            this.AppendToExcelToolStripMenuItem.Click += new System.EventHandler(this.AppendToExcelToolStripMenuItem_Click);
            // 
            // 导出到ToolStripMenuItem
            // 
            this.导出到ToolStripMenuItem.Name = "导出到ToolStripMenuItem";
            this.导出到ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导出到ToolStripMenuItem.Text = "导出到Excel";
            this.导出到ToolStripMenuItem.Click += new System.EventHandler(this.导出到ToolStripMenuItem_Click);
            // 
            // 导出数据ToolStripMenuItem
            // 
            this.导出数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出到ToolStripMenuItem,
            this.AppendToExcelToolStripMenuItem});
            this.导出数据ToolStripMenuItem.Name = "导出数据ToolStripMenuItem";
            this.导出数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.导出数据ToolStripMenuItem.Text = "导出数据";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出数据ToolStripMenuItem,
            this.求平均值ToolStripMenuItem,
            this.fitCurveStripMenuItem,
            this.saveConcenStripMenuItem,
            this.concenStripTextBox,
            this.inputConcenMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(833, 27);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // showResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 782);
            this.Controls.Add(this.resultGrid);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "showResult";
            this.Text = "分析结果";
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fitCurveStripMenuItem;
        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn area;
        private System.Windows.Forms.DataGridViewTextBoxColumn meanGray;
        private System.Windows.Forms.DataGridViewTextBoxColumn meanOpticalDensity;
        private System.Windows.Forms.ToolStripMenuItem saveConcenStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CalcMeanToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveToExcel;
        private System.Windows.Forms.ToolStripTextBox concenStripTextBox;
        private System.Windows.Forms.ToolStripMenuItem 求平均值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputConcenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AppendToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出到ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出数据ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog AppendToExcelOFD;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}