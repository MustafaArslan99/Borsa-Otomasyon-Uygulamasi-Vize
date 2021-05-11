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
    public partial class SatinAlinmisUrunler : Form
    {
        public SatinAlinmisUrunler()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti;
        OleDbDataAdapter adapter;
        DataSet ds;

        public string kullaniciad;
        private void SatinAlinmisUrunler_Load(object sender, EventArgs e)
        {
            //veritabanından satın alınmış ürünleri getir.
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=C:/Users/marsl/OneDrive/Masaüstü/Dönem Projesi/YazılımProje.accdb");
                adapter = new OleDbDataAdapter("Select UrunNo, KullaniciAd, SatinAlan, UrunAd, UrunMiktar, MiktarTur, Fiyat, SatisTarih, AlisTarih from Urunler where SatinAlan='" + kullaniciad + "' AND Satildimi='Evet' ", baglanti);
                ds = new DataSet();
                baglanti.Open();

                adapter.Fill(ds, "Urunler");
                dataGridView2.DataSource = ds.Tables["Urunler"];
                dataGridView2.Columns[0].HeaderText = "Ürün Numarası";
                dataGridView2.Columns[1].HeaderText = "Satıcı Adı";
                dataGridView2.Columns[2].HeaderText = "Satın Alan Adı";
                dataGridView2.Columns[3].HeaderText = "Ürün İsmi";
                dataGridView2.Columns[4].HeaderText = "Miktar";
                dataGridView2.Columns[5].HeaderText = "Tür";
                dataGridView2.Columns[6].HeaderText = "Fiyat";
                dataGridView2.Columns[7].HeaderText = "Satışa Çıktığı Tarih";
                dataGridView2.Columns[8].HeaderText = "Satın Alındığı Tarih";


                baglanti.Close();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Bir Hata Meydana Geldi Lütfen Tekrar Deneyiniz.");
            }          
        }
    }
}
