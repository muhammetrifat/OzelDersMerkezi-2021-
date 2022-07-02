using iTextSharp.text;
using iTextSharp.text.pdf;
//using Syncfusion.Pdf;
//using Syncfusion.Pdf.Graphics;
//using Syncfusion.Pdf.Grid;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace degisimAkademi
{
    public partial class detayliDenemeReport : Form
    {
        public detayliDenemeReport()
        {
            InitializeComponent();
        }

        private void detayliDenemeReport_Load(object sender, EventArgs e)
        {
            label3.Text = "Detaylı İnceleme Paneli " + "   Öğrenci Adı: " + report.ogrName + " " + report.ogrsurname;

            if (report.ogrAlani == "Lise")
            {
                tabPage1.Text = "TYT Denemeleri";
                tabPage2.Text = "AYT Denemeleri";
            }
            else
            {
                metroTabControl1.TabPages.Remove(tabPage2);
                tabPage1.Text = "LGS Denemeleri";
                
            }
            metroTabControl1_SelectedIndexChanged(sender, e);
            WindowState = FormWindowState.Maximized;

            UpdateFont();
        }
        programLog prlg;
        void doldurDetayTYT()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT t.trd,t.try,t.trn,t.sysd,t.sysy,t.sysn,t.matd,t.maty,t.matn,t.fend,t.feny,t.fenn,t.topnet, convert(varchar, d.denemeTarihi, 104) as tarih FROM Ogrenciler o inner join tytPuanlari t on o.ogrId = t.ogrId inner join denemeler d on d.denemeId = t.denemeId where o.ogrId = '"+report.ogrid+"' ", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView2.DataSource = ds.Tables["Ogrenciler"];
                dataGridView2.Columns[0].HeaderText = "Doğru";
                dataGridView2.Columns[1].HeaderText = "Yanlış";
                dataGridView2.Columns[2].HeaderText = "Net";
                dataGridView2.Columns[3].HeaderText = "Doğru";
                dataGridView2.Columns[4].HeaderText = "Yanlış";
                dataGridView2.Columns[5].HeaderText = "Net";
                dataGridView2.Columns[6].HeaderText = "Doğru";
                dataGridView2.Columns[7].HeaderText = "Yanlış";
                dataGridView2.Columns[8].HeaderText = "Net";
                dataGridView2.Columns[9].HeaderText = "Doğru";
                dataGridView2.Columns[10].HeaderText = "Yanlış";
                dataGridView2.Columns[11].HeaderText = "Net";
                dataGridView2.Columns[12].HeaderText = "Toplam Net";
                dataGridView2.Columns[13].HeaderText = "Deneme Tarihi";

                dataGridView2.BorderStyle = BorderStyle.None;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView2.EnableHeadersVisualStyles = false;
                dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                dataGridView1.Columns.Add("tr", "Türkçe");
                dataGridView1.Columns.Add("sy", "Sosyal");
                dataGridView1.Columns.Add("mat", "Matematik");
                dataGridView1.Columns.Add("fn", "Fen");
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnHeadersHeight = 30;


                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView1.Columns[i];
                    column.Width = 240;
                }

                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView2.Columns[i];
                    column.Width = 80;
                    if (i == 13 || i == 14)
                    {
                        column.Width = 140;
                    }
                }
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG1", "Sistem Mesajı");
            }
        }

        void doldurDetayAYT()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT t.trd,t.try,t.trn,t.sysd,t.sysy,t.sysn,t.matd,t.maty,t.matn,t.fend,t.feny,t.fenn,t.topnet, convert(varchar, d.denemeTarihi, 104) as tarih FROM Ogrenciler o inner join aytPuanlari t on o.ogrId = t.ogrId inner join denemeler d on d.denemeId = t.denemeId where o.ogrId = '"+report.ogrid+"'", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView2.DataSource = ds.Tables["Ogrenciler"];
                dataGridView2.Columns[0].HeaderText = "Doğru";
                dataGridView2.Columns[1].HeaderText = "Yanlış";
                dataGridView2.Columns[2].HeaderText = "Net";
                dataGridView2.Columns[3].HeaderText = "Doğru";
                dataGridView2.Columns[4].HeaderText = "Yanlış";
                dataGridView2.Columns[5].HeaderText = "Net";
                dataGridView2.Columns[6].HeaderText = "Doğru";
                dataGridView2.Columns[7].HeaderText = "Yanlış";
                dataGridView2.Columns[8].HeaderText = "Net";
                dataGridView2.Columns[9].HeaderText = "Doğru";
                dataGridView2.Columns[10].HeaderText = "Yanlış";
                dataGridView2.Columns[11].HeaderText = "Net";
                dataGridView2.Columns[12].HeaderText = "Toplam Net";
                dataGridView2.Columns[13].HeaderText = "Deneme Tarihi";

                dataGridView1.Columns.Add("tr", "Türk Dili-Sosyal - 1");
                dataGridView1.Columns.Add("sy", "Sosyal - 2");
                dataGridView1.Columns.Add("mat", "Matematik");
                dataGridView1.Columns.Add("fn", "Fen");
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnHeadersHeight = 30;


                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView1.Columns[i];
                    column.Width = 240;
                }

                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView2.Columns[i];
                    column.Width = 80;
                    if (i == 13 || i == 14)
                    {
                        column.Width = 140;
                    }
                }
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG2");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG2", "Sistem Mesajı");
            }
        }

        void doldurDetayLGS()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            DataSet ds = new DataSet();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT t.trd,t.try,t.trn,t.sysd,t.sysy,t.sysn,t.dind,t.diny,t.dinn,t.ingd,t.ingy,t.ingn,t.matd,t.maty,t.matn,t.fend,t.feny,t.fenn,t.topnet, convert(varchar, d.denemeTarihi, 104) as tarih FROM Ogrenciler o inner join LgsPuanlari t on o.ogrId = t.ogrId inner join denemeler d on d.denemeId = t.denemeId where o.ogrId = '"+report.ogrid+"' ", con);
            try
            {
                adtr.Fill(ds, "Ogrenciler");
                dataGridView2.DataSource = ds.Tables["Ogrenciler"];
                dataGridView2.Columns[0].HeaderText = "Doğru";
                dataGridView2.Columns[1].HeaderText = "Yanlış";
                dataGridView2.Columns[2].HeaderText = "Net";
                dataGridView2.Columns[3].HeaderText = "Doğru";
                dataGridView2.Columns[4].HeaderText = "Yanlış";
                dataGridView2.Columns[5].HeaderText = "Net";
                dataGridView2.Columns[6].HeaderText = "Doğru";
                dataGridView2.Columns[7].HeaderText = "Yanlış";
                dataGridView2.Columns[8].HeaderText = "Net";
                dataGridView2.Columns[9].HeaderText = "Doğru";
                dataGridView2.Columns[10].HeaderText = "Yanlış";
                dataGridView2.Columns[11].HeaderText = "Net";
                dataGridView2.Columns[12].HeaderText = "Doğru";
                dataGridView2.Columns[13].HeaderText = "Yanlış";
                dataGridView2.Columns[14].HeaderText = "Net";
                dataGridView2.Columns[15].HeaderText = "Doğru";
                dataGridView2.Columns[16].HeaderText = "Yanlış";
                dataGridView2.Columns[17].HeaderText = "Net";
                dataGridView2.Columns[18].HeaderText = "Toplam Net";
                dataGridView2.Columns[19].HeaderText = "Deneme Tarihi";

                dataGridView1.Columns.Add("tr", "Türkçe");
                dataGridView1.Columns.Add("sy", "İnkılap");
                dataGridView1.Columns.Add("dn", "Din");
                dataGridView1.Columns.Add("ing", "İngilizce");
                dataGridView1.Columns.Add("mat", "Matematik");
                dataGridView1.Columns.Add("fn", "Fen");

                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnHeadersHeight = 30;


                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView1.Columns[i];
                    column.Width = 180;
                }

                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView2.Columns[i];
                    column.Width = 60;
                    if (i == 18 || i == 19)
                    {
                        column.Width = 110;
                    }
                }
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, this.Text, "PRLG3");//PROGRAMLOG
                prlg.databaseinsert();
                MessageBox.Show("Veritabanı hatası meydana geldi. Lütfen bu mesajın ekran görüntüsünü alıp sağlayıcınıza iletiniz. Kod:PRLG3", "Sistem Mesajı");
            }
        }


        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 15F, GraphicsUnit.Pixel);
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, GraphicsUnit.Pixel);

            }
            foreach (DataGridViewColumn c in dataGridView2.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 15F, GraphicsUnit.Pixel);
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, GraphicsUnit.Pixel);
            }
        }

        void colheaderfirst()
        {
            if (metroTabControl1.SelectedTab.Text == "TYT Denemeleri")
            {
                dataGridView2.Columns[0].HeaderText = "Türkçe Doğru";
                dataGridView2.Columns[1].HeaderText = "Türkçe Yanlış";
                dataGridView2.Columns[2].HeaderText = "Türkçe Net";
                dataGridView2.Columns[3].HeaderText = "Sosyal Doğru";
                dataGridView2.Columns[4].HeaderText = "Sosyal Yanlış";
                dataGridView2.Columns[5].HeaderText = "Sosyal Net";
                dataGridView2.Columns[6].HeaderText = "Matematik Doğru";
                dataGridView2.Columns[7].HeaderText = "Matematik Yanlış";
                dataGridView2.Columns[8].HeaderText = "Matematik Net";
                dataGridView2.Columns[9].HeaderText = "Fen Doğru";
                dataGridView2.Columns[10].HeaderText = "Fen Yanlış";
                dataGridView2.Columns[11].HeaderText = "Fen Net";
                dataGridView2.Columns[12].HeaderText = "Toplam Net";
                dataGridView2.Columns[13].HeaderText = "Deneme Tarihi";
            }
            else if (metroTabControl1.SelectedTab.Text == "AYT Denemeleri")
            {
                dataGridView2.Columns[0].HeaderText = "Türkçe-Sosyal-1 Doğru";
                dataGridView2.Columns[1].HeaderText = "Türkçe-Sosyal-1 Yanlış";
                dataGridView2.Columns[2].HeaderText = "Türkçe-Sosyal-1 Net";
                dataGridView2.Columns[3].HeaderText = "Sosyal-2 Doğru";
                dataGridView2.Columns[4].HeaderText = "Sosyal-2 Yanlış";
                dataGridView2.Columns[5].HeaderText = "Sosyal-2 Net";
                dataGridView2.Columns[6].HeaderText = "Matematik Doğru";
                dataGridView2.Columns[7].HeaderText = "Matematik Yanlış";
                dataGridView2.Columns[8].HeaderText = "Matematik Net";
                dataGridView2.Columns[9].HeaderText = "Fen Doğru";
                dataGridView2.Columns[10].HeaderText = "Fen Yanlış";
                dataGridView2.Columns[11].HeaderText = "Fen Net";
                dataGridView2.Columns[12].HeaderText = "Toplam Net";
                dataGridView2.Columns[13].HeaderText = "Deneme Tarihi";
            }
            else if (metroTabControl1.SelectedTab.Text == "LGS Denemeleri")
            {
                dataGridView2.Columns[0].HeaderText = "Türkçe Doğru";
                dataGridView2.Columns[1].HeaderText = "Türkçe Yanlış";
                dataGridView2.Columns[2].HeaderText = "Türkçe Net";
                dataGridView2.Columns[3].HeaderText = "İnkılap Doğru";
                dataGridView2.Columns[4].HeaderText = "İnkılap Yanlış";
                dataGridView2.Columns[5].HeaderText = "İnkılap Net";
                dataGridView2.Columns[6].HeaderText = "Din Doğru";
                dataGridView2.Columns[7].HeaderText = "Din Yanlış";
                dataGridView2.Columns[8].HeaderText = "Din Net";
                dataGridView2.Columns[9].HeaderText = "İngilizce Doğru";
                dataGridView2.Columns[10].HeaderText = "İngilizce Yanlış";
                dataGridView2.Columns[11].HeaderText = "İngilizce Net";
                dataGridView2.Columns[12].HeaderText = "Matematik Doğru";
                dataGridView2.Columns[13].HeaderText = "Matematik Yanlış";
                dataGridView2.Columns[14].HeaderText = "Matematik Net";
                dataGridView2.Columns[15].HeaderText = "Fen Doğru";
                dataGridView2.Columns[16].HeaderText = "Fen Yanlış";
                dataGridView2.Columns[17].HeaderText = "Fen Net";
                dataGridView2.Columns[18].HeaderText = "Toplam Net";
                dataGridView2.Columns[19].HeaderText = "Deneme Tarihi";
            }
        }

        void colheadersecond()
        {
            if (metroTabControl1.SelectedTab.Text == "TYT Denemeleri")
            {
                dataGridView2.Columns[0].HeaderText = "Doğru";
                dataGridView2.Columns[1].HeaderText = "Yanlış";
                dataGridView2.Columns[2].HeaderText = "Net";
                dataGridView2.Columns[3].HeaderText = "Doğru";
                dataGridView2.Columns[4].HeaderText = "Yanlış";
                dataGridView2.Columns[5].HeaderText = "Net";
                dataGridView2.Columns[6].HeaderText = "Doğru";
                dataGridView2.Columns[7].HeaderText = "Yanlış";
                dataGridView2.Columns[8].HeaderText = "Net";
                dataGridView2.Columns[9].HeaderText = "Doğru";
                dataGridView2.Columns[10].HeaderText = "Yanlış";
                dataGridView2.Columns[11].HeaderText = "Net";
                dataGridView2.Columns[12].HeaderText = "Toplam Net";
                dataGridView2.Columns[13].HeaderText = "Deneme Tarihi";
            }
            else if (metroTabControl1.SelectedTab.Text == "AYT Denemeleri")
            {
                dataGridView2.Columns[0].HeaderText = "Doğru";
                dataGridView2.Columns[1].HeaderText = "Yanlış";
                dataGridView2.Columns[2].HeaderText = "Net";
                dataGridView2.Columns[3].HeaderText = "Doğru";
                dataGridView2.Columns[4].HeaderText = "Yanlış";
                dataGridView2.Columns[5].HeaderText = "Net";
                dataGridView2.Columns[6].HeaderText = "Doğru";
                dataGridView2.Columns[7].HeaderText = "Yanlış";
                dataGridView2.Columns[8].HeaderText = "Net";
                dataGridView2.Columns[9].HeaderText = "Doğru";
                dataGridView2.Columns[10].HeaderText = "Yanlış";
                dataGridView2.Columns[11].HeaderText = "Net";
                dataGridView2.Columns[12].HeaderText = "Toplam Net";
                dataGridView2.Columns[13].HeaderText = "Deneme Tarihi";
            }
            else if (metroTabControl1.SelectedTab.Text == "LGS Denemeleri")
            {
                dataGridView2.Columns[0].HeaderText = "Doğru";
                dataGridView2.Columns[1].HeaderText = "Yanlış";
                dataGridView2.Columns[2].HeaderText = "Net";
                dataGridView2.Columns[3].HeaderText = "Doğru";
                dataGridView2.Columns[4].HeaderText = "Yanlış";
                dataGridView2.Columns[5].HeaderText = "Net";
                dataGridView2.Columns[6].HeaderText = "Doğru";
                dataGridView2.Columns[7].HeaderText = "Yanlış";
                dataGridView2.Columns[8].HeaderText = "Net";
                dataGridView2.Columns[9].HeaderText = "Doğru";
                dataGridView2.Columns[10].HeaderText = "Yanlış";
                dataGridView2.Columns[11].HeaderText = "Net";
                dataGridView2.Columns[12].HeaderText = "Doğru";
                dataGridView2.Columns[13].HeaderText = "Yanlış";
                dataGridView2.Columns[14].HeaderText = "Net";
                dataGridView2.Columns[15].HeaderText = "Doğru";
                dataGridView2.Columns[16].HeaderText = "Yanlış";
                dataGridView2.Columns[17].HeaderText = "Net";
                dataGridView2.Columns[18].HeaderText = "Toplam Net";
                dataGridView2.Columns[19].HeaderText = "Deneme Tarihi";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            if (dataGridView2.Rows.Count > 0)
            {
                colheaderfirst();
                
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = report.ogrName + " " + report.ogrsurname + " " + metroTabControl1.SelectedTab.Text;
                bool ErrorMessage = false;

                BaseFont STF_Helvetica_Turkish = BaseFont.CreateFont("Helvetica", "CP1254", BaseFont.NOT_EMBEDDED);

                iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 7, iTextSharp.text.Font.NORMAL);

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("PDF dosyası diske yazılamadı." + ex.Message, "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView2.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            foreach (DataGridViewColumn col in dataGridView2.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, fontNormal));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dataGridView2.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    PdfPCell pCell = new PdfPCell(new Phrase(dcell.Value.ToString(), fontNormal));
                                    pTable.AddCell(pCell);
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("PDF'e aktarma yapıldı", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata :" + ex.Message, "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                colheadersecond();
            }
            else
            {
                MessageBox.Show("Tabloda kayıt olmadığı için PDF'e aktarım işlemi iptal oldu.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedTab.Text == "TYT Denemeleri")
            {
                dataGridView1.Columns.Clear();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                metroTabControl1.SelectedTab.Controls.Add(dataGridView2);
                metroTabControl1.SelectedTab.Controls.Add(panel1);
                doldurDetayTYT();
            }
            else if (metroTabControl1.SelectedTab.Text == "AYT Denemeleri")
            {
                dataGridView1.Columns.Clear();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                metroTabControl1.SelectedTab.Controls.Add(dataGridView2);
                metroTabControl1.SelectedTab.Controls.Add(panel1);
                doldurDetayAYT();
            }
            else if (metroTabControl1.SelectedTab.Text == "LGS Denemeleri")
            {
                dataGridView1.Columns.Clear();
                metroTabControl1.SelectedTab.Controls.Clear();
                metroTabControl1.SelectedTab.Controls.Add(dataGridView1);
                metroTabControl1.SelectedTab.Controls.Add(dataGridView2);
                metroTabControl1.SelectedTab.Controls.Add(panel1);
                doldurDetayLGS();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            colheaderfirst();
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.FileName = report.ogrName + " " + report.ogrsurname + " " + metroTabControl1.SelectedTab.Text;
            save.Title = "Excel Dosyaları";
            save.DefaultExt = "xlsx";
            save.Filter = "xlsx Dosyaları (*.xlsx)|*.xlsx|Tüm Dosyalar(*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Sayfa1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = metroTabControl1.SelectedTab.Text;
                for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                    }
                }
                workbook.SaveAs(save.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Excel'e aktarma yapıldı", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            colheadersecond();
        }
    }
}
