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
    public partial class ogrenciler : Form
    {
        public ogrenciler()
        {
            InitializeComponent();
        }
        public static int kayitSayisi = 0;
        private void ogrenciler_Load(object sender, EventArgs e)
        {
            doldurOgrenci();
            //metroTabControl1_SelectedIndexChanged(sender, e);
            //kayitSayisi = dataGridView1.Rows.Count;
            UpdateFont();
            comboBox1.Text = "Öğrenci Adına Göre";
            label2.Text = "Şuan Değişim Akademi'de " + dataGridView1.Rows.Count.ToString() + " öğrenci kaydı bulunmaktadır.";
        }
        programLog prlg;
        public static string wherekosulu = "";
        public void doldurOgrenci()
        {

            //if (dataGridView1.Columns.Count > 0)
            //{
            //    dataGridView1.Columns.Clear();
            //}
            //dataGridView1.Columns.Add("count", "Sayı");
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from Ogrenciler where status = '1'" + wherekosulu + "", con);
            try
            {
                
                adtr.Fill(ds, "Ogrenciler");
                dataGridView1.DataSource = ds.Tables["Ogrenciler"];
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adtr.Dispose();

                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    dataGridView1.Rows[i].Cells["count"].Value = (i + 1).ToString();
                //}

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
        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ogrEkle og = new ogrEkle(this);
            og.Show();
        }

        public static string ogrId, ogrNo, ogrName, ogrSurname, ogrSchool, ogrTcNo, ogrGsm, ogrVeli, veliGsm, ogrGrup, ogrAlan, status, ogrlevel;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Öğrenci Adına Göre")
            {
                colname3 = "name";
            }
            else if (comboBox1.Text == "Öğrenci Soyadına Göre")
            {
                colname3 = "surname";
            }
            else if (comboBox1.Text == "Gruba Göre")
            {
                colname3 = "ogrGrup";
            }
            else if (comboBox1.Text == "Alana Göre")
            {
                colname3 = "ogrAlan";
            }
            else if (comboBox1.Text == "Okul Adına Göre")
            {
                colname3 = "schoolName";
            }
            else if (comboBox1.Text == "Telefon Numarasına Göre")
            {
                colname3 = "gsm";
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Ara")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    doldurOgrenci();
                }
                else
                {
                    SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
                    string sorgu = "select * from Ogrenciler where " + colname3 + " like '%" + textBox1.Text + "%'";
                    con.Open();
                    SqlDataAdapter getir = new SqlDataAdapter(sorgu, con);
                    DataSet goster = new DataSet();
                    try
                    {
                        getir.Fill(goster, "Ogrenciler");
                        dataGridView1.DataSource = goster.Tables["Ogrenciler"];
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        getir.Dispose();
                    }
                    catch (SqlException ex)
                    {
                        prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                        prlg.databaseinsert();
                        MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
                    }
                    con.Close();
                    this.dataGridView1.Columns["ogrId"].Visible = false;
                    this.dataGridView1.Columns["ogrNo"].Visible = false;
                    this.dataGridView1.Columns["status"].Visible = false;
                    this.dataGridView1.Columns["userId"].Visible = false;
                    this.dataGridView1.Columns["editDate"].Visible = false;
                    this.dataGridView1.Columns["tcNo"].Visible = false;
                    this.dataGridView1.Columns["ogrAddDate"].Visible = false;
                    this.dataGridView1.Columns["veliName"].Visible = false;
                    this.dataGridView1.Columns["veligsm"].Visible = false;
                    this.dataGridView1.Columns["insertDate"].Visible = false;
                }
            }
        }

        public static string colname3;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = dataGridView1.CurrentCell.ColumnIndex;

            if (dataGridView1.Columns[columnIndex].Name == "name")
            {
                comboBox1.Text = "Öğrenci Adına Göre";
                colname3 = "name";
            }
            else if (dataGridView1.Columns[columnIndex].Name == "surname")
            {
                comboBox1.Text = "Öğrenci Soyadına Göre";
                colname3 = "surname";
            }
            else if (dataGridView1.Columns[columnIndex].Name == "ogrGrup")
            {
                comboBox1.Text = "Gruba Göre";
                colname3 = "ogrGrup";
            }
            else if (dataGridView1.Columns[columnIndex].Name == "ogrAlan")
            {
                comboBox1.Text = "Alana Göre";
                colname3 = "ogrAlan";
            }
            else if (dataGridView1.Columns[columnIndex].Name == "schoolName")
            {
                comboBox1.Text = "Okul Adına Göre";
                colname3 = "schoolName";
            }
            else if (dataGridView1.Columns[columnIndex].Name == "gsm")
            {
                comboBox1.Text = "Telefon Numarasına Göre";
                colname3 = "gsm";
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = "Ara";
            }
        }

        public static DateTime ogrAddDate;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ogrId = dataGridView1.CurrentRow.Cells["ogrId"].Value.ToString();
            ogrNo = dataGridView1.CurrentRow.Cells["ogrNo"].Value.ToString();
            ogrName = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            ogrSurname = dataGridView1.CurrentRow.Cells["surname"].Value.ToString();
            ogrSchool = dataGridView1.CurrentRow.Cells["schoolName"].Value.ToString();
            ogrTcNo = dataGridView1.CurrentRow.Cells["tcNo"].Value.ToString();
            ogrGsm = dataGridView1.CurrentRow.Cells["gsm"].Value.ToString();
            ogrVeli = dataGridView1.CurrentRow.Cells["veliName"].Value.ToString();
            veliGsm = dataGridView1.CurrentRow.Cells["veliGsm"].Value.ToString();
            ogrGrup = dataGridView1.CurrentRow.Cells["ogrGrup"].Value.ToString();
            ogrAlan = dataGridView1.CurrentRow.Cells["ogrAlan"].Value.ToString();
            ogrAddDate = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["ogrAddDate"].Value);
            status = dataGridView1.CurrentRow.Cells["status"].Value.ToString();
            ogrlevel = dataGridView1.CurrentRow.Cells["ogrLevel"].Value.ToString();
            ogrenciDetay od = new ogrenciDetay(this);
            od.Show();
        }



        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedTab.Text == "Tüm Öğrenciler")
            {
                tumogrenciler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                metroTabControl1.SelectedTab.Controls.Add(label2);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "Mezun")
            {
                mezunlar();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "12.Sınıflar")
            {
                onikiler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "11.Sınıflar")
            {
                onbirler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "10.Sınıflar")
            {
                onlar();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "9.Sınıflar")
            {
                dokuzlar();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "8.Sınıflar")
            {
                sekizler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "7.Sınıflar")
            {
                yediler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "6.Sınıflar")
            {
                altilar();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "5.Sınıflar")
            {
                besler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "4.Sınıflar")
            {
                dortler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "3.Sınıflar")
            {
                ucler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "2.Sınıflar")
            {
                ikiler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
            }
            else if (metroTabControl1.SelectedTab.Text == "1.Sınıflar")
            {
                birler();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                doldurOgrenci();
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
    }
}
