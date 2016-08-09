using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proteinDetect
{
    /// <summary>
    /// Description of L3Pcurve.
    /// </summary>
    [Serializable]
    public class L3Pcurve : curveBase
    {
        //曲线系数，A 为最大值， C为最小值
        public double A;
        public double B;
        public double C;
        public double[] cf = new double[3];

        public L3Pcurve()
        {

        }

        public L3Pcurve(double a, double b, double c, double ss, double rs, double df, double rm, double ad)
            : base(ss, rs, df, ad, rm)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.cf[0] = this.A;
            this.cf[1] = this.B;
            this.cf[2] = this.C;

        }
        public double mA
        {
            get
            {
                return this.A;
            }
        }

        public double mB
        {
            get
            {
                return this.B;
            }
        }

        public double mC
        {
            get
            {
                return this.C;
            }
        }

    }
}
