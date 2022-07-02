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

namespace degisimAkademi
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
        }
        public static string tarih;
        programLog prlg;
        public void doldur()
        {
            tarih = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            //SqlDataAdapter adtr = new SqlDataAdapter("select * from denemeler where cast (datediff (day, 0, denemeTarihi) as datetime) = '" + tarih + "'", con);
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from Ogrenciler where status = '1'" + wherekosulu + "", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView1.DataSource = ds.Tables["Ogrenciler"];
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adtr.Dispose();


                this.dataGridView1.Columns["ogrId"].Visible = false;
                this.dataGridView1.Columns["ogrNo"].Visible = false;
                this.dataGridView1.Columns["status"].Visible = false;
                this.dataGridView1.Columns["userId"].Visible = false;
                this.dataGridView1.Columns["editDate"].Visible = false;
                this.dataGridView1.Columns["tcNo"].Visible = false;
                this.dataGridView1.Columns["ogrAddDate"].Visible = false;
                this.dataGridView1.Columns["veliName"].Visible = false;
                this.dataGridView1.Columns["veligsm"].Visible = false;
                this.dataGridView1.Columns["veligsm"].Visible = false;
                this.dataGridView1.Columns["insertDate"].Visible = false;
                this.dataGridView1.Columns["ogrGrup"].Visible = false;

                //dataGridView1.Columns[1].HeaderText = "Cari Kart No";
                dataGridView1.Columns[2].HeaderText = "Adı";
                dataGridView1.Columns[3].HeaderText = "Soyadı";
                dataGridView1.Columns[4].HeaderText = "Grubu";
                dataGridView1.Columns[5].HeaderText = "Alanı";
                dataGridView1.Columns[6].HeaderText = "Okul İsmi";
                dataGridView1.Columns[7].HeaderText = "Kayıt Tarihi";
                //dataGridView1.Columns[8].HeaderText = "Tc Kimlik No";
                dataGridView1.Columns[9].HeaderText = "GSM";
                dataGridView1.Columns[10].HeaderText = "Veli Adı";
                dataGridView1.Columns[11].HeaderText = "Veli GSM";
                dataGridView1.Columns[16].HeaderText = "Sınıf";

                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                //{
                //    DataGridViewColumn column = dataGridView1.Columns[i];
                //    column.Width = 224;
                //    //if (i == 2)
                //    //{
                //    //    column.Width = 300;
                //    //}
                //}
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView1.Columns[i];
                    column.Width = 150;
                    if (i == 2 || i == 3)
                    {
                        column.Width = 250;
                    }
                }
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
            con.Close();
            UpdateFont();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doldur();
        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            }
        }

        private void report_Load(object sender, EventArgs e)
        {
            metroComboBox1.Text = "Genel Durum Grafiği";
            metroTabControl1_SelectedIndexChanged(sender, e);
        }

        //public static string denemeid, tabloname, denemetarihi, ogrAlani;
        public static string ogrAlani, ogrName, ogrsurname, ogrid;
        public static string wherekosulu = "";
        private void metroTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl2.SelectedTab.Text == "Tüm Öğrenciler")
            {
                tumogrenciler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "Mezun")
            {
                mezunlar();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "12.Sınıflar")
            {
                onikiler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "11.Sınıflar")
            {
                onbirler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "10.Sınıflar")
            {
                onlar();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "9.Sınıflar")
            {
                dokuzlar();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "8.Sınıflar")
            {
                sekizler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "7.Sınıflar")
            {
                yediler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "6.Sınıflar")
            {
                altilar();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "5.Sınıflar")
            {
                besler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "4.Sınıflar")
            {
                dortler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "3.Sınıflar")
            {
                ucler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "2.Sınıflar")
            {
                ikiler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
            else if (metroTabControl2.SelectedTab.Text == "1.Sınıflar")
            {
                birler();
                metroTabControl2.SelectedTab.Controls.Clear();
                metroTabControl2.SelectedTab.Controls.Add(dataGridView1);
                doldur();
            }
        }

        void tumogrenciler()
        {
            wherekosulu = "";
        }
        void mezunlar()
        {
            wherekosulu = "and ogrLevel = 'Mezun'";
        }
        void onikiler()
        {
            wherekosulu = "and ogrLevel = '12'";
        }
        void onbirler()
        {
            wherekosulu = "and ogrLevel = '11'";
        }
        void onlar()
        {
            wherekosulu = "and ogrLevel = '10'";
        }
        void dokuzlar()
        {
            wherekosulu = "and ogrLevel = '9'";
        }
        void sekizler()
        {
            wherekosulu = "and ogrLevel = '8'";
        }
        void yediler()
        {
            wherekosulu = "and ogrLevel = '7'";
        }
        void altilar()
        {
            wherekosulu = "and ogrLevel = '6'";
        }
        void besler()
        {
            wherekosulu = "and ogrLevel = '5'";
        }
        void dortler()
        {
            wherekosulu = "and ogrLevel = '4'";
        }
        void ucler()
        {
            wherekosulu = "and ogrLevel = '3'";
        }
        void ikiler()
        {
            wherekosulu = "and ogrLevel = '2'";
        }
        void birler()
        {
            wherekosulu = "and ogrLevel = '1'";
        }

        public static string reportparam = "";

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedTab.Text == "Deneme Sonuçları")
            {
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(panel1);
                reportparam = "deneme";
                panel1.Visible = true;
                label3.Text = "Deneme Sonucu Raporlama";
                doldur();
                if (metroComboBox1.Items.Count == 1)
                {
                    metroComboBox1.Items.Add("Detaylı İnceleme");
                }
            }
            else if (metroTabControl1.SelectedTab.Text == "Ödev Raporu")
            {
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(panel1);
                reportparam = "odev";
                panel1.Visible = true;
                label3.Text = "Ödev Raporlama";
                doldur();
                metroComboBox1.Text = "Genel Durum Grafiği";
                if (metroComboBox1.Items.Count > 1)
                {
                    metroComboBox1.Items.RemoveAt(1);
                }
            }
        }


        //public static DateTime ;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ogrid = dataGridView1.CurrentRow.Cells["ogrId"].Value.ToString();
            ogrAlani = dataGridView1.CurrentRow.Cells["ogrGrup"].Value.ToString();
            ogrName = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            ogrsurname = dataGridView1.CurrentRow.Cells["surname"].Value.ToString();
            //tabloname = dataGridView1.CurrentRow.Cells["denemeAlani"].Value.ToString();
            //const int MaxLength = 10;
            //var name = dataGridView1.CurrentRow.Cells["denemeTarihi"].Value.ToString();
            //if (name.Length > MaxLength)
            //    name = name.Substring(0, MaxLength); // name = "Chris"
            //denemetarihi = name.ToString();
            if (reportparam == "odev")
            {
                chartReportOdev co = new chartReportOdev();
                co.Show();
            }
            else
            {
                if (metroComboBox1.Text == "Genel Durum Grafiği")
                {
                    chartReport cr = new chartReport();
                    cr.Show();
                }
                else
                {
                    detayliDenemeReport dr = new detayliDenemeReport();
                    dr.Show();
                }
            }
        }
    }
}
