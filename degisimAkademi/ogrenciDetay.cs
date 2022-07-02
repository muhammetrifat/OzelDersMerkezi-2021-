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
    public partial class ogrenciDetay : Form
    {
        ogrenciler ogrid;
        public ogrenciDetay(ogrenciler og)
        {
            InitializeComponent();
            this.ogrid = og;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == false)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                checkBox1.Enabled = true;
                metroComboBox1.Enabled = true;
                metroComboBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                checkBox1.Enabled = false;
                metroComboBox1.Enabled = false;
                metroComboBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }

        programLog prlg;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == false)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                checkBox1.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                metroComboBox1.Enabled = true;
                metroComboBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                comboBox1.Enabled = true;
                label2.Enabled = true;
                button2.Text = "Kaydet";
                button4.Visible = true;
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == "Öğrenci Adı" || textBox2.Text == "Öğrenci Soyadı")
                {
                    MessageBox.Show("Ad - Soyad alanı boş bırakılamaz.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
                    con.Open();
                    SqlCommand command5 = new SqlCommand("Insert Into userlog(user_id,form_name,islem,log_date) Values (@userid,@formname,@islem, @logdate)", con);
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
                        prlg = new programLog(ex.Message, this.Text, "PRLG3");//PROGRAMLOG
                        prlg.databaseinsert();
                        MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG3", "Sistem Mesajı");
                    }


                    SqlCommand command = new SqlCommand("update Ogrenciler set name=@name," +
                        "surname=@surname,ogrGrup=@grup," +
                        "ogrAlan=@alan, schoolName=@school,ogrAddDate=@ogradddate," +
                        "tcNo=@tcno," +
                        "gsm=@gsm,veliName=@veliname, veliGsm=@veligsm, status=@status," +
                        "userId=@userid,editDate=@editdate, ogrLevel=@ogrlevel where ogrId = '" + ogrenciler.ogrId + "'", con);
                    //command.Parameters.AddWithValue("@ogrno", textBox1.Text);
                    command.Parameters.AddWithValue("@ogradddate", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@name", textBox1.Text);
                    command.Parameters.AddWithValue("@surname", textBox2.Text);
                    command.Parameters.AddWithValue("@school", textBox3.Text);
                    command.Parameters.AddWithValue("@tcno", textBox4.Text);
                    command.Parameters.AddWithValue("@gsm", textBox5.Text);
                    command.Parameters.AddWithValue("@veliname", textBox6.Text);
                    command.Parameters.AddWithValue("@veligsm", textBox7.Text);
                    command.Parameters.AddWithValue("@grup", metroComboBox1.Text);
                    if (metroComboBox2.Visible == false) { command.Parameters.AddWithValue("@alan", ""); }
                    else { command.Parameters.AddWithValue("@alan", metroComboBox2.Text); }



                    if (radioButton1.Checked == true) { command.Parameters.AddWithValue("@status", "1"); }
                    else { command.Parameters.AddWithValue("@status", "0"); }
                    command.Parameters.AddWithValue("@userid", login.userid);
                    command.Parameters.AddWithValue("@editdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (comboBox1.Text != "")
                    {
                        command.Parameters.AddWithValue("@ogrlevel", comboBox1.Text);
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                        prlg.databaseinsert();

                        if (ex.Number == 2627)
                        {
                            MessageBox.Show("Bu Müşteri numarası zaten kullanılıyor.", "Sistem Mesajı");
                        }
                        else
                        {
                            MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
                        }
                    }
                    con.Close();
                    ogrid.doldurOgrenci();
                    this.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            checkBox1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            metroComboBox1.Enabled = false;
            metroComboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
            label2.Enabled = false;
            button2.Text = "Düzenle";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                DialogResult c;
                c = MessageBox.Show("Kaydı pasifleştirmek istediğinizden emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (c == DialogResult.Yes)
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;
                }
            }
        }

        private void ogrenciDetay_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = ogrenciler.ogrAddDate;
            textBox1.Text = ogrenciler.ogrName;
            textBox2.Text = ogrenciler.ogrSurname;
            textBox3.Text = ogrenciler.ogrSchool;
            textBox4.Text = ogrenciler.ogrTcNo;
            textBox5.Text = ogrenciler.ogrGsm;
            textBox6.Text = ogrenciler.ogrVeli;
            textBox7.Text = ogrenciler.veliGsm;
            metroComboBox1.Text = ogrenciler.ogrGrup;
            metroComboBox2.Text = ogrenciler.ogrAlan;
            comboBox1.Text = ogrenciler.ogrlevel;
            if (ogrenciler.ogrGrup == "Lise") { metroComboBox2.Visible = true; }
            else { metroComboBox2.Visible = false; }
            if (ogrenciler.status == "1") { radioButton1.Checked = true; }
            else { radioButton1.Checked = false; }
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
