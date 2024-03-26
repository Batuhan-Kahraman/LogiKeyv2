using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace logikeyv2.Controllers
{
    public class LoginController : Controller
    {
        KullanicilarManager kullaniciManager = new KullanicilarManager(new EFKullanicilarRepository());
        FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(Kullanicilar kullanici)
        {

            var hashPswd = ComputeSHA256Hash(kullanici.Kullanici_Sifre);
            var item = kullaniciManager.GetAllList(x => x.Kullanici_Durum == 1 && x.Kullanici_Eposta == kullanici.Kullanici_Eposta && x.Kullanici_Sifre == hashPswd);
            if (item.Count() > 0)
            {
                var KullaniciID = item[0].Kullanici_ID;
                var FirmaID = item[0].Firma_ID;
                HttpContext.Session.SetInt32("KullaniciID", KullaniciID);
                HttpContext.Session.SetString("Eposta", item[0].Kullanici_Eposta);
                HttpContext.Session.SetInt32("FirmaID", FirmaID);

                var modul = firmaManager.GetByID(FirmaID).FirmaModul_ID;
                if (modul != null)
                {

                    HttpContext.Session.SetString("Moduller", modul);
                }


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Msg = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
