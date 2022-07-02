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
    public partial class odevEkle : Form
    {
        public odevEkle()
        {
            InitializeComponent();
        }
        public static string ttl = "";
        public static bool nullmu = false;
        public static bool acikmi = false;
        private void odevEkle_Load(object sender, EventArgs e)
        {
            acikmi = true;
            doldurodev();
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            label1.Text = homework.ogrAdih + " " + homework.ogrsoyadih + "      " + homework.tarihh;
            string sqlquery = "SELECT ogrId FROM ogrWork where ogrId = '" + homework.ogridh + "' and convert(varchar, insertDate, 104) = '" + homework.tarihh + "'";
            SqlCommand command2 = new SqlCommand(sqlquery, con);
            con.Open();
            object nullable1 = command2.ExecuteScalar();
            con.Close();
            if (nullable1 == null || nullable1 == DBNull.Value)
            {
                ttl = "";
            }
            else
            {
                ttl = nullable1.ToString();
            }
            doldurodevmiktar();
        }

        programLog prlg;
        void doldurodev()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select ogrId,turkce,paragraf,sosyal,cografya,matematik,fen,kimya,biyoloji,dinkulturu,ingilizce,diger,deneme from homework where ogrId = '" + homework.ogridh + "'", con);
            try
            {
                adtr.Fill(ds, "homework");
                dataGridView1.DataSource = ds.Tables["homework"];
                adtr.Dispose();

            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
            con.Close();

            if (dataGridView1.Rows[0].Cells[0].Value == null)
            {
                nullmu = true;
            }
            else
            {
                nullmu = false;
            }
        }
        void doldurodevmiktar()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();

            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select ogrId,turkce,paragraf,sosyal,cografya,matematik,fen,kimya,biyoloji,dinkulturu,ingilizce,diger,deneme from ogrWork where ogrId = '" + homework.ogridh + "' and convert(varchar, insertDate, 104) = '" + homework.tarihh + "'", con);
            try
            {
                adtr.Fill(ds, "homework");
                dataGridView2.DataSource = ds.Tables["homework"];
                adtr.Dispose();
                int a = dataGridView2.Rows.Count;
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
            }
            con.Close();

            if (nullmu == false)
            {
                label8.Text = "(" + dataGridView1.Rows[0].Cells["turkce"].Value.ToString() + ")";
                label16.Text = "(" + dataGridView1.Rows[0].Cells["paragraf"].Value.ToString() + ")";
                label9.Text = "(" + dataGridView1.Rows[0].Cells["sosyal"].Value.ToString() + ")";
                label10.Text = "(" + dataGridView1.Rows[0].Cells["matematik"].Value.ToString() + ")";
                label11.Text = "(" + dataGridView1.Rows[0].Cells["fen"].Value.ToString() + ")";
                label12.Text = "(" + dataGridView1.Rows[0].Cells["dinkulturu"].Value.ToString() + ")";
                label13.Text = "(" + dataGridView1.Rows[0].Cells["ingilizce"].Value.ToString() + ")";
                label14.Text = "(" + dataGridView1.Rows[0].Cells["diger"].Value.ToString() + ")";
                label18.Text = "(" + dataGridView1.Rows[0].Cells["deneme"].Value.ToString() + ")";
            }
            
            if (ttl != "")
            {
                textBox1.Text = dataGridView2.Rows[0].Cells["turkce"].Value.ToString();
                textBox8.Text = dataGridView2.Rows[0].Cells["paragraf"].Value.ToString();
                textBox2.Text = dataGridView2.Rows[0].Cells["sosyal"].Value.ToString();
                textBox3.Text = dataGridView2.Rows[0].Cells["matematik"].Value.ToString();
                textBox4.Text = dataGridView2.Rows[0].Cells["fen"].Value.ToString();
                textBox5.Text = dataGridView2.Rows[0].Cells["dinkulturu"].Value.ToString();
                textBox6.Text = dataGridView2.Rows[0].Cells["ingilizce"].Value.ToString();
                textBox7.Text = dataGridView2.Rows[0].Cells["diger"].Value.ToString();
                textBox9.Text = dataGridView2.Rows[0].Cells["deneme"].Value.ToString();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Değiştir")
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox7.Enabled = true;
                button2.Text = "Kaydet";
            }
            else
            {
                if (nullmu == false)
                {
                    if (ttl == "")
                    {
                        odevekle();
                        ttl = "";
                    }
                    else
                    {
                        odeveditle();
                        ttl = "";
                    }
                }
                else
                {
                    MessageBox.Show(homework.ogrAdih + " " + homework.ogrsoyadih + " adlı öğrenciye tanımlı bir ödev bulunmuyor. Önce ödev tanımlayınız.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        void iftext()
        {
            if (textBox1.Text == "" || textBox1.Text == "Türkçe")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "" || textBox2.Text == "Sosyal")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "" || textBox3.Text == "Matematik")
            {
                textBox3.Text = "0";
            }
            if (textBox4.Text == "" || textBox4.Text == "Fen")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "" || textBox5.Text == "Din Kültürü ve Ahlak Bilgisi")
            {
                textBox5.Text = "0";
            }
            if (textBox6.Text == "" || textBox6.Text == "İngilizce")
            {
                textBox6.Text = "0";
            }
            if (textBox8.Text == "")
            {
                textBox8.Text = "0";
            }
            if (textBox7.Text == "")
            {          
                textBox7.Text = "0";
            }
            if (textBox9.Text == "")
            {
                textBox9.Text = "0";
            }

            tr = Convert.ToInt32(textBox1.Text);
            prg = Convert.ToInt32(textBox8.Text);
            sy = Convert.ToInt32(textBox2.Text);
            cg = 0;
            mat = Convert.ToInt32(textBox3.Text);
            fn = Convert.ToInt32(textBox4.Text);
            kim = 0;
            biy = 0;
            dn = Convert.ToInt32(textBox5.Text);
            ing = Convert.ToInt32(textBox6.Text);
            dg = Convert.ToInt32(textBox7.Text);
            den = Convert.ToInt32(textBox9.Text);
        }

        public static bool checkb = false;
        public static int totalintlim = 0;
        void kiyas()
        {
            if (Convert.ToInt32(dataGridView1.Rows[0].Cells["turkce"].Value) <= Convert.ToInt32(textBox1.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["paragraf"].Value) <= Convert.ToInt32(textBox8.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["sosyal"].Value) <= Convert.ToInt32(textBox2.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["matematik"].Value) <= Convert.ToInt32(textBox3.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["fen"].Value) <= Convert.ToInt32(textBox4.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["dinkulturu"].Value) <= Convert.ToInt32(textBox5.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["ingilizce"].Value) <= Convert.ToInt32(textBox6.Text) && Convert.ToInt32(dataGridView1.Rows[0].Cells["diger"].Value) <= Convert.ToInt32(textBox7.Text))
            {
                checkb = true;
            }
            else
            {
                checkb = false;
            }
        }
        public int limtr, limprg, limsy, limmat, limfn, limdn, liming, limdg;
        void findtotalintlimit()
        {
            limtr  = Convert.ToInt32(dataGridView1.Rows[0].Cells["turkce"].Value);
            limprg = Convert.ToInt32(dataGridView1.Rows[0].Cells["paragraf"].Value);
            limsy  = Convert.ToInt32(dataGridView1.Rows[0].Cells["sosyal"].Value);
            limmat = Convert.ToInt32(dataGridView1.Rows[0].Cells["matematik"].Value);
            limfn  = Convert.ToInt32(dataGridView1.Rows[0].Cells["fen"].Value);
            limdn  = Convert.ToInt32(dataGridView1.Rows[0].Cells["dinkulturu"].Value);
            liming = Convert.ToInt32(dataGridView1.Rows[0].Cells["ingilizce"].Value);
            limdg = Convert.ToInt32(dataGridView1.Rows[0].Cells["diger"].Value);
            totalintlim = limtr + limprg + limsy + limmat + limfn + limdn + liming + limdg;
        }
        ogrWorkTabloislemleri ogis;
        public int tr, prg, sy, cg, mat, fn, kim, biy, dn, ing, dg, den;
        void odevekle()
        {
            iftext();
            kiyas();
            findtotalintlimit();
            ogis = new ogrWorkTabloislemleri(tr, prg, sy, cg, mat, fn, kim, biy, dn, ing, dg, den, checkb, totalintlim);
            ogis.databaseinsert();
            MessageBox.Show("Yapılan Ödev Eklendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        void odeveditle()
        {
            iftext();
            kiyas();
            ogis = new ogrWorkTabloislemleri(tr, prg, sy, cg, mat, fn, kim, biy, dn, ing, dg, den, checkb, totalintlim);
            ogis.databaseEdit();
            MessageBox.Show("Ödev Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void odevEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            acikmi = false;
        }
    }
}
