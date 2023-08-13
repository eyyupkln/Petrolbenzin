using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Petrolbenzin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-AJM9AQ8\SQLEXPRESS;Initial Catalog=Petrolbenzin;Integrated Security=True");

        
           void listele()
        {
            //kurşunsuz95
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from tblbenzin where petroltür='Kurşunsuz95' ", baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblkursunsuz.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblkursunsuzlitre.Text = dr[4].ToString() ;
                lbldepokursunsuz .Text = dr[2].ToString() ;
            }
            baglantı.Close();

            //dizel
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("select * from tblbenzin where petroltür='Dizel' ", baglantı);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lbldizel.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                lbldizellitre.Text = dr2[4].ToString();
                lbldepodizel .Text = dr2[2].ToString();
            }
            baglantı.Close();

            //prodizel
            baglantı.Open();
            SqlCommand komut3 = new SqlCommand("select * from tblbenzin where petroltür='Prodizel' ", baglantı);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblprodizel.Text = dr3[3].ToString() ;
                progressBar3.Value = int.Parse(dr3[4].ToString());
                lblprodizellitre.Text = dr3[4].ToString();
                lbldepoprodizel .Text = dr3[2].ToString();
            }
            baglantı.Close();

            //lpg
            baglantı.Open();
            SqlCommand komut4 = new SqlCommand("select * from tblbenzin where petroltür='LPG' ", baglantı);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lbllpg.Text = dr4[3].ToString() ;
                progressBar4.Value = int.Parse(dr4[4].ToString());
                lbllpglitre.Text = dr4[4].ToString();
                lbldepolpg .Text = dr4[2].ToString() ;
            }
            baglantı.Close();
        }
           
        void kasa()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select*from tblkasa  " , baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblkasa .Text = dr[0].ToString() + " TL";
            }
                
            
            baglantı .Close();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            kasa ();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert .ToDouble (lblkursunsuz.Text);
            litre = Convert .ToDouble  (numericUpDown1 .Value);
            tutar = kursunsuz95 * litre;
            txtkursunsuzfiyat .Text = tutar .ToString() ;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double dizel, litre, tutar;
            dizel = Convert .ToDouble (lbldizel .Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = dizel * litre;
            txtdizelfiyat .Text = tutar .ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double prodizel , litre, tutar; 
            prodizel = Convert .ToDouble (lblprodizel .Text);
            litre = Convert.ToDouble (numericUpDown3.Value);
            tutar = prodizel * litre;
            txtprodizelfiyat .Text = tutar .ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double lpg ,litre, tutar;
            lpg = Convert .ToDouble (lbllpg .Text);
            litre = Convert .ToDouble (numericUpDown4.Value);
            tutar = lpg * litre;
            txtlpgfiyat .Text = tutar .ToString();
        }

        private void btndepodoldur_Click(object sender, EventArgs e)
        {
            if (numericUpDown1 .Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (PLAKA,BENZİNTÜRÜ,LİTRE,FİYAT) values (@p1,@p2,@p3,@p4)", baglantı);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "Kurşunsuz 95");
                komut .Parameters .AddWithValue ("@p3",numericUpDown1 .Value);
                komut .Parameters .AddWithValue ("@p4",decimal.Parse (txtkursunsuzfiyat.Text));
                komut .ExecuteNonQuery ();
                baglantı .Close();
                

                baglantı .Open ();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR+@p1", baglantı );
                komut2 .Parameters .AddWithValue ("@p1",decimal .Parse (txtkursunsuzfiyat .Text ));
                komut2 .ExecuteNonQuery () ;
                baglantı .Close ();


                baglantı .Open ();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK-@p1 where PETROLTÜR= 'Kursunsuz95'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut3 .ExecuteNonQuery ( );
                baglantı .Close () ;
                MessageBox.Show("Satış yapıldı");
                listele();
                kasa();
            }
            if (numericUpDown2.Value != 0)

            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (PLAKA,BENZİNTÜRÜ,LİTRE,FİYAT) values (@p1,@p2,@p3,@p4)", baglantı);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "Dizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtdizelfiyat .Text));
                komut.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR+@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtdizelfiyat .Text));
                komut2.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK-@p1 where PETROLTÜR= 'Dizel'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                komut3.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Satış yapıldı");
                listele();
                kasa();
            }
            if (numericUpDown3.Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (PLAKA,BENZİNTÜRÜ,LİTRE,FİYAT) values (@p1,@p2,@p3,@p4)", baglantı);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "Prodizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtprodizelfiyat .Text));
                komut.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR+@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtprodizelfiyat .Text));
                komut2.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK-@p1 where PETROLTÜR= 'Prodizel'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                komut3.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Satış yapıldı");
                listele();
                kasa();
            }
            if (numericUpDown4.Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (PLAKA,BENZİNTÜRÜ,LİTRE,FİYAT) values (@p1,@p2,@p3,@p4)", baglantı);
                komut.Parameters.AddWithValue("@p1", txtplaka.Text);
                komut.Parameters.AddWithValue("@p2", "LPG");
                komut.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtlpgfiyat .Text));
                komut.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR+@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtlpgfiyat .Text));
                komut2.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK-@p1 where PETROLTÜR= 'LPG'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                komut3.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Satış yapıldı");
                listele();
                kasa();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (numericUpDown5.Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket2 (BENZİNTÜRÜ,LİTRE,FİYAT) values (@p2,@p3,@p4)", baglantı);

                komut.Parameters.AddWithValue("@p2", "Kurşunsuz 95");
                komut.Parameters.AddWithValue("@p3", numericUpDown5.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(textBox4.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();



                baglantı.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR-@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(textBox4.Text));
                komut2.ExecuteNonQuery();
                baglantı.Close();

                baglantı.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK+@p1 where PETROLTÜR= 'Kursunsuz95'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown5.Value);
                komut3.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Alım yapıldı");
                listele();
                kasa();
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lbldepokursunsuz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = kursunsuz95 * litre;
            textBox4 .Text = tutar.ToString();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lbldepodizel .Text);
            litre = Convert.ToDouble(numericUpDown6.Value);
            tutar = kursunsuz95 * litre;
            textBox3.Text = tutar.ToString();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lbldepoprodizel .Text);
            litre = Convert.ToDouble(numericUpDown7.Value);
            tutar = kursunsuz95 * litre;
            textBox2.Text = tutar.ToString();
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lbldepolpg .Text);
            litre = Convert.ToDouble(numericUpDown8.Value);
            tutar = kursunsuz95 * litre;
            textBox1.Text = tutar.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (numericUpDown6.Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (BENZİNTÜRÜ,LİTRE,FİYAT) values (@p2,@p3,@p4)", baglantı);
               
                komut.Parameters.AddWithValue("@p2", "Dizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown6.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(textBox3 .Text));
                komut.ExecuteNonQuery();
                baglantı.Close();


                baglantı.Open();
            SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR-@p1", baglantı);
            komut2.Parameters.AddWithValue("@p1", decimal.Parse(textBox3.Text));
            komut2.ExecuteNonQuery();
            baglantı.Close();

            baglantı.Open();
            SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK+@p1 where PETROLTÜR= 'Dizel'", baglantı);
            komut3.Parameters.AddWithValue("@p1", numericUpDown6.Value);
            komut3.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Alım yapıldı");
            listele();
            kasa();
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (numericUpDown7.Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (BENZİNTÜRÜ,LİTRE,FİYAT) values (@p2,@p3,@p4)", baglantı);

                komut.Parameters.AddWithValue("@p2", "Prodizel");
                komut.Parameters.AddWithValue("@p3", numericUpDown7.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(textBox2.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();




                baglantı.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR-@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(textBox2.Text));
                komut2.ExecuteNonQuery();
                baglantı.Close();

                baglantı.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK+@p1 where PETROLTÜR= 'Prodizel'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown7.Value);
                komut3.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Alım yapıldı");
                listele();
                kasa();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            if (numericUpDown8.Value != 0)
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("insert into tblhareket (BENZİNTÜRÜ,LİTRE,FİYAT) values (@p2,@p3,@p4)", baglantı);

                komut.Parameters.AddWithValue("@p2", "LPG");
                komut.Parameters.AddWithValue("@p3", numericUpDown8.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(textBox1.Text));
                komut.ExecuteNonQuery();
                baglantı.Close();



                baglantı.Open();
                SqlCommand komut2 = new SqlCommand("update tblkasa set MİKTAR=MİKTAR-@p1", baglantı);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(textBox1.Text));
                komut2.ExecuteNonQuery();
                baglantı.Close();

                baglantı.Open();
                SqlCommand komut3 = new SqlCommand("update tblbenzin set STOK= STOK+@p1 where PETROLTÜR= 'LPG'", baglantı);
                komut3.Parameters.AddWithValue("@p1", numericUpDown8.Value);
                komut3.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Alım yapıldı");
                listele();
                kasa();
            }
        }
    }
}
