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
    public partial class denemeEkle : Form
    {
        denemeler dnrid;
        public denemeEkle(denemeler dn)
        {
            InitializeComponent();
            this.dnrid = dn;
        }

        public static string alan = "";
        public static string grup = "";
        public static string yayinAdi = "";
        public static string denemeno = "";
        public static string denemeid = "";
        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (metroComboBox1.Text == "TYT" || metroComboBox1.Text == "AYT")
                {
                    grup = "Lise";
                }
                else
                {
                    grup = "Ortaokul";
                }
                alan = metroComboBox1.Text;
                yayinAdi = textBox1.Text;
            }
            else
            {
                MessageBox.Show("Deneme Adı alanı boş bırakılamaz.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Deneme Adı")
            {
                textBox1.Text = "";
            }
        }

        public static bool kaydet = false;
        private void button2_Click(object sender, EventArgs e)
        {
            denemeler.editing = false;
            kaydet = true;
            denemePuanlama dn = new denemePuanlama();
            dn.Show();
            this.Close();
        }
        programLog prlg;
        private void denemeEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kaydet == true && textBox1.Text != "Deneme Adı" && metroComboBox1.Text != "")
            {
                denemeler.opencontrol = false;
                SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
                con.Open();
                string sqlquery1 = "SELECT MAX(denemeId) FROM denemeler";
                SqlCommand command3 = new SqlCommand(sqlquery1, con);
                string denemeNoo = "";
                try { denemeNoo = command3.ExecuteScalar().ToString(); } catch (SqlException ex) { prlg = new programLog(ex.Message, this.Text, "PRLG4"); prlg.databaseinsert(); MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG4", "Sistem Mesajı"); }
                //PROGRAMLOG
                if (denemeNoo == "")
                {
                    denemeNoo = "0";
                }
                SqlCommand command = new SqlCommand("Insert Into denemeler(denemeNo, yayinAdi, denemeTarihi,denemeAlani,status,insertDate,userId, editDate) Values (" +
                "@denemeNo, @yayinAdi, @denemeTarihi,@denemeAlani,@status,@insertDate,@userId, @editDate)", con);
                command.Parameters.AddWithValue("@denemeNo", Convert.ToInt32(denemeNoo) + 1);
                denemeno = (Convert.ToInt32(denemeNoo) + 1).ToString();
                command.Parameters.AddWithValue("@yayinAdi", textBox1.Text);
                command.Parameters.AddWithValue("@denemeTarihi", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@denemeAlani", metroComboBox1.Text);
                command.Parameters.AddWithValue("@status", "1");
                command.Parameters.AddWithValue("@insertDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@userId", login.userid);
                command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    command.ExecuteNonQuery();

                    string sqlquery = "SELECT denemeId FROM denemeler where denemeNo = '" + denemeno + "'";
                    SqlCommand command2 = new SqlCommand(sqlquery, con);
                    //denemeid = "";
                    try { denemeid = command2.ExecuteScalar().ToString(); } catch (SqlException ex) { prlg = new programLog(ex.Message, this.Text, "PRLG3"); prlg.databaseinsert(); MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG3", "Sistem Mesajı"); }
                    //PROGRAMLOG
                }
                catch (SqlException ex)
                {
                    prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                    prlg.databaseinsert();

                    MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
                }
                con.Close();
                dnrid.doldurDeneme();
            }
            denemeler.opencontrol = false;
        }
    }
}
