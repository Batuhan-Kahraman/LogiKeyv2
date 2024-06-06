using DataAccessLayer.Concrate;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;


namespace BusinessLayer.Concrate
{
    public class Helper : Context
    {
        public static bool CariBorcAlacakKalanHesapla(int CariID)
        {
            using (var context = new Context())
            {
                try
                {

                    var cariHareket = context.CariHareket.Where(e => e.CariID == CariID && e.Durum==true).ToList();
                    double akaryakitTutar = cariHareket.Where(e => e.AkaryakitFaturaTutar.HasValue).Sum(e => e.AkaryakitFaturaTutar.GetValueOrDefault());
                    double normalTutar = cariHareket.Where(e => e.NormalFaturaTutar.HasValue).Sum(e => e.NormalFaturaTutar.GetValueOrDefault());
                    double krediTutar = cariHareket.Where(e => e.KrediTutar.HasValue).Sum(e => e.KrediTutar.GetValueOrDefault());
                    double cekTutar = cariHareket.Where(e => e.CekTutar.HasValue).Sum(e => e.CekTutar.GetValueOrDefault());
                    double senetTutar = cariHareket.Where(e => e.SenetTutar.HasValue).Sum(e => e.SenetTutar.GetValueOrDefault());
                    double havaleTutar = cariHareket.Where(e => e.HavaleTutar.HasValue).Sum(e => e.HavaleTutar.GetValueOrDefault());


                    double borc = 0, alacak = 0, kalan = 0;
                    borc = akaryakitTutar + normalTutar;
                    alacak = krediTutar + cekTutar + senetTutar + havaleTutar;
                    kalan = borc - alacak;

                    var cari = context.Cari.Where(x => x.Cari_ID == CariID).SingleOrDefault();
                    if (cari != null)
                    {
                        cari.ToplamAlacak = alacak;
                        cari.ToplamBorc = borc;
                        cari.ToplamKalan = kalan;
                        // Değişiklikleri kaydedin
                        context.SaveChanges();
                        return true;
                    }
                    else return false;
                }
                catch
                {
                    return false;
                }
            }
        }


        public static string AkaryakitTasimaIlkMusteri(int TasimaID)
        {
            using (var context = new Context())
            {

                var entity = context.AkaryakitTasimaDetay.OrderBy(x => x.ID).FirstOrDefault(e => e.AkaryakitTasimaID == TasimaID);

                if (entity != null)
                {
                    return CariUnvan(entity.GondericiID);
                }
                else
                    return "";

            }
        }
        public static string AkaryakitTasimaMusteriSayi(int TasimaID)
        {
            using (var context = new Context())
            {

                var entity = context.AkaryakitTasimaDetay.Count(e => e.AkaryakitTasimaID == TasimaID);

                if (entity != null)
                {
                    return entity.ToString();
                }
                else
                    return "";

            }
        }
        public static string CariBorcAlacak(int CariID)
        {
            using (var context = new Context())
            {
                if (CariID != 0)
                {

                    var entity = context.Cari.FirstOrDefault(e => e.Cari_ID == CariID);

                    if (entity != null)
                    {
                        return "<span style='color:red;'>Borç:" + entity.ToplamBorc + "  </span><span style='color:green;'>Alacak:" + entity.ToplamAlacak + "  </span><span style='color:blue;'>Kalan:" + entity.ToplamKalan+"</span>";
                    }
                    else
                        return "";
                }
                else
                {
                    return "";
                }
            }
        }
        public static string StokKategori(int id)
        {
            using (var context = new Context())
            {
                var entity = context.StokKategori.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.StokKategoriAdi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string MasrafTuru(int id)
        {
            using (var context = new Context())
            {
                var entity = context.MasrafTuru.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string Istasyon(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Istasyon.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string Tank(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Tank.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string YakitTipi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.YakitTipi.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string YakitAltTipi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.YakitAltTipi.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string Modul(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Moduller.FirstOrDefault(e => e.Modul_ID == id);
                if (entity != null)
                {
                    return entity.Modul_Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string KullaniciAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Kullanicilar.FirstOrDefault(e => e.Kullanici_ID == id);
                if (entity != null)
                {
                    return entity.Kullanici_Isim + " " + entity.Kullanici_Soyisim;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string ServisDurumAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.ServisBakimDurum.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string ServisTurAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.ServisBakimTuru.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string UyariTipAd(int id)
        {
            using (var context = new Context())
            {
                var entity = context.UyariTip.FirstOrDefault(e => e.UyariTipID == id);
                if (entity != null)
                {
                    return entity.Ad;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string CariUnvan(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Cari.FirstOrDefault(e => e.Cari_ID == id);
                if (entity != null)
                {
                    return entity.Cari_Unvan;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string FirmaUnvan(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Firma.FirstOrDefault(e => e.Firma_ID == id);
                if (entity != null)
                {
                    return entity.Firma_Unvan;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string UrunAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.TasinacakUrun.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }

        public static AkaryakitTasima AkaryakitTasiCariHareket(int ID)
        {

            using (var context = new Context())
            {
                AkaryakitTasima liste = context.AkaryakitTasima.Where(e => e.ID == ID).SingleOrDefault();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }



        public static AkaryakitTasimaDetay AkaryakitTasimaDetayCariHareket(int ID)
        {

            using (var context = new Context())
            {
                AkaryakitTasimaDetay liste = context.AkaryakitTasimaDetay.Where(e => e.ID == ID).SingleOrDefault();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }

        public static AkaryakitTasimaDetayUrun AkaryakitTasimaDetayUrunCariHareket(int ID)
        {

            using (var context = new Context())
            {
                AkaryakitTasimaDetayUrun liste = context.AkaryakitTasimaDetayUrun.Where(e => e.ID == ID).SingleOrDefault();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }





        public static List<AkaryakitTasimaDetay> AkaryakitTasimaDetayList(int ID)
        {
            using (var context = new Context())
            {
                List<AkaryakitTasimaDetay> liste = context.AkaryakitTasimaDetay.Where(e => e.AkaryakitTasimaID == ID).ToList();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<AkaryakitTasimaDetayUrun> AkaryakitTasimaDetayUrunList(int tasimaID, int detayID)
        {
            using (var context = new Context())
            {
                List<AkaryakitTasimaDetayUrun> liste = context.AkaryakitTasimaDetayUrun.Where(e => e.AkaryakitTasimaID == tasimaID && e.AkaryakitTasimaDetayID == detayID).ToList();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }
        public static string AracPlaka(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Arac.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Plaka;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string TasimaTipi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.TasimaTipi.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static bool OkulAracTurKontrol(int id)
        {
            using (var context = new Context())
            {
                var entity = context.OkulAracTur.FirstOrDefault(e => e.TurID == id);
                if (entity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool AkaryakitAracTurKontrol(int id)
        {
            using (var context = new Context())
            {
                var entity = context.AkaryakitAracTur.FirstOrDefault(e => e.TurID == id);
                if (entity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static string OkulAracTur(int id)
        {
            using (var context = new Context())
            {
                var entity = context.AracTur.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string AkaryakitAracTur(int id)
        {
            using (var context = new Context())
            {
                var entity = context.AracTur.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string UnAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.UnListesi.FirstOrDefault(e => e.Un_ID == id);
                if (entity != null)
                {
                    return entity.Un_Isim;
                }
                else
                {
                    return "";
                }
            }
        }

        public static bool AkaryakitCariHareketEkle(AkaryakitCariHareket cariHareket)
        {
            using (var context = new Context())
            {
                try
                {
                    context.AkaryakitCariHareket.Add(cariHareket);

                    // Değişiklikleri kaydedin
                    context.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool BakimStokTemizle(int BakimID)
        {
            using (var context = new Context())
            {
                try
                {

                    var silinecek = context.BakimStok.Where(e => e.BakimID == BakimID).ToList();

                    context.BakimStok.RemoveRange(silinecek);

                    // Değişiklikleri kaydedin
                    context.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool AkaryakitCariHareketTemizle(int FaturaID)
        {
            using (var context = new Context())
            {
                try
                {

                    var silinecekUrunler = context.AkaryakitCariHareket.Where(e => e.FaturaID == FaturaID).ToList();

                    context.AkaryakitCariHareket.RemoveRange(silinecekUrunler);

                    // Değişiklikleri kaydedin
                    context.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool TumKullaniciEvraklariSil(int KullaniciID)
        {
            using (var context = new Context())
            {
                try
                {

                    var silinecekEvraklar = context.KullaniciEvraklar.Where(e => e.KullaniciID == KullaniciID).ToList();

                    context.KullaniciEvraklar.RemoveRange(silinecekEvraklar);

                    // Değişiklikleri kaydedin
                    context.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        public static bool NormalCariHareketEkle(NormalCariHareket cariHareket)
        {
            using (var context = new Context())
            {
                try
                {
                    context.NormalCariHareket.Add(cariHareket);

                    // Değişiklikleri kaydedin
                    context.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool NormalCariHareketTemizle(int FaturaID)
        {
            using (var context = new Context())
            {
                try
                {

                    var silinecekUrunler = context.NormalCariHareket.Where(e => e.FaturaID == FaturaID).ToList();

                    context.NormalCariHareket.RemoveRange(silinecekUrunler);

                    // Değişiklikleri kaydedin
                    context.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        public static bool NormalAracTurKontrol(int id)
        {
            using (var context = new Context())
            {
                var entity = context.NormalAracTur.FirstOrDefault(e => e.TurID == id);
                if (entity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static string NormalAracTur(int id)
        {
            using (var context = new Context())
            {
                var entity = context.AracTur.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }


        public static string TurAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.AracTur.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }


        public static List<NormalTasimaDetay> NormalTasimaDetayList(int ID)
        {
            using (var context = new Context())
            {
                List<NormalTasimaDetay> liste = context.NormalTasimaDetay.Where(e => e.NormalTasimaID == ID).ToList();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<NormalTasimaDetayUrun> NormalTasimaDetayUrunList(int tasimaID, int detayID)
        {
            using (var context = new Context())
            {
                List<NormalTasimaDetayUrun> liste = context.NormalTasimaDetayUrun.Where(e => e.NormalTasimaID == tasimaID && e.NormalTasimaDetayID == detayID).ToList();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool AkaryakitSurucuKopar(int Kullanici1ID, int Kullanici2ID, int Kullanici3ID)
        {
            using (var context = new Context())
            {
                try
                {

                    var selectQuery1 = "update AkaryakitTasima set Kullanici1ID=-1 where ID in (select ID from AkaryakitTasima where Kullanici1ID=" + Kullanici1ID + ")";
                    var selectQuery2 = "update AkaryakitTasima set Kullanici2ID=-1 where ID in (select ID from AkaryakitTasima where Kullanici2ID=" + Kullanici2ID + ")";
                    var selectQuery3 = "update AkaryakitTasima set Kullanici3ID=-1 where ID in (select ID from AkaryakitTasima where Kullanici3ID=" + Kullanici3ID + ")";


                    context.Database.ExecuteSqlRaw(selectQuery1);
                    context.Database.ExecuteSqlRaw(selectQuery2);
                    context.Database.ExecuteSqlRaw(selectQuery3);
                    return true;
                }
                catch
                {
                    return false;

                }

            }
        }
        public static bool NormalSurucuKopar(int Kullanici1ID, int Kullanici2ID, int Kullanici3ID)
        {
            using (var context = new Context())
            {
                try
                {

                    var selectQuery1 = "update NormalTasima set Kullanici1ID=-1 where ID in (select ID from NormalTasima where Kullanici1ID=" + Kullanici1ID + ")";
                    var selectQuery2 = "update NormalTasima set Kullanici2ID=-1 where ID in (select ID from NormalTasima where Kullanici2ID=" + Kullanici2ID + ")";
                    var selectQuery3 = "update NormalTasima set Kullanici3ID=-1 where ID in (select ID from NormalTasima where Kullanici3ID=" + Kullanici3ID + ")";


                    context.Database.ExecuteSqlRaw(selectQuery1);
                    context.Database.ExecuteSqlRaw(selectQuery2);
                    context.Database.ExecuteSqlRaw(selectQuery3);
                    return true;
                }
                catch
                {
                    return false;

                }

            }
        }
    }
}
