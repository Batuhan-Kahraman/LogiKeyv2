using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class CariController : BaseController
    {
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariGrupManager cariGrupManager = new CariGrupManager(new EFCariGrupRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());

        AkaryakitTasimaManager akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        AkaryakitTasimaDetayManager akaryakitTasimaDetayManager = new AkaryakitTasimaDetayManager(new EFAkaryakitTasimaDetayRepository());
        AkaryakitTasimaDetayUrunManager akaryakitTasimaDetayUrunManager = new AkaryakitTasimaDetayUrunManager(new EFAkaryakitTasimaDetayUrunRepository());
        AkaryakitFaturaManager akaryakitFaturaManager = new AkaryakitFaturaManager(new EFAkaryakitFaturaRepository());
    

        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Cari> liste = cariManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
           
            return View(liste);

        }
        public IActionResult Ekle()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var CariGrupList = cariGrupManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
            ViewBag.CariGrup = CariGrupList;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Cari cari)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        cari.Durum = 1;
                        cari.FaturaDurum = false;
                        cari.EkleyenKullanici_ID = KullaniciID;
                        cari.DuzenleyenKullanici_ID = KullaniciID;
                        cari.Firma_ID = FirmaID;
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
            return RedirectToAction("Index");
        }

        public IActionResult Duzenle(int ID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var CariGrupList = cariGrupManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
            ViewBag.CariGrup = CariGrupList;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            Cari cari=cariManager.GetByID(ID);
            return View(cari);
        }
        [HttpPost]
        public IActionResult Duzenle(Cari cari)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Cari item = cariManager.GetByID(cari.Cari_ID);
                        item.DuzenleyenKullanici_ID = KullaniciID;
                        item.Duzenleme_Tarihi = DateTime.UtcNow;
                        item.Cari_CepNo = cari.Cari_CepNo;
                        item.Cari_ILCE_ID = cari.Cari_ILCE_ID;
                        item.Cari_IL_ID = cari.Cari_IL_ID;
                        item.Cari_Unvan = cari.Cari_Unvan;
                        item.Cari_Adres = cari.Cari_Adres;
                        item.Cari_TCNO_VergiNo = cari.Cari_TCNO_VergiNo;
                        item.Cari_Tipi= cari.Cari_Tipi;
                        item.Cari_VergiDairesi = cari.Cari_VergiDairesi;
   item.Cari_GrupID=cari.Cari_GrupID;
                        item.Cari_Kodu = cari.Cari_Kodu;
                        item.Cari_WebSitesi = cari.Cari_WebSitesi;
                        item.Cari_FirmaEposta = cari.Cari_FirmaEposta;
                        item.Cari_FirmaTelefon = cari.Cari_FirmaTelefon;
                        item.Cari_YetkiliAdi = cari.Cari_YetkiliAdi;
                        item.Cari_YetkiliSoyadi = cari.Cari_YetkiliSoyadi;

                        item.Cari_BankaAdi1 = cari.Cari_BankaAdi1;
                        item.Cari_BankaAdi2 = cari.Cari_BankaAdi2;
                        item.Cari_BankaAdi3 = cari.Cari_BankaAdi3;
                        item.Cari_BankaIBAN1 = cari.Cari_BankaIBAN1;
                        item.Cari_BankaIBAN2 = cari.Cari_BankaIBAN2;
                        item.Cari_BankaIBAN3 = cari.Cari_BankaIBAN3;
                        item.Cari_CepNo=cari.Cari_CepNo;
                     
                        

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
        public IActionResult CariHareket(int CariID)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AkaryakitFatura> faturaList = akaryakitFaturaManager.GetAllList(x => x.FaturaKesenID == CariID && x.FirmaID == FirmaID);
            

            ViewBag.Fatura = faturaList;
            ViewBag.CariID = CariID;


            return View();
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
                        Cari item = cariManager.GetByID(int.Parse(form["ID"]));
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
