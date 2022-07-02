using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace degisimAkademi
{
    public partial class chartReport : Form
    {
        public chartReport()
        {
            InitializeComponent();
        }

        programLog prlg;

        private void chartReport_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            label3.Text = "Grafik Paneli " + "   Öğrenci Adı: " + report.ogrName + " " + report.ogrsurname;
            con.Open();
            if (report.ogrAlani == "Lise")
            {
                chart1doldur();//TYT
                chart2doldur();//AYT
            }
            else
            {
                chart1lgsdoldur();//LGS
            }
            con.Close();
        }

        void chart1doldur()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT sum(t.topnet) as net, convert(varchar, d.denemeTarihi, 104) as tarih FROM Ogrenciler o inner join tytPuanlari t on o.ogrId = t.ogrId inner join denemeler d on d.denemeId = t.denemeId where o.ogrId = '"+report.ogrid+"' and o.status = '1' GROUP BY t.denemeId, d.denemeTarihi", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView2.DataSource = ds.Tables["Ogrenciler"];
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(Convert.ToDateTime(dataGridView2.Rows[i].Cells["tarih"].Value), dataGridView2.Rows[i].Cells["net"].Value);
                    chart1.Series["Series1"].Points[i].MarkerSize = 10;
                }
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG3");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG3", "Sistem Mesajı");
            }
            //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart1.ChartAreas[0].AxisY.Maximum = 120;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.Series[0].BorderWidth = 3;
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart1.ChartAreas[0].AxisY.Interval = 10;
        }

        void chart2doldur()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT sum(a.topnet) as net, convert(varchar, d.denemeTarihi, 104) as tarih FROM Ogrenciler o inner join aytPuanlari a on o.ogrId = a.ogrId inner join denemeler d on d.denemeId = a.denemeId where o.ogrId = '"+report.ogrid+"' and o.status = '1' GROUP BY a.denemeId, d.denemeTarihi", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView1.DataSource = ds.Tables["Ogrenciler"];
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    chart2.Series["Series1"].Points.AddXY(Convert.ToDateTime(dataGridView1.Rows[i].Cells["tarih"].Value), dataGridView1.Rows[i].Cells["net"].Value);
                    chart2.Series["Series1"].Points[i].MarkerSize = 10;
                }
                //chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
                //chart2.ChartAreas[0].Area3DStyle.Inclination = 80;
                //chart2.ChartAreas[0].AxisY.Maximum = 100;
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG3");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG3", "Sistem Mesajı");
            }
            chart2.ChartAreas[0].AxisY.Maximum = 160;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.Series[0].BorderWidth = 3;
            chart2.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart2.ChartAreas[0].AxisY.Interval = 10;
        }

        void chart1lgsdoldur()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT sum(l.topnet) as net, convert(varchar, d.denemeTarihi, 104) as tarih FROM Ogrenciler o inner join LgsPuanlari l on o.ogrId = l.ogrId inner join denemeler d on d.denemeId = l.denemeId where o.ogrId = '"+report.ogrid+"' and o.status = '1' GROUP BY l.denemeId, d.denemeTarihi", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView2.DataSource = ds.Tables["Ogrenciler"];
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(Convert.ToDateTime(dataGridView2.Rows[i].Cells["tarih"].Value), dataGridView2.Rows[i].Cells["net"].Value);
                    chart1.Series["Series1"].Points[i].MarkerSize = 10;
                }
                //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                //chart1.ChartAreas[0].Area3DStyle.Inclination = 80;
                //chart1.ChartAreas[0].AxisY.Maximum = 100;
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG3");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG3", "Sistem Mesajı");
            }
            chart1.ChartAreas[0].AxisY.Maximum = 90;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.Series[0].BorderWidth = 3;
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            metroTabControl1.TabPages.Remove(tabPage2);
            tabPage1.Text = "LGS Grafiği";
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            chart1.Series[0].ToolTip = "Net: #VALY ~ Tarih: #VALX";
        }

        private void chart2_MouseMove(object sender, MouseEventArgs e)
        {
            chart2.Series[0].ToolTip = "Net: #VALY ~ Tarih: #VALX";
        }

    }
}
