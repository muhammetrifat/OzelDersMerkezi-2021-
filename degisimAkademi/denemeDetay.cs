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
    public partial class denemeDetay : Form
    {
        public denemeDetay()
        {
            InitializeComponent();
        }
        programLog prlg;
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Değiştir")
            {
                dateTimePicker1.Enabled = true;
                textBox1.Enabled = true;
                metroComboBox1.Enabled = true;
                button1.Enabled = true;
                button2.Text = "Güncelle";
            }
            else
            {
                SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
                SqlCommand command = new SqlCommand("update denemeler set yayinAdi=@yayinAdi, denemeTarihi=@denemeTarihi, denemeAlani=@denemeAlani," +
                            "userId=@userId,editDate=@editDate where denemeId = '" + denemeler.denemeaydi + "'", con);
                command.Parameters.AddWithValue("@yayinAdi", textBox1.Text);
                command.Parameters.AddWithValue("@denemeTarihi", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@denemeAlani", metroComboBox1.Text);
                command.Parameters.AddWithValue("@userId", login.userid);
                command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                con.Open();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Deneme Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                    prlg.databaseinsert();

                    MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
                }
                con.Close();
                this.Close();
            }
        }

        public string tabloadi;
        private void denemeDetay_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(denemeler.tarih);
            textBox1.Text = denemeler.denemeadi;
            metroComboBox1.Text = denemeler.denemealani;
            if (metroComboBox1.Text == "TYT")
            {
                tabloadi = "tytPuanlari";
            }
            else if (metroComboBox1.Text == "AYT")
            {
                tabloadi = "aytPuanlari";
            }
            else
            {
                tabloadi = "LgsPuanlari";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command = new SqlCommand("delete from denemeler where denemeId = '" + denemeler.denemeaydi + "'", con);
            SqlCommand command1 = new SqlCommand("delete from "+tabloadi+" where denemeId = '" + denemeler.denemeaydi + "'", con);
            con.Open();
            try
            {
                command.ExecuteNonQuery();
                command1.ExecuteNonQuery();
                MessageBox.Show("Deneme Silindi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();

                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
            con.Close();
            this.Close();
        }//SİL
    }
}
