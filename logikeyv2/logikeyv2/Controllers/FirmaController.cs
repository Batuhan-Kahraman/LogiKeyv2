using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class FirmaController : Controller
    {
        FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
        KullanicilarManager kullaniciManager = new KullanicilarManager(new EFKullanicilarRepository());
        public IActionResult Index()
        {
            List<Firma> liste = firmaManager.GetAllList(x => x.Firma_Durum == 1);
            return View(liste);
           
        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Firma firma)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var hashPswd = ComputeSHA256Hash(firma.Firma_Sifre);
                        firma.Firma_Sifre = hashPswd;
                        firma.Firma_Durum = 1;
                        firma.EkleyenKullanici_ID = KullaniciID;
                        firma.OlusturmaTarihi = DateTime.UtcNow;
                        firma.DuzenlemeTarihi = DateTime.UtcNow;
                        firmaManager.TAdd(firma);
                        Kullanicilar kullanicilar = new Kullanicilar();
                        kullanicilar.KullaniciGrup_ID = 3;
                        kullanicilar.Kullanici_Eposta = firma.Firma_YetkiliEposta;
                        kullanicilar.Kullanici_Isim = firma.Firma_YetkiliAdi;
                        kullanicilar.Kullanici_Soyisim = firma.Firma_YetkiliSoyadi;
                        kullanicilar.Kullanici_Sifre = hashPswd;
                        kullanicilar.Kullanici_Durum = 1;
                        kullanicilar.EkleyenKullanici_ID = KullaniciID;
                        kullanicilar.Firma_ID = FirmaID;
                        kullanicilar.DuzenleyenID = KullaniciID;
                        kullanicilar.Kullanici_OlusturmaTarihi = DateTime.UtcNow;
                        kullanicilar.Kullanici_DuzenlemeTarihi = DateTime.UtcNow;
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
            return View(firma);
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
    }
}

   
