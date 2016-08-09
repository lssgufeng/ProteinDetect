using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proteinDetect
{
    public class CircleAnalyse
    {
        private int m_area;
        private float m_meanGray;
        private float m_meanOpticalDensity;
        private int m_col;
        private int m_row;

        public int M_area
        {
            get { return this.m_area; }
            set { this.m_area = value; }
        }

        public float M_meanGray 
        {
            get
            {
                return this.m_meanGray;
            }
            set { this.m_meanGray = value; }
        }

        public float M_meanOpticalDensity
        {
            get { return this.m_meanOpticalDensity; }
            set { this.m_meanOpticalDensity = value; }
        }

        public int M_col
        {
            get { return this.m_col; }
            set { this.m_col = value; }
        }

        public int M_row
        {
            get { return this.m_row; }
            set { this.m_row = value; }
        }

        public CircleAnalyse()
        {
            
        }

        public CircleAnalyse(int area, float meanGray, float meanOptical)
        {
            this.m_area = area;
            this.m_meanGray = meanGray;
            this.M_meanOpticalDensity = meanOptical;
        }
    }
}
