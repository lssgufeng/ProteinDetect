using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace proteinDetect
{
    public class MyCircle
    {
        private Rectangle m_rectangle = new Rectangle();
        private bool m_checkFlag = false;
        //平均灰度
        private int m_meanGray = 0;
        private int m_centerOneMoment = 0;

        public Rectangle M_rectangle
        {
            get { return this.m_rectangle; }
            set { this.m_rectangle = value; }
        }

        public bool M_checkFlag
        {
            get { return this.m_checkFlag; }
            set { this.m_checkFlag = value; }
        }

        public int M_meanGray
        {
            get { return this.m_meanGray; }
            set { this.m_meanGray = value; }
        }

        public int M_centerOneMoment
        {
            get { return this.m_centerOneMoment; }
            set { this.m_centerOneMoment = value; }
        }

        public MyCircle()
        {
            
        }

        public MyCircle(Rectangle r)
        {
            this.m_rectangle = r;
        }
    }
}
