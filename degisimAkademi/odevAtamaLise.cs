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
    public partial class odevAtamaLise : Form
    {
        public odevAtamaLise()
        {
            InitializeComponent();
        }
        public static bool acikmi = false;
        private void odevAtamaLise_Load(object sender, EventArgs e)
        {
            acikmi = true;
            label1.Text = "Öğrenci Adı: " + homework.ogrAdi + " " + homework.ogrsoyadi;

            if (homework.odevverisi == true)
            {
                textBox1.Text = homework.tr.ToString();
                textBox2.Text = homework.sy.ToString();
                textBox3.Text = homework.cg.ToString();
                textBox4.Text = homework.mat.ToString();
                textBox5.Text = homework.fn.ToString();
                textBox6.Text = homework.km.ToString();
                textBox7.Text = homework.biy.ToString();
                textBox8.Text = homework.prg.ToString();
                textBox9.Text = homework.den.ToString();
                textBox10.Text = homework.dg.ToString();
            }
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
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                button2.Text = "Ödev Ata";
            }
            else
            {
                SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
                if (homework.odevverisi == false)
                {
                    odevkaydet();
                }
                else
                {
                    odeveditle();
                }

            }
        }
        programLog prlg;
        homeworkTabloislemleri htis;
        void odeveditle()
        {
            iftext();
            htis = new homeworkTabloislemleri(tr, prg, sy, cg, mat, fn, kim, biy, dn, ing, dg, den);
            htis.databaseEdit();
            MessageBox.Show("Ödev Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        void odevkaydet()
        {
            iftext();
            htis = new homeworkTabloislemleri(tr, prg, sy, cg, mat, fn, kim, biy, dn, ing, dg, den);
            htis.databaseinsert();
            MessageBox.Show("Ödev Ataması Yapıldı", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        public int tr, prg, sy, cg, mat, fn, kim, biy, dn, ing, dg, den;
        void iftext()
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }
            if (textBox6.Text == "")
            {
                textBox6.Text = "0";
            }
            if (textBox7.Text == "")
            {
                textBox7.Text = "0";
            }
            if (textBox8.Text == "")
            {          
                textBox8.Text = "0";
            }
            if (textBox9.Text == "")
            {          
                textBox9.Text = "0";
            }
            if (textBox10.Text == "")
            {          
                textBox10.Text = "0";
            }

            tr = Convert.ToInt32(textBox1.Text);
            prg = Convert.ToInt32(textBox8.Text);
            sy = Convert.ToInt32(textBox2.Text);
            cg = Convert.ToInt32(textBox3.Text);
            mat = Convert.ToInt32(textBox4.Text);
            fn = Convert.ToInt32(textBox5.Text);
            kim = Convert.ToInt32(textBox6.Text);
            biy = Convert.ToInt32(textBox7.Text);
            dn = 0;
            ing = 0;
            dg = Convert.ToInt32(textBox10.Text);
            den = Convert.ToInt32(textBox9.Text);
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
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void odevAtamaLise_FormClosing(object sender, FormClosingEventArgs e)
        {
            acikmi = false;
        }
    }
}
