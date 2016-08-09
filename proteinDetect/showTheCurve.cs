using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using curveFit;
using ZedGraph;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

namespace proteinDetect
{
	public partial class showTheCurve : Form
	{
		public curveFit.Curve mFit = null;
		public double concentration = 0.0f;
		public MainForm mMainForm  = null;
		
		public ComboBox mCombox = null;
		
		//获取chart控件对象
		public GraphPane mPane = null;
		
		//the list of the curve
		public PointPairList listNSE = new PointPairList();
		public PointPairList listCEA = new PointPairList();
		public PointPairList listDKK1 = new PointPairList();
		public PointPairList listCY211 = new PointPairList();
		
		//样本的点集
		public PointPairList conlistNSE = new PointPairList();
		public PointPairList conlistCEA = new PointPairList();
		public PointPairList conlistDKK1 = new PointPairList();
        public PointPairList conlistCY211 = new PointPairList();
        //去掉异常值的点集
        public PointPairList reviseConNSE = new PointPairList();
        public PointPairList reviseConCEA = new PointPairList();
        public PointPairList reviseConDKK1 = new PointPairList();
        public PointPairList reviseConCY211 = new PointPairList();
		
		
		public Dictionary<string, L4PCurve> mL4P = null;
		public Dictionary<string, L3Pcurve> mL3P = null;
		public Dictionary<string, LE4PCurve> mLE4P = null;
		
		public double[] x = null;
		public double[] yNSE = null;
		public double[] yCEA = null;
		public double[] yDKK1 = null;
        public double[] yCY211 = null;
        ////存储平均值，因为拟合的返回值没有期望值
        //public double meanNSE = 0.0;
        //public double meanCEA = 0.0;
        //public double meanDKK1 = 0.0;
        //public double meanCY211 = 0.0;
	    //public int n = 0;//存储样本的个数
		
		
		//标记浓度的最大最小值
		private double minCen = 0;
		private double maxCen = 0;
		
		private string stringL4P = "Y= (A-B)/" +
		                           "[1\t\n+(X/LogC)^D]+B";
		private string stringL3P = "Y=B+(A-B)/" +
		                           "    (1+\t\n  10^((LogC-X)))";
		private string stringLE4P = "Y=B+(A-B)/" +
		                            "   (1+\t\n  10^((LogC-X)*D))";
		private StringBuilder sbR2 = new StringBuilder("");
		
		private StringBuilder sbCEAR2 = new StringBuilder("");
		private StringBuilder sbNSER2 = new StringBuilder("");
		private StringBuilder sbDKK1R2 = new StringBuilder("");
		private StringBuilder sbCY211R2 = new StringBuilder("");
		
		
		public bool isFit = false;
		
		public showTheCurve()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/// <summary>
		/// 用历史数据进行初始化
		/// </summary>
		/// <param name="sd"></param>
		public showTheCurve(storeData sd, MainForm f)
		{
			InitializeComponent();
			mPane = zGControl.GraphPane;
			
			if(sd.hasL4Pcurve)
				curveComboBox.SelectedIndex = 0;
			else if(sd.hasL3Pcurve)
				curveComboBox.SelectedIndex = 1;
			else if(sd.hasLE4Pcurve)
				curveComboBox.SelectedIndex = 2;
			
			mMainForm = f;
			//等待知道拟合环境准备完成。
			while(f.mFit == null);
			
			mFit = f.mFit;
			
			
			initSomeThing();

			mL4P = sd.mL4P;
			mL3P = sd.mL3P;
			mLE4P = sd.mLE4P;
			
			conlistCEA = sd.conlistCEA;
			conlistCY211 = sd.conlistCY211;
			conlistDKK1 = sd.conlistDKK1;
			conlistNSE = sd.conlistNSE;
			
			listNSE = sd.listNSE;
			listCEA = sd.listCEA;
			listCY211 = sd.listCY211;
			listDKK1 = sd.listDKK1;
			
			//设置为有拟合曲线
			isFit = true;
			
			drawTheCurve();
		}
		
		
		
		public showTheCurve(MainForm f)
		{
			InitializeComponent();
			
			mPane = zGControl.GraphPane;
			curveComboBox.SelectedIndex = 0;
			
			//初始化Form
			mMainForm = f;
			while(f.mFit == null);
			mFit = f.mFit;
			//将两个map传递过来，进行初始化。
			mL4P = mMainForm.mL4P;
			mL3P = mMainForm.mL3P;
			mLE4P = mMainForm.mLE4P;
			
			initSomeThing();
			
			
		}
		
		/// <summary>
		/// 将需要拟合的数据取出到数组中去
		/// </summary>
		public void initSomeThing()
		{
//			int n = mMainForm.mConcentrationMaps.Count;
//			x = new double[n];
//			yNSE = new double[n];
//			yCEA = new double[n];
//			yDKK1 = new double[n];
//			yCY211 = new double[n];
//			int i = 0;
			
			//lx 保存当前的浓度信息
			List<double> lx = new List<double>();
			
			List<double> lyNSE = new List<double>();
			List<double> lyCEA = new List<double>();
			List<double> lyDKK1 = new List<double>();
			List<double> lyCY211 = new List<double>();
			
			foreach (KeyValuePair<double, ConcentrationInfo> kvp in mMainForm.mConcentrationMaps)
			{
				//跳过0值，因为0不可以取对数，
				if(kvp.Key == 0.0)
				{
					continue;
				}
				lx.Add(kvp.Key);
				lyNSE.Add(kvp.Value.AveNSE);
				lyCEA.Add(kvp.Value.AveCEA);
				lyDKK1.Add(kvp.Value.AveDKK1);
				lyCY211.Add(kvp.Value.AveCY211);
			}
			
			int n = lx.Count;
			//将计算的数据初始化了
			//使用一个list可以解决数组刚开始申请空间较大会有0值存在的情况。
			x = lx.ToArray();
			yNSE = lyNSE.ToArray();
			yCEA = lyCEA.ToArray();
			yDKK1 = lyDKK1.ToArray();
			yCY211 = lyCY211.ToArray();
			
			
			for(int i = 0; i < n; i++)
			{
				//初始化点集，绘制原来的点集合。
				conlistCEA.Add(Math.Log10(x[i]), yCEA[i]);
				conlistNSE.Add(Math.Log10(x[i]), yNSE[i]);
				conlistDKK1.Add(Math.Log10(x[i]), yDKK1[i]);
				conlistCY211.Add(Math.Log10(x[i]), yCY211[i]);
			}
		}
		
		
		//计算工作挺慢的，可以考虑放到后台线程。
		/// <summary>
		/// 拟合曲线
		/// </summary>
		/// <param name="parameters">
		/// 需要拟合的曲线的参数个数
		/// </param>
		public void fitTheCurve(int parameters)
		{
			//清空内容。
			mL4P.Clear();
			mL3P.Clear();
			mLE4P.Clear();
            dataGridView1.Rows.Clear();

			if(parameters == 4)
			{
				double[,] confNSE = (double[,])((mMainForm.mFit.L4P((MWNumericArray)x, (MWNumericArray)yNSE)).ToArray());
				double[,] confCEA = (double[,])((mMainForm.mFit.L4P((MWNumericArray)x, (MWNumericArray)yCEA)).ToArray());
				double[,] confDKK1 = (double[,])((mMainForm.mFit.L4P((MWNumericArray)x, (MWNumericArray)yDKK1)).ToArray());
				double[,] confCY211 = (double[,])((mMainForm.mFit.L4P((MWNumericArray)x, (MWNumericArray)yCY211)).ToArray());
				
				mL4P.Add("NSE", new L4PCurve(confNSE[0,0], confNSE[0,1], confNSE[0,2], confNSE[0,3], confNSE[0,4], confNSE[0,5], confNSE[0,6], confNSE[0,7], confNSE[0,8]));
				mL4P.Add("CEA", new L4PCurve(confCEA[0,0], confCEA[0,1], confCEA[0,2], confCEA[0,3], confCEA[0,4], confCEA[0,5], confCEA[0,6], confCEA[0,7], confCEA[0,8]));
				mL4P.Add("DKK1", new L4PCurve(confDKK1[0,0], confDKK1[0,1], confDKK1[0,2], confDKK1[0,3], confDKK1[0,4], confDKK1[0,5], confDKK1[0,6], confDKK1[0,7], confDKK1[0,8]));
				mL4P.Add("CY211", new L4PCurve(confCY211[0,0], confCY211[0,1], confCY211[0,2], confCY211[0,3], confCY211[0,4], confCY211[0,5], confCY211[0,6], confCY211[0,7], confCY211[0,8]));

                //新增显示参数216.3.22
                string[] nse = new string[5] { "NSE", confNSE[0, 0].ToString("0.000"), confNSE[0, 1].ToString("0.000"), confNSE[0, 2].ToString("0.000"), confNSE[0, 3].ToString("0.000") };
                string[] cea = new string[5] { "CEA", confCEA[0, 0].ToString("0.000"), confCEA[0, 1].ToString("0.000"), confCEA[0, 2].ToString("0.000"), confCEA[0, 3].ToString("0.000") };
                string[] dkk1 = new string[5] { "DKK1", confDKK1[0, 0].ToString("0.000"), confDKK1[0, 1].ToString("0.000"), confDKK1[0, 2].ToString("0.000"), confDKK1[0, 3].ToString("0.000") };
                string[] cy211 = new string[5] { "CY211", confCY211[0, 0].ToString("0.000"), confCY211[0, 1].ToString("0.000"), confCY211[0, 2].ToString("0.000"), confCY211[0, 3].ToString("0.000") };
			    dataGridView1.Rows.Add(nse);
			    dataGridView1.Rows.Add(cea);
			    dataGridView1.Rows.Add(dkk1);
			    dataGridView1.Rows.Add(cy211);
			}
			if(parameters == 3)
			{
				double[,] confNSE = (double[,])((mMainForm.mFit.L3P((MWNumericArray)x, (MWNumericArray)yNSE)).ToArray());
				double[,] confCEA = (double[,])((mMainForm.mFit.L3P((MWNumericArray)x, (MWNumericArray)yCEA)).ToArray());
				double[,] confDKK1 = (double[,])((mMainForm.mFit.L3P((MWNumericArray)x, (MWNumericArray)yDKK1)).ToArray());
				double[,] confCY211 = (double[,])((mMainForm.mFit.L3P((MWNumericArray)x, (MWNumericArray)yCY211)).ToArray());
				
				mL3P.Add("NSE", new L3Pcurve(confNSE[0,0], confNSE[0,1], confNSE[0,2], confNSE[0,4], confNSE[0,5], confNSE[0,6], confNSE[0,7], confNSE[0,8]));
				mL3P.Add("CEA", new L3Pcurve(confCEA[0,0], confCEA[0,1], confCEA[0,2], confCEA[0,4], confCEA[0,5], confCEA[0,6], confCEA[0,7], confCEA[0,8]));
				mL3P.Add("DKK1", new L3Pcurve(confDKK1[0,0], confDKK1[0,1], confDKK1[0,2], confDKK1[0,4], confDKK1[0,5], confDKK1[0,6], confDKK1[0,7], confDKK1[0,8]));
				mL3P.Add("CY211", new L3Pcurve(confCY211[0,0], confCY211[0,1], confCY211[0,2], confCY211[0,4], confCY211[0,5], confCY211[0,6], confCY211[0,7], confCY211[0,8]));

                //新增显示参数216.3.22
                string[] nse = new string[5] { "NSE", confNSE[0, 0].ToString("0.000"), confNSE[0, 1].ToString("0.000"), confNSE[0, 4].ToString("0.000"), "" };
                string[] cea = new string[5] { "CEA", confCEA[0, 0].ToString("0.000"), confCEA[0, 1].ToString("0.000"), confCEA[0, 4].ToString("0.000"), "" };
                string[] dkk1 = new string[5] { "DKK1", confDKK1[0, 0].ToString("0.000"), confDKK1[0, 1].ToString("0.000"), confDKK1[0, 4].ToString("0.000"), "" };
                string[] cy211 = new string[5] { "CY211", confCY211[0, 0].ToString("0.000"), confCY211[0, 1].ToString("0.000"), confCY211[0, 4].ToString("0.000"), "" };
                dataGridView1.Rows.Add(nse);
                dataGridView1.Rows.Add(cea);
                dataGridView1.Rows.Add(dkk1);
                dataGridView1.Rows.Add(cy211);
			}
			if(parameters == 5)
			{
				double[,] confNSE = (double[,])((mMainForm.mFit.LE4P((MWNumericArray)x, (MWNumericArray)yNSE)).ToArray());
				double[,] confCEA = (double[,])((mMainForm.mFit.LE4P((MWNumericArray)x, (MWNumericArray)yCEA)).ToArray());
				double[,] confDKK1 = (double[,])((mMainForm.mFit.LE4P((MWNumericArray)x, (MWNumericArray)yDKK1)).ToArray());
				double[,] confCY211 = (double[,])((mMainForm.mFit.LE4P((MWNumericArray)x, (MWNumericArray)yCY211)).ToArray());
				
				mLE4P.Add("NSE", new LE4PCurve(confNSE[0,0], confNSE[0,1], confNSE[0,2], confNSE[0,3], confNSE[0,4], confNSE[0,5], confNSE[0,6], confNSE[0,7], confNSE[0,8]));
				mLE4P.Add("CEA", new LE4PCurve(confCEA[0,0], confCEA[0,1], confCEA[0,2], confCEA[0,3], confCEA[0,4], confCEA[0,5], confCEA[0,6], confCEA[0,7], confCEA[0,8]));
				mLE4P.Add("DKK1", new LE4PCurve(confDKK1[0,0], confDKK1[0,1], confDKK1[0,2], confDKK1[0,3], confDKK1[0,4], confDKK1[0,5], confDKK1[0,6], confDKK1[0,7], confDKK1[0,8]));
				mLE4P.Add("CY211", new LE4PCurve(confCY211[0,0], confCY211[0,1], confCY211[0,2], confCY211[0,3], confCY211[0,4], confCY211[0,5], confCY211[0,6], confCY211[0,7], confCY211[0,8]));

                //新增显示参数216.3.22
                string[] nse = new string[5] { "NSE", confNSE[0, 0].ToString("0.000"), confNSE[0, 1].ToString("0.000"), confNSE[0, 2].ToString("0.000"), confNSE[0, 3].ToString("0.000") };
                string[] cea = new string[5] { "CEA", confCEA[0, 0].ToString("0.000"), confCEA[0, 1].ToString("0.000"), confCEA[0, 2].ToString("0.000"), confCEA[0, 3].ToString("0.000") };
                string[] dkk1 = new string[5] { "DKK1", confDKK1[0, 0].ToString("0.000"), confDKK1[0, 1].ToString("0.000"), confDKK1[0, 2].ToString("0.000"), confDKK1[0, 3].ToString("0.000") };
                string[] cy211 = new string[5] { "CY211", confCY211[0, 0].ToString("0.000"), confCY211[0, 1].ToString("0.000"), confCY211[0, 2].ToString("0.000"), confCY211[0, 3].ToString("0.000") };
                dataGridView1.Rows.Add(nse);
                dataGridView1.Rows.Add(cea);
                dataGridView1.Rows.Add(dkk1);
                dataGridView1.Rows.Add(cy211);
            }
            //进行了拟合，将标志设为true
            isFit = true;

		}
		
		
		//生成绘曲线的点集
		/// <summary>
		/// 计算曲线上的点
		/// </summary>
		/// 
		/// <param name="i">
		/// 表示曲线的类型，用来计算不同的点集
		/// </param>
		public void calculateThePoints(int i)
		{
			listNSE.Clear();
		
			listCEA.Clear();
			listDKK1.Clear();
			listCY211.Clear();
			
			switch(i)
			{
				case 4:
					calculateTheL4PPoints();
					break;
				case 3:
					calculateTheL3PPoints();
					break;
				case 5:
					calculateTheLE4PPoints();
					break;
			}
			
		}
		
		
		//计算LE4P曲线上面的点，进行绘图
		public void calculateTheLE4PPoints()
		{
			LE4PCurve le4p = null;
			MWArray result = null;
			double numb = 0;
			for(int i = -10; i < 70; i++)
			{
				double xp = (double)i / 25;
				le4p = mLE4P["NSE"];
				result	= mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listNSE.Add(xp, numb);
				
				le4p = mLE4P["CEA"];
				result	= mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listCEA.Add(xp, numb);
				
				le4p = mLE4P["DKK1"];
				result	= mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listDKK1.Add(xp, numb);
				
				le4p = mLE4P["CY211"];
				result	= mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listCY211.Add(xp, numb);
			}
		}
		
		//计算L4P曲线上面的点，进行绘图
		public void calculateTheL4PPoints()
		{
			L4PCurve l4p = null;
			MWArray result = null;
			double numb = 0;
			for(int i = -10; i < 70; i++)
			{
				double xp = (double)i / 25;
				l4p = mL4P["NSE"];
				result	= mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listNSE.Add(xp, numb);
				
				l4p = mL4P["CEA"];
				result	= mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listCEA.Add(xp, numb);
				
				l4p = mL4P["DKK1"];
				result	= mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listDKK1.Add(xp, numb);
				
				l4p = mL4P["CY211"];
				result	= mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listCY211.Add(xp, numb);
			}
		}
		
		//计算L3P曲线上的点
		public void calculateTheL3PPoints()
		{
			L3Pcurve l3p = null;
			MWArray result = null;
			double numb = 0;
			for(int i = -10; i < 70; i+=5)
			{
				double xp = (double)i / 25;
				l3p = mL3P["NSE"];
				result	= mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listNSE.Add(xp, numb);
				
				l3p = mL3P["CEA"];
				result	= mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listCEA.Add(xp, numb);
				
				l3p = mL3P["DKK1"];
				result	= mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listDKK1.Add(xp, numb);
				
				l3p = mL3P["CY211"];
				result	= mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp) ;
				numb = ((MWNumericArray)result).ToScalarDouble();
				listCY211.Add(xp, numb);
			}
		}
		
		//draw nse
		public void drawNSECurve()
		{
		    LineItem myCurve;
		    if (reviseCheckBox.Checked == true)
            {
                myCurve = mPane.AddCurve("NSE", reviseConNSE, Color.Blue, SymbolType.Circle);
		    }
		    else
            {
               myCurve = mPane.AddCurve("NSE", conlistNSE, Color.Blue, SymbolType.Circle);
		    }
			myCurve.Symbol.Fill = new Fill(Color.Blue);
			myCurve.Line.IsVisible = false;
			
			myCurve = mPane.AddCurve(null, listNSE, Color.Blue, SymbolType.None);
			myCurve.Line.Width = 3;
		}
		
		//draw cea
		public void drawCEACurve()
        {
		    LineItem myCurve;
		    if (reviseCheckBox.Checked == true)
            {
                myCurve = mPane.AddCurve("CEA", reviseConCEA, Color.Red, SymbolType.Diamond);
		    }
		    else
		    {
                myCurve = mPane.AddCurve("CEA", conlistCEA, Color.Red, SymbolType.Diamond);
		    }
			myCurve.Symbol.Fill = new Fill(Color.Red);
			myCurve.Line.IsVisible = false;
			//画出标准曲线
			myCurve = mPane.AddCurve(null, listCEA, Color.Red, SymbolType.None);
			myCurve.Line.Width = 3;
		}
		
		//draw dkk1
		public void drawDKK1Curve()
        {
			LineItem myCurve;
		    if (reviseCheckBox.Checked == true)
            {
                myCurve = mPane.AddCurve("DKK1", reviseConDKK1, Color.Purple, SymbolType.Triangle);
		    }
		    else
		    {
                myCurve = mPane.AddCurve("DKK1", conlistDKK1, Color.Purple, SymbolType.Triangle);
		    }
			myCurve.Symbol.Fill = new Fill(Color.Purple);
			myCurve.Line.IsVisible = false;
			
			myCurve = mPane.AddCurve(null, listDKK1, Color.Purple, SymbolType.None);
			myCurve.Line.Width = 3;
		}

        //draw cy211
		public void drawCY211Curve()
        {
            LineItem myCurve;
            if (reviseCheckBox.Checked == true)
            {
                //double SCY211 = 0;
                //reviseConCY211.Clear();
                //MWArray result = null;
                //double tempCY211;
                //if (curveComboBox.SelectedIndex == 0)
                //{
                //    SCY211 = mL4P["CY211"].rmse;
                //    for (int i = 0; i < x.Count(); i++)
                //    {
                //        //double tempCY211 = ((MWNumericArray)mFit.L4PtoY((MWNumericArray)mL4P["CY211"].cf, (MWNumericArray)x[i])).ToScalarDouble();
                //        result = mFit.L4PtoY((MWNumericArray)mL4P["CY211"].cf, (MWNumericArray)x[i]);
                //        tempCY211 = ((MWNumericArray)result).ToScalarDouble();
                //        if (Math.Abs(yCY211[i] - tempCY211) < 3 * SCY211)
                //        {
                //            reviseConCY211.Add(Math.Log10(x[i]), yCY211[i]);
                //        }
                //    }
                //}
                //else if (curveComboBox.SelectedIndex == 1)
                //{
                //    SCY211 = mL3P["CY211"].rmse;
                //    for (int i = 0; i < x.Count(); i++)
                //    {
                //        //double tempCY211 = ((MWNumericArray)mFit.L3PtoY((MWNumericArray)mL3P["CY211"].cf, (MWNumericArray)x[i])).ToScalarDouble();
                //        result = mFit.L3PtoY((MWNumericArray)mL3P["CY211"].cf, (MWNumericArray)x[i]);
                //        tempCY211 = ((MWNumericArray)result).ToScalarDouble();
                //        if (Math.Abs(yCY211[i] - tempCY211) < 3 * SCY211)
                //        {
                //            reviseConCY211.Add(Math.Log10(x[i]), yCY211[i]);
                //        }
                //    }
                //}
                //else if (curveComboBox.SelectedIndex == 2)
                //{
                //    SCY211 = mLE4P["CY211"].rmse;
                //    for (int i = 0; i < x.Count(); i++)
                //    {
                //        //double tempCY211 = ((MWNumericArray)mFit.LE4PtoY((MWNumericArray)mLE4P["CY211"].cf, (MWNumericArray)x[i])).ToScalarDouble();
                //        result = mFit.LE4PtoY((MWNumericArray)mLE4P["CY211"].cf, (MWNumericArray)x[i]);
                //        tempCY211 = ((MWNumericArray)result).ToScalarDouble();
                //        if (Math.Abs(yCY211[i] - tempCY211) < 3 * SCY211)
                //        {
                //            reviseConCY211.Add(Math.Log10(x[i]), yCY211[i]);
                //        }
                //    }
                //}

                myCurve = mPane.AddCurve("CY211", reviseConCY211, Color.Green, SymbolType.TriangleDown);
            }
            else
            {
                myCurve = mPane.AddCurve("CY211", conlistCY211, Color.Green, SymbolType.TriangleDown);
            }
			myCurve.Symbol.Fill = new Fill(Color.Green);
			myCurve.Line.IsVisible = false;
			
			myCurve = mPane.AddCurve(null, listCY211, Color.Green, SymbolType.None);
			myCurve.Line.Width = 3;
		}
		
		public void drawTheCurve()
		{
			//清除所有的line
			mPane.CurveList.Clear();
			
			//清除掉文字块标示
			mPane.GraphObjList.Clear();
			
			//清空所有的字段标记
			sbR2 = new StringBuilder("");
			sbNSER2 = new StringBuilder("NSE    R\u00B2:");
            sbCEAR2 = new StringBuilder("CEA    R\u00B2:");
            sbDKK1R2 = new StringBuilder("DKK1  R\u00B2:");
            sbCY211R2 = new StringBuilder("CY211 R\u00B2:");
			
			//初始化各个R2的值
			switch(curveComboBox.SelectedIndex)
			{
				case 0:
					sbR2.Append(stringL4P);
					sbCEAR2.Append(mL4P["CEA"].rsquare.ToString("f4"));
					sbNSER2.Append(mL4P["NSE"].rsquare.ToString("f4"));
					sbDKK1R2.Append(mL4P["DKK1"].rsquare.ToString("f4"));
					sbCY211R2.Append(mL4P["CY211"].rsquare.ToString("f4"));
					break;
				case 1:
					sbR2.Append(stringL3P);
					sbCEAR2.Append(mL3P["CEA"].rsquare.ToString("f4"));
					sbNSER2.Append(mL3P["NSE"].rsquare.ToString("f4"));
					sbDKK1R2.Append(mL3P["DKK1"].rsquare.ToString("f4"));
					sbCY211R2.Append(mL3P["CY211"].rsquare.ToString("f4"));
					break;
				case 2:
					sbR2.Append(stringLE4P);
					sbCEAR2.Append(mLE4P["CEA"].rsquare.ToString("f4"));
					sbNSER2.Append(mLE4P["NSE"].rsquare.ToString("f4"));
					sbDKK1R2.Append(mLE4P["DKK1"].rsquare.ToString("f4"));
					sbCY211R2.Append(mLE4P["CY211"].rsquare.ToString("f4"));
					break;
			}
			sbR2.Append("\n");
			
			if(NSEcheckBox.Checked == true)
			{
				sbR2.Append(sbNSER2);
				sbR2.Append("\n");
				
				drawNSECurve();
			}
			
			if(CEAcheckBox.Checked == true)
			{
				sbR2.Append(sbCEAR2);
				sbR2.Append("\n");;
				drawCEACurve();
			}
			
			if(CY211checkBox.Checked == true)
			{
				sbR2.Append(sbCY211R2);
				sbR2.Append("\n");
				drawCY211Curve();
			}
			
			if(DKK1checkBox.Checked == true)
			{
				sbR2.Append(sbDKK1R2);
				sbR2.Append("\n");
				drawDKK1Curve();
			}
			
			mPane.Title.Text = "OD--浓度曲线";
			mPane.XAxis.Title.Text = "浓度(ng/ml)";
			mPane.YAxis.Title.Text = "OD";
			
			
			TextObj text = new TextObj(sbR2.ToString(), 0.05f, 0.05f, CoordType.ChartFraction, AlignH.Left, AlignV.Top);
			text.FontSpec.StringAlignment = StringAlignment.Near;
			mPane.GraphObjList.Add(text);
			
			setSize();
			
			zGControl.AxisChange();
			zGControl.Invalidate();
		}
		
		
		public void setSize()
		{
			zGControl.Location = new Point(10,10);

		}
		
		
		private void calMinMax(out double min, out double max, string name, int index)
		{
			//2014.11.18 比较好的方式是，计算其拐点，用拐点来作为最大或者最小值，
			//double maxT, minT 
			double maxT = 0;
			double minT = 0;
			
			//暂定差值在max 0
			//min 在 0
			double diffMax = 0.000000001;
			double diffMin = 0.000000001;
			int ind = 0;
			double cen = 0;
			switch(name)
			{
				case "NSE":
					ind = 1;
					break;
				case "CEA":
					ind = 2;
					break;
					
				case "DKK1":
					ind = 3;
					break;
					
				case "CY211":
					ind = 4;
					break;
				default:
					ind = 5;
					break;
			}
			
			switch(index)
			{
				case 1:
					//L4p
					max = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P[name].cf, (MWNumericArray) (mL4P[name].cf[3]) ))).ToScalarDouble();
					min = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P[name].cf, (MWNumericArray) (mL4P[name].cf[0]) ))).ToScalarDouble();
					maxT = mL4P[name].cf[3];
					minT = mL4P[name].cf[0];
					
					foreach(double d in mMainForm.mConcentrationMaps.Keys)
					{
						switch(ind)
						{
							case 1:
								
								cen = mMainForm.mConcentrationMaps[d].AveNSE;
								break;
								
							case 2:
								
								cen = mMainForm.mConcentrationMaps[d].AveCEA;
								break;
								
								
							case 3 :
								
								cen = mMainForm.mConcentrationMaps[d].AveDKK1;
								break;
							case 4:
								
								cen = mMainForm.mConcentrationMaps[d].AveCY211;
								break;
											
						}
						
						if(Math.Abs(maxT - cen) < diffMax && d < max)
						{
							max = d;
						}
					}
					
					foreach(double d in mMainForm.mConcentrationMaps.Keys)
					{
						switch(ind)
						{
							case 1:
								
								cen = mMainForm.mConcentrationMaps[d].AveNSE;
								break;
								
							case 2:
								
								cen = mMainForm.mConcentrationMaps[d].AveCEA;
								break;
								
								
							case 3 :
								
								cen = mMainForm.mConcentrationMaps[d].AveDKK1;
								break;
							case 4:
								
								cen = mMainForm.mConcentrationMaps[d].AveCY211;
								break;
											
						}
						
						
						if(Math.Abs(minT - cen) < diffMin && d > min)
						{
							min = d;
							
						}
					}
					
					//如果计算出来是正无穷大
					//设置为125
					if(max > 100000)
						max = 125;
					break;
					
				case 2:
					
					max = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P[name].cf, (MWNumericArray) (mL3P[name].cf[0]) ))).ToScalarDouble();
					min = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P[name].cf, (MWNumericArray) (mL3P[name].cf[2]) ))).ToScalarDouble();
					maxT = mL3P[name].cf[0];
					minT = mL3P[name].cf[2];
					
					foreach(double d in mMainForm.mConcentrationMaps.Keys)
					{
						switch(ind)
						{
							case 1:
								
								cen = mMainForm.mConcentrationMaps[d].AveNSE;
								break;
								
							case 2:
								
								cen = mMainForm.mConcentrationMaps[d].AveCEA;
								break;
								
								
							case 3 :
								
								cen = mMainForm.mConcentrationMaps[d].AveDKK1;
								break;
							case 4:
								
								cen = mMainForm.mConcentrationMaps[d].AveCY211;
								break;
											
						}
						
						if(Math.Abs(maxT - cen) < diffMax && d < max)
						{
							max = d;
						}
					}
					
					foreach(double d in mMainForm.mConcentrationMaps.Keys)
					{
						switch(ind)
						{
							case 1:
								
								cen = mMainForm.mConcentrationMaps[d].AveNSE;
								break;
								
							case 2:
								
								cen = mMainForm.mConcentrationMaps[d].AveCEA;
								break;
								
								
							case 3 :
								
								cen = mMainForm.mConcentrationMaps[d].AveDKK1;
								break;
							case 4:
								
								cen = mMainForm.mConcentrationMaps[d].AveCY211;
								break;
											
						}
						
						
						if(Math.Abs(minT - cen) < diffMin && d > min)
						{
							min = d;
							
						}
					}
					
					//如果计算出来是正无穷大
					//设置为125
					if(max > 10000)
						max = 125;
					break;
				case 3:
					
					max = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P[name].cf, (MWNumericArray) (mLE4P[name].cf[3]) ))).ToScalarDouble();
					min = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P[name].cf, (MWNumericArray) (mLE4P[name].cf[0]) ))).ToScalarDouble();
					
					maxT = mLE4P[name].cf[3];
					minT = mLE4P[name].cf[0];
					
					foreach(double d in mMainForm.mConcentrationMaps.Keys)
					{
						switch(ind)
						{
							case 1:
								
								cen = mMainForm.mConcentrationMaps[d].AveNSE;
								break;
								
							case 2:
								
								cen = mMainForm.mConcentrationMaps[d].AveCEA;
								break;
								
								
							case 3 :
								
								cen = mMainForm.mConcentrationMaps[d].AveDKK1;
								break;
							case 4:
								
								cen = mMainForm.mConcentrationMaps[d].AveCY211;
								break;
											
						}
						
						if(Math.Abs(maxT - cen) < diffMax && d < max)
						{
							max = d;
						}
					}
					
					foreach(double d in mMainForm.mConcentrationMaps.Keys)
					{
						switch(ind)
						{
							case 1:
								
								cen = mMainForm.mConcentrationMaps[d].AveNSE;
								break;
								
							case 2:
								
								cen = mMainForm.mConcentrationMaps[d].AveCEA;
								break;
								
								
							case 3 :
								
								cen = mMainForm.mConcentrationMaps[d].AveDKK1;
								break;
							case 4:
								
								cen = mMainForm.mConcentrationMaps[d].AveCY211;
								break;
											
						}
						
						
						if(Math.Abs(minT - cen) < diffMin && d > min)
						{
							min = d;
							
						}
					}
					
					//如果计算出来是正无穷大
					//设置为125
					if(max > 10000)
						max = 125;
					break;
				default:
					min = 0;
					max = 0;
					
					break;
			}
						
		}
		
		
		/// <summary>
		/// 通过Y值来计算x值
		/// </summary>
		/// <param name="y"></param>
		/// <returns></returns>
		string calcConcentration(double y)
		{
			//新增了对浓度拟合的提示，当所填写的OD值大于最大值或者小于最小值的时候，
			//输出为 大于125
			//或者小于1.98
			//2014.11.12
			if(mMainForm.hasL4Pcurve == true)
			{
				if(NSEcheckBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "NSE", 1);
					
                    //if( y > mL4P["NSE"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
                    //}
                    //else if( y < mL4P["NSE"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["NSE"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				
				if(CEAcheckBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "CEA", 1);
                    //if( y > mL4P["CEA"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mL4P["CEA"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					
					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["CEA"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(DKK1checkBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "DKK1", 1);
                    //if( y > mL4P["DKK1"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mL4P["DKK1"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["DKK1"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(CY211checkBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "CY211", 1);
                    //if( y > mL4P["CY211"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mL4P["CY211"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					
					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["CY211"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
			}
			
			if(mMainForm.hasL3Pcurve == true)
			{
				if(NSEcheckBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "NSE", 2);
                    //if( y > mL3P["NSE"].cf[0] )
                    //{
                    //    return ">max:" + maxCen;
                    //}
                    //else if( y < mL3P["NSE"].cf[2] )
                    //{
                    //    return "<min:" + minCen;
                    //}
					
					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["NSE"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(CEAcheckBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "CEA", 2);
                    //if( y > mL3P["CEA"].cf[0] )
                    //{
                    //    return ">max:" + maxCen;
                    //}
                    //else if( y < mL3P["CEA"].cf[2] )
                    //{
                    //    return "<min:" + minCen;
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["CEA"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(DKK1checkBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "DKK1", 2);
                    //if( y > mL3P["DKK1"].cf[0] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mL3P["DKK1"].cf[2] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["DKK1"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(CY211checkBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "CY211", 2);
                    //if( y > mL3P["CY211"].cf[0] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mL3P["CY211"].cf[2] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["CY211"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
			}
			
			if(mMainForm.hasLE4Pcurve == true)
			{
				
				if(NSEcheckBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "NSE", 3);
                    //if( y > mLE4P["NSE"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mLE4P["NSE"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["NSE"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(CEAcheckBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "CEA", 3);
                    //if( y > mLE4P["CEA"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mLE4P["CEA"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["CEA"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					 
				}
				if(DKK1checkBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "DKK1", 3);
					
                    //if( y > mLE4P["DKK1"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
						 
                    //}
                    //else if( y < mLE4P["DKK1"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
						 
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["DKK1"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
					
				}
				if(CY211checkBox.Checked == true)
				{
                    //calMinMax(out minCen, out maxCen, "CY211", 3);
					
                    //if( y > mLE4P["CY211"].cf[3] )
                    //{
                    //    return ">max:" + maxCen;
                    //}
                    //else if( y < mLE4P["CY211"].cf[0] )
                    //{
                    //    return "<min:" + minCen;
                    //}
					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["CY211"].cf, (MWNumericArray)y))).ToScalarDouble();
					return concentration.ToString("f2");
				}
			}
			
			return null;
			
		}
			
		
		
		
		/// <summary>
		/// 通过OD值来计算其浓度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CalculateButtonClick(object sender, EventArgs e)
		{
			

			//2015.4.2 
			// add: 添加一次可以计算一列数据的情况。
			//string str = ODTextbox.Text;
			
			StringBuilder strBuilder = new StringBuilder(ODTextbox.Text);
			
			//strBuilder = strBuilder.Replace('\n', ' ');
			//strBuilder = strBuilder.Replace('\r', ' ');
			
			string str = strBuilder.ToString();
			
			if(string.IsNullOrEmpty(str))
				return;
			
			//去除结尾的空格符，否则在split之后会生成一个空的字符串
			str = str.TrimEnd(new char[]{' '});
			
			
			string[] lines = str.Split(null);
			string[] outputConcentration = new string[lines.Length];
			
			for ( int i = 0; i < lines.Length; ++i)
 			{
				if(lines[i] == "")
					continue;
				
				double y = 0;
				try {
					double temp = double.Parse( lines[i] );
					y = temp;
				} catch (Exception) {
					MessageBox.Show("灰度输入有误，请重新输入");
					return;
				}
				outputConcentration[i] = calcConcentration(y);
			}
			
			strBuilder = new StringBuilder();
			for( int i = 0; i < outputConcentration.Length; ++i)
			{
				if(string.IsNullOrEmpty(outputConcentration[i]) )
				   continue;
				
//				strBuilder.Append(outputConcentration[i] + "\n\r");
				strBuilder.Append(outputConcentration[i] + "\r\n");
			}
			
			concentrationLabel.Text = strBuilder.ToString();
			
			
			
// 			这是一次只能计算一个结果的情况
//			double y = 0;
//			try
//			{
//				double temp = double.Parse(ODTextbox.Text);
//				y = temp;
//			}
//			catch
//			{
//				MessageBox.Show("灰度输入有误，请重新输入。");
//				return;
//			}
			
			
			
//			concentrationLabel.Text = calcConcentration(y);
//			concentration = mFit.
			//新增了对浓度拟合的提示，当所填写的OD值大于最大值或者小于最小值的时候，
			//输出为 大于125
			//或者小于1.98
			//2014.11.12
//			if(mMainForm.hasL4Pcurve == true)
//			{
//				if(NSEcheckBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "NSE", 1);
//					
//					if( y > mL4P["NSE"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL4P["NSE"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["NSE"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				
//				if(CEAcheckBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "CEA", 1);
//					if( y > mL4P["CEA"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL4P["CEA"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					
//					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["CEA"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(DKK1checkBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "DKK1", 1);
//					if( y > mL4P["DKK1"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL4P["DKK1"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["DKK1"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(CY211checkBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "CY211", 1);
//					if( y > mL4P["CY211"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL4P["CY211"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					
//					concentration = ((MWNumericArray)(mMainForm.mFit.L4Pinv((MWNumericArray)mL4P["CY211"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//			}
//			
//			if(mMainForm.hasL3Pcurve == true)
//			{
//				if(NSEcheckBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "NSE", 2);
//					if( y > mL3P["NSE"].cf[0] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL3P["NSE"].cf[2] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					
//					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["NSE"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(CEAcheckBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "CEA", 2);
//					if( y > mL3P["CEA"].cf[0] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL3P["CEA"].cf[2] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["CEA"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(DKK1checkBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "DKK1", 2);
//					if( y > mL3P["DKK1"].cf[0] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL3P["DKK1"].cf[2] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["DKK1"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(CY211checkBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "CY211", 2);
//					if( y > mL3P["CY211"].cf[0] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mL3P["CY211"].cf[2] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.L3Pinv((MWNumericArray)mL3P["CY211"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//			}
//			
//			if(mMainForm.hasLE4Pcurve == true)
//			{
//				
//				if(NSEcheckBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "NSE", 3);
//					if( y > mLE4P["NSE"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mLE4P["NSE"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["NSE"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(CEAcheckBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "CEA", 3);
//					if( y > mLE4P["CEA"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mLE4P["CEA"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["CEA"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(DKK1checkBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "DKK1", 3);
//					
//					if( y > mLE4P["DKK1"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mLE4P["DKK1"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["DKK1"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//				if(CY211checkBox.Checked == true)
//				{
//					calMinMax(out minCen, out maxCen, "CY211", 3);
//					
//					if( y > mLE4P["CY211"].cf[3] )
//					{
//						concentrationLabel.Text = ">" + maxCen;
//						return;
//					}
//					else if( y < mLE4P["CY211"].cf[0] )
//					{
//						concentrationLabel.Text = "<" + minCen;
//						return;
//					}
//					concentration = ((MWNumericArray)(mMainForm.mFit.LE4Pinv((MWNumericArray)mLE4P["CY211"].cf, (MWNumericArray)y))).ToScalarDouble();
//					concentrationLabel.Text = concentration.ToString("f2");
//					return;
//				}
//			}
			
			
		}
		
		
		
		
		
		
		/// <summary>
		/// load 函数，在Form最后呈现之前是会显示的。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ShowTheCurveLoad(object sender, EventArgs e)
		{
			//当鼠标定位到某一个点上的时候，会显示这一点的数据。
			zGControl.IsShowPointValues = false;
			zGControl.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);
			
			mPane.XAxis.ScaleFormatEvent += new XAxis.ScaleFormatHandler( MyScaleFormatEventHandler);
			
			mPane.XAxis.MajorGrid.IsVisible = false;
			mPane.XAxis.MinorGrid.IsVisible = false;
			mPane.XAxis.MajorTic.IsOpposite = false;
			mPane.XAxis.MinorTic.Size = 0;
			mPane.XAxis.MinorTic.IsOpposite = false;
			
			
			mPane.YAxis.MajorGrid.IsVisible = false;
			mPane.YAxis.MinorGrid.IsVisible = false;
			mPane.YAxis.MajorTic.IsOpposite = false;
			mPane.YAxis.MinorTic.Size = 0;
			mPane.YAxis.MinorTic.IsOpposite = false;
			
			
			mPane.XAxis.Scale.Min = -0.5;
			mPane.XAxis.Scale.Max = 2.82;

            curveComboBox.SelectedIndex = 0;
            mMainForm.hasL4Pcurve = true;
            mMainForm.hasL3Pcurve = false;
            mMainForm.hasLE4Pcurve = false;
            fitTheCurve(4);
            reviseThePoint(4);
            calculateThePoints(4);

            //drawTheCurve();
		}
		
		/// <summary>
		/// 显示点数据响应事件
		/// </summary>
		/// <param name="c"></param>
		/// <param name="p"></param>
		/// <param name="curve"></param>
		/// <param name="iPt"></param>
		/// <returns></returns>
		private string MyPointValueHandler(ZedGraphControl c, GraphPane p, CurveItem curve, int iPt)
		{
			PointPair pt = curve[iPt];

            return "浓度是：" + Math.Pow(10, pt.X) + "\nOD 是：" + pt.Y;
		}
		
		
		/// <summary>
		/// x坐标轴的重新显示
		/// </summary>
		/// <param name="gp"></param>
		/// <param name="axis"></param>
		/// <param name="val"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private string MyScaleFormatEventHandler(GraphPane gp, Axis axis, double val, int index)
		{
			return Math.Pow(10, val).ToString("f2");
		}
		
		
		void FitStripMenuItemClick(object sender, EventArgs e)
		{
			switch(curveComboBox.SelectedIndex)
			{
				case 0:
					//拟合曲线 L4P
					mMainForm.hasL4Pcurve = true;
					mMainForm.hasL3Pcurve = false;
					mMainForm.hasLE4Pcurve = false;
					fitTheCurve(4);
                    reviseThePoint(4);
					calculateThePoints(4);
					
//					drawTheCurve();
					mMainForm.setTheCurveFlag(4);
					break;
				case 1:
					//L3P
					mMainForm.hasL4Pcurve = false;
					mMainForm.hasL3Pcurve = true;
					mMainForm.hasLE4Pcurve = false;
					fitTheCurve(3);
                    reviseThePoint(3);
					calculateThePoints(3);
					
//					drawTheCurve();
					mMainForm.setTheCurveFlag(3);
					break;
				case 2:
					//LE4P
					mMainForm.hasL4Pcurve = false;
					mMainForm.hasL3Pcurve = false;
					mMainForm.hasLE4Pcurve = true;
					fitTheCurve(5);
                    reviseThePoint(5);
					calculateThePoints(5);
					
//					drawTheCurve();
					mMainForm.setTheCurveFlag(5);
					break;
			}
			
			drawTheCurve();
		}
		
		
		
		void NSEcheckBoxCheckedChanged(object sender, EventArgs e)
		{
			if(isFit == false)
				return;
			drawTheCurve();
		}
		
		void CEAcheckBoxCheckedChanged(object sender, EventArgs e)
		{
			if(isFit == false)
				return;
			drawTheCurve();
		}
		
		void CY211checkBoxCheckedChanged(object sender, EventArgs e)
		{
			if(isFit == false)
				return;
			drawTheCurve();
		}
		
		void DKK1checkBoxCheckedChanged(object sender, EventArgs e)
		{
			if(isFit == false)
				return;
			drawTheCurve();
		}
		
		void SavsDataMenuitemClick(object sender, EventArgs e)
		{
            saveFileDataDialog.Filter = "拟合文件(*.SIMIT)|*.SIMIT";
			saveFileDataDialog.RestoreDirectory = true;
			string filePath;
			if(saveFileDataDialog.ShowDialog() == DialogResult.OK)
			{
				if(saveFileDataDialog.FileName != "")
				{
					filePath = saveFileDataDialog.FileName;
					storeData sd = new storeData(listNSE, listCEA, listDKK1, listCY211, 
					                             conlistNSE, conlistCEA, conlistDKK1, conlistCY211,
					                             mL4P, mL3P, mLE4P,
					                             mMainForm.hasL4Pcurve, mMainForm.hasL3Pcurve, mMainForm.hasLE4Pcurve,
					                            mMainForm.mConcentrationMaps);
					FileStream fs = new FileStream(filePath, FileMode.Create);
					BinaryFormatter bf = new BinaryFormatter();
					bf.Serialize(fs, sd);
					fs.Close();
				}
			}
			
		}

        private void reviseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isFit == false)
                return;
            drawTheCurve();
        }

	    private void reviseThePoint(int i)
        {
            reviseConNSE.Clear();
            reviseConCEA.Clear();
            reviseConDKK1.Clear();
            reviseConCY211.Clear();

            switch (i)
            {
                case 4:
                    reviseTheL4PPoints();
                    break;
                case 3:
                    reviseTheL3PPoints();
                    break;
                case 5:
                    reviseTheLE4PPoints();
                    break;
            }
	    }

        private void reviseTheLE4PPoints()
        {
            LE4PCurve le4p = null;
            MWArray result = null;
            double numb = 0;

            for (int i = 0; i < x.Count(); i++)
            {
                double xp = Math.Log10(x[i]);//要先去对数在进行拟合
                le4p = mLE4P["NSE"];
                double SNSE = Math.Sqrt(le4p.sse / le4p.dfe);//理应为rmse的，但估计matlab程序有bug，只能变通用sse和dfe转化
                //double rm1 = le4p.rmse;
                //double df1 = le4p.dfe;
                result = mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yNSE[i] - numb) < 2 * SNSE)
                {
                    reviseConNSE.Add(Math.Log10(x[i]), yNSE[i]);
                }

                le4p = mLE4P["CEA"];
                double SCEA = Math.Sqrt(le4p.sse / le4p.dfe);
                //double rm2 = le4p.rmse;
                //double df2 = le4p.dfe;
                result = mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yCEA[i] - numb) < 2 * SCEA)
                {
                    reviseConCEA.Add(Math.Log10(x[i]), yCEA[i]);
                }

                le4p = mLE4P["DKK1"];
                double SDKK1 = Math.Sqrt(le4p.sse / le4p.dfe);
                //double rm3 = le4p.rmse;
                //double df3 = le4p.dfe;
                result = mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yDKK1[i] - numb) < 2 * SDKK1)
                {
                    reviseConDKK1.Add(Math.Log10(x[i]), yDKK1[i]);
                }

                le4p = mLE4P["CY211"];
                double SCY211 = Math.Sqrt(le4p.sse / le4p.dfe);
                //double rm4 = le4p.rmse;
                //double df4 = le4p.dfe;
                result = mFit.LE4PtoY((MWNumericArray)le4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yCY211[i] - numb) < 2 * SCY211)
                {
                    reviseConCY211.Add(Math.Log10(x[i]), yCY211[i]);
                }
            }
        }

        private void reviseTheL3PPoints()
        {
            L3Pcurve l3p = null;
            MWArray result = null;
            double numb = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                double xp = Math.Log10(x[i]);

                l3p = mL3P["NSE"];
                double SNSE = Math.Sqrt(l3p.sse / l3p.dfe);
                //double rm1 = l3p.rmse;
                //double df1 = l3p.dfe;
                result = mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yNSE[i] - numb) < 2 * SNSE)
                {
                    reviseConNSE.Add(Math.Log10(x[i]), yNSE[i]);
                }

                l3p = mL3P["CEA"];
                double SCEA = Math.Sqrt(l3p.sse / l3p.dfe);
                //double rm2 = l3p.rmse;
                //double df2 = l3p.dfe;
                result = mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yCEA[i] - numb) < 2 * SCEA)
                {
                    reviseConCEA.Add(Math.Log10(x[i]), yCEA[i]);
                }

                l3p = mL3P["DKK1"];
                double SDKK1 = Math.Sqrt(l3p.sse / l3p.dfe);
                //double rm3 = l3p.rmse;
                //double df3 = l3p.dfe;
                result = mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yDKK1[i] - numb) < 2 * SDKK1)
                {
                    reviseConDKK1.Add(Math.Log10(x[i]), yDKK1[i]);
                }

                l3p = mL3P["CY211"];
                double SCY211 = Math.Sqrt(l3p.sse / l3p.dfe);
                //double rm4 = l3p.rmse;
                //double df4 = l3p.dfe;
                result = mFit.L3PtoY((MWNumericArray)l3p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yCY211[i] - numb) < 2 * SCY211)
                {
                    reviseConCY211.Add(Math.Log10(x[i]), yCY211[i]);
                }
            }
        }

        //去掉异常值,原理是：回归残差服从正态分布的随机变量，其取值95%左右的概率
        //应分布在均值加减2倍标准差的范围内,99%在三倍
        private void reviseTheL4PPoints()
        {
            L4PCurve l4p = null;
            MWArray result = null;
            double numb = 0;

            for (int i = 0; i < x.Count(); i++)
            {
                double xp = Math.Log10(x[i]);

                l4p = mL4P["NSE"];
                double SNSE = Math.Sqrt(l4p.sse / l4p.dfe);
                //double rm1 = l4p.rmse;
                //double df1 = l4p.dfe;
                result = mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yNSE[i] - numb) < 2 * SNSE)
                {
                    reviseConNSE.Add(Math.Log10(x[i]), yNSE[i]);
                }

                l4p = mL4P["CEA"];
                double SCEA = Math.Sqrt(l4p.sse / l4p.dfe);
                //double rm2 = l4p.rmse;
                //double df2 = l4p.dfe;
                result = mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yCEA[i] - numb) < 2 * SCEA)
                {
                    reviseConCEA.Add(Math.Log10(x[i]), yCEA[i]);
                }

                l4p = mL4P["DKK1"];
                double SDKK1 = Math.Sqrt(l4p.sse / l4p.dfe);
                //double rm3 = l4p.rmse;
                //double df3 = l4p.dfe;
                result = mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yDKK1[i] - numb) < 2 * SDKK1)
                {
                    reviseConDKK1.Add(Math.Log10(x[i]), yDKK1[i]);
                }

                l4p = mL4P["CY211"];
                double SCY211 = Math.Sqrt(l4p.sse / l4p.dfe);
                //double rm4 = l4p.rmse;
                //double df4 = l4p.dfe;
                result = mFit.L4PtoY((MWNumericArray)l4p.cf, (MWNumericArray)xp);
                numb = ((MWNumericArray)result).ToScalarDouble();
                if (Math.Abs(yCY211[i] - numb) < 2 * SCY211)
                {
                    reviseConCY211.Add(Math.Log10(x[i]), yCY211[i]);
                }
            }
        }
        
        private void curveComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (mMainForm != null)//避免窗体没有加载出来就执行导致错误
            {
                FitStripMenuItemClick(sender, e);
            }
        }

	}//class
}






































