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
    public partial class Form4 : Form
    {
        public Form4()
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
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From okul", bag);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "INSERT INTO okul(okul_adi,sinif_id) VALUES ('" + textBox1.Text + "','" + comboBox1.Text + "') ";
            kmt.ExecuteNonQuery();
            bag.Close();
            listele();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            listele();

            bag.Open();
            kmt = new OleDbCommand("Select * from sinif", bag);
            OleDbDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["sinif_id"].ToString());
            }
            bag.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "DELETE from okul WHERE okul_adi='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' ";
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string sorgu = "UPDATE okul SET okul_adi='" + textBox1.Text +"',sinif_id='" + comboBox1.Text + "'where okul_adi='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
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
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * From okul where okul_adi='" + textBox3.Text + "'", bag);
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
