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
    public partial class denemeler : Form
    {
        public denemeler()
        {
            InitializeComponent();
        }

        public static int kayitSayisi = 0;
        private void denemeler_Load(object sender, EventArgs e)
        {
            //alanfilter = "TYT";
            //doldurDeneme();
            metroTabControl1_SelectedIndexChanged(sender, e);
            kayitSayisi = dataGridView1.Rows.Count;
            UpdateFont();
            comboBox1.Text = "Yayın Adına Göre";
        }
        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            }
        }
        programLog prlg;
        public void doldurDeneme()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from denemeler where status = '1' and denemeAlani = '" + alanfilter + "' order by denemeTarihi desc;", con);
            try
            {
                adtr.Fill(ds, "denemeler");
                dataGridView1.DataSource = ds.Tables["denemeler"];
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adtr.Dispose();

                this.dataGridView1.Columns["denemeId"].Visible = false;
                this.dataGridView1.Columns["insertDate"].Visible = false;
                this.dataGridView1.Columns["userId"].Visible = false;
                this.dataGridView1.Columns["editDate"].Visible = false;
                this.dataGridView1.Columns["status"].Visible = false;

                dataGridView1.Columns[1].HeaderText = "Deneme Numarası";
                dataGridView1.Columns[2].HeaderText = "Yayın Adı";
                dataGridView1.Columns[3].HeaderText = "Deneme Tarihi";
                dataGridView1.Columns[4].HeaderText = "Deneme Alanı";

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
                    column.Width = 270;
                    if (i == 2)
                    {
                        column.Width = 300;
                    }
                }
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //for (int i = 0; i < ; i++)
                //{
                //    DataGridViewRow row = dataGridView1.Rows[i];
                //    row.Height = 50;
                //}
                denemerowcount = dataGridView1.Rows.Count;
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
            con.Close();
        }

        public static int denemerowcount = 0;
        public static bool opencontrol = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (opencontrol == false)
            {
                denemeEkle dnek = new denemeEkle(this);
                dnek.Show();
                opencontrol = true;
            }
        }

        public static bool editing = false;
        public static string denemeid, denemeAlan;


        public static string alanfilter;


        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedTab.Text == "TYT")
            {
                alanfilter = "TYT";
                doldurDeneme();
                //metroTabControl1.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
            }

            else if (metroTabControl1.SelectedTab.Text == "AYT")
            {
                alanfilter = "AYT";
                doldurDeneme();
                //metroTabControl1.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
            }

            else if (metroTabControl1.SelectedTab.Text == "LGS")
            {
                alanfilter = "LGS";
                doldurDeneme();
                //metroTabControl1.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
            }
        }

        public static int currentMouseOverRow;

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contexMenu = new ContextMenuStrip();


                currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    contexMenu.Items.Add("Detayları Görüntüle");
                }

                contexMenu.Show(dataGridView1, new Point(e.X, e.Y));
                contexMenu.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);
            }
        }

        public static string tarih, denemeadi, denemealani, denemeaydi;

        void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            denemeaydi = dataGridView1.Rows[currentMouseOverRow].Cells["denemeId"].Value.ToString();
            denemealani = dataGridView1.Rows[currentMouseOverRow].Cells["denemeAlani"].Value.ToString();
            denemeadi = dataGridView1.Rows[currentMouseOverRow].Cells["yayinAdi"].Value.ToString();
            tarih = dataGridView1.Rows[currentMouseOverRow].Cells["denemeTarihi"].Value.ToString();
            denemeDetay dt = new denemeDetay();
            dt.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            denemeid = dataGridView1.CurrentRow.Cells["denemeId"].Value.ToString();
            denemeAlan = dataGridView1.CurrentRow.Cells["denemeAlani"].Value.ToString();
            editing = true;
            denemePuanlama dn = new denemePuanlama();
            dn.Show();
            //editing = false;
            //denemeAlan = "";
        }
    }
}
