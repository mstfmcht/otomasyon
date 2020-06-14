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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public DataTable tablo = new DataTable();
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veritabani.accdb");
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public OleDbCommand kmt = new OleDbCommand();
        void listele()
        {
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            comboBox1.Text = "";

            tablo.Clear();
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From ders_saatleri", bag);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "INSERT INTO ders_saatleri(ders_id,ders_tarihi,ders_saati) VALUES ('" + comboBox1.Text + "','" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "') ";
            kmt.ExecuteNonQuery();
            bag.Close();
            listele();
            MessageBox.Show("Ekleme İşlemi Başarılı");
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            listele();

            bag.Open();
            kmt = new OleDbCommand("Select * from dersler", bag);
            OleDbDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ders_id"].ToString());
            }
            bag.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "DELETE from ders_saatleri WHERE ders_id='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' ";
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE ders_saatleri SET ders_id='" + comboBox1.Text + "',ders_tarihi='" + maskedTextBox1.Text + "',ders_saati='" + maskedTextBox2.Text + "'where ders_id='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
            OleDbCommand kmt = new OleDbCommand(sorgu, bag);
            bag.Open();
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
      comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
           maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            comboBox1.Text = "";
            try
            {
                tablo.Clear();
                bag.Open();
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * From ders_saatleri where ders_id='" + textBox3.Text + "'", bag);
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
