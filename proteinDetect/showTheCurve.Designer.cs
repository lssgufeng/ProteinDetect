/*
 * Created by SharpDevelop.
 * author:ligong
 * Date: 2014/6/26
 * Time: 9:40
 */
namespace proteinDetect
{
	partial class showTheCurve
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(showTheCurve));
            this.zGControl = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ODTextbox = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.curveComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.savsDataMenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reviseCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.concentrationLabel = new System.Windows.Forms.TextBox();
            this.曲线选择 = new System.Windows.Forms.GroupBox();
            this.CEAcheckBox = new System.Windows.Forms.CheckBox();
            this.NSEcheckBox = new System.Windows.Forms.CheckBox();
            this.DKK1checkBox = new System.Windows.Forms.CheckBox();
            this.CY211checkBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDataDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.曲线选择.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // zGControl
            // 
            this.zGControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zGControl.Location = new System.Drawing.Point(0, 0);
            this.zGControl.Name = "zGControl";
            this.zGControl.ScrollGrace = 0D;
            this.zGControl.ScrollMaxX = 0D;
            this.zGControl.ScrollMaxY = 0D;
            this.zGControl.ScrollMaxY2 = 0D;
            this.zGControl.ScrollMinX = 0D;
            this.zGControl.ScrollMinY = 0D;
            this.zGControl.ScrollMinY2 = 0D;
            this.zGControl.Size = new System.Drawing.Size(728, 600);
            this.zGControl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "OD值";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(120, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "浓度";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ODTextbox
            // 
            this.ODTextbox.Location = new System.Drawing.Point(8, 56);
            this.ODTextbox.Multiline = true;
            this.ODTextbox.Name = "ODTextbox";
            this.ODTextbox.Size = new System.Drawing.Size(96, 280);
            this.ODTextbox.TabIndex = 3;
            // 
            // calculateButton
            // 
            this.calculateButton.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.calculateButton.Location = new System.Drawing.Point(32, 360);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(160, 32);
            this.calculateButton.TabIndex = 5;
            this.calculateButton.Text = "计   算";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.CalculateButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.curveComboBox,
            this.savsDataMenuitem,
            this.fitStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 29);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // curveComboBox
            // 
            this.curveComboBox.Items.AddRange(new object[] {
            "4参数logistic拟合",
            "3参数指数拟合",
            "4参数指数拟合"});
            this.curveComboBox.Name = "curveComboBox";
            this.curveComboBox.Size = new System.Drawing.Size(121, 25);
            this.curveComboBox.SelectedIndexChanged += new System.EventHandler(this.curveComboBox_SelectedIndexChanged_1);
            // 
            // savsDataMenuitem
            // 
            this.savsDataMenuitem.Name = "savsDataMenuitem";
            this.savsDataMenuitem.Size = new System.Drawing.Size(68, 25);
            this.savsDataMenuitem.Text = "保存数据";
            this.savsDataMenuitem.Click += new System.EventHandler(this.SavsDataMenuitemClick);
            // 
            // fitStripMenuItem
            // 
            this.fitStripMenuItem.Name = "fitStripMenuItem";
            this.fitStripMenuItem.Size = new System.Drawing.Size(48, 25);
            this.fitStripMenuItem.Text = "拟 合";
            this.fitStripMenuItem.Click += new System.EventHandler(this.FitStripMenuItemClick);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.reviseCheckBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.曲线选择);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(733, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 629);
            this.panel1.TabIndex = 11;
            // 
            // reviseCheckBox
            // 
            this.reviseCheckBox.AutoSize = true;
            this.reviseCheckBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reviseCheckBox.Location = new System.Drawing.Point(90, 16);
            this.reviseCheckBox.Name = "reviseCheckBox";
            this.reviseCheckBox.Size = new System.Drawing.Size(110, 18);
            this.reviseCheckBox.TabIndex = 13;
            this.reviseCheckBox.Text = "去除异常值点";
            this.reviseCheckBox.UseVisualStyleBackColor = true;
            this.reviseCheckBox.CheckedChanged += new System.EventHandler(this.reviseCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.concentrationLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.calculateButton);
            this.groupBox1.Controls.Add(this.ODTextbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(8, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 408);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计算浓度";
            // 
            // concentrationLabel
            // 
            this.concentrationLabel.Location = new System.Drawing.Point(120, 56);
            this.concentrationLabel.Multiline = true;
            this.concentrationLabel.Name = "concentrationLabel";
            this.concentrationLabel.ReadOnly = true;
            this.concentrationLabel.Size = new System.Drawing.Size(96, 280);
            this.concentrationLabel.TabIndex = 6;
            // 
            // 曲线选择
            // 
            this.曲线选择.Controls.Add(this.CEAcheckBox);
            this.曲线选择.Controls.Add(this.NSEcheckBox);
            this.曲线选择.Controls.Add(this.DKK1checkBox);
            this.曲线选择.Controls.Add(this.CY211checkBox);
            this.曲线选择.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.曲线选择.Location = new System.Drawing.Point(8, 40);
            this.曲线选择.Name = "曲线选择";
            this.曲线选择.Size = new System.Drawing.Size(224, 108);
            this.曲线选择.TabIndex = 10;
            this.曲线选择.TabStop = false;
            this.曲线选择.Text = "曲线选择";
            // 
            // CEAcheckBox
            // 
            this.CEAcheckBox.Checked = true;
            this.CEAcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CEAcheckBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CEAcheckBox.Location = new System.Drawing.Point(144, 24);
            this.CEAcheckBox.Name = "CEAcheckBox";
            this.CEAcheckBox.Size = new System.Drawing.Size(56, 24);
            this.CEAcheckBox.TabIndex = 1;
            this.CEAcheckBox.Text = "CEA";
            this.CEAcheckBox.UseVisualStyleBackColor = true;
            this.CEAcheckBox.CheckedChanged += new System.EventHandler(this.CEAcheckBoxCheckedChanged);
            // 
            // NSEcheckBox
            // 
            this.NSEcheckBox.Checked = true;
            this.NSEcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NSEcheckBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NSEcheckBox.Location = new System.Drawing.Point(40, 24);
            this.NSEcheckBox.Name = "NSEcheckBox";
            this.NSEcheckBox.Size = new System.Drawing.Size(56, 24);
            this.NSEcheckBox.TabIndex = 0;
            this.NSEcheckBox.Text = "NSE";
            this.NSEcheckBox.UseVisualStyleBackColor = true;
            this.NSEcheckBox.CheckedChanged += new System.EventHandler(this.NSEcheckBoxCheckedChanged);
            // 
            // DKK1checkBox
            // 
            this.DKK1checkBox.Checked = true;
            this.DKK1checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DKK1checkBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DKK1checkBox.Location = new System.Drawing.Point(144, 72);
            this.DKK1checkBox.Name = "DKK1checkBox";
            this.DKK1checkBox.Size = new System.Drawing.Size(56, 24);
            this.DKK1checkBox.TabIndex = 3;
            this.DKK1checkBox.Text = "DKK1";
            this.DKK1checkBox.UseVisualStyleBackColor = true;
            this.DKK1checkBox.CheckedChanged += new System.EventHandler(this.DKK1checkBoxCheckedChanged);
            // 
            // CY211checkBox
            // 
            this.CY211checkBox.Checked = true;
            this.CY211checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CY211checkBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CY211checkBox.Location = new System.Drawing.Point(40, 72);
            this.CY211checkBox.Name = "CY211checkBox";
            this.CY211checkBox.Size = new System.Drawing.Size(64, 24);
            this.CY211checkBox.TabIndex = 2;
            this.CY211checkBox.Text = "CY211";
            this.CY211checkBox.UseVisualStyleBackColor = true;
            this.CY211checkBox.CheckedChanged += new System.EventHandler(this.CY211checkBoxCheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.zGControl);
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(728, 600);
            this.panel2.TabIndex = 12;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(256, 447);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(445, 78);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.Visible = false;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "蛋白";
            this.Column5.Name = "Column5";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "A";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "B";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "C";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "D";
            this.Column4.Name = "Column4";
            // 
            // showTheCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 629);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(528, 0);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "showTheCurve";
            this.Text = "showTheCurve";
            this.Load += new System.EventHandler(this.ShowTheCurveLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.曲线选择.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.TextBox concentrationLabel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox NSEcheckBox;
		private System.Windows.Forms.CheckBox CEAcheckBox;
		private System.Windows.Forms.CheckBox CY211checkBox;
		private System.Windows.Forms.CheckBox DKK1checkBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox 曲线选择;
		private System.Windows.Forms.ToolStripMenuItem fitStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox curveComboBox;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.Button calculateButton;
		private System.Windows.Forms.TextBox ODTextbox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private ZedGraph.ZedGraphControl zGControl;
		private System.Windows.Forms.SaveFileDialog saveFileDataDialog;
        private System.Windows.Forms.ToolStripMenuItem savsDataMenuitem;
        private System.Windows.Forms.CheckBox reviseCheckBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
	}
}
