using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class KullaniciController : Controller
    {
        KullanicilarManager kullaniciManager = new KullanicilarManager(new EFKullanicilarRepository());
        public IActionResult Index()
        {
            List<Kullanicilar> liste = kullaniciManager.GetAllList(x => x.Kullanici_Durum == true);
            return View(liste);

        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kullanicilar kullanici)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        kullanici.Kullanici_Durum = true;
                        kullanici.EkleyenKullanici_ID = 1;//değişçek
                        kullanici.Firma_ID = 1;//değişçek
                        kullanici.Kullanici_OlusturmaTarihi = DateTime.UtcNow;
                        kullanici.Kullanici_DuzenlemeTarihi = DateTime.UtcNow;
                        kullaniciManager.TAdd(kullanici);
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
            return View(kullanici);
        }
    }
}
