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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        public DataTable tablo = new DataTable();
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veritabani.accdb");
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public OleDbCommand kmt = new OleDbCommand();
        void listele()
        {
            textBox1.Text = "";
            comboBox1.Text = "";

            tablo.Clear();
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From sinif", bag);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {

            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "INSERT INTO sinif(sinif_sube,bolum_id) VALUES ('" + textBox1.Text + "','" + comboBox1.Text + "') ";
            kmt.ExecuteNonQuery();
            bag.Close();
            listele();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            listele();

            bag.Open();
            kmt = new OleDbCommand("Select * from sinif", bag);
            OleDbDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["bolum_id"].ToString());
            }
            bag.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "DELETE from sinif WHERE sinif_sube='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' ";
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE sinif SET sinif_sube='" + textBox1.Text + "',bolum_id='" + comboBox1.Text + "'where sinif_sube='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
            OleDbCommand kmt = new OleDbCommand(sorgu, bag);
            bag.Open();
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";

            comboBox1.Text = "";
            try
            {
                tablo.Clear();
                bag.Open();
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * From sinif where sinif_sube='" + textBox3.Text + "'", bag);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                adtr.Dispose();
                bag.Close();
            }
            catch
            {

                ;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
