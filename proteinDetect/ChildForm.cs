using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
//using AForge.Math;
using System.Runtime.InteropServices;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;

namespace proteinDetect
{
    public enum operation
    {
        NO_USE = 0,
        ADDCIRCLE,
        DELETECIRCLE,
        MOVECIRCLE,
        CHANGECIRCLE,
        MOVEALLCIRCLE,
    };
    public partial class ChildForm : Form
    {
        //定义标志，来确定是否已经生成了子窗口
        public static bool flag = false;

        private Bitmap m_srcBitmap = null;
        private Bitmap m_oldBitmap = null;
        private Bitmap m_newBitmap = null;
        private Bitmap m_grayBitmap = null;

        private float zoom = 0.8f;
        private int m_Width;
        private int m_Height;
        private Size m_CurrentSize;
        private static int disX = 155;
        MainForm mMainForm = null;

        //保存图片中圆圈的位置
        public List<MyCircle> m_circle = new List<MyCircle>(30);
        public int proteinRows = 4;

        //保存为当前要操作的圆圈
        private Rectangle m_oldRectangle = new Rectangle(0, 0, 10, 10);
        private MyCircle m_oldCircle = new MyCircle(new Rectangle(0, 0, 10, 10));

        //定义鼠标的状态，检测其是否在按下状态
        public bool m_mouseDown = false;
        //定义zoom状态改变的事件
        public event EventHandler zoomChanged;
        //定义是否使用了撤销操作
        private bool m_undoFlag = true;
        //用来作为改变大小的标志
        public bool m_changeCircle = false;

        private static int HEIGHT = 40;
        private const int CHANGE = 1;
        private operation m_operation = operation.NO_USE;
        private Point m_oldPoint;

        //保存分析结果
        private List<CircleAnalyse> m_analyseCircle = new List<CircleAnalyse>(30);
        //保存自动检测第一个圆圈结果
        public CircleF firstCilrcle = new CircleF();
        //区别自动和手动,刚打开图片的时候就后台运行，节省时间
        public List<MyCircle> autoCircle = new List<MyCircle>(30);
        private bool endFlag = false;
        private SetControlRectangle rectory;

        public bool M_changeCircle
        {
            set
            {
                m_changeCircle = value;
            }
        }

        public operation M_operation
        {
            get
            {
                return m_operation;
            }

            set
            {
                m_operation = value;
            }
        }


        public bool M_Undoflag
        {
            get
            {
                return m_undoFlag;
            }
        }
        public Bitmap M_oldBitmap
        {
            get
            {
                return m_oldBitmap;
            }
        }

        public Bitmap M_SrcBitmap
        {
            get
            {
                return m_srcBitmap;
            }
            set
            {
                m_srcBitmap = value;
            }
        }

        public int M_Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                m_Width = value;
            }
        }
        public int M_Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                m_Height = value;
            }
        }
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
            }
        }
        public Size CurrentSize
        {
            set
            {
                m_CurrentSize = value;
            }
            get
            {
                return m_CurrentSize;
            }
            }

        //设置属性，用来设定子框体PictureBox的显示
        public Bitmap M_pictureBox
        {
            set { this.childPb.Image = value; }
        }

        public void clearTheList()
        {
            m_circle.Clear();
            HEIGHT = 40;
        } 

        public ChildForm()
        {
            InitializeComponent();
        }

        public ChildForm(Bitmap bitmap)
        {
            if (flag == false)
            {
                InitializeComponent();
                flag = true;
                m_srcBitmap = bitmap;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.GetType() == typeof(MainForm))
                    {
                        mMainForm = (MainForm)f;
                        break;
                    }
                }
                proteinRows = mMainForm.chipRows;
                pbInit();
                if (m_srcBitmap.PixelFormat != PixelFormat.Format8bppIndexed)
                {
                    m_grayBitmap = RGBToGray(m_srcBitmap);
                }
                else
                {
                    m_grayBitmap = (Bitmap) m_srcBitmap.Clone();
                }

                endFlag = false;
                Thread autoDetectThread = new Thread(new ThreadStart(autoDetect));
                autoDetectThread.Name = "Detect first circle";
                autoDetectThread.Priority = ThreadPriority.Normal;
                autoDetectThread.Start();
            }
            else
            {
                return;
            }
            this.pbPanel.MouseWheel += new MouseEventHandler(this.pbPanel_MouseWheel);

        }

        private Bitmap RGBToGray(Bitmap srcBitmap)
        {
            return Grayscale.CommonAlgorithms.Y.Apply(srcBitmap);
        }

        /// <summary>
        /// 初始化pictureBox的一些状态
        /// 设置图像的缩放等信息
        /// </summary>
        private void pbInit()
        {
            if (M_SrcBitmap == null)
            {
                return;
            }
            else
            {
                M_Width = M_SrcBitmap.Width;
                M_Height = M_SrcBitmap.Height;
                if (M_Height < 1500)
                {
                    HEIGHT = 40;
                    CurrentSize = new Size(M_Width, M_Height);
                    zoom = 1.0f;
                }
                else
                {
                    CurrentSize = new Size((int)(M_Width * Zoom), (int)(M_Height * Zoom));
                }
            }
            Zoom = (float) CurrentSize.Height/M_Height;
            childPb.Size = CurrentSize;
            childPb.Image = m_srcBitmap;
        }
        
        private void pbPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            pbPanel.VerticalScroll.Value += 10;
            pbPanel.Refresh();
            pbPanel.Invalidate();
            pbPanel.Update();
        }

        /// <summary>
        /// 更新图片的zoom状态
        /// </summary>
        public void updateZoomSize()
        {
            if (zoomChanged != null)
            {
                zoomChanged(this, null);
            }
            //计算pictureBox的位置，使其一直处于居中状态
            int x = (pbPanel.Width - childPb.Width)/2;
            x = x > 0 ? x : 0;
            int y = (pbPanel.Height - childPb.Height)/2;
            y = y > 0 ? y : 0;
            childPb.Location = new Point(x, y);
            //事实证明，当pannel的dock处于fill的状态时，他的size是不会自动改变的。
            //childPb.Location = new Point(((pbPanel.Width - childPb.Width)/2), ((pbPanel.Height - childPb.Height)/2));
        }

        /// <summary>
        /// 放大
        /// </summary>
        public void ZoomIn()
        {
            //表明是在改变圆的大小的状态下按下的放大键
            if (m_changeCircle == true && m_operation == operation.CHANGECIRCLE)
            {
                changeCircle(CHANGE);
                reDrawCircle();
            }
            else
            {
                float z = Zoom * 1.2f;
                if (z < 10.0f)
                {
                    Zoom = z;
                    CurrentSize = new Size((int)(M_Width * Zoom), (int)(M_Height * Zoom));
                    //pbPanel.Size = CurrentSize;
                    childPb.Size = CurrentSize;

                    updateZoomSize();
                }
                else
                    return;
            }
        }
        /// <summary>
        /// 缩小
        /// </summary>
        public void ZoomOut()
        {
            if (m_changeCircle == true && m_operation == operation.CHANGECIRCLE)
            {
                changeCircle(-CHANGE);
                reDrawCircle();
            }
            else
            {
                float z = Zoom / 1.2f;
                if (z > 0.1f)
                {
                    Zoom = z;
                    CurrentSize = new Size((int)(M_Width * Zoom), (int)(M_Height * Zoom));
                    this.childPb.Size = CurrentSize;
                    updateZoomSize();
                }
                else
                    return;
            }
        }

        private void changeCircle(int n)
        {
            foreach (MyCircle circle in m_circle)
            {
                if (circle.M_checkFlag == true)
                {
                    m_oldRectangle.X = circle.M_rectangle.X - n;
                    m_oldRectangle.Y = circle.M_rectangle.Y - n;
                    m_oldRectangle.Width = circle.M_rectangle.Width + 2*n;
                    m_oldRectangle.Height = circle.M_rectangle.Height + 2*n;
                    circle.M_rectangle = m_oldRectangle;
                }
                //else
                //{
                //    circle.M_checkFlag = true;
                //}
            }
            //修改完之后的重绘操作，不要放在这个子函数里比较好。放到上层调用的函数里。
        }

        /// <summary>
        /// 重绘所有的圆
        /// </summary>
        public void reDrawCircle()
        {
            try
            {
                calcTheHEIGHT();
                m_circle.Sort(CompareRectangleByX);
                
                /*
					  * change date 2016.2.28
					  * can use the gray src
					  * comment the four lines
					  * */
                if (m_srcBitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    m_newBitmap = AForge.Imaging.Image.Clone(m_srcBitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                }
                else
                {
                    m_newBitmap = (Bitmap)(m_srcBitmap.Clone());
                }

                Graphics gp = Graphics.FromImage(m_newBitmap);
                Pen p = new Pen(Color.Red);
                Pen p2 = new Pen(Color.Blue);
                Font f = new Font("Arial", 12);
                SolidBrush brush = new SolidBrush(Color.Red);
                SolidBrush brush2 = new SolidBrush(Color.Blue);

                foreach (MyCircle c in m_circle)
                {
                    if (c.M_checkFlag == false)
                    {
                        gp.DrawEllipse(p, c.M_rectangle);
                        gp.DrawString((m_circle.IndexOf(c) + 1).ToString(), f, brush, (float)c.M_rectangle.X - 20, (float)c.M_rectangle.Y - 15);
                    }
                    else
                    {
                        gp.DrawEllipse(p2, c.M_rectangle);
                        gp.DrawString((m_circle.IndexOf(c) + 1).ToString(), f, brush2, (float)c.M_rectangle.X - 20, (float)c.M_rectangle.Y - 15);
                    }
                }

                gp.Dispose();
                if (m_oldBitmap != null)
                {
                    m_oldBitmap.Dispose();
                    GC.Collect();
                }
                m_oldBitmap = m_newBitmap;
                childPb.Image = m_newBitmap;

            }
            catch (Exception)
            {
                MessageBox.Show("请选择未经压缩过的图像进行操作。");
                throw;
            }
        }

        /// <summary>
        /// 重新计算用来排序的HEIGHT
        /// </summary>
        private void calcTheHEIGHT()
        {
            int i = 0;
            int sumHeight = 0;
            foreach (MyCircle c in m_circle)
            {
                i++;
                sumHeight += c.M_rectangle.Height;
            }
            HEIGHT = (sumHeight > 0) ? (sumHeight / i) : HEIGHT;
        }

        //设置矩形框的位置比较，进行排序
        private static int CompareRectangleByX(MyCircle c1, MyCircle c2)
        {
            if (c1.M_rectangle.Y >= (c2.M_rectangle.Y + HEIGHT))
                return 1;
            else if (c1.M_rectangle.Y >= (c2.M_rectangle.Y - HEIGHT) && c1.M_rectangle.Y < (c2.M_rectangle.Y + HEIGHT))
            {
                if (c1.M_rectangle.X > c2.M_rectangle.X)
                    return 1;
                else if (c1.M_rectangle.X < c2.M_rectangle.X)
                    return -1;
                else
                    return 0;
            }
            else
                return -1;
        }

        public void AutoAddCircle()
        {
            if (m_circle.Count == 0)
            {
                MessageBox.Show("请添加第一个目标位置。");
                return;
            }
            else if (m_circle.Count > 12)
            {
                return;
            }

            //针对40倍的图像进行自动生成。
            if (m_Height > 1000)
            {
                int width = m_circle[0].M_rectangle.Width;
                int height = m_circle[0].M_rectangle.Height;
                int x = m_circle[0].M_rectangle.X;
                int y = m_circle[0].M_rectangle.Y;

                for (int i = 0; i < proteinRows; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        int newx = x + j * disX;
                        int newy = y + i * disX;

                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        m_circle.Add(new MyCircle(new Rectangle(newx, newy, width, height)));
                    }
                }
            }
            //不加auto search的原因是若是空白图像区域的话，加了反而会影响位置信息。
            //			for(int i = 0; i < 2; i++)
            //			{
            //				autoSearchCenter();
            //			}
            reDrawCircle();
        }

        /// <summary>
        /// 添加圆圈的位置，根据传递进来的参数来设置
        /// </summary>
        /// <param name="Point">
        /// point指出鼠标点击的位置
        /// </param>
        private void addCircle(Point p)
        {
            foreach (MyCircle c in m_circle)
            {
                if (isCanBeDraw(p, c))
                {
                    //说明已经存在一个圆圈，提示操作失败，不做添加操作
                    //MessageBox.Show("此位置已经有圆圈了不需要添加！");
                    return;
                }
            }

            //若是程序执行到这个地方，表示此点不存在圆圈，进行添加操作
            foreach (MyCircle c in m_circle)
            {
                if (ishasRow(p, c))
                {
                    m_circle.Add(new MyCircle(new Rectangle(p.X - c.M_rectangle.Width / 2, p.Y - c.M_rectangle.Width / 2, c.M_rectangle.Width, c.M_rectangle.Width)));
                    return;
                }
            }

            //若是当前行不存在，按list中的第一个的大小进行添加圆
            if (m_circle.Count == 0)
            {
                m_circle.Add(new MyCircle(new Rectangle(p.X - HEIGHT / 2, p.Y - HEIGHT / 2, HEIGHT, HEIGHT)));
            }
            else
            {
                //在鼠标点击的位置进行添加。
                m_circle.Add(new MyCircle(new Rectangle(p.X - m_circle[0].M_rectangle.Width / 2, p.Y - m_circle[0].M_rectangle.Width / 2, m_circle[0].M_rectangle.Width, m_circle[0].M_rectangle.Width)));
            }
        }

        /// <summary>
        /// 判断鼠标点击的当前行有没有数据
        /// </summary>
        /// <param name="p"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool ishasRow(Point p, MyCircle c)
        {
            if ((p.Y >= c.M_rectangle.Y - HEIGHT) && (p.Y <= c.M_rectangle.Y + c.M_rectangle.Height + HEIGHT))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断点是否在一个圆圈内
        /// </summary>
        /// <param name="p"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool isCanBeDraw(Point p, MyCircle c)
        {
            int x = p.X;
            int y = p.Y;
            int radius = (c.M_rectangle.Height / 2);
            int centerX = c.M_rectangle.X + c.M_rectangle.Width / 2;
            int centerY = c.M_rectangle.Y + c.M_rectangle.Height / 2;

            if ((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) - 4 * (radius * radius) <= 1)
            {
                return true;
            }
            else
                return false;
        }



        //只判断点是否存在于一个圆内
        public bool isIntheCircle(Point p, MyCircle c)
        {
            int x = p.X;
            int y = p.Y;
            int radius = (c.M_rectangle.Height / 2);
            int centerX = c.M_rectangle.X + c.M_rectangle.Width / 2;
            int centerY = c.M_rectangle.Y + c.M_rectangle.Height / 2;

            if ((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) - (radius * radius) <= 1)
            {
                return true;
            }
            else
                return false;
        }

        //进行更改，只通过左上角第一个点的位置，进而确定其他点的位置。
        //可以让用户先在图像上设定一个点，然后点击自动添加，就可以准确的添加其他的点了。
        private void 目标定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_srcBitmap != null)
            {
                if (m_circle != null)
                {
                    m_circle.Clear();
                }

                FiltersSequence filter = new FiltersSequence();
                m_oldBitmap = (Bitmap)m_grayBitmap.Clone();
                filter.Add(new OtsuThreshold());
                filter.Add(new Invert());
                m_newBitmap = filter.Apply(m_oldBitmap);
                m_oldBitmap.Dispose();

                BlobCounter bc = new BlobCounter();
                bc.ProcessImage(m_newBitmap);
                Blob[] blobs = bc.GetObjects(m_newBitmap, false);

                foreach (Blob b in blobs)
                {
                    if ((float)b.Rectangle.Height / b.Rectangle.Width > 0.8 && (float)b.Rectangle.Height / b.Rectangle.Width < 1.2 && (b.Rectangle.Height > HEIGHT / 2))
                    {
                        m_circle.Add(new MyCircle(new Rectangle(b.Rectangle.X, b.Rectangle.Y, b.Rectangle.Width, b.Rectangle.Width)));

                    }
                }
                m_circle.Sort(CompareRectangleByX);
                reDrawCircle();
            }
        }

        private void deleteCircle(Point p)
        {
            foreach (MyCircle c in m_circle)
            {
                if (isIntheCircle(p, c))
                {
                    m_circle.Remove(c);
                    return;
                }
            }
        }

        private void moveCircle(Point p)
        {
            m_mouseDown = true;
            m_oldPoint = p;
            foreach (MyCircle c in m_circle)
            {
                if (isIntheCircle(p, c))
                {
                    //仅仅只是改变了圆心的位置而已，也即矩形框的起点坐标。
                    m_oldRectangle.X = p.X - c.M_rectangle.Width / 2;
                    m_oldRectangle.Y = p.Y - c.M_rectangle.Height / 2;
                    m_oldRectangle.Width = c.M_rectangle.Width;
                    m_oldRectangle.Height = c.M_rectangle.Height;
                    c.M_rectangle = m_oldRectangle;
                    return;
                }
            }
        }

        private void changeCircleMode(Point point)
        {
            //设置为允许改变大小的状态
            m_changeCircle = true;

            foreach (MyCircle c in m_circle)
            {
                if (isIntheCircle(point, c))
                {
                    //说明为选择了某个圆
                    c.M_checkFlag = !c.M_checkFlag;
                    return;
                }
            }
        }

        public void changeCircleModeToFalse()
        {
            foreach (MyCircle c in m_circle)
            {
                if (c.M_checkFlag)
                {
                    c.M_checkFlag = false;
                }
                //c.M_checkFlag = !c.M_checkFlag;
            }
            //改变状态之后需要重绘
            reDrawCircle();
        }

        private void ChildPbMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.pbPanel.Focus();
                //由于图片的放大缩小，需要进行目标的坐标校正
                Point point = new Point((int)(e.X / zoom), (int)(e.Y / zoom));

                if (childPb.Image != null)
                {
                    if (m_circle.Count >= 0)
                    {
                        switch (m_operation)
                        {
                            case operation.ADDCIRCLE:
                                {
                                    //确保列表中已经存在一些圆圈之后再进行添加的操作。
                                    addCircle(point);
                                    reDrawCircle();
                                    break;
                                }
                            case operation.DELETECIRCLE:
                                {
                                    deleteCircle(point);
                                    reDrawCircle();
                                    break;
                                }
                            case operation.MOVECIRCLE:
                                {
                                    moveCircle(point);
                                    reDrawCircle();
                                    break;
                                }
                            case operation.CHANGECIRCLE:
                                {
                                    changeCircleMode(point);
                                    reDrawCircle();
                                    break;
                                }
                            case operation.MOVEALLCIRCLE:
                                {
                                    //								moveAllCircle
                                    break;
                                }
                            case operation.NO_USE:
                            {

                                changeCircleModeToFalse();
                                reDrawCircle();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
            }
        }

        private void ChildPbMouseMove(object sender, MouseEventArgs e)
        {
            Point point = new Point((int)(e.X / zoom), (int)(e.Y / zoom));
            if (m_mouseDown == true && m_operation == operation.MOVECIRCLE)
            {
                //foreach (MyCircle circle in m_circle)
                //{
                //    if (circle.M_checkFlag)
                //    {
                //        moveCircle(point);
                //    }
                //}
                moveCircle(point);
                reDrawCircle();
            }
        }

        private void ChildPbMouseUp(object sender, MouseEventArgs e)
        {
            if (m_mouseDown == true)
            {
                m_mouseDown = false;
            }
        }

        private bool isPointInCircle(int x, int y, int width, int height)
        {
            int centerX = (int)(width / 2);
            int centerY = (int)(height / 2);
            int radius = (int)(width / 2);
            if ((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) - (radius * radius) <= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 找到目标的圆圈位置
        /// </summary>
        /// <param name="c">
        /// 要操作的圆圈
        /// </param>
        /// <param name="x">
        /// x方向上偏移的位置
        /// </param>
        /// <param name="y"></param>
        /// <returns>
        /// 返回目标点的矩阵
        /// </returns>
        private unsafe Rectangle searchCenter(MyCircle c, int x, int y)
        {
            //这个初值的设定最好是遍历的第一个值，单有点麻烦，在此先设定为一个预估的值。
            int sumGray = 0;
            int minGray = 256 * 100 * 100;
            int row = c.M_rectangle.Height;
            int column = c.M_rectangle.Width;
            int srcWidth = m_grayBitmap.Width;
            int srcHeight = m_grayBitmap.Height;
            Rectangle goalRec = new Rectangle(0, 0, 0, 0);

            for (int i = -x; i < x; i++)
            {
                int rectangleX = c.M_rectangle.X + i;
                if (!((rectangleX >= 0) && (rectangleX < (srcWidth - column))))
                {
                    continue;
                }

                for (int j = -y; j < y; j++)
                {
                    int rectangleY = c.M_rectangle.Y + j;
                    if (!((rectangleY >= 0) && (rectangleY < (srcHeight - row))))
                    {
                        continue;
                    }

                    Rectangle r = new Rectangle(rectangleX, rectangleY, c.M_rectangle.Width, c.M_rectangle.Height);

                    BitmapData bmpdata = m_grayBitmap.LockBits(r, ImageLockMode.ReadOnly, m_grayBitmap.PixelFormat);

                    byte* ptr = (byte*)bmpdata.Scan0;
                    for (int m = 0; m < row; m++)
                    {
                        for (int n = 0; n < column; n++)
                        {
                            if (isPointInCircle(m, n, column, row))
                            {
                                sumGray += *ptr;
                            }
                            ptr++;
                        }
                        ptr += bmpdata.Stride - column;
                    }

                    if (sumGray < minGray)
                    {
                        minGray = sumGray;
                        goalRec = r;
                    }
                    sumGray = 0;
                    m_grayBitmap.UnlockBits(bmpdata);
                }
            }
            return goalRec;
        }

        public unsafe void autoSearchCenter()
        {
            if (m_circle.Count > 0)
            {
                Rectangle r = new Rectangle();
                foreach (MyCircle c in m_circle)
                {
                    r = searchCenter(c, HEIGHT, HEIGHT);
                    c.M_rectangle = r;
                }
            }

            reDrawCircle();
        }

        /// <summary>
        /// 分析圆内的灰度和光密度等值。 
        /// 
        /// 2014.3.25
        /// 添加上分行的处理能力。
        /// 在circleAnalyse内添加两个变量，标示位置。
        /// 
        /// 由于会出现行与行之间间距变小的情况，在此将其判断不同行的阈值缩小
        /// 2014.9.2
        /// </summary>
        private unsafe void analyseCircle()
        {
            //清空分析结果
            m_analyseCircle.Clear();

            for (int i = 0; i < m_circle.Count; i++)
            {
                m_analyseCircle.Add(new CircleAnalyse(0, 0.0f, 0.0f));
            }

            //2014.3.25
            //定义一个表示每行第一个圆的值，用来确定属于某一行
            MyCircle Ay = m_circle[0];

            //表示行列的值。
            int mRow = 1;
            int mCol = 0;


            //求出阈值，利用所选的圆内的平均灰度值作为阈值
            int row = 0;
            int column = 0;
            foreach (MyCircle c in m_circle)
            {
                BitmapData bmpdata = m_grayBitmap.LockBits(c.M_rectangle, ImageLockMode.ReadOnly, m_grayBitmap.PixelFormat);
                row = c.M_rectangle.Height;
                column = c.M_rectangle.Width;
                int index = m_circle.IndexOf(c);


                // 判断分行，使用的标准是通过每行的第一个圆当作判断标准，使用 其他圆的圆心来查看是否和标准元属于同一行来进行判断。
                if (ishasRow(new Point(c.M_rectangle.X + HEIGHT / 2, c.M_rectangle.Y + HEIGHT / 2), Ay))
                {
                    mCol++;
                }
                else
                {
                    Ay = c;
                    mRow++;
                    mCol = 1;
                }

                float threshold = 0.0f;
                int centerOneMoment = 0;

                //求出园内的平均灰度值，之前一直求的是矩形内的平均值
                int trueCount = 0;
                byte* ptr = (byte*)bmpdata.Scan0;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (isPointInCircle(i, j, column, row))
                        {
                            threshold += *ptr;
                            ptr++;
                            trueCount++;
                        }
                        //修改计算均值、灰度值方式 2016.3.24 LiuSongsheng
                        //Point tempPoint = new Point(c.M_rectangle.X + j, c.M_rectangle.Y + i);
                        //if (isIntheCircle(tempPoint, c))
                        //{
                        //    threshold += *ptr;
                        //    ptr++;
                        //    trueCount++;
                        //}
                        else
                        {
                            ptr++;
                        }
                    }
                    ptr += bmpdata.Stride - column;
                }
                //求出阈值
                threshold /= trueCount;
                //灰度均值
                c.M_meanGray = (int)threshold;


                //计算圆内的一阶中心距

                trueCount = 0;
                ptr = (byte*)bmpdata.Scan0;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (isPointInCircle(i, j, column, row))
                        {
                            centerOneMoment += Math.Abs((*ptr) - c.M_meanGray);
                            ptr++;
                            trueCount++;
                        }
                        //Point tempPoint = new Point(c.M_rectangle.X + j, c.M_rectangle.Y + i);
                        //if (isIntheCircle(tempPoint, c))
                        //{
                        //    centerOneMoment += Math.Abs((*ptr) - c.M_meanGray);
                        //    ptr++;
                        //    trueCount++;
                        //}
                        else
                        {
                            ptr++;
                        }
                    }
                    ptr += bmpdata.Stride - column;
                }
                c.M_centerOneMoment = centerOneMoment / trueCount;



                //进行阈值分割,阈值分割的原理在于利用一阶中心距作为阈值
                ptr = (byte*)bmpdata.Scan0;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        //if((*ptr < threshold) && (isPointInCircle(i, j, column, row)))
                        //以一阶中心距作为判断，貌似数据量还是过大，当图像放大缩小时变化比较明显。
                        //将阈值重新设置，做成向下可以到一阶中心距的位置，向上只能到一半的中心距的位置，可好？
                        //效果还可以，暂时先这样，后期再做其他处理。
                        int meanGraySegment = c.M_centerOneMoment;

                        meanGraySegment = meanGraySegment > 15 ? meanGraySegment / 2 : meanGraySegment;
                        //检测是否为0，是0则赋一个新值给他。为了使面积扩大
                        meanGraySegment = meanGraySegment == 0 ? 3 : meanGraySegment;
                        if ((isPointInCircle(i, j, column, row)) && ((*ptr - c.M_meanGray) <= meanGraySegment) && ((*ptr - c.M_meanGray) >= -c.M_centerOneMoment))
                        {
                            m_analyseCircle[index].M_meanGray += *ptr;
                            m_analyseCircle[index].M_area++;
                            //m_analyseCircle[index].M_meanOpticalDensity += (float)Math.Log10(256) - (float)Math.Log10(*ptr);
                            ptr++;
                            //							trueCount++;
                        }
                        //Point tempPoint = new Point(c.M_rectangle.X + j, c.M_rectangle.Y + i);
                        //if ((isIntheCircle(tempPoint, c)) && ((*ptr - c.M_meanGray) <= meanGraySegment) && ((*ptr - c.M_meanGray) >= -c.M_centerOneMoment))
                        //{
                        //    m_analyseCircle[index].M_meanGray += *ptr;
                        //    m_analyseCircle[index].M_area++;
                        //    //m_analyseCircle[index].M_meanOpticalDensity += (float)Math.Log10(256) - (float)Math.Log10(*ptr);
                        //    ptr++;
                        //    //							trueCount++;
                        //}
                        else
                        {
                            ptr++;
                        }
                    }
                    ptr += bmpdata.Stride - column;
                }
                //不对吧，貌似，有效数据数量需要统计滴。
                //				m_analyseCircle[index].M_meanGray /= trueCount;
                m_analyseCircle[index].M_meanGray /= m_analyseCircle[index].M_area;
                //m_analyseCircle[index].M_meanOpticalDensity /= row * column;

                //所乘的数据1000是为了调整最后的数据显示，因为本来这个值是较小的。
                // 判断是否等于0，等于0不能够进行取对数操作
                if (m_analyseCircle[index].M_meanGray < 0.0000001 && m_analyseCircle[index].M_meanGray > -0.0000001)
                {
                    m_analyseCircle[index].M_meanOpticalDensity = 1000 * (float)Math.Log10(256);
                }
                else
                    m_analyseCircle[index].M_meanOpticalDensity = 1000 * (float)Math.Log10(256) - 1000 * (float)Math.Log10(m_analyseCircle[index].M_meanGray);

                m_analyseCircle[index].M_col = mCol;
                m_analyseCircle[index].M_row = mRow;
                //释放锁定的位图资源
                m_grayBitmap.UnlockBits(bmpdata);
            }

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_circle.Count == 0)
            {
                MessageBox.Show("请先定位圆的位置。");
                return;
            }
            analyseCircle();
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(showResult))
                {
                    ((showResult)f).showTheResult(m_analyseCircle);
                    f.Activate();
                    return;
                }
            }

            showResult showR = new showResult();
            showR.showTheResult(m_analyseCircle);
            showR.Show();
            //(new showResult(m_analyseCircle)).Show();
        }

        private void 分析AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testToolStripMenuItem_Click(sender, e);
        }

        //public void autoDetect()
        //{
        //    if (m_grayBitmap == null)
        //    {
        //        MessageBox.Show("请先选择图片！");
        //        return;
        //    }
        //    Image<Bgr, byte> scr;
        //    Image<Gray, byte> scr_gray;
        //    Bitmap cp_bitmap;
        //    double T = 0;
        //    double T0 = 0;
        //    try
        //    {
        //        scr = new Image<Bgr, byte>(m_grayBitmap);
        //        scr_gray = scr.Convert<Gray, byte>().PyrDown().PyrUp();
        //        cp_bitmap = scr_gray.Bitmap;

        //        if (cp_bitmap.PixelFormat != PixelFormat.Format8bppIndexed)
        //        {
        //            MessageBox.Show("不是灰度图像");
        //            return;
        //        }
        //        Rectangle rect = new Rectangle(0, 0, cp_bitmap.Width, cp_bitmap.Height);
        //        BitmapData bmpdata = cp_bitmap.LockBits(rect, ImageLockMode.ReadWrite, cp_bitmap.PixelFormat);
        //        //图像二值化
        //        unsafe
        //        {
        //            byte* ptr = (byte*)bmpdata.Scan0;
        //            for (int i = 0; i < bmpdata.Height; i++)
        //            {
        //                for (int j = 0; j < bmpdata.Width; j++)
        //                {
        //                    T0 += *ptr;
        //                    ptr++;
        //                }
        //                ptr += bmpdata.Stride - bmpdata.Width;
        //            }
        //            T = T0 / cp_bitmap.Width / cp_bitmap.Height - 70;
        //            ptr = (byte*)bmpdata.Scan0;
        //            for (int i = 0; i < cp_bitmap.Height; i++)
        //            {
        //                for (int j = 0; j < cp_bitmap.Width; j++)
        //                {
        //                    if (*ptr > T)
        //                    {
        //                        *ptr = 0;
        //                    }
        //                    else
        //                    {
        //                        *ptr = 255;
        //                    }
        //                    ptr++;
        //                }
        //                ptr += bmpdata.Stride - bmpdata.Width;
        //            }
        //        }
        //        cp_bitmap.UnlockBits(bmpdata);
        //        Invalidate();

        //        scr_gray = new Image<Gray, byte>(cp_bitmap);
        //        List<CircleF> detectCircle = new List<CircleF>();
        //        Rectangle roi = new Rectangle(0, 0, 350, scr_gray.Height);
        //        scr_gray.ROI = roi;
        //        Gray cannyThreshold = new Gray(23);
        //        Gray circleAccumulatorThreshold = new Gray(40);
        //        CircleF[] circle = scr_gray.HoughCircles(cannyThreshold, circleAccumulatorThreshold,
        //           2.10, 62, 0, 0)[0];
        //        if (circle.Length == 0)
        //        {
        //            MessageBox.Show("检测出错，请手动添加");
        //            return;
        //        }
        //        foreach (CircleF c in circle)
        //        {
        //            if (c.Center.X > 10 && c.Center.X < 350 && c.Radius > 10 && c.Radius < 40 && c.Center.Y > 160 && c.Center.Y < 880)
        //            {
        //                detectCircle.Add(c);
        //            }
        //        }

        //        //圆心X是否相差在15以内，原理是：找出每个点15范围内的点集，然后找出点集数最大的点集
        //        int[] num = new int[detectCircle.Count()];
        //        List<CircleF>[] pointFs = new List<CircleF>[detectCircle.Count()];
        //        for (int i = 0; i < detectCircle.Count(); i++)
        //        {
        //            pointFs[i] = new List<CircleF>();//记得先初始化，不然会报错
        //            float c = detectCircle[i].Center.X;
        //            num[i] = 0;//确保从零计数
        //            for (int j = 0; j < detectCircle.Count(); j++)
        //            {
        //                if (Math.Abs(c - detectCircle[j].Center.X) < 15)
        //                {
        //                    num[i]++;
        //                    pointFs[i].Add(detectCircle[j]);
        //                }
        //            }
        //        }
        //        //num数组中最大的即为最佳的
        //        int blow = 0;
        //        float m = num[0];
        //        for (int i = 0; i < num.Length; i++)
        //        {
        //            if (num[i] > m)
        //            {
        //                blow = i;
        //                m = num[i];
        //            }
        //        }
        //        //剔除圆心Y相差100以内的点
        //        int[] numY = new int[pointFs[blow].Count];
        //        List<CircleF>[] correctCircle = new List<CircleF>[pointFs[blow].Count];
        //        for (int i = 0; i < pointFs[blow].Count; i++)
        //        {
        //            numY[i] = 0;
        //            correctCircle[i] = new List<CircleF>();
        //            for (int j = 0; j < pointFs[blow].Count; j++)
        //            {
        //                if (i != j)
        //                {
        //                    if (Math.Abs(pointFs[blow][i].Center.Y - pointFs[blow][j].Center.Y) > 100)
        //                    {
        //                        numY[i]++;
        //                        correctCircle[i].Add(pointFs[blow][j]);
        //                    }
        //                }
        //                else
        //                {
        //                    numY[i]++;
        //                    correctCircle[i].Add(pointFs[blow][j]);
        //                }
        //            }
        //        }
        //        int blowY = 0;
        //        float mY = num[0];
        //        for (int i = 0; i < numY.Length; i++)
        //        {
        //            if (numY[i] > mY)
        //            {
        //                blowY = i;
        //                mY = num[i];
        //            }
        //        }

        //        firstCilrcle = new CircleF();
        //        correctCircle[blowY].Sort(CompareCircleByY);
        //        PointF p = new PointF();
        //        //画circle出来
        //        //最小圆的半径作为新的点圆半径
        //        firstCilrcle.Radius = correctCircle[blowY][0].Radius;
        //        foreach (CircleF circleF in correctCircle[blowY])
        //        {
        //            //firstCilrcle.Radius += circleF.Radius / correctCircle[blowY].Count;
        //            if (firstCilrcle.Radius > circleF.Radius)
        //            {
        //                firstCilrcle.Radius = circleF.Radius;
        //            }
        //        }
        //        if (correctCircle[blowY].Count > 4)
        //        {
        //            MessageBox.Show("检测有误，请手动检测");
        //            return;
        //        }
        //        if (correctCircle[blowY].Count == 4)
        //        {
        //            p.X = correctCircle[blowY][0].Center.X + 260;
        //            p.Y = correctCircle[blowY][0].Center.Y - 17;
        //            firstCilrcle.Center = p;
        //        }
        //        if (correctCircle[blowY].Count < 4)
        //        {
        //            p.X = correctCircle[blowY][0].Center.X + 260;
        //            if (correctCircle[blowY][0].Center.Y < 333)
        //            {
        //                p.Y = correctCircle[blowY][0].Center.Y - 17;
        //            }
        //            else if (correctCircle[blowY][0].Center.Y >= 333 && correctCircle[blowY][0].Center.Y < 493)
        //            {
        //                p.Y = correctCircle[blowY][0].Center.Y - 17 - 155;
        //            }
        //            else if (correctCircle[blowY][0].Center.Y >= 493 && correctCircle[blowY][0].Center.Y < 653)
        //            {
        //                p.Y = correctCircle[blowY][0].Center.Y - 17 - 155 - 155;
        //            }
        //            else if (correctCircle[blowY][0].Center.Y >= 653)
        //            {
        //                p.Y = correctCircle[blowY][0].Center.Y - 17 - 155 - 155 - 155;
        //            }
        //            firstCilrcle.Center = p;
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("检测出错，请手动添加圆");
        //    }
        //    if (firstCilrcle.Radius > (HEIGHT / 2))
        //    {
        //        autoCircle.Add(new MyCircle(new Rectangle((int)firstCilrcle.Center.X - HEIGHT / 2, (int)firstCilrcle.Center.Y - HEIGHT / 2, HEIGHT, HEIGHT)));
        //    }
        //    else
        //    {
        //        autoCircle.Add(new MyCircle(new Rectangle((int)(firstCilrcle.Center.X - firstCilrcle.Radius), (int)(firstCilrcle.Center.Y - firstCilrcle.Radius), (int)firstCilrcle.Radius * 2, (int)firstCilrcle.Radius * 2)));
        //    }
        //    //先定位再添加
        //    if (autoCircle.Count > 0)
        //    {
        //        Rectangle r = new Rectangle();
        //        foreach (MyCircle c in autoCircle)
        //        {
        //            r = searchCenter(c, c.M_rectangle.Width / 2, c.M_rectangle.Height / 2);
        //            c.M_rectangle = r;
        //        }
        //    }
        //    //针对40倍的图像进行自动生成。
        //    if (m_Height > 1000)
        //    {
        //        int width = autoCircle[0].M_rectangle.Width;
        //        int height = autoCircle[0].M_rectangle.Height;
        //        int x = autoCircle[0].M_rectangle.X;
        //        int y = autoCircle[0].M_rectangle.Y;

        //        for (int i = 0; i < 4; i++)
        //        {
        //            for (int j = 0; j < 6; j++)
        //            {
        //                int newx = x + j * disX;
        //                int newy = y + i * disX;

        //                if (i == 0 && j == 0)
        //                {
        //                    continue;
        //                }

        //                autoCircle.Add(new MyCircle(new Rectangle(newx, newy, width, height)));
        //            }
        //        }
        //    }
        //    //再全部定位
        //    if (autoCircle.Count > 0)
        //    {
        //        Rectangle r = new Rectangle();
        //        foreach (MyCircle c in autoCircle)
        //        {
        //            r = searchCenter(c, c.M_rectangle.Width / 2, c.M_rectangle.Width / 2);
        //            c.M_rectangle = r;
        //            r = searchCenter(c, c.M_rectangle.Width / 2, c.M_rectangle.Width / 2);//一次搜索不够精确，得增加多一次搜索
        //            c.M_rectangle = r;
        //        }
        //    }

        //    endFlag = true;
        //}

        public void autoDetect()
        {
            if (m_grayBitmap == null)
            {
                MessageBox.Show("请先选择图片！");
                return;
            }
            Image<Bgr, byte> scr;
            Image<Gray, byte> scr_gray;
            Bitmap cp_bitmap;
            double T = 0;
            double T0 = 0;
            //try
            //{
                scr = new Image<Bgr, byte>(m_grayBitmap);
                scr_gray = scr.Convert<Gray, byte>().PyrDown().PyrUp();
                cp_bitmap = scr_gray.Bitmap;

                if (cp_bitmap.PixelFormat != PixelFormat.Format8bppIndexed)
                {
                    MessageBox.Show("不是灰度图像");
                    return;
                }
                Rectangle rect = new Rectangle(0, 0, cp_bitmap.Width, cp_bitmap.Height);
                BitmapData bmpdata = cp_bitmap.LockBits(rect, ImageLockMode.ReadWrite, cp_bitmap.PixelFormat);
                //图像二值化
                unsafe
                {
                    byte* ptr = (byte*)bmpdata.Scan0;
                    for (int i = 0; i < bmpdata.Height; i++)
                    {
                        for (int j = 0; j < bmpdata.Width; j++)
                        {
                            T0 += *ptr;
                            ptr++;
                        }
                        ptr += bmpdata.Stride - bmpdata.Width;
                    }
                    T = T0 / cp_bitmap.Width / cp_bitmap.Height - 70;
                    ptr = (byte*)bmpdata.Scan0;
                    for (int i = 0; i < cp_bitmap.Height; i++)
                    {
                        for (int j = 0; j < cp_bitmap.Width; j++)
                        {
                            if (*ptr > T)
                            {
                                *ptr = 0;
                            }
                            else
                            {
                                *ptr = 255;
                            }
                            ptr++;
                        }
                        ptr += bmpdata.Stride - bmpdata.Width;
                    }
                }
                cp_bitmap.UnlockBits(bmpdata);
                Invalidate();

                scr_gray = new Image<Gray, byte>(cp_bitmap);
                List<CircleF> detectCircle = new List<CircleF>();
                Rectangle roi = new Rectangle(0, 0, 350, scr_gray.Height);
                scr_gray.ROI = roi;
                Gray cannyThreshold = new Gray(23);
                Gray circleAccumulatorThreshold = new Gray(40);
                CircleF[] circle = scr_gray.HoughCircles(cannyThreshold, circleAccumulatorThreshold,
                   2.10, 62, 0, 0)[0];
                if (circle.Length == 0)
                {
                    MessageBox.Show("检测出错，请手动添加");
                    return;
                }
                foreach (CircleF c in circle)
                {
                    if (c.Center.X > 10 && c.Center.X < 350 && c.Radius > 10 && c.Radius < 40 && c.Center.Y > 160 && c.Center.Y < 880)
                    {
                        detectCircle.Add(c);
                    }
                }

                //圆心X是否相差在15以内，原理是：找出每个点15范围内的点集，然后找出点集数最大的点集
                int[] num = new int[detectCircle.Count()];
                List<CircleF>[] pointFs = new List<CircleF>[detectCircle.Count()];
                for (int i = 0; i < detectCircle.Count(); i++)
                {
                    pointFs[i] = new List<CircleF>();//记得先初始化，不然会报错
                    float c = detectCircle[i].Center.X;
                    num[i] = 0;//确保从零计数
                    for (int j = 0; j < detectCircle.Count(); j++)
                    {
                        if (Math.Abs(c - detectCircle[j].Center.X) < 15)
                        {
                            num[i]++;
                            pointFs[i].Add(detectCircle[j]);
                        }
                    }
                }
                //num数组中最大的即为最佳的
                int blow = 0;
                float m = num[0];
                for (int i = 0; i < num.Length; i++)
                {
                    if (num[i] > m)
                    {
                        blow = i;
                        m = num[i];
                    }
                }
                //剔除圆心Y相差100以内的点
                int[] numY = new int[pointFs[blow].Count];
                List<CircleF>[] correctCircle = new List<CircleF>[pointFs[blow].Count];
                for (int i = 0; i < pointFs[blow].Count; i++)
                {
                    numY[i] = 0;
                    correctCircle[i] = new List<CircleF>();
                    for (int j = 0; j < pointFs[blow].Count; j++)
                    {
                        if (i != j)
                        {
                            if (Math.Abs(pointFs[blow][i].Center.Y - pointFs[blow][j].Center.Y) > 100)
                            {
                                numY[i]++;
                                correctCircle[i].Add(pointFs[blow][j]);
                            }
                        }
                        else
                        {
                            numY[i]++;
                            correctCircle[i].Add(pointFs[blow][j]);
                        }
                    }
                }
                int blowY = 0;
                float mY = num[0];
                for (int i = 0; i < numY.Length; i++)
                {
                    if (numY[i] > mY)
                    {
                        blowY = i;
                        mY = num[i];
                    }
                }

                firstCilrcle = new CircleF();
                correctCircle[blowY].Sort(CompareCircleByY);
                PointF p = new PointF();
                //画circle出来
                //最小圆的半径作为新的点圆半径
                firstCilrcle.Radius = correctCircle[blowY][0].Radius;
                foreach (CircleF circleF in correctCircle[blowY])
                {
                    //firstCilrcle.Radius += circleF.Radius / correctCircle[blowY].Count;
                    if (firstCilrcle.Radius > circleF.Radius)
                    {
                        firstCilrcle.Radius = circleF.Radius;
                    }
                }
                if (correctCircle[blowY].Count > 4)
                {
                    MessageBox.Show("检测有误，请手动检测");
                    return;
                }
                if (correctCircle[blowY].Count == 4)
                {
                    p.X = correctCircle[blowY][0].Center.X + 260;
                    p.Y = correctCircle[blowY][0].Center.Y - 17;
                    firstCilrcle.Center = p;
                }
                if (correctCircle[blowY].Count < 4)
                {
                    p.X = correctCircle[blowY][0].Center.X + 260;
                    if (correctCircle[blowY][0].Center.Y < 333)
                    {
                        p.Y = correctCircle[blowY][0].Center.Y - 17;
                    }
                    else if (correctCircle[blowY][0].Center.Y >= 333 && correctCircle[blowY][0].Center.Y < 493)
                    {
                        p.Y = correctCircle[blowY][0].Center.Y - 17 - 155;
                    }
                    else if (correctCircle[blowY][0].Center.Y >= 493 && correctCircle[blowY][0].Center.Y < 653)
                    {
                        p.Y = correctCircle[blowY][0].Center.Y - 17 - 155 - 155;
                    }
                    else if (correctCircle[blowY][0].Center.Y >= 653)
                    {
                        p.Y = correctCircle[blowY][0].Center.Y - 17 - 155 - 155 - 155;
                    }
                    firstCilrcle.Center = p;
                }

            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("检测出错，请手动添加圆");
            //}
            if (firstCilrcle.Radius > (HEIGHT / 2))
            {
                autoCircle.Add(new MyCircle(new Rectangle((int)firstCilrcle.Center.X - HEIGHT / 2, (int)firstCilrcle.Center.Y - HEIGHT / 2, HEIGHT, HEIGHT)));
            }
            else
            {
                autoCircle.Add(new MyCircle(new Rectangle((int)(firstCilrcle.Center.X - firstCilrcle.Radius), (int)(firstCilrcle.Center.Y - firstCilrcle.Radius), (int)firstCilrcle.Radius * 2, (int)firstCilrcle.Radius * 2)));
            }
            //先定位再添加
            if (autoCircle.Count > 0)
            {
                Rectangle r = new Rectangle();
                foreach (MyCircle c in autoCircle)
                {
                    r = searchCenter(c, c.M_rectangle.Width , c.M_rectangle.Height);
                    c.M_rectangle = r;
                    r = searchCenter(c, c.M_rectangle.Width / 2, c.M_rectangle.Height / 2);
                    c.M_rectangle = r;
                }
            }
            //针对40倍的图像进行自动生成。
            if (m_Height > 1000)
            {
                int width = autoCircle[0].M_rectangle.Width;
                int height = autoCircle[0].M_rectangle.Height;
                int x = autoCircle[0].M_rectangle.X;
                int y = autoCircle[0].M_rectangle.Y;

                for (int i = 0; i < proteinRows; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        int newx = x + j * disX;
                        int newy = y + i * disX;

                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        autoCircle.Add(new MyCircle(new Rectangle(newx, newy, width, height)));
                    }
                }
            }
            //再全部定位
            if (autoCircle.Count > 0)
            {
                Rectangle r = new Rectangle();
                foreach (MyCircle c in autoCircle)
                {
                    r = searchCenter(c, c.M_rectangle.Width ,c.M_rectangle.Width);
                    c.M_rectangle = r;
                    r = searchCenter(c, c.M_rectangle.Width, c.M_rectangle.Width);//一次搜索不够精确，得增加多一次搜索
                    c.M_rectangle = r;
                }
            }

            endFlag = true;
        }

        public void AutoAnalyse()
        {
            //至此，找到了了第一个圆的位置和半径
            if (m_circle.Count > 0)
            {
                if ((MessageBox.Show("添加的圆将清除，重新检测，是否继续？", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel))
                {
                    return;
                }
            }
            m_circle.Clear();
            while (endFlag == false) ;
            autoCircle.ForEach(i => m_circle.Add(i));
            reDrawCircle();
        }


        //设置圆心Y的比较，进行排序
        public static int CompareCircleByY(CircleF c1, CircleF c2)
        {
            if (c1.Center.Y >= c2.Center.Y)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private void ChildForm_Load(object sender, EventArgs e)
        {
            rectory = new SetControlRectangle(childPb);
            rectory.SetRectangle += new SetControlRectangle.SelectRectangle(rectory_setRectangle);
        }


        public void rectory_setRectangle(object sender, Rectangle e)
        {
            //if (m_operation == operation.NO_USE)
            //{
            //    return;
            //}
            //this.Text = e.ToString();
            foreach (MyCircle circle in m_circle)
            {
                if (IsInTheRectangle(circle, e))
                {
                    circle.M_checkFlag = !circle.M_checkFlag;
                }
            }
            foreach (MyCircle c in m_circle)
            {
                if (c.M_checkFlag)
                {
                    m_changeCircle = true;
                    break;
                }
            }
            reDrawCircle();
        }

        private bool IsInTheRectangle(MyCircle c, Rectangle r)
        {
            if ((c.M_rectangle.X + c.M_rectangle.Width / 2) > r.X && (c.M_rectangle.X + c.M_rectangle.Width / 2) < (r.X + r.Width) &&
                (c.M_rectangle.Y + c.M_rectangle.Height / 2) > r.Y && (c.M_rectangle.Y + c.M_rectangle.Height / 2) < (r.Y + r.Height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }//class
}
