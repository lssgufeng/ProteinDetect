using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace proteinDetect
{
    /// <summary>
    /// 在一个控件上绘制虚线矩形，并返回矩形区域
    /// 2016.03.10
    /// </summary>
    class SetControlRectangle
    {
        /// <summary>
        /// 需要绘制的图形
        /// </summary>
        /// <param name="p_Control">控件</param>
        public SetControlRectangle(Control p_Control)
        {
            m_Control = p_Control;
            m_Control.MouseUp += new MouseEventHandler(MyControl_MouseUp);
            m_Control.MouseMove += new MouseEventHandler(MyControl_MouseMove);
            m_Control.MouseDown += new MouseEventHandler(MyControl_MouseDown);
        }
        private Control m_Control;
        /// <summary>
        /// 鼠标状态
        /// </summary>
        private bool m_MouseIsDown = false;
        /// <summary>
        /// 绘制区域
        /// </summary>
        private Rectangle m_MouseRect = Rectangle.Empty;
        /// <summary>
        /// 刷新绘制
        /// </summary>
        /// <param name="p"></param>
        private void ResizeToRectangle(Point p_Point)
        {
            DrawRectangle();
            m_MouseRect.Width = p_Point.X - m_MouseRect.Left;
            m_MouseRect.Height = p_Point.Y - m_MouseRect.Top;
            DrawRectangle();
        }
        /// <summary>
        /// 绘制区域
        /// </summary>
        private void DrawRectangle()
        {
            Rectangle _Rect = m_Control.RectangleToScreen(m_MouseRect);
            ControlPaint.DrawReversibleFrame(_Rect, Color.White, FrameStyle.Dashed);
        }
        /// <summary>
        /// 开始绘制 并且设置鼠标区域
        /// </summary>
        /// <param name="StartPoint">开始位置</param>
        private void DrawStart(Point p_Point)
        {
            m_Control.Capture = true;
            Cursor.Clip = m_Control.RectangleToScreen(new Rectangle(0, 0, m_Control.Width, m_Control.Height));
            m_MouseRect = new Rectangle(p_Point.X, p_Point.Y, 0, 0);
        }
        private void MyControl_MouseDown(object sender, MouseEventArgs e)
        {
            //当是移动圆时，点击的点不在圆内才开始绘制
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ChildForm))
                {
                    ChildForm cf = (ChildForm) form;
                    
                    if (cf.M_operation == operation.MOVECIRCLE) 
                    {
                        foreach (MyCircle circle in cf.m_circle)
                        {
                            if (cf.isIntheCircle(new Point(e.X, e.Y), circle))
                                return;
                        }
                    }
                }
            }
            m_MouseIsDown = true;
            DrawStart(new Point(e.X, e.Y));
        }
        private void MyControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseIsDown) ResizeToRectangle(new Point(e.X, e.Y));
        }
        private void MyControl_MouseUp(object sender, MouseEventArgs e)
        {
            m_Control.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            m_MouseIsDown = false;
            DrawRectangle();
            if (m_MouseRect.X == 0 || m_MouseRect.Y == 0 || m_MouseRect.Width == 0 || m_MouseRect.Height == 0)
            {
                //如果区域没0 就不执行委托
            }
            else
            {
                if (SetRectangle != null) SetRectangle(m_Control, m_MouseRect);
            }
            m_MouseRect = Rectangle.Empty;
        }
        public delegate void SelectRectangle(object sneder, Rectangle e);
        public event SelectRectangle SetRectangle;
    }
}
