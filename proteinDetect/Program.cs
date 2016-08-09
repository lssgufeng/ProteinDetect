using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace proteinDetect
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Thread to show splash window
                Thread thUI = new Thread(new ThreadStart(ShowSplashWindow));
                thUI.Name = "Splash UI";
                thUI.Priority = ThreadPriority.Normal;
                thUI.IsBackground = true;
                thUI.Start();
                //等待界面加载出来
                while (SplashForm == null)
                {

                }

                //thread to load time-consuming resources
                Thread th = new Thread(new ThreadStart(LoadResources));
                th.Name = "Resource Loader";
                th.Priority = ThreadPriority.Highest;
                th.Start();

                th.Join();

                if (SplashForm != null)
                {
                    SplashForm.Invoke(new MethodInvoker(delegate { SplashForm.Close(); }));
                }

                thUI.Join();
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static splash SplashForm
        {
            get; set;
        }

        private static void ShowSplashWindow()
        {
            SplashForm = new splash();
            Application.Run(SplashForm);
        }

        private static void LoadResources()
        {
            if (SplashForm != null)
            {
                SplashForm.Invoke(new MethodInvoker(delegate { SplashForm.labelStatus.Text = "检测所需文件"; }));
                Thread.Sleep(2000);
                SplashForm.Invoke(new MethodInvoker(delegate { SplashForm.labelStatus.Text = "检测运行环境"; }));
                Thread.Sleep(2000);

                string s1 = Environment.GetEnvironmentVariable("path");
                if (s1 == null)
                {
                    SplashForm.Invoke(
                        new MethodInvoker(delegate { SplashForm.labelStatus.Text = "曲线拟合所需编译环境没有安装，软件3秒钟后退出！"; }));
                    Thread.Sleep(3000);
                    Application.Exit();
                }
                if (s1.Contains("MATLAB Compiler Runtime"))
                {
                    SplashForm.Invoke(
                        new MethodInvoker(delegate { SplashForm.labelStatus.Text = "检测完成，马上进入软件！"; }));
                    Thread.Sleep(2000);
                }
                else
                {
                    SplashForm.Invoke(
                        new MethodInvoker(delegate { SplashForm.labelStatus.Text = "曲线拟合所需编译环境没有安装，软件3秒钟后退出！"; }));
                    Thread.Sleep(3000);
                    Application.Exit();
                }
            }
        }
    }
}
