using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
//using AForge.Imaging.Filters;
using curveFit;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZedGraph;

namespace proteinDetect
{
    public partial class MainForm : Form
    {
        private mostRecentFile recentFile;
        private mostRecentFile recentData;
        private Bitmap srcBitmap = null;
        private bool isHasCurve = false;
        public Dictionary<double, ConcentrationInfo> mConcentrationMaps = new Dictionary<double, ConcentrationInfo>();

        //拟合的曲线存储在此，方便后边的调用确保在各个界面都能访问到。
        public Dictionary<string, L4PCurve> mL4P = new Dictionary<string, L4PCurve>();
        public Dictionary<string, L3Pcurve> mL3P = new Dictionary<string, L3Pcurve>();
        public Dictionary<string, LE4PCurve> mLE4P = new Dictionary<string, LE4PCurve>();

        //区分不同曲线的标志
        public bool hasL4Pcurve = false;
        public bool hasL3Pcurve = false;
        public bool hasLE4Pcurve = false;
        public bool has3Rows = false;

        //一张图片只能有一个浓度值
        public Dictionary<string, double> pathConList = new Dictionary<string, double>();
        public string picturePath = null;
        //区分芯片的行数
        public int chipRows = 4;

        public void setTheCurveFlag(int i)
        {
            switch (i)
            {
                case 4:
                    this.hasL4Pcurve = true;
                    this.hasL3Pcurve = false;
                    this.hasLE4Pcurve = false;
                    break;
                case 3:
                    this.hasL3Pcurve = true;
                    this.hasL4Pcurve = false;
                    this.hasLE4Pcurve = false;
                    break;
                case 5:
                    this.hasL3Pcurve = false;
                    this.hasL4Pcurve = false;
                    this.hasLE4Pcurve = true;
                    break;
            }
        }


        //优先生成此类，因为加载assembly速度太慢
        //需要在backgroundwork中进行初始化。
        public curveFit.Curve mFit = null;


        public bool IsHasCurve
        {
            get
            {
                return this.isHasCurve;
            }
            set
            {
                this.isHasCurve = value;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            //打开后台线程，初始化fit类
            initTheCurvefitBGW.RunWorkerAsync(1);
        }


        void OpenFileClick(object sender, EventArgs e)
        {
            //OpenFileDialog op = new OpenFileDialog();
            //string filter = "bmp(*.bmp)|*.bmp|tif(*.tif)|*.tif|AllFile(*.*)|*.*";
            op.RestoreDirectory = true;
            string filter = "tif文件(*.tif)|*.tif|bmp文件(*.bmp)|*.bmp";
            op.Filter = filter;
            //op.FilterIndex = 2;
            op.InitialDirectory = @"E:\iamge";
            op.Multiselect = false;
            op.Title = "打开图片文件";
            //检测ShowDialog的返回值
            if (op.ShowDialog() == DialogResult.OK)
            {
                //添加到"最近打开的图片"中
                recentFile.AddRecentFile(op.FileName);

                OpenPicture(op.FileName);
            }
            else
                return;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(ChildForm))
                {
                    if (DialogResult.OK == MessageBox.Show("要关闭当前图片，并打开新的图片吗？", "打开图片", MessageBoxButtons.OKCancel))
                    {
                        f.Close();
                        //ChildForm.flag = false;这已经在dispose中处理了
                        OpenFileClick(sender, e);
                        return;
                    }
                    else
                        return;
                }
            }

            OpenFileClick(sender, e);
        }

        //show a abort dialog;
        private void AboutBox()
        {
            (new AboutBox()).ShowDialog();
        }
        //about dialog
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            AboutBox();
        }

        private void ZoomOutToolStripButton_Click(object sender, EventArgs e)
        {
            ChildForm childform = (ChildForm)this.ActiveMdiChild;
            if (childform != null)
            {
                childform.ZoomOut();
            }
        }

        private void ZoomInToolStripButton_Click(object sender, EventArgs e)
        {
            ChildForm childform = (ChildForm)this.ActiveMdiChild;
            if (childform != null)
            {
                childform.ZoomIn();
            }
        }

        public void ZoomChanged(object sender, EventArgs e)
        {
            ChildForm cfTemp = (ChildForm)sender;
            ZoomSize.Text = ((int)(cfTemp.Zoom * 100)).ToString() + '%';
        }

        /*        private void UndoToolStripButton_Click(object sender, EventArgs e)
        {
            ChildForm undoTemp = (ChildForm)this.ActiveMdiChild;
            if (undoTemp == null || undoTemp.M_Undoflag || (undoTemp.M_oldBitmap == null))//已经使用了撤销操作
            {
                return;
            }
            else
            {
                undoTemp.M_PictureBox = undoTemp.M_oldBitmap;
            }
        }
         */
        private void ReloadToolStripButton_Click(object sender, EventArgs e)
        {
            ChildForm undoTemp = (ChildForm)this.ActiveMdiChild;
            if (undoTemp == null)
            {
                return;
            }
            else
            {
                undoTemp.M_pictureBox = undoTemp.M_SrcBitmap;
                undoTemp.clearTheList();
            }
        }


        //这个button不去做具体的操作，只是改变了操作的状态，通过在buttondown中判断操作的状态来进行具体操作。
        //具体操作在childPbMouseDown中进行
        void AddCircleButtonClick(object sender, EventArgs e)
        {
            if (pathConList.ContainsKey(picturePath))
            {
                if (MessageBox.Show("该图已经分析过了哦！是否想再分析一下？", "分析过了", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    addCircleButton.Checked = false;
                    return;
                }
            }
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                if (addCircleButton.Checked == true)
                {
                    child.M_operation = operation.ADDCIRCLE;
                    deleteCircleButton.Checked = false;
                    moveCircleButton.Checked = false;
                    changeCircleButton.Checked = false;
                }
                else
                {
                    child.M_operation = operation.NO_USE;
                }
            }
            else
                addCircleButton.Checked = false;
        }




        void DeleteCircleButtonClick(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                if (deleteCircleButton.Checked == true)
                {
                    child.M_operation = operation.DELETECIRCLE;
                    addCircleButton.Checked = false;
                    moveCircleButton.Checked = false;
                    changeCircleButton.Checked = false;
                }
                else
                {
                    child.M_operation = operation.NO_USE;
                }
            }
            else
                deleteCircleButton.Checked = false;
        }


        void AddCircleButtonCheckedChanged(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                if (addCircleButton.Checked == true)
                {
                    child.Cursor = Cursors.Cross;
                }
                else
                    child.Cursor = Cursors.Default;
            }
        }

        void MoveCircleButtonClick(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                if (moveCircleButton.Checked == true)
                {
                    child.M_operation = operation.MOVECIRCLE;
                    deleteCircleButton.Checked = false;
                    addCircleButton.Checked = false;
                    changeCircleButton.Checked = false;
                }
                else
                {
                    child.M_operation = operation.NO_USE;
                }
            }
            else
                moveCircleButton.Checked = false;
        }



        void ChangeCircleButtonClick(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                if (changeCircleButton.Checked == true)
                {
                    //foreach (MyCircle c in child.m_circle)
                    //{
                    //    c.M_checkFlag = true;
                    //}
                    //child.reDrawCircle();

                    child.M_operation = operation.CHANGECIRCLE;
                    deleteCircleButton.Checked = false;
                    addCircleButton.Checked = false;
                    moveCircleButton.Checked = false;
                }
                else
                {
                    child.M_operation = operation.NO_USE;
                    child.M_changeCircle = false;
                    child.changeCircleModeToFalse();
                    child.autoSearchCenter();
                    //增加改变其他标志位的操作
                }
            }
            else
                changeCircleButton.Checked = false;
        }


        void AutoSearchCenterButtonClick(object sender, EventArgs e)
        {
            ChildForm cf = (ChildForm)this.ActiveMdiChild;
            if (cf != null)
            {
                cf.autoSearchCenter();

                //移动默认按下
                //moveCircleButton.Checked = true;
                //cf.M_operation = operation.MOVECIRCLE;
                //deleteCircleButton.Checked = false;
                //addCircleButton.Checked = false;
                //changeCircleButton.Checked = false;
            }
        }

        void ChangeCircleButtonCheckedChanged(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if ((child != null) && (changeCircleButton.Checked == false))
            {
                child.changeCircleModeToFalse();
            }
        }

        void AutoAddCircleTSBClick(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                child.AutoAddCircle();
            }
        }

        void MoveCircleTSBClick(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child != null)
            {
                if (moveCircleTSB.Checked == true)
                {
                    child.M_operation = operation.MOVEALLCIRCLE;
                    deleteCircleButton.Checked = false;
                    addCircleButton.Checked = false;
                    changeCircleButton.Checked = false;
                }
                else
                {
                    child.M_operation = operation.NO_USE;
                }
            }
            else
                moveCircleTSB.Checked = false;
        }

        void InitTheCurvefitBGWDoWork(object sender, DoWorkEventArgs e)
        {
            mFit = new curveFit.Curve();
        }

        void InitTheCurvefitBGWRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //        	MessageBox.Show("初始化完成！");
        }

        void FittoolStripButtonClick(object sender, EventArgs e)
        {
            collectData cd = new collectData();
            cd.Show();
        }

        void OpenFileStoreDataClick(object sender, EventArgs e)
        {
            openFileStoreDataOFD.Filter = "拟合文件(*.SIMIT)|*.SIMIT|拟合文件(*.SIDAT)|*.SIDAT";
            openFileStoreDataOFD.RestoreDirectory = true;
            //storeData sd = new storeData();
            if (openFileStoreDataOFD.ShowDialog() == DialogResult.OK)
            {
                OpenData(openFileStoreDataOFD.FileName);
                //添加到注册表，添加到”最近打开的数据“
                recentData.AddRecentFile(openFileStoreDataOFD.FileName);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //窗体加载前把最近文件添加进去
            recentFile = new mostRecentFile(
				//the menu item that will contain the recent files
                最近的文件ToolStripMenuItem, 

				//the name of your program
				"recentPicture",
				
				//the funtion that will be called when a recent file gets clicked.
                RecentPictureGotClicked, 
				
				//an optional function to call when the user clears the list of recent items
				null);
            recentData = new mostRecentFile(最近历史数据ToolStripMenuItem, "recentData",
                                            RecentDataGotClicked, null);
        }

        private void RecentPictureGotClicked(object obj, EventArgs evt)
        {
            string fName = (obj as ToolStripItem).Text;
            if (!File.Exists(fName))
            {
                if (MessageBox.Show(string.Format("{0}不存在，是否从“最近打开的图片”中移除？", fName), "文件未找到", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    recentFile.RemoveRecentFile(fName);
                }
                return;
            }
            else
            {
                OpenPicture(fName);
            }
        }

        private void RecentDataGotClicked(object obj, EventArgs evt)
        {
            string fName = (obj as ToolStripItem).Text;
            if (!File.Exists(fName))
            {
                if (MessageBox.Show(string.Format("{0}不存在，是否从“最近打开的图片”中移除？", fName), "文件未找到", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    recentData.RemoveRecentFile(fName);
                }
                return;
            }
            OpenData(fName);
        }

        private void OpenPicture(string filename)
        {
            //将所有的checkbutton无效
            addCircleButton.Checked = false;
            changeCircleButton.Checked = false;
            deleteCircleButton.Checked = false;
            moveCircleButton.Checked = false;
            picturePath = filename;

            if (ChildForm.flag == false)
            {
                srcBitmap = new Bitmap(filename);

                //生成一个子窗口
                ChildForm cf = new ChildForm(srcBitmap);
                cf.MdiParent = this;
                cf.Text = filename;
                cf.zoomChanged += new EventHandler(this.ZoomChanged);
                cf.WindowState = FormWindowState.Maximized;
                cf.Show();
                cf.updateZoomSize();
            }
            else
            {
                MessageBox.Show("请关闭完成操作的图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OpenData(string filename)
        {
            if (filename != "")
            {
                string filepath = filename;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);

                //storeData sd = bf.Deserialize(fs) as storeData;
                //storeData sd = (storeData)bf.Deserialize(fs);

                Object ob = bf.Deserialize(fs);
                storeData sd = (storeData)ob;

                //storeData sd = new storeData();
                //BinaryFormatter b = new BinaryFormatter();
                ////b.Binder = new UBinder();
                //sd = b.Deserialize(fs) as storeData;

                fs.Close();

                //if (sd is storeData)
                //{
                //    MessageBox.Show("ob is storeData");
                //}
                //        			if(sd == null)
                //        			{
                //        				MessageBox.Show("sd is null");
                //        			}
                //        			
                this.hasL4Pcurve = sd.hasL4Pcurve;
                this.hasLE4Pcurve = sd.hasLE4Pcurve;
                this.hasL3Pcurve = sd.hasL3Pcurve;

                this.mConcentrationMaps = sd.mConcentrationMaps;

                (new showTheCurve(sd, this)).Show();
            }
        }

        //菜单栏的一些按键
        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOutToolStripButton_Click(sender, e);
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOutToolStripButton_Click(sender, e);
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCircleButtonClick(sender, e);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoAddCircleTSBClick(sender, e);
        }

        private void 自动定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoSearchCenterButtonClick(sender, e);
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteCircleButtonClick(sender, e);
        }

        private void 移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCircleButtonClick(sender, e);
        }

        private void 调整大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCircleButtonClick(sender, e);
        }

        private void 曲线拟合ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FittoolStripButtonClick(sender, e);
        }

        private void 重新载入图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadToolStripButton_Click(sender, e);
        }

        private void 生成曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mConcentrationMaps.Count < 4)
            {
                MessageBox.Show("保存的浓度只有" + mConcentrationMaps.Count + "个，至少需要4个！");
                return;
            }
            showTheCurve stc = new showTheCurve(this);
            stc.Show();
        }

        private void autoDetectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm cf = (ChildForm)this.ActiveMdiChild;
            if (cf != null)
            {
                cf.AutoAnalyse();
                //调整大小默认按下，并全部选中
                foreach (MyCircle c in cf.m_circle)
                {
                    c.M_checkFlag = true;
                }
                cf.m_changeCircle = true;
                cf.reDrawCircle();

                moveCircleButton.Checked = false;
                cf.M_operation = operation.CHANGECIRCLE;
                deleteCircleButton.Checked = false;
                addCircleButton.Checked = false;
                changeCircleButton.Checked = true;
            }
        }

        private void oneClickAdd_Click(object sender, EventArgs e)
        {
            autoDetectToolStripMenuItem_Click(sender, e);
        }

        private void rowCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (rowCheck.Checked)
            {
                chipRows = 3;
            }
            else
            {
                chipRows = 4;
            }
        }


    }//class
}//namespace


