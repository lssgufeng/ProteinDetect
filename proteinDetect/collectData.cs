using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace proteinDetect
{
    /// <summary>
    /// 收集用户的浓度信息等内容
    /// </summary>
    public partial class collectData : Form
    {
        private ConcentrationInfo mConcentration = null;
        private MainForm mMainForm = null;

        public collectData()
        {
            InitializeComponent();

            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(MainForm))
                {
                    mMainForm = (MainForm)f;
                    break;
                }
            }
            string[] row = new string[5];
            foreach (KeyValuePair<double, ConcentrationInfo> kvp in mMainForm.mConcentrationMaps)
            {
                row[0] = kvp.Key.ToString();
                row[1] = kvp.Value.AveNSE.ToString();
                row[2] = kvp.Value.AveCEA.ToString();
                row[3] = kvp.Value.AveDKK1.ToString();
                row[4] = kvp.Value.AveCY211.ToString();
                dataGridView.Rows.Add(row);
            }
        }

        public collectData(ConcentrationInfo info) : this()
        {
            mConcentration = info;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //清空之前存储的浓度信息
            mMainForm.mConcentrationMaps.Clear();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                float[] od = new float[4];
                double concen = 0;
                //跳过空行
                if (row.Cells[0].Value == null || string.IsNullOrEmpty(row.Cells[0].Value.ToString())) continue;
                concen = double.Parse(row.Cells[0].Value.ToString());

                for (int i = 0; i < row.Cells.Count - 1; i++)
                {
                    //若是cell为空，则其cell.value会返回一个null,由于od在初始化的时候会全部赋值为0，所以未输入的cell默认值为0
                    if (row.Cells[i + 1].Value == null || string.IsNullOrEmpty(row.Cells[i + 1].Value.ToString()))
                        continue;
                    od[i] = float.Parse(row.Cells[i + 1].Value.ToString());
                }
                mConcentration = new ConcentrationInfo(od[0], od[1], od[2], od[3]);
                mMainForm.mConcentrationMaps.Add(concen, mConcentration);
            }
        }

        private void CreateCurveButton_Click(object sender, EventArgs e)
        {
            if (mMainForm.mConcentrationMaps.Count < 4)
            {
                MessageBox.Show("至少先保存4组浓度值,现在只有" + mMainForm.mConcentrationMaps.Count + "组！");
                return;
            }
            showTheCurve stc = new showTheCurve(mMainForm);
            stc.Show();
        }

        private void collectData_Load(object sender, EventArgs e)
        {
            //初始化15个空行，进行粘贴操作
            string[] row = new string[5];
            for (int i = 0; i < 15; i++)
            {
                dataGridView.Rows.Add(row);
            }
        }

        /// <summary>
        /// 从剪切板将数据粘贴到datagridview中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell == null) return;

            int insertRowIndex = dataGridView.CurrentCell.RowIndex;
            int insertColumnIndex = dataGridView.CurrentCell.ColumnIndex;

            string pasteText = Clipboard.GetText();

            if (string.IsNullOrEmpty(pasteText))
                return;

            pasteText = pasteText.Replace('\n', ' ');
            pasteText = pasteText.Replace('\r', ' ');
            pasteText.TrimEnd(new char[] { ' ' });

            string[] lines = pasteText.Split(' ');

            foreach (string line in lines)
            {
                //在这里将分行产生的 空 字符串剔掉了
                if (line.Length == 0)
                {
                    continue;
                }
                string[] values = line.Split('\t');
                if (values.Length > dataGridView.ColumnCount - insertColumnIndex)
                {
                    MessageBox.Show("列数过多");
                    return;
                }

                DataGridViewRow row = dataGridView.Rows[insertRowIndex];

                for (int j = 0; j < values.Length; j++)
                {
                    row.Cells[j + insertColumnIndex].Value = values[j];
                }
                insertRowIndex++;
            }
        }

        private void DataGridViewKeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                if (sender != null && sender.GetType() == typeof(DataGridView))
                {
                    pasteButton_Click(sender, e);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet1");
            IRow row = sheet.CreateRow(0);

            //更新第一行中的内容为行的标号
            for (int i = 0; i < dataGridView.Rows[0].Cells.Count; i++)
            {
                row.CreateCell(i).SetCellValue(dataGridView.Rows[0].Cells[i].OwningColumn.HeaderText);
            }

            //将每一行的值填入
            //int sumCount = 0;
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                IRow rowTemp = sheet.CreateRow(i + 1);

                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                {
                    if (dataGridView.Rows[i].Cells[j].Value == null)
                    {
                        continue;
                    }

                    float temp = float.Parse(dataGridView.Rows[i].Cells[j].Value.ToString());

                    rowTemp.CreateCell(j).SetCellValue(temp);
                }
            }

            saveToExcel.Filter = "excel文件(*.xls)|*.xls";
            saveToExcel.RestoreDirectory = true;
            string filepath;

            if (saveToExcel.ShowDialog() == DialogResult.OK)
            {
                if (saveToExcel.FileName != "")
                {
                    filepath = saveToExcel.FileName;
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

        private void deleteData_Click(object sender, EventArgs e)
        {
            //清空之前存储的浓度信息
            mMainForm.mConcentrationMaps.Clear();
            dataGridView.Rows.Clear();
            //初始化15个空行，进行粘贴操作
            string[] row = new string[5];
            for (int i = 0; i < 15; i++)
            {
                dataGridView.Rows.Add(row);
            }
        }


    }//class
}
