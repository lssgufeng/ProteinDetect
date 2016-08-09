namespace proteinDetect
{
    partial class collectData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(collectData));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pasteButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CreateCurveButton = new System.Windows.Forms.Button();
            this.saveToExcel = new System.Windows.Forms.SaveFileDialog();
            this.deleteData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(907, 490);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewKeyDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "浓度";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "NSE";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CEA";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "CY211";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "DKK1";
            this.Column5.Name = "Column5";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.dataGridView);
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(907, 490);
            this.panel2.TabIndex = 8;
            // 
            // pasteButton
            // 
            this.pasteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pasteButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pasteButton.Location = new System.Drawing.Point(654, 0);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(64, 40);
            this.pasteButton.TabIndex = 4;
            this.pasteButton.Text = "粘 贴";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deleteData);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pasteButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.CreateCurveButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 40);
            this.panel1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "导出到excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveButton.Location = new System.Drawing.Point(726, 0);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Padding = new System.Windows.Forms.Padding(3);
            this.SaveButton.Size = new System.Drawing.Size(88, 40);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "保存浓度";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CreateCurveButton
            // 
            this.CreateCurveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateCurveButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CreateCurveButton.Location = new System.Drawing.Point(822, 0);
            this.CreateCurveButton.Name = "CreateCurveButton";
            this.CreateCurveButton.Padding = new System.Windows.Forms.Padding(3);
            this.CreateCurveButton.Size = new System.Drawing.Size(88, 40);
            this.CreateCurveButton.TabIndex = 3;
            this.CreateCurveButton.Text = "拟合曲线";
            this.CreateCurveButton.UseVisualStyleBackColor = true;
            this.CreateCurveButton.Click += new System.EventHandler(this.CreateCurveButton_Click);
            // 
            // deleteData
            // 
            this.deleteData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deleteData.Location = new System.Drawing.Point(102, 0);
            this.deleteData.Name = "deleteData";
            this.deleteData.Size = new System.Drawing.Size(101, 40);
            this.deleteData.TabIndex = 6;
            this.deleteData.Text = "删除历史数据";
            this.deleteData.UseVisualStyleBackColor = true;
            this.deleteData.Click += new System.EventHandler(this.deleteData_Click);
            // 
            // collectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 528);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "collectData";
            this.Text = "图片的浓度是";
            this.Load += new System.EventHandler(this.collectData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CreateCurveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveToExcel;
        private System.Windows.Forms.Button deleteData;
    }
}