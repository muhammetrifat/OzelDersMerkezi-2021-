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
    public partial class ogrEkle : Form
    {
        ogrenciler ogrid;
        public ogrEkle(ogrenciler og)
        {
            InitializeComponent();
            this.ogrid = og;
        }
        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedIndex + 1 > 1)
            {
                metroComboBox2.Visible = true;
            }
            else
            {
                metroComboBox2.Visible = false;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Öğrenci Adı")
            {
                textBox1.Text = "";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "Öğrenci Soyadı";
            }

            if (textBox3.Text == "")
            {
                textBox3.Text = "Eğitim Gördüğü Okul";
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "TC Kimlik No";
            }

            if (textBox5.Text == "")
            {
                textBox5.Text = "Öğrenci GSM";
            }

            if (textBox6.Text == "")
            {
                textBox6.Text = "Veli Adı";
            }

            if (textBox7.Text == "")
            {
                textBox7.Text = "Veli GSM";
            }

        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Öğrenci Soyadı")
            {
                textBox2.Text = "";
            }

            if (textBox1.Text == "")
            {
                textBox1.Text = "Öğrenci Adı";
            }

            if (textBox3.Text == "")
            {
                textBox3.Text = "Eğitim Gördüğü Okul";
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "TC Kimlik No";
            }

            if (textBox5.Text == "")
            {
                textBox5.Text = "Öğrenci GSM";
            }

            if (textBox6.Text == "")
            {
                textBox6.Text = "Veli Adı";
            }

            if (textBox7.Text == "")
            {
                textBox7.Text = "Veli GSM";
            }
        }
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox3.Text == "Eğitim Gördüğü Okul")
            {
                textBox3.Text = "";
            }

            if (textBox1.Text == "")
            {
                textBox1.Text = "Öğrenci Adı";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "Öğrenci Soyadı";
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "TC Kimlik No";
            }

            if (textBox5.Text == "")
            {
                textBox5.Text = "Öğrenci GSM";
            }

            if (textBox6.Text == "")
            {
                textBox6.Text = "Veli Adı";
            }

            if (textBox7.Text == "")
            {
                textBox7.Text = "Veli GSM";
            }

        }
        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox4.Text == "TC Kimlik No")
            {
                textBox4.Text = "";
            }

            if (textBox1.Text == "")
            {
                textBox1.Text = "Öğrenci Adı";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "Öğrenci Soyadı";
            }

            if (textBox3.Text == "")
            {
                textBox3.Text = "Eğitim Gördüğü Okul";
            }

            if (textBox5.Text == "")
            {
                textBox5.Text = "Öğrenci GSM";
            }

            if (textBox6.Text == "")
            {
                textBox6.Text = "Veli Adı";
            }

            if (textBox7.Text == "")
            {
                textBox7.Text = "Veli GSM";
            }

        }
        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox5.Text == "Öğrenci GSM")
            {
                textBox5.Text = "";
            }

            if (textBox1.Text == "")
            {
                textBox1.Text = "Öğrenci Adı";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "Öğrenci Soyadı";
            }

            if (textBox3.Text == "")
            {
                textBox3.Text = "Eğitim Gördüğü Okul";
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "TC Kimlik No";
            }

            if (textBox6.Text == "")
            {
                textBox6.Text = "Veli Adı";
            }

            if (textBox7.Text == "")
            {
                textBox7.Text = "Veli GSM";
            }

        }
        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox6.Text == "Veli Adı")
            {
                textBox6.Text = "";
            }

            if (textBox1.Text == "")
            {
                textBox1.Text = "Öğrenci Adı";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "Öğrenci Soyadı";
            }

            if (textBox3.Text == "")
            {
                textBox3.Text = "Eğitim Gördüğü Okul";
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "TC Kimlik No";
            }

            if (textBox5.Text == "")
            {
                textBox5.Text = "Öğrenci GSM";
            }

            if (textBox7.Text == "")
            {
                textBox7.Text = "Veli GSM";
            }

        }
        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox7.Text == "Veli GSM")
            {
                textBox7.Text = "";
            }

            if (textBox1.Text == "")
            {
                textBox1.Text = "Öğrenci Adı";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "Öğrenci Soyadı";
            }

            if (textBox3.Text == "")
            {
                textBox3.Text = "Eğitim Gördüğü Okul";
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "TC Kimlik No";
            }

            if (textBox5.Text == "")
            {
                textBox5.Text = "Öğrenci GSM";
            }

            if (textBox6.Text == "")
            {
                textBox6.Text = "Veli Adı";
            }

        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control && e.KeyChar == '*' || (Control.ModifierKeys & Keys.Shift) == Keys.Shift && e.KeyChar == ' ')
            {
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        programLog prlg;
        private void button2_Click(object sender, EventArgs e)//ÖĞRENCİ EKLEME BUTONU
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            //DateTime dt = DateTime.Now;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == "Öğrenci Adı" || textBox2.Text == "Öğrenci Soyadı")
            {
                MessageBox.Show("Ad - Soyad alanı boş bırakılamaz.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                con.Open();
                SqlCommand command5 = new SqlCommand("Insert Into userlog(user_id, form_name,islem,log_date) Values (@userid,@formname,@islem, @logdate)", con);
                command5.Parameters.AddWithValue("@userid", login.userid);
                command5.Parameters.AddWithValue("@formname", this.Text);
                command5.Parameters.AddWithValue("@islem", button2.Text);
                command5.Parameters.AddWithValue("@logdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                try
                {
                    command5.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                    prlg.databaseinsert();
                    MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
                }

                SqlCommand command = new SqlCommand("Insert Into Ogrenciler(ogrNo,name,surname,ogrGrup,ogrAlan, schoolName, ogrAddDate, tcNo, gsm, veliName,veliGsm, status, userId, editDate,insertDate,ogrLevel) Values (" +
                    "@ogrNo, @name, @surname,@ogrGrup,@ogrAlan,@schoolName,@ogrAddDate, @tcNo, @gsm,@veliName,@veliGsm, @status, @userId, @editDate,@insertDate,@ogrlevel)", con);
                command.Parameters.AddWithValue("@ogrNo", ogrenciler.kayitSayisi + 1);
                command.Parameters.AddWithValue("@name", textBox1.Text);
                command.Parameters.AddWithValue("@surname", textBox2.Text);
                if (textBox3.Text == "" || textBox3.Text == "Eğitim Gördüğü Okul") { command.Parameters.AddWithValue("@schoolName", ""); }
                else { command.Parameters.AddWithValue("@schoolName", textBox3.Text); }
                if (textBox4.Text == "" || textBox4.Text == "TC Kimlik No") { command.Parameters.AddWithValue("@tcNo", ""); }
                else { command.Parameters.AddWithValue("@tcNo", textBox4.Text); }
                if (textBox5.Text == "" || textBox5.Text == "Öğrenci GSM") { command.Parameters.AddWithValue("@gsm", ""); }
                else { command.Parameters.AddWithValue("@gsm", textBox5.Text); }
                if (textBox6.Text == "" || textBox6.Text == "Veli Adı") { command.Parameters.AddWithValue("@veliName", ""); }
                else { command.Parameters.AddWithValue("@veliName", textBox6.Text); }
                if (textBox7.Text == "" || textBox7.Text == "Veli GSM") { command.Parameters.AddWithValue("@veliGsm", ""); }
                else { command.Parameters.AddWithValue("@veliGsm", textBox7.Text); }

                command.Parameters.AddWithValue("@ogrGrup", metroComboBox1.Text);

                if (metroComboBox1.SelectedIndex + 1 > 1) { command.Parameters.AddWithValue("@ogrAlan", metroComboBox2.Text); }
                else { command.Parameters.AddWithValue("@ogrAlan", ""); }

                command.Parameters.AddWithValue("@ogrAddDate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@status", "1");
                command.Parameters.AddWithValue("@insertDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@userId", login.userid);
                command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (comboBox1.Text != "")
                {
                    command.Parameters.AddWithValue("@ogrlevel", comboBox1.Text);
                }
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Tamamlandı", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                    prlg.databaseinsert();

                    MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
                }
                con.Close();
                this.Controls.Clear();
                this.InitializeComponent();
                ogrid.doldurOgrenci();
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "9" || comboBox1.Text == "10" || comboBox1.Text == "11" || comboBox1.Text == "12" || comboBox1.Text == "Mezun")
            {
                metroComboBox1.Text = "Lise";
            }
            else
            {
                metroComboBox1.Text = "Ortaokul";
            }
        }
    }
}
