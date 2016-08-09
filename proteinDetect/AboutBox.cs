using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace proteinDetect
{
    public partial class AboutBox : Form
    {
        private string m_name = "蛋白浓度检测";
        private string m_Version = "1.0.4";
        private string m_Copyright = "中科院上海微系统与信息技术研究所一室DNA";
        private string[] m_Description = { "1.现阶段照片尺寸必须为：1360 * 1024 \r\n" +
                                           "2.操作的时候最好不要放大缩小图片 \r\n" +
                                           "3.联系方式：lee0220@mail.sim.ac.cn(有问题请联系此邮箱)" };

        public AboutBox()
        {
            InitializeComponent();

            this.textBoxDescription.Lines = m_Description;
            this.labelProductName.Text = string.Format("软件名称 : {0}", m_name);
            this.labelVersion.Text = string.Format("版本 : {0}", m_Version);
            this.labelCopyright.Text = string.Format("版权 ：{0}", m_Copyright);
        }
    }
}
