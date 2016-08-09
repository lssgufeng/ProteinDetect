using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace proteinDetect
{
    public partial class showResult : Form
    {
        List<CircleAnalyse> m_list = new List<CircleAnalyse>(30);
        private volatile float[] m_meanOfSix = new float[4];//存储每行的平均值
        private volatile int[] m_numberOfRow = new int[4];//存储每行的元素个数
        private volatile float m_meanOptical = 0.0f;
        private ConcentrationInfo mConcentration = null;
        private MainForm mMainForm = null;
        private String mFilename = null;
        //private bool saveFlag = false;//浓度保存标志

        public showResult()
        {
            InitializeComponent();
        }

        public showResult(List<CircleAnalyse> list)
        {
            InitializeComponent();

            //获得父窗口的句柄，从而可以获取父窗口的各个属性值。
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(MainForm))
                {
                    mMainForm = (MainForm)f;
                }
            }
            showTheResult(list);
        }

        /// <summary>
        /// 显示数据到表格当中，这边应该可以优化
        /// </summary>
        /// <param name="list"></param>
        public void showTheResult(List<CircleAnalyse> list)
        {
            //获得父窗口的句柄，从而可以获取父窗口的各个属性值
            FormCollection testFormC = Application.OpenForms;
            foreach (Form f in testFormC)
            {
                if (f.GetType() == typeof(MainForm))
                {
                    mMainForm = (MainForm)f;
                    break;
                }
            }

            if (this.resultGrid.Rows.Count != 0)
            {
                resultGrid.Rows.Clear();
            }
            m_list = list;
            //应该对数据进行清零操作
            //不然当不新建这个类的实例的时候，会继承上一次操作的数据
            clearArrays();

            //取出图像的名称
            foreach (Form f in Application.OpenForms)
            {
                //将荧光图片的数据导入。
                if (f.GetType() == typeof(ChildForm))
                {

                    mFilename = f.Text;

                    break;
                }
            }

            //记录需要删除的圆的位置
            List<int> index = new List<int>();
            int[] columns = new int[4];
            double[] mean = new double[4];

            //第一轮循环求出每行的均值
            foreach (CircleAnalyse ca in m_list)
            {
                int i = ca.M_row;
                int j = ca.M_col;

                try
                {
                    //统计每一行的列数，圆的个数
                    // i - 1 表示数组的下标从0开始
                    if (j > columns[i - 1])
                    {
                        columns[i - 1] = j;
                    }
                    mean[i - 1] += ca.M_meanGray;
                }
                catch (Exception)
                {
                    MessageBox.Show("请注意对齐每行的圆");
                    return;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (columns[i] == 0)
                {
                    continue;
                }
                mean[i] /= columns[i];
            }

            //第二层循环找出找出需要剔除的圆的index
            foreach (CircleAnalyse ca in m_list)
            {
                string[] row = new string[4];
                int i = ca.M_row;
                int j = ca.M_col;
                string name = "";
                m_meanOfSix[i - 1] += ca.M_meanOpticalDensity;

                //统计每一行的列数，圆的个数
                if (j > m_numberOfRow[i - 1])
                {
                    m_numberOfRow[i - 1] = j;
                }

                switch (i - 1)
                {
                    case 0:
                        {
                            name = "NSE-";
                            break;
                        }
                    case 1:
                        {
                            name = "CEA-";
                            break;
                        }
                    case 2:
                        {
                            name = "CYF211-";
                            break;
                        }
                    case 3:
                        {
                            name = "DKK1-";
                            break;
                        }
                }

                row[0] = (name + j).ToString();
                row[1] = ca.M_area.ToString();
                row[2] = ca.M_meanGray.ToString();
                row[3] = ca.M_meanOpticalDensity.ToString();
                resultGrid.Rows.Add(row);
            }

            for (int i = 0; i < 4; i++)
            {
                //跳过列数为0的行
                if (m_numberOfRow[i] == 0)
                {
                    continue;
                }

                string[] row = new string[2];
                string name = "";

                switch (i%4)
                {
                    case 0:
                    {
                        name = "NSE均值";
                        break;
                    }
                    case 1:
                    {
                        name = "CEA均值";
                        break;
                    }
                    case 2:
                    {
                        name = "CYF211均值";
                        break;
                    }
                    case 3:
                    {
                        name = "DKK1均值";
                        break;
                    }
                }
                row[0] = name;
                //避免产生除0的问题。
                if (m_numberOfRow[i] > 0)
                {
                    m_meanOfSix[i] /= m_numberOfRow[i];
                }
                row[1] = m_meanOfSix[i] + "";
                resultGrid.Rows.Add(row);
            }

            //生成类信息，存储浓度信息
            mConcentration = new ConcentrationInfo(m_meanOfSix[0], m_meanOfSix[1], m_meanOfSix[2], m_meanOfSix[3]);
            //把浓度信息显示出来
            if (mMainForm.pathConList.ContainsKey(mMainForm.picturePath))
            {
                concenStripTextBox.Text = string.Format("{0}   浓度已存", mMainForm.pathConList[mMainForm.picturePath]);
                //显示浓度信息
                string[] row = new string[2];
                row[0] = "浓度";
                row[1] = (mMainForm.pathConList[mMainForm.picturePath]).ToString();
                resultGrid.Rows.Add(row);
            }
        }

        private void clearArrays()
        {
            for (int i = m_meanOfSix.Length; i > 0; i--)
            {
                m_meanOfSix[i - 1] = 0.0f;
                m_numberOfRow[i - 1] = 0;
            }
        }

        private void CalcMeanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcMeanClick();
        }
        //计算平均值
        private void CalcMeanClick()
        {
            int i = 0;
            string[] row = new string[2];
            string rows = "";
            int selectNumber = resultGrid.SelectedCells.Count;
            float meanValue = 0.0f;

            if (selectNumber == 0 || resultGrid.SelectedCells[0].OwningColumn.HeaderText == "序号")
            {
                MessageBox.Show("请选择要求取平均值的单元格。");
                return;
            }
            else
            {
                for (i = 0; i < selectNumber; i++)
                {
                    meanValue += float.Parse(resultGrid.SelectedCells[i].FormattedValue.ToString());
                    rows += (resultGrid.SelectedCells[i].OwningRow.Index + 1).ToString() + "，";
                }

                //rows.Remove(rows.Length - 3);
                rows += "的" + resultGrid.SelectedCells[0].OwningColumn.HeaderText;
                rows += "的平均值是";
                meanValue /= selectNumber;
            }
            row[0] = rows;
            row[1] = meanValue.ToString();
            resultGrid.Rows.Add(row);
        }

        private void 导出到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet1");
            IRow row = sheet.CreateRow(0);

            //更新第一行中的内容为行的标号
            for (int i = 0; i < resultGrid.Rows[0].Cells.Count; i++)
            {
                row.CreateCell(i).SetCellValue(resultGrid.Rows[0].Cells[i].OwningColumn.HeaderText);
            }

            //将每一行的值填入
            int sumCount = 0;
            for (int i = 0; i < resultGrid.Rows.Count - 1; i++)
            {
                IRow rowTemp = sheet.CreateRow(i + 1);

                for (int j = 0; j < resultGrid.Rows[i].Cells.Count; j++)
                {
                    if (j == 0)
                    {
                        rowTemp.CreateCell(j).SetCellValue(resultGrid.Rows[i].Cells[j].Value.ToString());
                        continue;
                    }

                    if (resultGrid.Rows[i].Cells[j].Value == null)
                    {
                        continue;
                    }

                    float temp = float.Parse(resultGrid.Rows[i].Cells[j].Value.ToString());

                    rowTemp.CreateCell(j).SetCellValue(temp);
                    if (j == resultGrid.Rows[i].Cells.Count - 1)
                    {
                        //将灰度值存储到m_meanGray中。
                        m_meanOptical += temp;
                    }
                }
                sumCount++;
            }
            m_meanOptical /= sumCount;

            String filename = "";
            foreach (Form f in Application.OpenForms)
            {
                //将荧光图片的数据导入。
                if (f.GetType() == typeof(ChildForm))
                {

                    filename = f.Text;

                    break;
                }
            }

            saveToExcel.Filter = "excel文件(*.xls)|*.xls";
            saveToExcel.RestoreDirectory = true;
            string filepath;

            if (saveToExcel.ShowDialog() == DialogResult.OK)
            {
                if (saveToExcel.FileName != "")
                {
                    filepath = saveToExcel.FileName.ToString();
                    if (!filepath.Contains(".xls"))
                    {
                        filepath += ".xls";
                    }
                    //MessageBox.Show(filepath);

                    try
                    {
                        FileStream sw = File.Create(filepath);
                        workbook.Write(sw);
                        sw.Close();
                        MessageBox.Show("数据导出已完成。");
                    }
                    catch (Exception exce)
                    {
                        MessageBox.Show(exce.Message + "请关闭文件后操作！");
                    }

                }
            }
        }

        private void AppendToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppendToExcelOFD.Filter = "excel文件(*.xls)|*.xls";
            AppendToExcelOFD.RestoreDirectory = true;
            string filepath;

            if (AppendToExcelOFD.ShowDialog() == DialogResult.OK)
            {
                if (AppendToExcelOFD.FileName != "")
                {
                    filepath = AppendToExcelOFD.FileName;

                    try
                    {
                        FileStream sw = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);

                        IWorkbook workbook = new HSSFWorkbook(sw);
                        ISheet sheet = workbook.GetSheetAt(0);
                        IRow row = sheet.GetRow(0);
                        int cellCount = row.LastCellNum;
                        int rowCount = sheet.LastRowNum;

                        //增加多5个空行，区分不同文件的数据
                        for (int i = 0; i < 5; i++)
                        {
                            row = sheet.CreateRow(i + rowCount + 1);
                        }

                        String filename = "";
                        foreach (Form f in Application.OpenForms)
                        {
                            //将荧光图片的数据导入。
                            if (f.GetType() == typeof(ChildForm))
                            {
                                filename = f.Text;
                                break;
                            }
                        }
                        row = sheet.CreateRow(rowCount + 6);
                        row.CreateCell(0).SetCellValue(filename);

                        //将每行的值填入，内部有求平均值和最大值的功能
                        int sumCount = 0;
                        for (int i = 0; i < resultGrid.Rows.Count - 1; i++)
                        {
                            row = sheet.CreateRow(i + rowCount + 6 + 1);
                            for (int j = 0; j < resultGrid.Rows[i].Cells.Count; j++)
                            {
                                if (j == 0)
                                {
                                    row.CreateCell(j).SetCellValue(resultGrid.Rows[i].Cells[j].Value.ToString());
                                    continue;
                                }
                                if (resultGrid.Rows[i].Cells[j].Value == null)
                                {
                                    continue;
                                }

                                float temp = float.Parse(resultGrid.Rows[i].Cells[j].Value.ToString());
                                row.CreateCell(j).SetCellValue(temp);
                                if (j == resultGrid.Rows[i].Cells.Count - 1)
                                {
                                    //将灰度存储到m_meanGray
                                    m_meanOptical += temp;
                                }
                            }
                            sumCount++;
                        }
                        //求出平均值
                        m_meanOptical /= sumCount;

                        //当某一行为空行的时候，row.lastcellNum == -1；
                        //当某一行只有一个元素的时候，返回值是1；
                        //也即是1-based下标
                        //定义最长的一行。
                        IRow longestRow = sheet.GetRow(1);
                        int longestRowNum = 0;
                        for (int i = 0; i < sheet.LastRowNum; i++)
                        {
                            //加上大于等于的话，应该能定位到最后的一个最长的行
                            //解决空行报错问题，有点奇葩的问题。
                            if ((sheet.GetRow(i) != null) && sheet.GetRow(i).LastCellNum >= longestRow.LastCellNum)
                            {
                                //获取最长一行数据的位置
                                longestRow = sheet.GetRow(i);
                                longestRowNum = i;
                            }
                        }

                        m_meanOptical = 0;
                        FileStream fs = new FileStream(filepath, FileMode.Open);
                        workbook.Write(fs);

                        sw.Close();
                        fs.Close();
                        MessageBox.Show("导出完成!!!");
                    }
                    catch (Exception exce)
                    {
                        MessageBox.Show(exce.Message + "请关闭文件后操作！");
                    }
                }
            }
        }

        /// <summary>
        /// /保存浓度按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveConcenStripMenuItem_Click(object sender, EventArgs e)
        {
            double mConcen = 0.0;
            string[] row = new string[2];
            row[0] = "浓度值";
            try
            {
                double concentration = double.Parse(concenStripTextBox.Text);
                mConcen = concentration;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("你输入的浓度格式不正确，请重新输入.");  
                return;
            }


            if (mMainForm.pathConList.ContainsKey(mMainForm.picturePath))//每张图片只有一个浓度
            {
                if (MessageBox.Show("此浓度的数据已经保存，是否需要替换数据？", "替换数据", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    mMainForm.mConcentrationMaps.Remove(mConcen);
                    mMainForm.mConcentrationMaps.Add(mConcen, mConcentration);

                    mMainForm.pathConList.Remove(mMainForm.picturePath);
                    mMainForm.pathConList.Add(mMainForm.picturePath, mConcen);

                    //row[1] = (mConcen).ToString();
                    resultGrid.Rows.Clear();
                    showTheResult(m_list);
                }
            }
            else
            {
                try
                {
                    mMainForm.mConcentrationMaps.Add(mConcen, mConcentration);
                    mMainForm.pathConList.Add(mMainForm.picturePath, mConcen);

                    row[1] = (mMainForm.pathConList[mMainForm.picturePath]).ToString();
                    resultGrid.Rows.Add(row);
                    //saveFlag = true;
                    //提示保存完成
                    concenStripTextBox.Text += "       保存完成";

                }
                catch (Exception)
                {
                    if (MessageBox.Show("此浓度的数据已经保存，是否需要替换数据？", "替换数据", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        mMainForm.mConcentrationMaps.Remove(mConcen);
                        mMainForm.mConcentrationMaps.Add(mConcen, mConcentration);
                        mMainForm.pathConList.Remove(mMainForm.picturePath);
                        mMainForm.pathConList.Add(mMainForm.picturePath, mConcen);
                        resultGrid.Rows.Clear();
                        showTheResult(m_list);
                    }
                    else
                    {
                        return;
                    }
                }
            }

        }

        private void fitCurveStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMainForm.mConcentrationMaps.Count < 4)
            {
                MessageBox.Show("保存的浓度只有" + mMainForm.mConcentrationMaps.Count + "个，至少需要4个！");
                return;
            }
            showTheCurve stc = new showTheCurve(mMainForm);
            stc.Show();
        }

        private void ConcenStripTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saveConcenStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// 当鼠标点击此控件的时候清空上次的输入内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConcenStripTextBoxClick(object sender, EventArgs e)
        {
            concenStripTextBox.Text = "";
        }


    }
}
