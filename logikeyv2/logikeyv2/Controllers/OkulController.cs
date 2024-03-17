using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class OkulController : Controller
    {
        CariManager cariManager = new CariManager(new EFCariRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());

        public IActionResult Index()
        {
            List<Cari> liste = cariManager.GetAllList(x => x.Durum == 1);

            return View(liste);
        }
        public IActionResult OkulEkle()
        {
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            return View();
        }
        [HttpPost]
        public IActionResult OkulEkle(Cari cari)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        cari.Durum = 1;
                        cari.Cari_GrupID = 13;//Okul
                        cari.Cari_Kodu ="Okulkodu";//Okul

                        cari.Cari_TCNO_VergiNo = "11111111111";//Okul modülünde olmadı için sabit değer verildi.
                        cari.Cari_Tipi = 2;//kurumsal
                        cari.Cari_VergiDairesi = "okul";
                        cari.EkleyenKullanici_ID = 1;//değişçek
                        cari.DuzenleyenKullanici_ID = 1;//değişçek
                        cari.Firma_ID = 1;//değişçek
                        cari.Olusturma_Tarihi = DateTime.UtcNow;
                        cari.Duzenleme_Tarihi = DateTime.UtcNow;
                        cariManager.TAdd(cari);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return View(cari);
        }
        public IActionResult OkulDuzenle(int Cari_ID)
        {
            Cari okul = cariManager.GetByID(Cari_ID);
            return View(okul);
        }
        [HttpPost]
        public IActionResult OkulDuzenle(Cari cari)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Cari item = cariManager.GetByID(cari.Cari_ID);
                        item.Cari_Unvan = cari.Cari_Unvan;
                        item.Cari_FirmaTelefon = cari.Cari_FirmaTelefon;
                        item.Cari_IL_ID = cari.Cari_IL_ID;
                        item.Cari_ILCE_ID = cari.Cari_ILCE_ID;
                        item.Cari_Adres = cari.Cari_Adres;
                        item.Cari_FirmaEposta = cari.Cari_FirmaEposta;
                        item.Cari_WebSitesi = cari.Cari_WebSitesi;
                        item.Cari_CepNo = cari.Cari_CepNo;
                        item.Cari_YetkiliAdi = cari.Cari_YetkiliAdi;
                        item.Cari_YetkiliSoyadi = cari.Cari_YetkiliSoyadi;
                        item.Cari_BankaAdi1 = cari.Cari_BankaAdi1;
                        item.Cari_BankaAdi2 = cari.Cari_BankaAdi2;
                        item.Cari_BankaAdi3 = cari.Cari_BankaAdi3;
                        item.Cari_BankaIBAN1 = cari.Cari_BankaIBAN1;
                        item.Cari_BankaIBAN2 = cari.Cari_BankaIBAN2;
                        item.Cari_BankaIBAN3 = cari.Cari_BankaIBAN3;
                      
                       
                     
                      

                        item.Duzenleme_Tarihi = DateTime.UtcNow;
                        item.DuzenleyenKullanici_ID = 1;//değişcek
                        cariManager.TUpdate(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return View(cari);
        }
    }
}
