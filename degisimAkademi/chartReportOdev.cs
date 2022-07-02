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
    public partial class chartReportOdev : Form
    {
        public chartReportOdev()
        {
            InitializeComponent();
        }

        programLog prlg;
        void chartdoldur()
        {
            if (dataGridView2.ColumnCount > 1)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
            }
            DateTime start = new DateTime();
            DateTime target = new DateTime();
            if (ayno == 12)
            {
                start = new DateTime(yearr, ayno, 1);
                target = new DateTime(yearplus, 1, 1);
            }
            else
            {
                start = new DateTime(yearr, ayno, 1);
                target = new DateTime(yearplus, ayno + 1, 1);
            }
            while (start < target)
            {
                DataGridViewTextBoxColumn chx = new DataGridViewTextBoxColumn();
                dataGridView2.Columns.Add(chx);
                chx.HeaderText = start.ToString("dd.MM.yyyy");
                chx.Name = start.ToString("dd.MM.yyyy");
                start = start.AddDays(1);
            }


            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT sum(a.totalint) as yapilan, convert(varchar, a.insertDate, 104) as tarih FROM Ogrenciler o inner join ogrWork a on o.ogrId = a.ogrId where o.ogrId = '" + report.ogrid + "' and convert(varchar, a.insertDate, 104) like '%" + wherekosulu + "' GROUP BY a.total, a.insertDate", con);
            con.Open();
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView1.DataSource = ds.Tables["Ogrenciler"];
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == dataGridView2.Columns[j].HeaderText)
                        {
                            dataGridView2.Rows[0].Cells[j].Value = dataGridView1.Rows[i].Cells[0].Value;
                        }
                    }
                }
                foreach (DataGridViewRow rw in this.dataGridView2.Rows)
                {
                    for (int i = 0; i < rw.Cells.Count; i++)
                    {
                        if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                        {
                            rw.Cells[i].Value = "0";
                        }
                    }
                }


                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(dataGridView2.Columns[i].HeaderText, dataGridView2.Rows[0].Cells[i].Value);

                    string sqlquery = "SELECT total FROM ogrWork where ogrId = '" + report.ogrid + "'  and convert(varchar, insertDate, 104) = '" + dataGridView2.Columns[i].Name + "'";
                    SqlCommand command2 = new SqlCommand(sqlquery, con);
                    object nullable1 = command2.ExecuteScalar();
                    string total = "";
                    if (nullable1 == null || nullable1 == DBNull.Value)
                    {
                        total = "";
                    }
                    else
                    {
                        total = nullable1.ToString();
                    }

                    if (total == "True")
                    {
                        string sqlquery4 = "SELECT totalintlimit FROM ogrWork where ogrId = '" + report.ogrid + "' and convert(varchar, insertDate, 104) = '" + dataGridView2.Columns[i].Name + "'";
                        SqlCommand command4 = new SqlCommand(sqlquery4, con);
                        object topo = command4.ExecuteScalar();
                        string toplam = "";
                        if (topo == null || topo == DBNull.Value)
                        {
                            toplam = "";
                        }
                        else
                        {
                            toplam = topo.ToString();
                        }

                        string sqlquery5 = "SELECT totalint FROM ogrWork where ogrId = '" + report.ogrid + "' and convert(varchar, insertDate, 104) = '" + dataGridView2.Columns[i].Name + "'";
                        SqlCommand command5 = new SqlCommand(sqlquery5, con);
                        object topino = command5.ExecuteScalar().ToString();
                        string totalint = "";
                        if (topino == null || topino == DBNull.Value)
                        {
                            totalint = "";
                        }
                        else
                        {
                            totalint = topino.ToString();
                        }

                        if (totalint != "" && toplam != "")
                        {
                            if (Convert.ToInt32(totalint) > Convert.ToInt32(toplam))
                            {
                                chart1.Series["Series1"].Points[i].MarkerColor = Color.FromArgb(89, 213, 55);
                            }
                            else if (Convert.ToInt32(totalint) == Convert.ToInt32(toplam))
                            {
                                chart1.Series["Series1"].Points[i].MarkerColor = Color.FromArgb(130, 0, 127);
                            }
                        }

                        
                        
                    }
                    else
                    {
                        chart1.Series["Series1"].Points[i].MarkerColor = Color.FromArgb(255, 166, 0);
                    }



                    chart1.Series["Series1"].Points[i].MarkerStyle = MarkerStyle.Circle;
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
            con.Close();
            //chart1.ChartAreas[0].AxisY.Maximum = 160;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.Series[0].BorderWidth = 3;
            //chart1.Series[0].MarkerStyle = MarkerStyle.Circle;

        }

        private void chartReportOdev_Load(object sender, EventArgs e)
        {
            metroTabControl1_SelectedIndexChanged(sender, e);
            WindowState = FormWindowState.Maximized;
            label3.Text = "Grafik Paneli " + "   Öğrenci Adı: " + report.ogrName + " " + report.ogrsurname;
        }
        public static string wherekosulu = "";
        public static int yearr;//HERSENE DEĞİŞECEK SATIRLAR//buradaydımmmmmmmmmm
        public static int yearplus;
        public static int degisebiliryear = 2021;
        public static int ayno = 0;
        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)//HERSENE DEĞİŞECEK SATIRLAR
        {
            if (metroTabControl1.SelectedTab.Text == "Ağustos")
            {
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                chart1.Series["Series1"].Points.Clear();
                wherekosulu = "08." + yearr.ToString();
                ayno = 8;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Eylül")
            {
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                chart1.Series["Series1"].Points.Clear();
                wherekosulu = "09." + yearr.ToString();
                ayno = 9;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Ekim")
            {
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                chart1.Series[0].Points.Clear();
                wherekosulu = "10." + yearr.ToString();
                ayno = 10;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Kasım")
            {
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                chart1.Series[0].Points.Clear();
                wherekosulu = "11." + yearr.ToString();
                ayno = 11;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Aralık")
            {
                yearr = degisebiliryear;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "12." + yearr.ToString();
                ayno = 12;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Ocak")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "01." + yearplus.ToString();
                ayno = 1;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Şubat")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "02." + yearplus.ToString();
                ayno = 2;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Mart")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "03." + yearplus.ToString();
                ayno = 3;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Nisan")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "04." + yearplus.ToString();
                ayno = 4;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Mayıs")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "05." + yearplus.ToString();
                ayno = 5;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Haziran")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "06." + yearplus.ToString();
                ayno = 6;
                
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
            else if (metroTabControl1.SelectedTab.Text == "Temmuz")
            {
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                chart1.Series[0].Points.Clear();
                wherekosulu = "07." + yearplus.ToString();
                ayno = 7;
                chartdoldur();
                metroTabControl1.SelectedTab.Controls.Add(chart1);
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            chart1.Series[0].ToolTip = "Soru Sayısı: #VALY ~ Tarih: #VALX";
        }

    }
}
