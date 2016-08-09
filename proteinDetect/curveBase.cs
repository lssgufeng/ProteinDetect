using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proteinDetect
{
    /// <summary>
    /// 曲线拟合程度的相关参数
    /// </summary>
    [Serializable]
    public class curveBase
    {
        public double sse;//sum of square due to error
        public double rsquare;//coefficent of determination
        public double dfe;//degrees of freedom
        public double rmse;//root mean squared error
        public double adjrsquare;//adjuested r square

        public curveBase()
        {
            
        }

        public curveBase(double ss, double rs, double df, double ad, double rm)
        {
            this.sse = ss;
            this.rsquare = rs;
            this.dfe = df;
            this.rmse = rm;
            this.adjrsquare = ad;
        }

        public double Sse
        {
            get { return this.Sse; }
        }

        public double Rsquare
        {
            get { return this.rsquare; }
        }

        public double Dfe
        {
            get { return this.dfe; }
        }

        public double Rmse
        {
            get { return this.rmse; }
        }

        public double Adjrsquare
        {
            get { return this.adjrsquare; }
        }

    }
}
