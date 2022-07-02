using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Data.SqlClient;

namespace degisimAkademi
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void home_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            panelAligner();
        }
        void panelAligner()
        {
            panelOgrenciler.Dock = DockStyle.Fill;
            panelOgretmen.Dock = DockStyle.Fill;
            panelRaporlama.Dock = DockStyle.Fill;
            panelDenemeler.Dock = DockStyle.Fill;
            panelOdevler.Dock = DockStyle.Fill;
        }
        private void button6_Click(object sender, EventArgs e)//ÖĞRENCİLER
        {
            if (panelOgrenciler.Visible == true)
            {
                panelOgrenciler.Visible = false;
            }
            else
            {
                panelOgrenciler.Controls.Clear();
                panelOgrenciler.Visible = true;
                panelOgretmen.Visible = false;
                panelRaporlama.Visible = false;
                panelDenemeler.Visible = false;
                panelOdevler.Visible = false;
                ogrenciler pr = new ogrenciler() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                pr.FormBorderStyle = FormBorderStyle.None;
                this.panelOgrenciler.Controls.Add(pr);
                pr.Show();
            }
        }

        private void home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

            //string[] filePaths = Directory.GetFiles(@"C:/Users/Muhammed/Documents/DegisimAkademiBackups");
            //foreach (string filePath in filePaths)
            //    File.Delete(filePath);
            //
            //
            //string tarihselkayit = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");
            //SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            //SqlCommand cmd = new SqlCommand("BACKUP DATABASE [DegisimAkademi] TO DISK = N'C:/Users/Muhammed/Documents/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".bak' WITH INIT", con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //string dosya1;
            //dosya1 = "C:/Users/Muhammed/Documents/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".bak";
            //
            //using (ZipFile zip = new ZipFile())
            //{
            //    zip.Password = "fatihbetulhafsaahsenmusabdemirci";
            //    zip.AddFile(dosya1);
            //    zip.Save("C:/Users/Muhammed/Documents/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".rar");
            //}
            //
            //File.Delete("C:/Users/Muhammed/Documents/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".bak");

            string[] filePaths = Directory.GetFiles(@"C:/DegisimAkademiBackups");
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            
            
            string tarihselkayit = DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss");
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand cmd = new SqlCommand("BACKUP DATABASE [DegisimAkademi] TO DISK = N'C:/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".bak' WITH INIT", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string dosya1;
            dosya1 = "C:/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".bak";
            
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = "fatihbetulhafsaahsenmusabdemirci";
                zip.AddFile(dosya1);
                zip.Save("C:/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".rar");
            }
            
            File.Delete("C:/DegisimAkademiBackups/DegisimAkademi" + tarihselkayit + ".bak");
        }

        private void button2_Click(object sender, EventArgs e)//DENEMELER
        {
            if (panelDenemeler.Visible == true)
            {
                panelDenemeler.Visible = false;
            }
            else
            {
                panelDenemeler.Controls.Clear();
                panelDenemeler.Visible = true;
                panelOgretmen.Visible = false;
                panelRaporlama.Visible = false;
                panelOgrenciler.Visible = false;
                panelOdevler.Visible = false;
                denemeler pr = new denemeler() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                pr.FormBorderStyle = FormBorderStyle.None;
                this.panelDenemeler.Controls.Add(pr);
                pr.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)//RAPORLAR
        {
            if (panelRaporlama.Visible == true)
            {
                panelRaporlama.Visible = false;
            }
            else
            {
                panelRaporlama.Controls.Clear();
                panelRaporlama.Visible = true;
                panelOgretmen.Visible = false;
                panelDenemeler.Visible = false;
                panelOgrenciler.Visible = false;
                panelOdevler.Visible = false;
                report re = new report() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                re.FormBorderStyle = FormBorderStyle.None;
                this.panelRaporlama.Controls.Add(re);
                re.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)//ÖĞRETMENLER
        {

        }

        private void button3_Click(object sender, EventArgs e)//ÖDEVLER
        {
            if (panelOdevler.Visible == true)
            {
                panelOdevler.Visible = false;
            }
            else
            {
                panelOdevler.Controls.Clear();
                panelOdevler.Visible = true;
                panelOgretmen.Visible = false;
                panelDenemeler.Visible = false;
                panelOgrenciler.Visible = false;
                panelRaporlama.Visible = false;
                homework hw = new homework() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                hw.FormBorderStyle = FormBorderStyle.None;
                this.panelOdevler.Controls.Add(hw);
                hw.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
