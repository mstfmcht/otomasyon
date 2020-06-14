using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Ders_Programi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string kadi = "";
        string sifre = "";
        private void button2_Click(object sender, EventArgs e)
        {

            string kontroladi = textBox1.Text;
            string kontrolsifre = textBox2.Text;
            OleDbConnection con;
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
            con.Open();
            OleDbCommand okuma = new OleDbCommand();
            okuma.Connection = con;
            okuma.CommandText = "SELECT * FROM kullanici WHERE k_adi= '" + textBox1.Text + "'";
            OleDbDataReader reader = okuma.ExecuteReader();
            while (reader.Read())
            {
                kadi = reader["k_adi"].ToString();
                sifre = reader["k_sifre"].ToString();

            }
            con.Close();
            if (kadi == kontroladi && sifre == kontrolsifre)
            {
               
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Yapdınız");
            }
        }
    }
}
