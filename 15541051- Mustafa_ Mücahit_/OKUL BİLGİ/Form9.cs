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
    public partial class Form9 : Form
    {
        public Form9()
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
            comboBox2.Text = "";
            comboBox3.Text = "";

            tablo.Clear();
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From bolum", bag);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            listele();
            bag.Open();
            kmt = new OleDbCommand("Select * from sinif", bag);
            OleDbDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["sinif_id"].ToString());
            }
            OleDbCommand cmd = new OleDbCommand("select * from ogrenciler",bag);
            OleDbDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                comboBox2.Items.Add(dr2["id"].ToString());
            }
            OleDbCommand cmd2 = new OleDbCommand("select * from ogretmenler", bag);
            OleDbDataReader dr3= cmd2.ExecuteReader();
            while (dr3.Read())
            {
                comboBox3.Items.Add(dr3["ogretmen_id"].ToString());
            }




            bag.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "INSERT INTO bolum(bolum_adi,sinif_id,ogrenci_id,ogretmen_id) VALUES ('" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','"+comboBox3.Text+"') ";
            kmt.ExecuteNonQuery();
            bag.Close();
            listele();
            MessageBox.Show("Ekleme İşlemi Başarılı");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "DELETE from bolum WHERE bolum_adi='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' ";
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
            MessageBox.Show("Silme İşlemi Başarılı");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE bolum SET bolum_adi='" + textBox1.Text + "',sinif_id='" + comboBox1.Text + "',ogrenci_id='" + comboBox2.Text + "',ogretmen_id='"+comboBox3.Text+ "'where  bolum_adi='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
            OleDbCommand kmt = new OleDbCommand(sorgu, bag);
            bag.Open();
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            bag.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tablo.Clear();
                bag.Open();
                OleDbDataAdapter adtr = new OleDbDataAdapter("select * From bolum where bolum_adi='" + textBox3.Text + "'", bag);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
