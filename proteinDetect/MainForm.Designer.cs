namespace proteinDetect
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileStoreData = new System.Windows.Forms.ToolStripMenuItem();
            this.重新载入图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.最近的文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最近历史数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动定位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.移动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调整大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.曲线拟合ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDetectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initTheCurvefitBGW = new System.ComponentModel.BackgroundWorker();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ReloadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.addCircleButton = new System.Windows.Forms.ToolStripButton();
            this.oneClickAdd = new System.Windows.Forms.ToolStripButton();
            this.autoAddCircleTSB = new System.Windows.Forms.ToolStripButton();
            this.autoSearchCenterButton = new System.Windows.Forms.ToolStripButton();
            this.deleteCircleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveCircleButton = new System.Windows.Forms.ToolStripButton();
            this.changeCircleButton = new System.Windows.Forms.ToolStripButton();
            this.fittoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.moveCircleTSB = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.openFileStoreDataOFD = new System.Windows.Forms.OpenFileDialog();
            this.op = new System.Windows.Forms.OpenFileDialog();
            this.rowCheck = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.曲线ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.autoDetectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(923, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.openFileStoreData,
            this.重新载入图片ToolStripMenuItem,
            this.toolStripSeparator2,
            this.最近的文件ToolStripMenuItem,
            this.最近历史数据ToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // OpenFile
            // 
            this.OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("OpenFile.Image")));
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(160, 22);
            this.OpenFile.Text = "打开图片(&O)";
            this.OpenFile.Click += new System.EventHandler(this.OpenFileClick);
            // 
            // openFileStoreData
            // 
            this.openFileStoreData.Name = "openFileStoreData";
            this.openFileStoreData.Size = new System.Drawing.Size(160, 22);
            this.openFileStoreData.Text = "打开历史数据";
            this.openFileStoreData.Click += new System.EventHandler(this.OpenFileStoreDataClick);
            // 
            // 重新载入图片ToolStripMenuItem
            // 
            this.重新载入图片ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("重新载入图片ToolStripMenuItem.Image")));
            this.重新载入图片ToolStripMenuItem.Name = "重新载入图片ToolStripMenuItem";
            this.重新载入图片ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.重新载入图片ToolStripMenuItem.Text = "重新载入图片";
            this.重新载入图片ToolStripMenuItem.Click += new System.EventHandler(this.重新载入图片ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // 最近的文件ToolStripMenuItem
            // 
            this.最近的文件ToolStripMenuItem.Name = "最近的文件ToolStripMenuItem";
            this.最近的文件ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.最近的文件ToolStripMenuItem.Text = "最近打开的图片";
            // 
            // 最近历史数据ToolStripMenuItem
            // 
            this.最近历史数据ToolStripMenuItem.Name = "最近历史数据ToolStripMenuItem";
            this.最近历史数据ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.最近历史数据ToolStripMenuItem.Text = "最近历史数据";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放大ToolStripMenuItem,
            this.缩小ToolStripMenuItem,
            this.toolStripSeparator4,
            this.添加ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.自动定位ToolStripMenuItem,
            this.删除ToolStripMenuItem1,
            this.toolStripSeparator6,
            this.移动ToolStripMenuItem,
            this.调整大小ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("放大ToolStripMenuItem.Image")));
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.放大ToolStripMenuItem_Click);
            // 
            // 缩小ToolStripMenuItem
            // 
            this.缩小ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("缩小ToolStripMenuItem.Image")));
            this.缩小ToolStripMenuItem.Name = "缩小ToolStripMenuItem";
            this.缩小ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.缩小ToolStripMenuItem.Text = "缩小";
            this.缩小ToolStripMenuItem.Click += new System.EventHandler(this.缩小ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(121, 6);
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("添加ToolStripMenuItem.Image")));
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("删除ToolStripMenuItem.Image")));
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除ToolStripMenuItem.Text = "自动添加";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 自动定位ToolStripMenuItem
            // 
            this.自动定位ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("自动定位ToolStripMenuItem.Image")));
            this.自动定位ToolStripMenuItem.Name = "自动定位ToolStripMenuItem";
            this.自动定位ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.自动定位ToolStripMenuItem.Text = "自动定位";
            this.自动定位ToolStripMenuItem.Click += new System.EventHandler(this.自动定位ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem1
            // 
            this.删除ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("删除ToolStripMenuItem1.Image")));
            this.删除ToolStripMenuItem1.Name = "删除ToolStripMenuItem1";
            this.删除ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.删除ToolStripMenuItem1.Text = "删除";
            this.删除ToolStripMenuItem1.Click += new System.EventHandler(this.删除ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(121, 6);
            // 
            // 移动ToolStripMenuItem
            // 
            this.移动ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("移动ToolStripMenuItem.Image")));
            this.移动ToolStripMenuItem.Name = "移动ToolStripMenuItem";
            this.移动ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.移动ToolStripMenuItem.Text = "移动";
            this.移动ToolStripMenuItem.Click += new System.EventHandler(this.移动ToolStripMenuItem_Click);
            // 
            // 调整大小ToolStripMenuItem
            // 
            this.调整大小ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("调整大小ToolStripMenuItem.Image")));
            this.调整大小ToolStripMenuItem.Name = "调整大小ToolStripMenuItem";
            this.调整大小ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.调整大小ToolStripMenuItem.Text = "调整大小";
            this.调整大小ToolStripMenuItem.Click += new System.EventHandler(this.调整大小ToolStripMenuItem_Click);
            // 
            // 曲线ToolStripMenuItem
            // 
            this.曲线ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成曲线ToolStripMenuItem,
            this.曲线拟合ToolStripMenuItem1});
            this.曲线ToolStripMenuItem.Name = "曲线ToolStripMenuItem";
            this.曲线ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.曲线ToolStripMenuItem.Text = "曲线";
            // 
            // 生成曲线ToolStripMenuItem
            // 
            this.生成曲线ToolStripMenuItem.Name = "生成曲线ToolStripMenuItem";
            this.生成曲线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.生成曲线ToolStripMenuItem.Text = "生成曲线";
            this.生成曲线ToolStripMenuItem.Click += new System.EventHandler(this.生成曲线ToolStripMenuItem_Click);
            // 
            // 曲线拟合ToolStripMenuItem1
            // 
            this.曲线拟合ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("曲线拟合ToolStripMenuItem1.Image")));
            this.曲线拟合ToolStripMenuItem1.Name = "曲线拟合ToolStripMenuItem1";
            this.曲线拟合ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.曲线拟合ToolStripMenuItem1.Text = "曲线拟合";
            this.曲线拟合ToolStripMenuItem1.Click += new System.EventHandler(this.曲线拟合ToolStripMenuItem1_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助ToolStripMenuItem.Text = "帮助(&H)";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // autoDetectToolStripMenuItem
            // 
            this.autoDetectToolStripMenuItem.Name = "autoDetectToolStripMenuItem";
            this.autoDetectToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.autoDetectToolStripMenuItem.Text = "一键添加";
            this.autoDetectToolStripMenuItem.Click += new System.EventHandler(this.autoDetectToolStripMenuItem_Click);
            // 
            // initTheCurvefitBGW
            // 
            this.initTheCurvefitBGW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InitTheCurvefitBGWDoWork);
            this.initTheCurvefitBGW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.InitTheCurvefitBGWRunWorkerCompleted);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.ReloadToolStripButton,
            this.toolStripSeparator,
            this.ZoomInToolStripButton,
            this.ZoomOutToolStripButton,
            this.toolStripSeparator5,
            this.addCircleButton,
            this.oneClickAdd,
            this.autoAddCircleTSB,
            this.autoSearchCenterButton,
            this.deleteCircleButton,
            this.toolStripSeparator1,
            this.moveCircleButton,
            this.changeCircleButton,
            this.toolStripSeparator3,
            this.fittoolStripButton,
            this.moveCircleTSB,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(923, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.openToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.openToolStripButton.Text = "打开图片";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // ReloadToolStripButton
            // 
            this.ReloadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ReloadToolStripButton.Image")));
            this.ReloadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReloadToolStripButton.Name = "ReloadToolStripButton";
            this.ReloadToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.ReloadToolStripButton.Text = "重新载入";
            this.ReloadToolStripButton.Click += new System.EventHandler(this.ReloadToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // ZoomInToolStripButton
            // 
            this.ZoomInToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomInToolStripButton.Image")));
            this.ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInToolStripButton.Name = "ZoomInToolStripButton";
            this.ZoomInToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.ZoomInToolStripButton.Text = "放大";
            this.ZoomInToolStripButton.Click += new System.EventHandler(this.ZoomInToolStripButton_Click);
            // 
            // ZoomOutToolStripButton
            // 
            this.ZoomOutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutToolStripButton.Image")));
            this.ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutToolStripButton.Name = "ZoomOutToolStripButton";
            this.ZoomOutToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.ZoomOutToolStripButton.Text = "缩小";
            this.ZoomOutToolStripButton.Click += new System.EventHandler(this.ZoomOutToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // addCircleButton
            // 
            this.addCircleButton.CheckOnClick = true;
            this.addCircleButton.Image = ((System.Drawing.Image)(resources.GetObject("addCircleButton.Image")));
            this.addCircleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addCircleButton.Name = "addCircleButton";
            this.addCircleButton.Size = new System.Drawing.Size(52, 22);
            this.addCircleButton.Text = "添加";
            this.addCircleButton.CheckedChanged += new System.EventHandler(this.AddCircleButtonCheckedChanged);
            this.addCircleButton.Click += new System.EventHandler(this.AddCircleButtonClick);
            // 
            // oneClickAdd
            // 
            this.oneClickAdd.Image = ((System.Drawing.Image)(resources.GetObject("oneClickAdd.Image")));
            this.oneClickAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.oneClickAdd.Name = "oneClickAdd";
            this.oneClickAdd.Size = new System.Drawing.Size(76, 22);
            this.oneClickAdd.Text = "一键添加";
            this.oneClickAdd.Click += new System.EventHandler(this.oneClickAdd_Click);
            // 
            // autoAddCircleTSB
            // 
            this.autoAddCircleTSB.Image = ((System.Drawing.Image)(resources.GetObject("autoAddCircleTSB.Image")));
            this.autoAddCircleTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoAddCircleTSB.Name = "autoAddCircleTSB";
            this.autoAddCircleTSB.Size = new System.Drawing.Size(76, 22);
            this.autoAddCircleTSB.Text = "自动添加";
            this.autoAddCircleTSB.Click += new System.EventHandler(this.AutoAddCircleTSBClick);
            // 
            // autoSearchCenterButton
            // 
            this.autoSearchCenterButton.Image = ((System.Drawing.Image)(resources.GetObject("autoSearchCenterButton.Image")));
            this.autoSearchCenterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoSearchCenterButton.Name = "autoSearchCenterButton";
            this.autoSearchCenterButton.Size = new System.Drawing.Size(76, 22);
            this.autoSearchCenterButton.Text = "自动定位";
            this.autoSearchCenterButton.Click += new System.EventHandler(this.AutoSearchCenterButtonClick);
            // 
            // deleteCircleButton
            // 
            this.deleteCircleButton.CheckOnClick = true;
            this.deleteCircleButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteCircleButton.Image")));
            this.deleteCircleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteCircleButton.Name = "deleteCircleButton";
            this.deleteCircleButton.Size = new System.Drawing.Size(52, 22);
            this.deleteCircleButton.Text = "删除";
            this.deleteCircleButton.Click += new System.EventHandler(this.DeleteCircleButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // moveCircleButton
            // 
            this.moveCircleButton.CheckOnClick = true;
            this.moveCircleButton.Image = ((System.Drawing.Image)(resources.GetObject("moveCircleButton.Image")));
            this.moveCircleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveCircleButton.Name = "moveCircleButton";
            this.moveCircleButton.Size = new System.Drawing.Size(52, 22);
            this.moveCircleButton.Text = "移动";
            this.moveCircleButton.Click += new System.EventHandler(this.MoveCircleButtonClick);
            // 
            // changeCircleButton
            // 
            this.changeCircleButton.CheckOnClick = true;
            this.changeCircleButton.Image = ((System.Drawing.Image)(resources.GetObject("changeCircleButton.Image")));
            this.changeCircleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.changeCircleButton.Name = "changeCircleButton";
            this.changeCircleButton.Size = new System.Drawing.Size(76, 22);
            this.changeCircleButton.Text = "调整大小";
            this.changeCircleButton.CheckedChanged += new System.EventHandler(this.ChangeCircleButtonCheckedChanged);
            this.changeCircleButton.Click += new System.EventHandler(this.ChangeCircleButtonClick);
            // 
            // fittoolStripButton
            // 
            this.fittoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fittoolStripButton.Image")));
            this.fittoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fittoolStripButton.Name = "fittoolStripButton";
            this.fittoolStripButton.Size = new System.Drawing.Size(76, 22);
            this.fittoolStripButton.Text = "曲线拟合";
            this.fittoolStripButton.Click += new System.EventHandler(this.FittoolStripButtonClick);
            // 
            // moveCircleTSB
            // 
            this.moveCircleTSB.CheckOnClick = true;
            this.moveCircleTSB.Enabled = false;
            this.moveCircleTSB.Image = ((System.Drawing.Image)(resources.GetObject("moveCircleTSB.Image")));
            this.moveCircleTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveCircleTSB.Name = "moveCircleTSB";
            this.moveCircleTSB.Size = new System.Drawing.Size(76, 22);
            this.moveCircleTSB.Text = "全部移动";
            this.moveCircleTSB.Visible = false;
            this.moveCircleTSB.Click += new System.EventHandler(this.MoveCircleTSBClick);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(88, 22);
            this.helpToolStripButton.Text = "关于与注意";
            this.helpToolStripButton.ToolTipText = "关于&注意";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // ZoomSize
            // 
            this.ZoomSize.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.ZoomSize.Name = "ZoomSize";
            this.ZoomSize.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomSize});
            this.statusStrip1.Location = new System.Drawing.Point(0, 622);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(923, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // op
            // 
            this.op.FileName = "openFileDialog1";
            // 
            // rowCheck
            // 
            this.rowCheck.AutoSize = true;
            this.rowCheck.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rowCheck.Location = new System.Drawing.Point(377, 6);
            this.rowCheck.Name = "rowCheck";
            this.rowCheck.Size = new System.Drawing.Size(82, 18);
            this.rowCheck.TabIndex = 7;
            this.rowCheck.Text = "三行检测";
            this.rowCheck.UseVisualStyleBackColor = true;
            this.rowCheck.CheckedChanged += new System.EventHandler(this.rowCheck_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 644);
            this.Controls.Add(this.rowCheck);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "蛋白质浓度测定";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem openFileStoreData;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton autoSearchCenterButton;
        private System.Windows.Forms.ToolStripButton changeCircleButton;
        private System.Windows.Forms.ToolStripButton moveCircleButton;
        private System.Windows.Forms.ToolStripButton deleteCircleButton;
        private System.Windows.Forms.ToolStripButton addCircleButton;
        private System.Windows.Forms.ToolStripButton autoAddCircleTSB;
        private System.ComponentModel.BackgroundWorker initTheCurvefitBGW;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton fittoolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton moveCircleTSB;
        private System.Windows.Forms.ToolStripButton ZoomOutToolStripButton;
        private System.Windows.Forms.ToolStripButton ZoomInToolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton ReloadToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel ZoomSize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileStoreDataOFD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 重新载入图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最近的文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最近历史数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动定位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 移动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调整大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 曲线拟合ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem autoDetectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog op;
        private System.Windows.Forms.ToolStripButton oneClickAdd;
        private System.Windows.Forms.CheckBox rowCheck;
    }
}

