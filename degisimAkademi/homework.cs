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
    public partial class homework : Form
    {
        public homework()
        {
            InitializeComponent();
        }

        private void homework_Load(object sender, EventArgs e)
        {
            //yearr = 2021;
            //yearplus = 2021;
            doldurogr();
            UpdateFont();
        }
        programLog prlg;
        public static int yearr;//HERSENE DEĞİŞECEK SATIRLAR
        public static int yearplus;
        public static int degisebiliryear = 2021;
        void doldurogr()
        {
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
                //for (int i = 0; i < ; i++)
                //{
                //    DataGridViewRow row = dataGridView1.Rows[i];
                //    row.Height = 50;
                //}
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
            con.Close();
        }

        void doldurodev()//ÖĞRENCİLERİN TEK SATIRLIK ÖDEV BİLGİSİNİ GETİREN KOD PARÇASI
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from homework where ogrId = '" + ogrid + "'", con);
            try
            {
                adtr.Fill(ds, "homework");
                dataGridView3.DataSource = ds.Tables["homework"];
                adtr.Dispose();
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
            }
            con.Close();
        }

        public static string ogrid, ogrAdi, ogrsoyadi, ogrGrup;
        public static int tr, prg, sy, cg, mat, fn,km,biy, dn, ing, dg, den;

        public static string ogridh, ogrAdih, ogrsoyadih, ogrGruph, tarihh;

        private void metroTabControl5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl5.SelectedTab.Text == "Tüm Öğrenciler")
            {
                tumogrenciler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "Mezun")
            {
                mezunlar();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "12.Sınıflar")
            {
                onikiler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "11.Sınıflar")
            {
                onbirler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "10.Sınıflar")
            {
                onlar();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "9.Sınıflar")
            {
                dokuzlar();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "8.Sınıflar")
            {
                sekizler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "7.Sınıflar")
            {
                yediler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "6.Sınıflar")
            {
                altilar();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "5.Sınıflar")
            {
                besler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "4.Sınıflar")
            {
                dortler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "3.Sınıflar")
            {
                ucler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "2.Sınıflar")
            {
                ikiler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
            else if (metroTabControl5.SelectedTab.Text == "1.Sınıflar")
            {
                birler();
                metroTabControl5.SelectedTab.Controls.Clear();
                metroTabControl5.SelectedTab.Controls.Add(dataGridView1);
                doldurogr();
            }
        }

        public static string wherekosulu = "";

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

        //public static int trh, syh, math, fnh, dnh, ingh;

        public static bool odevverisi = false;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ogrid = dataGridView1.CurrentRow.Cells["ogrId"].Value.ToString();
            doldurodev();
            ogrAdi = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            ogrsoyadi = dataGridView1.CurrentRow.Cells["surname"].Value.ToString();
            ogrGrup = dataGridView1.CurrentRow.Cells["ogrGrup"].Value.ToString();
            if (dataGridView3.Rows[0].Cells[0].Value == null || dataGridView3.Rows[0].Cells[0].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView3.Rows[0].Cells[0].Value.ToString()))
            {
                odevverisi = false;
            }
            else
            {
                odevverisi = true;
                tr = Convert.ToInt32(dataGridView3.Rows[0].Cells["turkce"].Value);
                prg = Convert.ToInt32(dataGridView3.Rows[0].Cells["paragraf"].Value);
                sy = Convert.ToInt32(dataGridView3.Rows[0].Cells["sosyal"].Value);
                cg = Convert.ToInt32(dataGridView3.Rows[0].Cells["cografya"].Value);
                mat = Convert.ToInt32(dataGridView3.Rows[0].Cells["matematik"].Value);
                fn = Convert.ToInt32(dataGridView3.Rows[0].Cells["fen"].Value);
                km = Convert.ToInt32(dataGridView3.Rows[0].Cells["kimya"].Value);
                biy = Convert.ToInt32(dataGridView3.Rows[0].Cells["biyoloji"].Value);
                dn = Convert.ToInt32(dataGridView3.Rows[0].Cells["dinkulturu"].Value);
                ing = Convert.ToInt32(dataGridView3.Rows[0].Cells["ingilizce"].Value);
                dg = Convert.ToInt32(dataGridView3.Rows[0].Cells["diger"].Value);
                den = Convert.ToInt32(dataGridView3.Rows[0].Cells["deneme"].Value);
            }

            if (ogrGrup == "Lise")
            {
                if (odevAtamaLise.acikmi == false)
                {
                    odevAtamaLise oal = new odevAtamaLise();
                    oal.Show();
                }
            }
            else
            {
                if (odevAtama.acikmi == false)
                {
                    odevAtama oa = new odevAtama();
                    oa.Show();
                }
            }
        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            }

            foreach (DataGridViewColumn c in dgagust.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            }

            foreach (DataGridViewColumn c in dataGridView3.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            }
        }

        private void metroTabControl3_SelectedIndexChanged(object sender, EventArgs e)//HERSENE DEĞİŞECEK SATIRLAR
        {
            if (metroTabControl3.SelectedTab.Text == "Ağustos")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 8;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Eylül")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 9;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Ekim")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 10;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Kasım")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 11;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Aralık")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 12;
                yearr = degisebiliryear;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Ocak")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 1;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Şubat")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 2;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Mart")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 3;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Nisan")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 4;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Mayıs")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 5;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Haziran")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 6;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl3.SelectedTab.Text == "Temmuz")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 7;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl3.SelectedTab.Controls.Add(dgagust);
            }
        }
        public static int ayno = 0;
        void doldur()
        {
            string grup = metroTabControl2.SelectedTab.Text;
            ogrGruph = metroTabControl2.SelectedTab.Text;
            if (grup == "Lise - Mezun")
            {
                grup = "Lise";
                ogrGruph = grup;
            }

            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            string sorgu1 = "select o.ogrId, o.name, o.surname from Ogrenciler o where ogrGrup = '" + grup + "' and o.status = '1'";
            con.Open();
            SqlCommand command = new SqlCommand(sorgu1, con);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dgagust.Columns.Add("id", "");
            dgagust.Columns.Add("isim", "Adı");
            dgagust.Columns.Add("soyisim", "Soyadı");
            this.dgagust.Columns["id"].Visible = false;
            foreach (DataRow dr in dt.Rows)
            {
                dgagust.Rows.Add(dr.ItemArray);
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
            //int i = 1;
            while (start < target)
            {
                DataGridViewCheckBoxColumn chx = new DataGridViewCheckBoxColumn();
                dgagust.Columns.Add(chx);
                chx.HeaderText = start.ToString("dd");
                chx.Name = start.ToString("dd.MM.yyyy");
                start = start.AddDays(1);
                //i++;
            }

            for (int k = 0; k < dgagust.Rows.Count; k++)
            {
                for (int b = 3; b < dgagust.Columns.Count; b++)
                {
                    string sqlquery = "SELECT total FROM ogrWork where ogrId = '" + dgagust.Rows[k].Cells[0].Value.ToString() + "'  and convert(varchar, insertDate, 104) = '" + dgagust.Columns[b].Name + "'";
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

                    string sqlquery2 = "SELECT convert(varchar, insertDate, 104) FROM ogrWork where ogrId = '" + dgagust.Rows[k].Cells[0].Value.ToString() + "' and convert(varchar, insertDate, 104) = '" + dgagust.Columns[b].Name + "'";
                    SqlCommand command3 = new SqlCommand(sqlquery2, con);
                    object nullable2 = command3.ExecuteScalar();
                    string tar = "";
                    if (nullable2 == null || nullable2 == DBNull.Value)
                    {
                        tar = "";
                    }
                    else
                    {
                        tar = nullable2.ToString();
                    }
                    if (total == "True")
                    {
                        string sqlquery4 = "SELECT totalintlimit FROM ogrWork where ogrId = '" + dgagust.Rows[k].Cells[0].Value.ToString() + "'and convert(varchar, insertDate, 104) = '" + dgagust.Columns[b].Name + "'";
                        SqlCommand command4 = new SqlCommand(sqlquery4, con);
                        object topo = command4.ExecuteScalar();
                        string toplam = "";//ÖĞRENCİYE VERİLEN TOPLAM SORU SAYISI
                        if (topo == null || topo == DBNull.Value)
                        {
                            toplam = "";
                        }
                        else
                        {
                            toplam = topo.ToString();
                        }

                        string sqlquery5 = "SELECT totalint FROM ogrWork where ogrId = '" + dgagust.Rows[k].Cells[0].Value.ToString() + "' and convert(varchar, insertDate, 104) = '" + dgagust.Columns[b].Name + "'";
                        SqlCommand command5 = new SqlCommand(sqlquery5, con);
                        object topino = command5.ExecuteScalar().ToString();
                        string totalint = "";//ÖĞRENCİNİN YAPTIĞI SORU SAYISI
                        if (topino == null || topino == DBNull.Value)
                        {
                            totalint = "";
                        }
                        else
                        {
                            totalint = topino.ToString();
                        }

                        //string sqlquery6 = "SELECT total FROM ogrWork where ogrId = '" + dgagust.Rows[k].Cells[0].Value.ToString() + "' and convert(varchar, insertDate, 104) = '" + dgagust.Columns[b].Name + "'";
                        //SqlCommand command6 = new SqlCommand(sqlquery6, con);
                        //object isnuno = command6.ExecuteScalar().ToString();
                        //string istru = "";
                        //if (isnuno == null || isnuno == DBNull.Value)
                        //{
                        //    istru = "";
                        //}
                        //else
                        //{
                        //    istru = isnuno.ToString();
                        //}

                        if (totalint != "" && toplam != "")
                        {
                            string ay = dgagust.Rows[k].Cells[0].Value.ToString();
                            if (Convert.ToInt32(totalint) > Convert.ToInt32(toplam))
                            {
                                dgagust.Rows[k].Cells[tar].Style.BackColor = Color.FromArgb(89, 213, 55);
                            }
                            else if (Convert.ToInt32(totalint) == Convert.ToInt32(toplam))
                            {
                                dgagust.Rows[k].Cells[tar].Style.BackColor = Color.FromArgb(130, 0, 127);
                            }
                        }

                        dgagust.Rows[k].Cells[tar].Value = true;
                    }
                    else
                    {
                        if (tar != "")
                        {
                            dgagust.Rows[k].Cells[tar].Style.BackColor = Color.FromArgb(255, 166, 0);
                        }
                    }
                }

            }
            dgagust.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgagust.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgagust.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgagust.EnableHeadersVisualStyles = false;
            dgagust.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgagust.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgagust.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            for (int j = 0; j < dgagust.Rows.Count; j++)
            {
                DataGridViewRow row = dgagust.Rows[j];
                row.Height = 30;
            }

            for (int j = 3; j < dgagust.Columns.Count; j++)//check box hücreleri
            {
                DataGridViewColumn row = dgagust.Columns[j];
                row.Width = 30;
            }
            for (int j = 0; j < 3; j++)//isimsoyisim
            {
                DataGridViewColumn row = dgagust.Columns[j];
                row.Width = 75;
            }
        }
        private void dgagust_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ogridh = dgagust.CurrentRow.Cells["id"].Value.ToString();
            ogrAdih = dgagust.CurrentRow.Cells["isim"].Value.ToString();
            ogrsoyadih = dgagust.CurrentRow.Cells["soyisim"].Value.ToString();
            int a = dgagust.CurrentCell.ColumnIndex;
            tarihh = dgagust.Columns[a].Name;
            if (metroTabControl2.SelectedTab.Text == "Lise - Mezun")
            {
                if (odevEkleLise.acikmi == false)
                {
                    odevEkleLise oel = new odevEkleLise();
                    oel.Show();
                }
            }
            else
            {
                if (odevEkle.acikmi == false)
                {
                    odevEkle oe = new odevEkle();
                    oe.Show();
                }
            }
        }

        private void metroTabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl4.SelectedTab.Text == "Ağustos")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 8;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Eylül")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 9;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Ekim")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 10;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Kasım")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 11;
                yearr = degisebiliryear;
                yearplus = degisebiliryear;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Aralık")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 12;
                yearr = degisebiliryear;
                yearplus = yearr + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Ocak")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 1;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Şubat")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 2;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Mart")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 3;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Nisan")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 4;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Mayıs")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 5;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Haziran")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 6;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
            else if (metroTabControl4.SelectedTab.Text == "Temmuz")
            {
                dgagust.Rows.Clear();
                dgagust.Columns.Clear();
                ayno = 7;
                yearr = degisebiliryear + 1;
                yearplus = degisebiliryear + 1;
                doldur();
                metroTabControl4.SelectedTab.Controls.Add(dgagust);
            }
        }

        private void metroTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl2.SelectedTab.Text == "Lise - Mezun")
            {
                metroTabControl2.SelectedTab.Controls.Remove(metroTabControl3);
                ogrGruph = "Lise";
                metroTabControl2.SelectedTab.Controls.Add(metroTabControl3);
            }
            else
            {
                //metroTabControl2.SelectedTab.Controls.Remove(metroTabControl4);
                ogrGruph = "Ortaokul";
                //metroTabControl2.SelectedTab.Controls.Add(dgagust);
            }
        }
    }
}
