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
    public partial class denemePuanlama : Form
    {
        public denemePuanlama()
        {
            InitializeComponent();
        }
        private void denemePuanlama_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            WindowState = FormWindowState.Maximized;
            doldurpuan();

            string tabloadi = "";
            if (denemeEkle.alan == "TYT" || denemeler.denemeAlan == "TYT")
            {
                tabloadi = "tytPuanlari";
                dataGridView2.Columns.Add("turkce1", "Türkçe");
                dataGridView2.Columns.Add("sos1", "Sosyal Bilimler");
                dataGridView2.Columns.Add("mat1", "Matematik");
                dataGridView2.Columns.Add("fen1", "Fen Bilimleri");
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView2.Columns[i];
                    column.Width = 150;
                    if (i == 2 || i == 3 || i == 4 || i == 5)
                    {
                        column.Width = 240;
                    }
                }
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else if (denemeEkle.alan == "AYT" || denemeler.denemeAlan == "AYT")
            {
                tabloadi = "aytPuanlari";
                dataGridView2.Columns.Add("turkce1", "Türk Dili ve Edebiyatı-Sosyal Bilimler - 1");
                dataGridView2.Columns.Add("sos1", "Sosyal Bilimler - 2");
                dataGridView2.Columns.Add("mat1", "Matematik");
                dataGridView2.Columns.Add("fen1", "Fen Bilimleri");
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView2.Columns[i];
                    column.Width = 150;
                    if (i == 2 || i == 3 || i == 4 || i == 5)
                    {
                        column.Width = 240;
                    }
                }
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else if (denemeEkle.alan == "LGS" || denemeler.denemeAlan == "LGS")
            {
                tabloadi = "LgsPuanlari";
                dataGridView2.Columns.Add("turkce1", "Türkçe");
                dataGridView2.Columns.Add("sos1", "Sosyal Bilgiler - İnkılap Tarihi");
                dataGridView2.Columns.Add("din1", "Din Kültürü");
                dataGridView2.Columns.Add("ing1", "İngilizce");
                dataGridView2.Columns.Add("mat1", "Matematik");
                dataGridView2.Columns.Add("fen1", "Fen Bilimleri");

                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView2.Columns[i];
                    column.Width = 150;
                    if (i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7)
                    {
                        column.Width = 180;
                    }
                }
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            if (denemeler.editing == true)//EDIT
            {
                dataGridView1.Visible = false;
                dataGridView3.Visible = true;
                string sorgu1 = "select o.ogrId as id, o.name as isim, o.surname as soyisim, t.trd as Doğru , t.try as Yanlış, t.trn as Net, t.sysd as Doğru, t.sysy as Yanlış, t.sysn as Net, t.matd as Doğru, t.maty as Yanlış, t.matn as Net, t.fend as Doğru, t.feny as Yanlış, t.fenn as Net from " + tabloadi + " t inner join Ogrenciler o on o.ogrId = t.ogrId where t.denemeId = '" + denemeler.denemeid + "'";
                string sorgu2 = "select o.ogrId as id, o.name as isim, o.surname as soyisim, t.trd as Doğru , t.try as Yanlış, t.trn as Net, t.sysd as Doğru, t.sysy as Yanlış, t.sysn as Net, t.dind as Doğru, t.diny as Yanlış, t.dinn as Net, t.ingd as Doğru, t.ingy as Yanlış, t.ingn as Net, t.matd as Doğru, t.maty as Yanlış, t.matn as Net, t.fend as Doğru, t.feny as Yanlış, t.fenn as Net from " + tabloadi + " t inner join Ogrenciler o on o.ogrId = t.ogrId where t.denemeId = '" + denemeler.denemeid + "'";
                con.Open();
                SqlCommand command = new SqlCommand(sorgu1, con);
                SqlCommand command2 = new SqlCommand(sorgu2, con);
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                if (tabloadi == "LgsPuanlari")
                {
                    dt.Load(command2.ExecuteReader());

                }
                else
                {
                    dt.Load(command.ExecuteReader());
                }
                dataGridView3.Columns.Add("id", "BOŞALAN");//
                doldurpuanedit();
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView3.Rows.Add(dr.ItemArray);
                }
                con.Close();
            }
            foreach (DataGridViewRow rw in this.dataGridView1.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        rw.Cells[i].Value = "0";
                    }
                }
            }
            foreach (DataGridViewRow rw in this.dataGridView3.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        rw.Cells[i].Value = "0";
                    }
                }
            }
        }
        programLog prlg;
        public void doldurpuan()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            con.Open();
            string grup = "";
            if (denemeEkle.grup == "Lise" || denemeler.denemeAlan == "TYT" || denemeler.denemeAlan == "AYT")
            {
                grup = "Lise";
            }
            else if (denemeEkle.grup == "Ortaokul" || denemeler.denemeAlan == "LGS")
            {
                grup = "Ortaokul";
            }
            SqlDataAdapter adtr = new SqlDataAdapter("Select ogrId, name, surname from Ogrenciler where status = '1' and ogrGrup = '" + grup + "'", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView1.DataSource = ds.Tables["Ogrenciler"];
                adtr.Dispose();
                int data = dataGridView1.Rows.Count;
                this.dataGridView1.Columns["ogrId"].Visible = false;
                if (denemeEkle.alan == "LGS" || denemeler.denemeAlan == "LGS")
                {
                    dataGridView1.Columns.Add("dgr1", "Doğru");//3
                    dataGridView1.Columns.Add("ynl1", "Yanlış");
                    dataGridView1.Columns.Add("net1", "Net");

                    dataGridView1.Columns.Add("dgr2", "Doğru");
                    dataGridView1.Columns.Add("ynl2", "Yanlış");
                    dataGridView1.Columns.Add("net2", "Net");

                    dataGridView1.Columns.Add("dgr5", "Doğru");
                    dataGridView1.Columns.Add("ynl5", "Yanlış");
                    dataGridView1.Columns.Add("net5", "Net");

                    dataGridView1.Columns.Add("dgr6", "Doğru");
                    dataGridView1.Columns.Add("ynl6", "Yanlış");
                    dataGridView1.Columns.Add("net6", "Net");

                    dataGridView1.Columns.Add("dgr3", "Doğru");
                    dataGridView1.Columns.Add("ynl3", "Yanlış");
                    dataGridView1.Columns.Add("net3", "Net");

                    dataGridView1.Columns.Add("dgr4", "Doğru");
                    dataGridView1.Columns.Add("ynl4", "Yanlış");
                    dataGridView1.Columns.Add("net4", "Net");

                    this.dataGridView1.Columns["net5"].ReadOnly = true;
                    this.dataGridView1.Columns["net6"].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Columns.Add("dgr1", "Doğru");//3
                    dataGridView1.Columns.Add("ynl1", "Yanlış");
                    dataGridView1.Columns.Add("net1", "Net");

                    dataGridView1.Columns.Add("dgr2", "Doğru");
                    dataGridView1.Columns.Add("ynl2", "Yanlış");
                    dataGridView1.Columns.Add("net2", "Net");

                    dataGridView1.Columns.Add("dgr3", "Doğru");
                    dataGridView1.Columns.Add("ynl3", "Yanlış");
                    dataGridView1.Columns.Add("net3", "Net");

                    dataGridView1.Columns.Add("dgr4", "Doğru");
                    dataGridView1.Columns.Add("ynl4", "Yanlış");
                    dataGridView1.Columns.Add("net4", "Net");
                }

                this.dataGridView1.Columns["net1"].ReadOnly = true;
                this.dataGridView1.Columns["net2"].ReadOnly = true;
                this.dataGridView1.Columns["net3"].ReadOnly = true;
                this.dataGridView1.Columns["net4"].ReadOnly = true;
                this.dataGridView1.Columns[1].ReadOnly = true;
                this.dataGridView1.Columns[2].ReadOnly = true;

                dataGridView1.Columns[1].HeaderText = "";
                dataGridView1.Columns[2].HeaderText = "";

                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (denemeEkle.alan == "TYT" || denemeEkle.alan == "AYT")
                    {
                        DataGridViewColumn column = dataGridView1.Columns[i];
                        column.Width = 150;
                        if (i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14)
                        {
                            column.Width = 80;
                        }
                    }
                    else
                    {
                        DataGridViewColumn column = dataGridView1.Columns[i];
                        column.Width = 150;
                        if (i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16 || i == 17 || i == 18 || i == 19 || i == 20)
                        {
                            column.Width = 60;
                        }
                    }
                }
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
                }
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
            con.Close();
            dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
        }
        public void doldurpuanedit()
        {
            if (denemeler.denemeAlan == "LGS")
            {
                dataGridView3.Columns.Add("bosalan1", "");//
                dataGridView3.Columns.Add("bosalan2", "");//
                //dataGridView3.Columns.Add("id", "BOŞALAN");//
                this.dataGridView3.Columns["id"].Visible = false;

                dataGridView3.Columns.Add("dgr1", "Doğru");//
                dataGridView3.Columns.Add("ynl1", "Yanlış");
                dataGridView3.Columns.Add("net1", "Net");

                dataGridView3.Columns.Add("dgr2", "Doğru");
                dataGridView3.Columns.Add("ynl2", "Yanlış");
                dataGridView3.Columns.Add("net2", "Net");

                dataGridView3.Columns.Add("dgr5", "Doğru");
                dataGridView3.Columns.Add("ynl5", "Yanlış");
                dataGridView3.Columns.Add("net5", "Net");

                dataGridView3.Columns.Add("dgr6", "Doğru");
                dataGridView3.Columns.Add("ynl6", "Yanlış");
                dataGridView3.Columns.Add("net6", "Net");

                dataGridView3.Columns.Add("dgr3", "Doğru");
                dataGridView3.Columns.Add("ynl3", "Yanlış");
                dataGridView3.Columns.Add("net3", "Net");

                dataGridView3.Columns.Add("dgr4", "Doğru");
                dataGridView3.Columns.Add("ynl4", "Yanlış");
                dataGridView3.Columns.Add("net4", "Net");

                this.dataGridView3.Columns["net5"].ReadOnly = true;
                this.dataGridView3.Columns["net6"].ReadOnly = true;
            }
            else
            {
                dataGridView3.Columns.Add("bosalan1", "");//3
                dataGridView3.Columns.Add("bosalan2", "");//3
                this.dataGridView3.Columns["id"].Visible = false;

                dataGridView3.Columns.Add("dgr1", "Doğru");//3
                dataGridView3.Columns.Add("ynl1", "Yanlış");
                dataGridView3.Columns.Add("net1", "Net");

                dataGridView3.Columns.Add("dgr2", "Doğru");
                dataGridView3.Columns.Add("ynl2", "Yanlış");
                dataGridView3.Columns.Add("net2", "Net");

                dataGridView3.Columns.Add("dgr3", "Doğru");
                dataGridView3.Columns.Add("ynl3", "Yanlış");
                dataGridView3.Columns.Add("net3", "Net");

                dataGridView3.Columns.Add("dgr4", "Doğru");
                dataGridView3.Columns.Add("ynl4", "Yanlış");
                dataGridView3.Columns.Add("net4", "Net");
            }

            this.dataGridView3.Columns["net1"].ReadOnly = true;
            this.dataGridView3.Columns["net2"].ReadOnly = true;
            this.dataGridView3.Columns["net3"].ReadOnly = true;
            this.dataGridView3.Columns["net4"].ReadOnly = true;

            this.dataGridView3.Columns[0].ReadOnly = true;
            this.dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[0].HeaderText = "";
            dataGridView3.Columns[1].HeaderText = "";

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (denemeler.denemeAlan == "TYT" || denemeler.denemeAlan == "AYT")
                {
                    DataGridViewColumn column = dataGridView3.Columns[i];
                    column.Width = 150;
                    if (i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15)
                    {
                        column.Width = 80;
                    }
                }
                else
                {
                    DataGridViewColumn column = dataGridView3.Columns[i];
                    column.Width = 150;
                    if (i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16 || i == 17 || i == 18 || i == 19 || i == 20)
                    {
                        column.Width = 60;
                    }
                }

            }
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            foreach (DataGridViewColumn col in dataGridView3.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            }
            dataGridView3.SelectionChanged += new System.EventHandler(this.dataGridView3_SelectionChanged);
        }
        void tb_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Visible == true)
            {
                if (denemeEkle.alan == "TYT" || denemeler.denemeAlan == "TYT")
                {
                    dataGridView1.CurrentRow.Cells["net1"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr1"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl1"].Value) / 4, 2);
                    dataGridView1.CurrentRow.Cells["net4"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr4"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl4"].Value) / 4, 2);
                    dataGridView1.CurrentRow.Cells["net2"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr2"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl2"].Value) / 4, 2);
                    dataGridView1.CurrentRow.Cells["net3"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr3"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl3"].Value) / 4, 2);
                }
                else if (denemeEkle.alan == "AYT" || denemeler.denemeAlan == "AYT")
                {
                    dataGridView1.CurrentRow.Cells["net1"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr1"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl1"].Value) / 4, 2);
                    dataGridView1.CurrentRow.Cells["net2"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr2"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl2"].Value) / 4, 2);
                    dataGridView1.CurrentRow.Cells["net3"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr3"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl3"].Value) / 4, 2);
                    dataGridView1.CurrentRow.Cells["net4"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr4"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl4"].Value) / 4, 2);
                }
                else if (denemeEkle.alan == "LGS" || denemeler.denemeAlan == "LGS")
                {
                    dataGridView1.CurrentRow.Cells["net1"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr1"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl1"].Value) / 3, 2);
                    dataGridView1.CurrentRow.Cells["net2"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr2"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl2"].Value) / 3, 2);
                    dataGridView1.CurrentRow.Cells["net3"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr3"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl3"].Value) / 3, 2);
                    dataGridView1.CurrentRow.Cells["net4"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr4"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl4"].Value) / 3, 2);
                    dataGridView1.CurrentRow.Cells["net5"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr5"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl5"].Value) / 3, 2);
                    dataGridView1.CurrentRow.Cells["net6"].Value = Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells["dgr6"].Value) - Convert.ToDouble(dataGridView1.CurrentRow.Cells["ynl6"].Value) / 3, 2);
                }
            }

            if (dataGridView3.Rows.Count > 0 && dataGridView3.Visible == true)
            {
                if (denemeler.denemeAlan == "TYT" || denemeler.denemeAlan == "TYT")
                {
                    dataGridView3.CurrentRow.Cells["net1"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr1"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl1"].Value) / 4, 2);
                    dataGridView3.CurrentRow.Cells["net2"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr2"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl2"].Value) / 4, 2);
                    dataGridView3.CurrentRow.Cells["net3"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr3"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl3"].Value) / 4, 2);
                    dataGridView3.CurrentRow.Cells["net4"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr4"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl4"].Value) / 4, 2);
                }
                else if (denemeler.denemeAlan == "AYT" || denemeler.denemeAlan == "AYT")
                {
                    dataGridView3.CurrentRow.Cells["net1"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr1"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl1"].Value) / 4, 2);
                    dataGridView3.CurrentRow.Cells["net2"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr2"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl2"].Value) / 4, 2);
                    dataGridView3.CurrentRow.Cells["net3"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr3"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl3"].Value) / 4, 2);
                    dataGridView3.CurrentRow.Cells["net4"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr4"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl4"].Value) / 4, 2);
                }
                else if (denemeEkle.alan == "LGS" || denemeler.denemeAlan == "LGS")
                {
                    dataGridView3.CurrentRow.Cells["net1"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr1"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl1"].Value) / 3, 2);
                    dataGridView3.CurrentRow.Cells["net2"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr2"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl2"].Value) / 3, 2);
                    dataGridView3.CurrentRow.Cells["net3"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr3"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl3"].Value) / 3, 2);
                    dataGridView3.CurrentRow.Cells["net4"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr4"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl4"].Value) / 3, 2);
                    dataGridView3.CurrentRow.Cells["net5"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr5"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl5"].Value) / 3, 2);
                    dataGridView3.CurrentRow.Cells["net6"].Value = Math.Round(Convert.ToDouble(dataGridView3.CurrentRow.Cells["dgr6"].Value) - Convert.ToDouble(dataGridView3.CurrentRow.Cells["ynl6"].Value) / 3, 2);
                }
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            tb_TextChanged(sender, e);
        }
        public static bool buyuksayi = false;
        private void denemePuanlama_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult c;
            c = MessageBox.Show("Değişiklikler kaydedilsin mi ?", "Sistem Mesajı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (c == DialogResult.Cancel)
            {
                e.Cancel = (c == DialogResult.Cancel);
            }
            else if (c == DialogResult.Yes)
            {
                columnlimiting();
                if (buyuksayi != true)
                {
                    if (denemeler.editing == true)
                    {
                        puaneditle();
                    }
                    else
                    {
                        puankaydet();
                    }
                    //denemeEkle obj = (denemeEkle)Application.OpenForms["denemeEkle"];
                    //obj.Close();
                    denemeler.denemeAlan = "";
                    denemeEkle.alan = "";
                    denemeler.editing = false;
                }
                else
                {
                    MessageBox.Show("Büyük sayi girildi.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    buyuksayi = false;
                }
            }
            else
            {
                if (dataGridView1.Visible == true)
                {
                    SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
                    SqlCommand command1 = new SqlCommand("delete from denemeler where denemeId = '" + denemeEkle.denemeid + "'", con);
                    con.Open();
                    command1.ExecuteNonQuery();
                    con.Close();
                }
                denemeler.denemeAlan = "";
                denemeEkle.alan = "";
                buyuksayi = false;
            }
        }


        void puaneditle()
        {
            string tabloAdi = "";
            string tabloEkAlanlar = "";
            string tabloEkAlanlarET = "";
            if (denemeler.denemeAlan == "TYT")
            {
                tabloAdi = "tytPuanlari";
                tabloEkAlanlar = "";
                tabloEkAlanlarET = "";
            }
            else if (denemeler.denemeAlan == "AYT")
            {
                tabloAdi = "aytPuanlari";
                tabloEkAlanlar = "";
                tabloEkAlanlarET = "";
            }
            else
            {
                tabloAdi = "LgsPuanlari";
                tabloEkAlanlar = ", dind=@dind, diny=@diny, dinn=@dinn, ingd=@ingd, ingy=@ingy, ingn=@ingn";
            }
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            con.Open();
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                SqlCommand command = new SqlCommand("update " + tabloAdi + " set trd=@trd,try=@try,trn=@trn,sysd=@sysd," +
                        "sysy=@sysy, sysn=@sysn, matd=@matd, maty=@maty, matn=@matn " + tabloEkAlanlar + " , fend=@fend,feny=@feny, fenn=@fenn, topnet=@topnet" +
                        ",userId=@userId,editDate=@editDate where denemeId = '" + denemeler.denemeid + "' and ogrId = '" + dataGridView3.Rows[i].Cells["id"].Value + "'", con);


                command.Parameters.AddWithValue("@trd", Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr1"].Value));
                command.Parameters.AddWithValue("@try", Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl1"].Value));
                command.Parameters.AddWithValue("@trn", Convert.ToDouble(dataGridView3.Rows[i].Cells["net1"].Value));
                command.Parameters.AddWithValue("@sysd", Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr2"].Value));
                command.Parameters.AddWithValue("@sysy", Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl2"].Value));
                command.Parameters.AddWithValue("@sysn", Convert.ToDouble(dataGridView3.Rows[i].Cells["net2"].Value));
                command.Parameters.AddWithValue("@matd", Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr3"].Value));
                command.Parameters.AddWithValue("@maty", Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl3"].Value));
                command.Parameters.AddWithValue("@matn", Convert.ToDouble(dataGridView3.Rows[i].Cells["net3"].Value));
                command.Parameters.AddWithValue("@fend", Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr4"].Value));
                command.Parameters.AddWithValue("@feny", Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl4"].Value));
                command.Parameters.AddWithValue("@fenn", Convert.ToDouble(dataGridView3.Rows[i].Cells["net4"].Value));

                if (denemeler.denemeAlan == "LGS")
                {
                    command.Parameters.AddWithValue("dind", Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr5"].Value));
                    command.Parameters.AddWithValue("diny", Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl5"].Value));
                    command.Parameters.AddWithValue("dinn", Convert.ToDouble(dataGridView3.Rows[i].Cells["net5"].Value));
                    command.Parameters.AddWithValue("ingd", Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr6"].Value));
                    command.Parameters.AddWithValue("ingy", Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl6"].Value));
                    command.Parameters.AddWithValue("ingn", Convert.ToDouble(dataGridView3.Rows[i].Cells["net6"].Value));
                    command.Parameters.AddWithValue("@topnet", Convert.ToDouble(dataGridView3.Rows[i].Cells["net1"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net2"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net3"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net4"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net5"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net6"].Value));
                }
                else
                {
                    command.Parameters.AddWithValue("@topnet", Convert.ToDouble(dataGridView3.Rows[i].Cells["net1"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net2"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net3"].Value) + Convert.ToDouble(dataGridView3.Rows[i].Cells["net4"].Value));
                }

                command.Parameters.AddWithValue("@userId", login.userid);
                command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    command.ExecuteNonQuery();

                    kayitcontrol = true;
                }
                catch (SqlException ex)
                {
                    prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                    prlg.databaseinsert();

                    MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
                }
            }
            if (kayitcontrol == true)
            {
                MessageBox.Show("Kayıt Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            con.Close();
        }
        void puankaydet()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            con.Open();
            SqlCommand command5 = new SqlCommand("Insert Into userlog(user_id, form_name,islem,log_date) Values (@userid,@formname,@islem, @logdate)", con);
            command5.Parameters.AddWithValue("@userid", login.userid);
            command5.Parameters.AddWithValue("@formname", this.Text);
            command5.Parameters.AddWithValue("@islem", "Puanı Kaydet");
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

            string tabloAdi = "";
            string tabloEkAlanlar = "";
            string tabloEkAlanlarET = "";
            if (denemeEkle.alan == "TYT")
            {
                tabloAdi = "tytPuanlari";
                tabloEkAlanlar = "";
                tabloEkAlanlarET = "";
            }
            else if (denemeEkle.alan == "AYT")
            {
                tabloAdi = "aytPuanlari";
                tabloEkAlanlar = "";
                tabloEkAlanlarET = "";
            }
            else
            {
                tabloAdi = "LgsPuanlari";
                tabloEkAlanlar = ", dind, diny, dinn, ingd, ingy, ingn";
                tabloEkAlanlarET = ", @dind, @diny, @dinn, @ingd, @ingy, @ingn";
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlCommand command = new SqlCommand("Insert Into " + tabloAdi + "(ogrId, denemeId, trd,try,trn, sysd, sysy, sysn, matd, maty, matn " + tabloEkAlanlar + ", fend,feny,fenn,topnet, insertDate, userId, editDate) Values (" +
        "@ogrid, @denemeId, @trd,@try,@trn, @sysd, @sysy, @sysn, @matd,@maty, @matn " + tabloEkAlanlarET + ", @fend,@feny,@fenn,@topnet,@insertDate,@userId,@editDate)", con);
                command.Parameters.AddWithValue("@ogrid", dataGridView1.Rows[i].Cells["ogrId"].Value.ToString());



                command.Parameters.AddWithValue("@denemeId", denemeEkle.denemeid);
                command.Parameters.AddWithValue("@trd", Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr1"].Value));
                command.Parameters.AddWithValue("@try", Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl1"].Value));
                command.Parameters.AddWithValue("@trn", Convert.ToDouble(dataGridView1.Rows[i].Cells["net1"].Value));
                command.Parameters.AddWithValue("@sysd", Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr2"].Value));
                command.Parameters.AddWithValue("@sysy", Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl2"].Value));
                command.Parameters.AddWithValue("@sysn", Convert.ToDouble(dataGridView1.Rows[i].Cells["net2"].Value));
                command.Parameters.AddWithValue("matd", Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr3"].Value));
                command.Parameters.AddWithValue("maty", Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl3"].Value));
                command.Parameters.AddWithValue("matn", Convert.ToDouble(dataGridView1.Rows[i].Cells["net3"].Value));
                command.Parameters.AddWithValue("fend", Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr4"].Value));
                command.Parameters.AddWithValue("feny", Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl4"].Value));
                command.Parameters.AddWithValue("fenn", Convert.ToDouble(dataGridView1.Rows[i].Cells["net4"].Value));

                if (denemeEkle.alan == "LGS")
                {
                    command.Parameters.AddWithValue("dind", Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr5"].Value));
                    command.Parameters.AddWithValue("diny", Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl5"].Value));
                    command.Parameters.AddWithValue("dinn", Convert.ToDouble(dataGridView1.Rows[i].Cells["net5"].Value));
                    command.Parameters.AddWithValue("ingd", Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr6"].Value));
                    command.Parameters.AddWithValue("ingy", Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl6"].Value));
                    command.Parameters.AddWithValue("ingn", Convert.ToDouble(dataGridView1.Rows[i].Cells["net6"].Value));
                    command.Parameters.AddWithValue("@topnet", Convert.ToDouble(dataGridView1.Rows[i].Cells["net1"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net2"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net3"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net4"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net5"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net6"].Value));
                }
                else
                {
                    command.Parameters.AddWithValue("@topnet", Convert.ToDouble(dataGridView1.Rows[i].Cells["net1"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net2"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net3"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["net4"].Value));
                }


                command.Parameters.AddWithValue("@insertDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@userId", login.userid);
                command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    command.ExecuteNonQuery();

                    kayitcontrol = true;
                }
                catch (SqlException ex)
                {
                    prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                    prlg.databaseinsert();

                    MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");

                    SqlCommand command3 = new SqlCommand("delete from denemeler where denemeNo = " + denemeler.denemerowcount.ToString() + "", con);
                    command3.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Kayıt Tamamlandı", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }



        public static bool kayitcontrol = false;
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (denemeEkle.alan != "LGS")
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6 || dataGridView1.CurrentCell.ColumnIndex == 7 || dataGridView1.CurrentCell.ColumnIndex == 8 || dataGridView1.CurrentCell.ColumnIndex == 9 || dataGridView1.CurrentCell.ColumnIndex == 10 || dataGridView1.CurrentCell.ColumnIndex == 11 || dataGridView1.CurrentCell.ColumnIndex == 12 || dataGridView1.CurrentCell.ColumnIndex == 13 || dataGridView1.CurrentCell.ColumnIndex == 14 || dataGridView1.CurrentCell.ColumnIndex == 15) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }
            else
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6 || dataGridView1.CurrentCell.ColumnIndex == 7 || dataGridView1.CurrentCell.ColumnIndex == 8 || dataGridView1.CurrentCell.ColumnIndex == 9 || dataGridView1.CurrentCell.ColumnIndex == 10 || dataGridView1.CurrentCell.ColumnIndex == 11 || dataGridView1.CurrentCell.ColumnIndex == 12 || dataGridView1.CurrentCell.ColumnIndex == 13 || dataGridView1.CurrentCell.ColumnIndex == 14 || dataGridView1.CurrentCell.ColumnIndex == 15 || dataGridView1.CurrentCell.ColumnIndex == 16 || dataGridView1.CurrentCell.ColumnIndex == 17 || dataGridView1.CurrentCell.ColumnIndex == 18 || dataGridView1.CurrentCell.ColumnIndex == 19 || dataGridView1.CurrentCell.ColumnIndex == 20) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }

        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            tb_TextChanged(sender, e);
        }

        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (denemeler.denemeAlan != "LGS")
            {
                if (dataGridView3.CurrentCell.ColumnIndex == 3 || dataGridView3.CurrentCell.ColumnIndex == 4 || dataGridView3.CurrentCell.ColumnIndex == 5 || dataGridView3.CurrentCell.ColumnIndex == 6 || dataGridView3.CurrentCell.ColumnIndex == 7 || dataGridView3.CurrentCell.ColumnIndex == 8 || dataGridView3.CurrentCell.ColumnIndex == 9 || dataGridView3.CurrentCell.ColumnIndex == 10 || dataGridView3.CurrentCell.ColumnIndex == 11 || dataGridView3.CurrentCell.ColumnIndex == 12 || dataGridView3.CurrentCell.ColumnIndex == 13 || dataGridView3.CurrentCell.ColumnIndex == 14 || dataGridView3.CurrentCell.ColumnIndex == 15) //Desired Column                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }
            else
            {
                if (dataGridView3.CurrentCell.ColumnIndex == 3 || dataGridView3.CurrentCell.ColumnIndex == 4 || dataGridView3.CurrentCell.ColumnIndex == 5 || dataGridView3.CurrentCell.ColumnIndex == 6 || dataGridView3.CurrentCell.ColumnIndex == 7 || dataGridView3.CurrentCell.ColumnIndex == 8 || dataGridView3.CurrentCell.ColumnIndex == 9 || dataGridView3.CurrentCell.ColumnIndex == 10 || dataGridView3.CurrentCell.ColumnIndex == 11 || dataGridView3.CurrentCell.ColumnIndex == 12 || dataGridView3.CurrentCell.ColumnIndex == 13 || dataGridView3.CurrentCell.ColumnIndex == 14 || dataGridView3.CurrentCell.ColumnIndex == 15 || dataGridView3.CurrentCell.ColumnIndex == 16 || dataGridView3.CurrentCell.ColumnIndex == 17 || dataGridView3.CurrentCell.ColumnIndex == 18 || dataGridView3.CurrentCell.ColumnIndex == 19 || dataGridView3.CurrentCell.ColumnIndex == 20) //Desired Column                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }
        }

        public void columnlimiting()
        {
            int turkcelimit, sosyallimit, fenlimit, matlimit, dinlimit, inglimit;
            if (denemeler.editing == true)
            {
                if (denemeEkle.alan == "TYT" || denemeler.denemeAlan == "TYT")
                {
                    turkcelimit = 40;
                    sosyallimit = 20;
                    matlimit = 40;
                    fenlimit = 20;

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr1"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl1"].Value);
                        if (limit > turkcelimit || Convert.ToDouble(dataGridView3.Rows[i].Cells["net1"].Value) > turkcelimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//TÜRKÇE LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr2"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl2"].Value);
                        if (limit > sosyallimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net2"].Value) > sosyallimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//SOSYAL LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr3"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl3"].Value);
                        if (limit > matlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net3"].Value) > matlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//MATEMATİK LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr4"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl4"].Value);
                        if (limit > fenlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net4"].Value) > fenlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//FEN LİMİT KONTROL
                }
                else if (denemeEkle.alan == "AYT" || denemeler.denemeAlan == "AYT")
                {
                    turkcelimit = 40;
                    sosyallimit = 40;
                    matlimit = 40;
                    fenlimit = 40;

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr1"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl1"].Value);
                        if (limit > turkcelimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net1"].Value) > turkcelimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//TÜRKÇE LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr2"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl2"].Value);
                        if (limit > sosyallimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net2"].Value) > sosyallimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//SOSYAL LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr3"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl3"].Value);
                        if (limit > matlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net3"].Value) > matlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//MATEMATİK LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr4"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl4"].Value);
                        if (limit > fenlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net4"].Value) > fenlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//FEN LİMİT KONTROL
                }
                else if (denemeEkle.alan == "LGS" || denemeler.denemeAlan == "LGS")
                {
                    turkcelimit = 20;
                    sosyallimit = 10;
                    dinlimit = 10;
                    inglimit = 10;
                    matlimit = 20;
                    fenlimit = 20;

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr1"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl1"].Value);
                        if (limit > turkcelimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net1"].Value) > turkcelimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//TÜRKÇE LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr2"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl2"].Value);
                        if (limit > sosyallimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net2"].Value) > sosyallimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//SOSYAL LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr5"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl5"].Value);
                        if (limit > dinlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net5"].Value) > dinlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//DİN LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr6"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl6"].Value);
                        if (limit > inglimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net6"].Value) > inglimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//İNGİLİZCE LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr3"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl3"].Value);
                        if (limit > matlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net3"].Value) > matlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//MATEMATİK LİMİT KONTROL

                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView3.Rows[i].Cells["dgr4"].Value) + Convert.ToInt32(dataGridView3.Rows[i].Cells["ynl4"].Value);
                        if (limit > fenlimit || Convert.ToInt32(dataGridView3.Rows[i].Cells["net4"].Value) > fenlimit)
                        {
                            buyuksayi = true;
                            dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//FEN LİMİT KONTROL
                }
            }
            else
            {
                if (denemeEkle.alan == "TYT" || denemeler.denemeAlan == "TYT")
                {
                    turkcelimit = 40;
                    sosyallimit = 20;
                    matlimit = 40;
                    fenlimit = 20;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr1"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl1"].Value);
                        if (limit > turkcelimit || Convert.ToDouble(dataGridView1.Rows[i].Cells["net1"].Value) > turkcelimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//TÜRKÇE LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr2"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl2"].Value);
                        if (limit > sosyallimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net2"].Value) > sosyallimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//SOSYAL LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr3"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl3"].Value);
                        if (limit > matlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net3"].Value) > matlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//MATEMATİK LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr4"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl4"].Value);
                        if (limit > fenlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net4"].Value) > fenlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//FEN LİMİT KONTROL
                }
                else if (denemeEkle.alan == "AYT" || denemeler.denemeAlan == "AYT")
                {
                    turkcelimit = 40;
                    sosyallimit = 40;
                    matlimit = 40;
                    fenlimit = 40;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr1"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl1"].Value);
                        if (limit > turkcelimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net1"].Value) > turkcelimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//TÜRKÇE LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr2"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl2"].Value);
                        if (limit > sosyallimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net2"].Value) > sosyallimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//SOSYAL LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr3"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl3"].Value);
                        if (limit > matlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net3"].Value) > matlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//MATEMATİK LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr4"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl4"].Value);
                        if (limit > fenlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net4"].Value) > fenlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//FEN LİMİT KONTROL
                }
                else if (denemeEkle.alan == "LGS" || denemeler.denemeAlan == "LGS")
                {
                    turkcelimit = 20;
                    sosyallimit = 10;
                    dinlimit = 10;
                    inglimit = 10;
                    matlimit = 20;
                    fenlimit = 20;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr1"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl1"].Value);
                        if (limit > turkcelimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net1"].Value) > turkcelimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//TÜRKÇE LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr2"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl2"].Value);
                        if (limit > sosyallimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net2"].Value) > sosyallimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//SOSYAL LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr5"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl5"].Value);
                        if (limit > dinlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net5"].Value) > dinlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//DİN LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr6"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl6"].Value);
                        if (limit > inglimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net6"].Value) > inglimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//İNGİLİZCE LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr3"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl3"].Value);
                        if (limit > matlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net3"].Value) > matlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//MATEMATİK LİMİT KONTROL

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int limit = Convert.ToInt32(dataGridView1.Rows[i].Cells["dgr4"].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells["ynl4"].Value);
                        if (limit > fenlimit || Convert.ToInt32(dataGridView1.Rows[i].Cells["net4"].Value) > fenlimit)
                        {
                            buyuksayi = true;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }//FEN LİMİT KONTROL
                }
            }

        }
    }
}
