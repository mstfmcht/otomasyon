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
    public partial class Form7 : Form
    {
        public Form7()
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
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";

            tablo.Clear();
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From ogrenciler", bag);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            listele();
            bag.Open();
            kmt = new OleDbCommand("Select * from dersler", bag);
            OleDbDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ders_id"].ToString());
            }
            OleDbCommand kmtt = new OleDbCommand("Select * from bolum", bag);
            OleDbDataReader drr = kmtt.ExecuteReader();
            while (drr.Read())
            {
                comboBox2.Items.Add(drr["bolum_id"].ToString());
            }
            bag.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "INSERT INTO ogrenciler(adi,soyadi,kimlik,d_yeri,ders_id,bolum_id) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" +textBox3.Text + "','"+textBox4.Text+"','"+comboBox1.Text+"','"+comboBox2.Text+"') ";
            kmt.ExecuteNonQuery();
            bag.Close();
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "DELETE from ogrenciler WHERE kimlik='" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "' ";
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE ogrenciler SET adi='" + textBox1.Text + "',soyadi='" +textBox2.Text + "',kimlik='" +textBox3.Text + "',d_yeri='"+textBox4.Text + "',ders_id='"+comboBox1.Text+ "',bolum_id='"+comboBox2.Text+"'where kimlik='" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "'";
            OleDbCommand kmt = new OleDbCommand(sorgu, bag);
            bag.Open();
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tablo.Clear();
                bag.Open();
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * From ogrenciler where kimlik='" + textBox5.Text + "'", bag);
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
    }
}
