using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class FirmaController : BaseController
    {
        FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
        KullanicilarManager kullaniciManager = new KullanicilarManager(new EFKullanicilarRepository());
        ModullerManager modullerManager = new ModullerManager(new EFModullerRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        public IActionResult Index()
        {
            List<Firma> liste = firmaManager.GetAllList(x => x.Firma_Durum == 1);
            return View(liste);

        }
        public IActionResult Ekle()
        {
            var adres = adresManager.List();
            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            List<Moduller> liste = modullerManager.GetAllList(x => x.Durum == 1);
            ViewBag.Moduller = liste;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Firma firma, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var moduller = form["FirmaModul_ID[]"];

                        var hashPswd = ComputeSHA256Hash(firma.Firma_Sifre);
                        firma.Firma_Sifre = hashPswd;
                        firma.Firma_Durum = 1;
                        firma.EkleyenKullanici_ID = KullaniciID;
                        firma.OlusturmaTarihi = DateTime.Now;
                        firma.DuzenlemeTarihi = DateTime.Now;
                        firma.FirmaModul_ID = moduller;
                        firmaManager.TAdd(firma);
                        Kullanicilar kullanicilar = new Kullanicilar();
                        kullanicilar.KullaniciGrup_ID = 3;
                        kullanicilar.Kullanici_Eposta = firma.Firma_YetkiliEposta;
                        kullanicilar.Kullanici_Isim = firma.Firma_YetkiliAdi;
                        kullanicilar.Kullanici_Soyisim = firma.Firma_YetkiliSoyadi;
                        kullanicilar.Kullanici_Sifre = hashPswd;
                        kullanicilar.Kullanici_Durum = 1;
                        kullanicilar.EkleyenKullanici_ID = KullaniciID;
                        kullanicilar.Firma_ID = firma.Firma_ID;
                        kullanicilar.DuzenleyenID = KullaniciID;
                        kullanicilar.Kullanici_OlusturmaTarihi = DateTime.Now;
                        kullanicilar.Kullanici_DuzenlemeTarihi = DateTime.Now;
                        kullaniciManager.TAdd(kullanicilar);
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

        public static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Convert to hexadecimal representation
                }

                return builder.ToString();
            }
        }

        public IActionResult Duzenle(int FirmaID)
        {
            var adres = adresManager.List();
            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            List<Moduller> liste = modullerManager.GetAllList(x => x.Durum == 1);
            ViewBag.Moduller = liste;
            Firma firma = firmaManager.GetByID(FirmaID);
            return View(firma);
        }
        [HttpPost]
        public IActionResult Duzenle(Firma firma, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Firma kayit = firmaManager.GetByID(firma.Firma_ID);
                        var moduller = form["FirmaModul_ID[]"];
                        var hashPswd = "";
                        if (firma.Firma_Sifre != null && firma.Firma_Sifre != "")
                        {
                            hashPswd = ComputeSHA256Hash(firma.Firma_Sifre);
                            kayit.Firma_Sifre = hashPswd;

                        }
                        kayit.Firma_Adres = firma.Firma_Adres;
                        kayit.Firma_TCNO_VKNO = firma.Firma_TCNO_VKNO;
                        kayit.Firma_AdresILCE_ID = firma.Firma_AdresILCE_ID;
                        kayit.Firma_AdresIL_ID = firma.Firma_AdresIL_ID;
                        kayit.Firma_CepTel = firma.Firma_CepTel;
                        kayit.Firma_Tipi = firma.Firma_Tipi;
                        kayit.Firma_VergiDairesi = firma.Firma_VergiDairesi;
                        kayit.Firma_YetkiliSoyadi = firma.Firma_YetkiliSoyadi;
                        kayit.Firma_YetkiliAdi = firma.Firma_YetkiliAdi;
                        kayit.Firma_Unvan = firma.Firma_Unvan;
                        kayit.Firma_WebSitesi = firma.Firma_WebSitesi;
                        kayit.Firma_YetkiliEposta = firma.Firma_YetkiliEposta;
                        kayit.DuzenlemeTarihi = DateTime.Now;
                        kayit.FirmaModul_ID = moduller;
                        kayit.Firma_EFatura_KullaniciAdi = firma.Firma_EFatura_KullaniciAdi;
                        kayit.Firma_EFatura_Sifre = firma.Firma_EFatura_Sifre;
                        firmaManager.TUpdate(kayit);
                        Kullanicilar kullanicilar = kullaniciManager.GetAllList(x => x.Kullanici_Eposta == kayit.Firma_YetkiliEposta).SingleOrDefault();
                        kullanicilar.KullaniciGrup_ID = 3;
                        kullanicilar.Kullanici_Eposta = firma.Firma_YetkiliEposta;
                        kullanicilar.Kullanici_Isim = firma.Firma_YetkiliAdi;
                        kullanicilar.Kullanici_Soyisim = firma.Firma_YetkiliSoyadi;
                        if (hashPswd != "")
                            kullanicilar.Kullanici_Sifre = hashPswd;


                        kullanicilar.DuzenleyenID = KullaniciID;
                        kullanicilar.Kullanici_DuzenlemeTarihi = DateTime.Now;
                        kullaniciManager.TUpdate(kullanicilar);
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
                        Firma item = firmaManager.GetByID(int.Parse(form["ID"]));
                        item.Firma_Durum = 0;
                        item.Firma_ID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        firmaManager.TUpdate(item);
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


