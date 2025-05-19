using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ORTALAMA_HESAPLAMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        OleDbConnection iletisim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0; Data Source=ders.accdb");
        void veritabani()
        {
            OleDbDataAdapter baglanti = new OleDbDataAdapter("SELECT * FROM TABLO1", iletisim);
            DataTable tablo2 = new DataTable();
            baglanti.Fill(tablo2);
            dataGridView1.DataSource = tablo2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] dersler = { "Matematik", "Türkçe", "Fizik", "Kimya", "Biyoloji" };

            for (int i = 0; i <= 10; i++)
            {
                comboBox2.Items.Add(i * 10);
                comboBox1.Items.Add(i * 10);
            }
            for (int i = 0; i < dersler.Length; i++)
            {
                comboBox3.Items.Add(dersler[i]);

            }
            veritabani();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("İsim Giriniz!");

            } if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Soyisim Giriniz!");
            }

            int vize = Convert.ToInt32(comboBox1.SelectedItem);
            int final = Convert.ToInt32(comboBox2.SelectedItem);
            double ortalama = ((vize * 0.4) + (final * 0.6));
            string durum = "";

            if (ortalama < 60)
            {
                durum = "KALDI";
            }
            else if ((comboBox2.SelectedIndex == -1))
            {
                durum = "KALDI";
            }
            else
            {
                durum = "GEÇTİ";
            }
            string sql = "INSERT INTO TABLO1 VALUES(A,S,D,V,F,O,G)";
            OleDbCommand kaydet = new OleDbCommand(sql, iletisim);
            kaydet.Parameters.AddWithValue("A", textBox1.Text);
            kaydet.Parameters.AddWithValue("S", textBox2.Text);
            kaydet.Parameters.AddWithValue("D", Convert.ToString(comboBox3.SelectedItem));
            kaydet.Parameters.AddWithValue("V", Convert.ToInt32(comboBox1.SelectedItem));
            kaydet.Parameters.AddWithValue("F", Convert.ToInt32(comboBox2.SelectedItem));
            kaydet.Parameters.AddWithValue("O", Convert.ToInt32(ortalama));
            kaydet.Parameters.AddWithValue("G", Convert.ToString(durum));

            iletisim.Open(); kaydet.ExecuteNonQuery(); iletisim.Close();
            veritabani();

            MessageBox.Show("Bilgiler Kaydedildi!");

         
   
        }
    }
}
