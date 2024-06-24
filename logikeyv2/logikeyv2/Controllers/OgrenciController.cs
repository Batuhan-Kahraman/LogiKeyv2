using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class OgrenciController : BaseController
    {
        CariManager cariManager = new CariManager(new EFCariRepository());
        KullanicilarManager kullanicilarManager = new KullanicilarManager(new EFKullanicilarRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        FaturaOkulManager faturaokulManager = new FaturaOkulManager(new EFFaturaOkulRepository());
        public IActionResult Index()
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery = from ogrencimodulu in ogrenciModuluManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                                join okul in cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13 && (y.Firma_ID == FirmaID || y.Firma_ID == -2))) on ogrencimodulu.CariOkul_ID equals okul.Cari_ID
                                join ogrenci in cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 14 && (y.Firma_ID == FirmaID || y.Firma_ID == -2))) on ogrencimodulu.CariOgrenci_ID equals ogrenci.Cari_ID

                                select new OkulOgrenciModel { OgrenciModulu = ogrencimodulu, Okul = okul, Ogrenci= ogrenci };

            List<OkulOgrenciModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
          
        }
        public IActionResult OgrenciEkle()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Cari> okullar = cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13 && (y.Firma_ID == FirmaID || y.Firma_ID == -2)));
            ViewBag.Okullar = okullar;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            List<Kullanicilar> servisSoforleri = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID == 5 && (y.Firma_ID == FirmaID || y.Firma_ID == -2)));
           ViewBag.Sofor= servisSoforleri;
            return View();
        }
        [HttpPost]
        public IActionResult OgrenciEkle(OkulOgrenciModel model, string Fatura_Adi, string Fatura_Soyadi, string Fatura_Telefon, string Fatura_TC,int Fatura_il, int Fatura_ilce, string Fatura_Adres, string Fatura_Eposta,string Fatura_BankaAdi, string Fatura_BankaIBAN)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");

            try
            {
                using (var context = new Context())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var ogrenci = cariManager.GetAllList(x => x.Cari_TCNO_VergiNo == model.Ogrenci.Cari_TCNO_VergiNo && x.Durum == 1 && x.Cari_GrupID == 14).FirstOrDefault();

                        if (ogrenci != null)
                        {
                            TempData["Msg"] = "İşlem başarısız. TC Kimlik numarası ile daha önce öğrenci kaydedilmiş.";
                            TempData["Bgcolor"] = "red";
                            transaction.Rollback();
                            return RedirectToAction("Index");
                        }
                        var ogrenciadi = model.Ogrenci.Cari_YetkiliAdi;
                        var ogrencisoyadi = model.Ogrenci.Cari_YetkiliSoyadi;
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

                        model.Ogrenci.EkleyenKullanici_ID = KullaniciID;
                        model.Ogrenci.DuzenleyenKullanici_ID = KullaniciID;
                        model.Ogrenci.Firma_ID = FirmaID;
                        model.Ogrenci.Olusturma_Tarihi = DateTime.Now;
                        model.Ogrenci.Duzenleme_Tarihi = DateTime.Now;
                        model.Ogrenci.Cari_CepNo = telno;
                        // Öğrenciyi ekleme işlemi
                        cariManager.TAdd(model.Ogrenci);
                        var kaydedilenOgrenci = cariManager.GetAllList(x => x.Cari_TCNO_VergiNo == model.Ogrenci.Cari_TCNO_VergiNo && x.Durum == 1 && x.Cari_GrupID == 14).FirstOrDefault();
                        model.OgrenciModulu.CariOgrenci_ID = kaydedilenOgrenci.Cari_ID;

                        model.OgrenciModulu.Durum = true;
                        model.OgrenciModulu.FirmaID = FirmaID;
                        model.OgrenciModulu.OlusturmaTarihi = DateTime.Now;
                        model.OgrenciModulu.DuzenlemeTarihi = DateTime.Now;
                        model.OgrenciModulu.EkleyenKullaniciID = KullaniciID;
                        model.OgrenciModulu.DuzenleyenKullaniciID = KullaniciID;
                        ogrenciModuluManager.TAdd(model.OgrenciModulu);
                        if (model.Ogrenci.FaturaDurum == true)
                        {
                            FaturaOkul okul = new FaturaOkul();
                            okul.Adi = Fatura_Adi;
                            okul.Soyadi = Fatura_Soyadi;
                            okul.Adres = Fatura_Adres;
                            okul.ilID = Fatura_il;
                            okul.ilceID = Fatura_ilce;
                            okul.TcKimlikNo = Fatura_TC;
                            okul.TelefonNo = Fatura_Telefon;
                            okul.Eposta = Fatura_Eposta;
                            okul.BankaAdi = Fatura_BankaAdi;
                            okul.IbanNo = Fatura_BankaIBAN;
                            okul.Durum = true;
                            okul.CariOgrenciID = model.Ogrenci.Cari_ID;
                            okul.OlusturmaTarihi = DateTime.Now;
                            okul.DuzenlemeTarihi = DateTime.Now;
                            okul.DuzenleyenKullaniciID = KullaniciID;
                            okul.EkleyenKullaniciID = KullaniciID;
                            okul.FirmaID = FirmaID;
                            faturaokulManager.TAdd(okul);
                        }
                        // Başarılı ekleme mesajı
                        ViewBag.SuccessMessage = "Öğrenci başarıyla eklendi.";

                        return RedirectToAction("Index");
                    }
                }
                       
            }
            catch (Exception ex)
            {
                // Hata durumunda ModelState'e hata ekleyerek, View'e geri dönme
                ModelState.AddModelError("", "Öğrenci eklenirken bir hata oluştu.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult OgrenciDuzenle(int ID)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Cari> okullar = cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13 && (y.Firma_ID == FirmaID || y.Firma_ID == -2)));
            ViewBag.Okullar = okullar;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            List<Kullanicilar> servisSoforleri = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID == 5 && (y.Firma_ID == FirmaID || y.Firma_ID == -2)));
            ViewBag.Sofor = servisSoforleri;
            var combinedQuery = from ogrencimodulu in ogrenciModuluManager.GetAllList(x => x.Durum == true && x.ID == ID )
                                join okul in cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13 && (y.Firma_ID == FirmaID || y.Firma_ID == -2))) on ogrencimodulu.CariOkul_ID equals okul.Cari_ID
                                join ogrenci in cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 14 && (y.Firma_ID == FirmaID || y.Firma_ID == -2))) on ogrencimodulu.CariOgrenci_ID equals ogrenci.Cari_ID
                                select new OkulOgrenciModel { OgrenciModulu = ogrencimodulu, Okul = okul, Ogrenci = ogrenci };

            var combinedModel = combinedQuery.FirstOrDefault();

            return View(combinedModel);



        }
        [HttpPost]
        public IActionResult OgrenciDuzenle(OkulOgrenciModel cari)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Cari item = cariManager.GetByID(cari.Ogrenci.Cari_ID);
                        item.Cari_Unvan = cari.Ogrenci.Cari_Unvan;
                        item.Cari_FirmaTelefon = cari.Ogrenci.Cari_FirmaTelefon;
                        item.Cari_IL_ID = cari.Ogrenci.Cari_IL_ID;
                        item.Cari_ILCE_ID = cari.Ogrenci.Cari_ILCE_ID;
                        item.Cari_Adres = cari.Ogrenci.Cari_Adres;
                        item.Cari_FirmaEposta = cari.Ogrenci.Cari_FirmaEposta;
                        item.Cari_WebSitesi = cari.Ogrenci.Cari_WebSitesi;
                        item.Cari_CepNo = cari.Ogrenci.Cari_CepNo;
                        item.Cari_YetkiliAdi = cari.Ogrenci.Cari_YetkiliAdi;
                        item.Cari_YetkiliSoyadi = cari.Ogrenci.Cari_YetkiliSoyadi;
                        item.Cari_BankaAdi1 = cari.Ogrenci.Cari_BankaAdi1;
                   
                        item.Cari_BankaIBAN1 = cari.Ogrenci.Cari_BankaIBAN1;
                        item.FaturaDurum = cari.Ogrenci.FaturaDurum;






                        item.Duzenleme_Tarihi = DateTime.Now;
                        item.DuzenleyenKullanici_ID = KullaniciID;
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
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Sil(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                       OgrenciModulu ogrenci= ogrenciModuluManager.GetByID(int.Parse(form["ID"]));
                        ogrenci.Durum = false;
                        ogrenciModuluManager.TUpdate(ogrenci);
                        Cari item = cariManager.GetByID(ogrenci.ID);
                        item.Durum = 0;
                        item.Firma_ID = FirmaID;
                        item.Duzenleme_Tarihi = DateTime.Now;
                        item.DuzenleyenKullanici_ID = KullaniciID;
                        cariManager.TUpdate(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }

        }
    }
}
