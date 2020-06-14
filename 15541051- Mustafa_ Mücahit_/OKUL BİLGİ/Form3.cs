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
    public partial class Form3 : Form
    {
        public Form3()
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
            textBox2.Text = "";
            comboBox1.Text = "";

            tablo.Clear();
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From dersler", bag);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            listele();

            bag.Open();
            kmt = new OleDbCommand("Select * from sinif", bag);
            OleDbDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["sinif_id"].ToString());
            }
            bag.Close(); listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "INSERT INTO dersler(ders_adi,ders_kodu,bolum_id) VALUES ('" +textBox1.Text+ "','" + textBox2.Text + "','" +comboBox1.Text+ "') ";
            kmt.ExecuteNonQuery();
            bag.Close();
            listele();



 
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "DELETE from dersler WHERE ders_kodu='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "' ";
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string sorgu = "UPDATE dersler SET ders_adi='" + textBox1.Text + "',ders_kodu='" + textBox2.Text + "',bolum_id='" + comboBox1.Text +"'where ders_kodu='"+ dataGridView1.CurrentRow.Cells[2].Value.ToString()+"'";
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
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            try
            {
                tablo.Clear();
                bag.Open();
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * From dersler where ders_kodu='" +textBox3.Text + "'", bag);
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
