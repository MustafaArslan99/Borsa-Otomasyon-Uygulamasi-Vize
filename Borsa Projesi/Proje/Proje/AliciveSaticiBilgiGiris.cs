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

namespace Proje
{
    public partial class AliciveSaticiBilgiGiris : Form
    {
        public AliciveSaticiBilgiGiris()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataReader dr;
        OleDbDataAdapter adapter;
        DataSet ds;

        public string KullaniciAd;
        public int para;
        private void AliciveSaticiBilgiGiris_Load(object sender, EventArgs e)
        {
            lbl_Kulad.Text = KullaniciAd;
            ParayiGetir(); //Kullanıcının Parasını label'a yazdırır
            UrunleriGetir(); //Satış listesindeki ürünleri getirir.       
        }

        private void ParayiGetir()
        {
            //veritabanından parayı çek. Paranın tutulduğu labela yaz.            
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=C:/Users/marsl/OneDrive/Masaüstü/Dönem Projesi/YazılımProje.accdb");
                komut = new OleDbCommand();
                komut.Connection = baglanti;
                baglanti.Open();
                komut.CommandText = "SELECT YukluPara FROM Kullanici where KullaniciAd='" + lbl_Kulad.Text + "'";
                dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    para = Convert.ToInt32(dr[0]);
                }

                baglanti.Close();
                dr.Close();
                lbl_Para.Text = para.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Meydana Geldi.\nLütfen Tekrar Deneyiniz.");
            }
            
        }

        private void UrunleriGetir()
        {
            //veritabanından satış listesindeki ürünleri getir.
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=C:/Users/marsl/OneDrive/Masaüstü/Dönem Projesi/YazılımProje.accdb");
                adapter = new OleDbDataAdapter("Select * from SatistakiUrunler", baglanti);
                ds = new DataSet();
                baglanti.Open();

                adapter.Fill(ds, "SatistakiUrunler");
                dataGridView1.DataSource = ds.Tables["SatistakiUrunler"];
                dataGridView1.Columns[0].HeaderText = "Ürün Numarası";
                dataGridView1.Columns[1].HeaderText = "Satıcı Adı";
                dataGridView1.Columns[2].HeaderText = "Ürün İsmi";
                dataGridView1.Columns[3].HeaderText = "Miktar";
                dataGridView1.Columns[4].HeaderText = "Tür";
                dataGridView1.Columns[5].HeaderText = "Fiyat";
                dataGridView1.Columns[6].HeaderText = "Satışa Çıktığı Tarih";

                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Meydana Geldi.\nLütfen Tekrar Deneyiniz.");
            }
            
        }
        private void btn_Parayükle_Click(object sender, EventArgs e)
        {
            //Kullanıcının para yükleme işlemleri
            try
            {
                ParaEkleme pe = new ParaEkleme();
                pe.IstenenPara = Convert.ToInt32(txt_parayükle.Text);
                pe.ParaEKle(KullaniciAd);
                txt_parayükle.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Meydana Geldi.\nLütfen Tekrar Deneyiniz.");
            }            
        }
            
        private void btn_urunekle_Click(object sender, EventArgs e)
        {
            //Kullanıcının ürün satışa çıkarmka için gerekli işlemleri
            try
            {
                UrunEkle ue = new UrunEkle();
                ue.UrunAd = txt_urunad.Text;
                ue.Miktar = Convert.ToInt32(txt_urunmiktar.Text);
                ue.Tur = txt_uruntur.Text;
                ue.Fiyat = Convert.ToInt32(txt_urunfiyat.Text);

                ue.UrunSatis(KullaniciAd);
                txt_urunad.Text = "";
                txt_urunfiyat.Text = "";
                txt_urunmiktar.Text = "";
                txt_uruntur.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Meydana Geldi.\nLütfen Tekrar Deneyiniz.");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Kullanıcının satış listesindeki ürünü almak için gerekli işlemleri
            try
            {
                UrunSatinAl usa = new UrunSatinAl();
                usa.UrunNo = Convert.ToInt32(txt_urunno.Text);
                usa.SatinAlan = lbl_Kulad.Text;
                usa.Bakiye = para;
                usa.UrunSatinAlma();
                UrunleriGetir();
                ParayiGetir();
                txt_urunno.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Meydana Geldi.\nLütfen Tekrar Deneyiniz.");
            }            
        }

        private void btn_satinalinmisurunler_Click(object sender, EventArgs e)
        {
            //Satın alınmış ürünlere bakmak isterse forma gönder.
            SatinAlinmisUrunler sau = new SatinAlinmisUrunler();
            sau.kullaniciad = lbl_Kulad.Text;
            sau.Show();
        }
    }
}
