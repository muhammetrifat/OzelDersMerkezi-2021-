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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Kullanıcı Adı")
            {
                textBox1.Text = "";
                if (textBox2.Text == "")
                {
                    textBox2.Text = "Şifre";
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    textBox2.Text = "Şifre";
                }
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Şifre")
            {
                textBox2.Text = "";
                if (textBox1.Text == "")
                {
                    textBox1.Text = "Kullanıcı Adı";
                }
            }
            else
            {
                if (textBox1.Text == "")
                {
                    textBox1.Text = "Kullanıcı Adı";
                }
            }
        }

        public static string userid;
        programLog prlg;
        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            metroComboBox1.Text = "2021 - 2022";
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from users", con);
            try
            {
                adtr.Fill(ds, "users");
                dataGridView1.DataSource = ds.Tables["users"];
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adtr.Dispose();
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }

            con.Close();
        }

        void giriskodu()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            con.Open();

            command.CommandText = "select * from users where userName = '" + textBox1.Text + "' and userPassword = '" + textBox2.Text + "' and userStatus = '1'";


            try
            {
                SqlDataReader reader = command.ExecuteReader();
                int sayi = 0;
                while (reader.Read())
                {
                    sayi++;
                }

                if (sayi == 1)
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adtr = new SqlDataAdapter("Select * from users", con);

                    int rowindex = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (row.Cells["userName"].Value.ToString() == textBox1.Text)
                        {
                            rowindex = row.Index;
                            break;
                        }
                    }

                    DataGridViewRow selectedRow = new DataGridViewRow();
                    selectedRow = dataGridView1.Rows[rowindex];

                    string kullaniciadi = selectedRow.Cells[2].Value.ToString();
                    AutoClosingMessageBox.Show("Sayın " + kullaniciadi + " hoşgeldiniz. Giriş işlemi yapılıyor..", "Sistem Mesajı", 2000);
                    userid = selectedRow.Cells[0].Value.ToString();
                    userlog();
                    home ho = new home();
                    ho.Show();
                    string metrocom = metroComboBox1.Text;
                    homework.degisebiliryear = Convert.ToInt32(metrocom.Remove(4));
                    chartReportOdev.degisebiliryear = Convert.ToInt32(metrocom.Remove(4));
                    this.Hide();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya parola hatalı", "Notice");
                }
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
            }
        }

        void userlog()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command5 = new SqlCommand("Insert Into userlog(user_id,form_name,islem,log_date) Values (@userid,@formname,@islem, @logdate)", con);
            command5.Parameters.AddWithValue("@userid", login.userid);
            command5.Parameters.AddWithValue("@formname", this.Text);
            command5.Parameters.AddWithValue("@islem", button1.Text);
            command5.Parameters.AddWithValue("@logdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
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
            con.Close();
        }
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            giriskodu();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                giriskodu();
            }
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                giriskodu();
            }
        }
    }
}

