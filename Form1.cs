using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawChart1_Application_Bezier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Cài đặt dữ liệu mẫu cho dataGridView1
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].HeaderCell.Value = "Năm 1";
            dataGridView1.Rows[0].Cells[0].Value = 16;
            dataGridView1.Rows[0].Cells[1].Value = 18;
            dataGridView1.Rows[0].Cells[2].Value = 20;
            dataGridView1.Rows[0].Cells[3].Value = 26;
            dataGridView1.Rows[0].Cells[4].Value = 30;
            dataGridView1.Rows[0].Cells[5].Value = 35;
            dataGridView1.Rows[0].Cells[6].Value = 40;
            dataGridView1.Rows[0].Cells[7].Value = 42;
            dataGridView1.Rows[0].Cells[8].Value = 37;
            dataGridView1.Rows[0].Cells[9].Value = 30;
            dataGridView1.Rows[0].Cells[10].Value = 23;
            dataGridView1.Rows[0].Cells[11].Value = 17;

            dataGridView1.Rows.Add();
            dataGridView1.Rows[1].HeaderCell.Value = "Năm 2";
            dataGridView1.Rows[1].Cells[0].Value = null;
            dataGridView1.Rows[1].Cells[1].Value = null;
            dataGridView1.Rows[1].Cells[2].Value = null;
            dataGridView1.Rows[1].Cells[3].Value = null;
            dataGridView1.Rows[1].Cells[4].Value = null;
            dataGridView1.Rows[1].Cells[5].Value = null;
            dataGridView1.Rows[1].Cells[6].Value = null;
            dataGridView1.Rows[1].Cells[7].Value = null;
            dataGridView1.Rows[1].Cells[8].Value = null;
            dataGridView1.Rows[1].Cells[9].Value = null;
            dataGridView1.Rows[1].Cells[10].Value = null;
            dataGridView1.Rows[1].Cells[11].Value = null;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            dataGridView1_MouseClick(null, null);

            chart1.Titles.Add("1234567890");
            chart1.Titles[0].Text = "BIỂU ĐỒ THEO DÕI NHIỆT ĐỘ TRONG NĂM";
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //Tìm và đặt giá trị MAX cho trục Y
            int max = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            for (int i = 0; i < 12; i++)
                if (max < Convert.ToInt32(dataGridView1.CurrentRow.Cells[i].Value))
                    max = Convert.ToInt32(dataGridView1.CurrentRow.Cells[i].Value);
            if (chart1.ChartAreas[0].AxisY.Maximum < max) chart1.ChartAreas[0].AxisY.Maximum = max;

            chart1.Series.Clear();
            for (int n = 0; n < dataGridView1.Rows.Count; n++) //Duyệt từ dòng đầu tiên đến dòng cuối cùng của dataGridView1
            {
                if (dataGridView1.Rows[n].Selected) //Nếu dòng thứ n được chọn thì thêm series cho dòng đó
                {
                    Series s = new Series();
                    for (int i = 0; i < 12; i++)
                    {
                        DataPoint p = new DataPoint();
                        p.XValue = i;
                        p.SetValueY(Convert.ToDouble(dataGridView1.Rows[n].Cells[i].Value));
                        p.AxisLabel = "Tháng " + (i + 1).ToString();
                        s.Points.Add(p);
                    }
                    chart1.Series.Add(s);
                }
            }
            foreach (Control a in Form1.ActiveForm.Controls)
                if (a.GetType().Name == "RadioButton")
                    if (((RadioButton)a).Checked)
                    {
                        rbSpline_Click(a, null);
                    }
            foreach (Control b in Form1.ActiveForm.Controls)
                if(b.GetType().Name == "RadioButton")
                    if(((RadioButton)b).Checked)
                    {
                        rbSplineArea_Click(b, null);
                    }
            foreach (Control c in Form1.ActiveForm.Controls)
                if (c.GetType().Name == "RadioButton")
                    if (((RadioButton)c).Checked)
                    {
                        rbLine_Click(c, null);
                    }
        }

        private void rbSpline_Click(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = SeriesChartType.Spline;
        }

        private void rbSplineArea_Click(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = SeriesChartType.SplineArea;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            }
            else
            {
                chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            }
        }

        private void rbLine_Click(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = SeriesChartType.Line;
        }
    }
}