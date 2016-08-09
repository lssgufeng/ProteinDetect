/*
 * Created by SharpDevelop.
 * User: learn
 * Date: 2015/3/18
 * Time: 10:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ZedGraph;
using System.Collections.Generic;

namespace proteinDetect
{
    /// <summary>
    /// Description of storeData.
    /// </summary>
    [Serializable]
    public class storeData
    {

        //the list of the curve
        public PointPairList listNSE;
        public PointPairList listCEA;
        public PointPairList listDKK1;
        public PointPairList listCY211;

        //the list of the concentration
        public PointPairList conlistNSE;
        public PointPairList conlistCEA;
        public PointPairList conlistDKK1;
        public PointPairList conlistCY211;


        public Dictionary<string, L4PCurve> mL4P = null;
        public Dictionary<string, L3Pcurve> mL3P = null;
        public Dictionary<string, LE4PCurve> mLE4P = null;


        public bool hasL4Pcurve;
        public bool hasL3Pcurve;
        public bool hasLE4Pcurve;

        public Dictionary<double, ConcentrationInfo> mConcentrationMaps = null;

        /// <summary>
        /// 构造一个可以serializable 的存储对象
        /// </summary>
        /// <param name="lNSE"> NSE curve 上的点集</param>
        /// <param name="lCEA">CEA curve 上的点集</param>
        /// <param name="lDKK1">DKK1 curve 上的点集</param>
        /// <param name="lCY211">CY211 curve 上的点集</param>
        /// <param name="clNSE">NSE curve 上的浓度集</param>
        /// <param name="clCEA">CEA curve 上的浓度集</param>
        /// <param name="clDKK1">DKK1 curve 上的浓度集</param>
        /// <param name="clCY211">CY211 curve 上的浓度集</param>
        /// <param name="m4P">l4P 曲线</param>
        /// <param name="m3P">l3P 曲线</param>
        /// <param name="mE4P">lE4P 曲线</param>
        /// <param name="hasL4">是否有L4P曲线</param>
        /// <param name="hasL3">是否有L3P曲线</param>
        /// <param name="hasLE4">是否有LE4P曲线</param>
        public storeData(PointPairList lNSE, PointPairList lCEA, PointPairList lDKK1, PointPairList lCY211,
                        PointPairList clNSE, PointPairList clCEA, PointPairList clDKK1, PointPairList clCY211,
                       Dictionary<string, L4PCurve> m4P, Dictionary<string, L3Pcurve> m3P, Dictionary<string, LE4PCurve> mE4P,
                        bool hasL4, bool hasL3, bool hasLE4,
                        Dictionary<double, ConcentrationInfo> mCMaps
                      )
        {
            listCEA = lCEA.Clone();
            listCY211 = lCY211.Clone();
            listDKK1 = lDKK1.Clone();
            listNSE = lNSE.Clone();

            conlistCEA = clCEA.Clone();
            conlistCY211 = clCY211.Clone();
            conlistDKK1 = clDKK1.Clone();
            conlistNSE = clNSE.Clone();


            this.mL4P = m4P;
            this.mL3P = m3P;
            this.mLE4P = mE4P;

            hasL4Pcurve = hasL4;
            hasL3Pcurve = hasL3;
            hasLE4Pcurve = hasLE4;

            this.mConcentrationMaps = mCMaps;

        }

        public storeData()
        {


        }
    }
}























































