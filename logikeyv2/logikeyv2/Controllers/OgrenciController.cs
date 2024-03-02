using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class OgrenciController : Controller
    {
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());

        public IActionResult Index()
        {
            var combinedQuery = from ogrencimodulu in ogrenciModuluManager.GetAllList(x => x.Durum == 1)
                                join okul in cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13)) on ogrencimodulu.CariOkul_ID equals okul.Cari_ID
                                join ogrenci in cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 14)) on ogrencimodulu.CariOgrenci_ID equals ogrenci.Cari_ID

                                select new OkulOgrenciModel { OgrenciModulu = ogrencimodulu, Okul = okul, Ogrenci= ogrenci };

            List<OkulOgrenciModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
          
        }
        public IActionResult OgrenciEkle()
        {
            List<Cari> okullar = cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13));
            ViewBag.Okullar = okullar;
            return View();
        }
        [HttpPost]
        public IActionResult OgrenciEkle(OkulOgrenciModel model)
        {
            
            try
            {
                var ogrenciadi = model.Ogrenci.Cari_YetkiliAdi;
                var ogrencisoyadi= model.Ogrenci.Cari_YetkiliSoyadi;
                var tamAd = String.Concat(ogrenciadi, " ", ogrencisoyadi);
                var telno = model.Ogrenci.Cari_FirmaTelefon;
                // Öğrenciye ait ek bilgilerin atanması
                model.Ogrenci.Durum = 1;
                model.Ogrenci.Cari_GrupID = 14;
                model.Ogrenci.Cari_Unvan = tamAd;

                model.Ogrenci.Cari_Kodu = "Ogrencikodu";
                model.Ogrenci.Cari_Tipi = 1;
                model.Ogrenci.Cari_VergiDairesi = "öğrenci";
                model.Ogrenci.Cari_WebSitesi = "öğrenci.com";

                model.Ogrenci.EkleyenKullanici_ID = 1;
                model.Ogrenci.DuzenleyenKullanici_ID = 1;
                model.Ogrenci.Firma_ID = 1;
                model.Ogrenci.Olusturma_Tarihi = DateTime.UtcNow;
                model.Ogrenci.Duzenleme_Tarihi = DateTime.UtcNow;
                model.Ogrenci.Cari_CepNo = telno;
                // Öğrenciyi ekleme işlemi
                cariManager.TAdd(model.Ogrenci);
                var ogrenci = cariManager.GetAllList(x => x.Cari_TCNO_VergiNo == model.Ogrenci.Cari_TCNO_VergiNo && x.Durum == 1 && x.Cari_GrupID == 14).FirstOrDefault();
                model.OgrenciModulu.CariOgrenci_ID = ogrenci.Cari_ID;
                model.OgrenciModulu.Durum = 1;
                ogrenciModuluManager.TAdd(model.OgrenciModulu);
                // Başarılı ekleme mesajı
                ViewBag.SuccessMessage = "Öğrenci başarıyla eklendi.";

                return RedirectToAction("Index", "Home"); // Örneğin, anasayfaya yönlendirme
            }
            catch (Exception ex)
            {
                // Hata durumunda ModelState'e hata ekleyerek, View'e geri dönme
                ModelState.AddModelError("", "Öğrenci eklenirken bir hata oluştu.");
                return View(model);
            }
        }

        public IActionResult OgrenciDuzenle(int ID)
        {
            OgrenciModulu ogrenci = ogrenciModuluManager.GetByID(ID);
            ViewBag.Okullar = cariManager.GetAllList(y => y.Durum == 1 && y.Cari_GrupID == 13);
            return View(ogrenci);
        }
        [HttpPost]
        public IActionResult OgrenciDuzenle(Cari cari)
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
