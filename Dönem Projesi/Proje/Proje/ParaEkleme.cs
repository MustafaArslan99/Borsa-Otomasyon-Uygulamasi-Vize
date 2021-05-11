using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Proje
{
    class ParaEkleme:Client
    {
        private int istenenpara;
        public int IstenenPara { get { return istenenpara; } set { this.istenenpara = value; } }

        OleDbConnection baglanti;
        OleDbCommand komut;

        public void ParaEKle(string kulad)
        {
            //Veritabanındaki parayı istenen para ile degistir.
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=C:/Users/marsl/OneDrive/Masaüstü/Dönem Projesi/YazılımProje.accdb");
            komut = new OleDbCommand();
            komut.Connection = baglanti;
            baglanti.Open();

            komut.CommandText = "update Kullanici set ParaIsteniyorMu='Evet', ParaIste='" + istenenpara + "' where KullaniciAd='" + kulad + "'";
            komut.ExecuteNonQuery();

            baglanti.Close();

            System.Windows.Forms.MessageBox.Show("Para Yükleme İsteğiniz Gönderildi Admin Onay Verdiğinde Hesabınıza Para Yüklenecektir.");
        }
    }
}
