using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class KullaniciController : Controller
    {
        KullanicilarManager kullaniciManager = new KullanicilarManager(new EFKullanicilarRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Kullanicilar> liste = kullaniciManager.GetAllList(x => x.Kullanici_Durum == 1 && x.Firma_ID == FirmaID);
            return View(liste);

        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kullanicilar kullanici)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        kullanici.Kullanici_Durum = 1;
                        kullanici.EkleyenKullanici_ID = KullaniciID;
                        kullanici.Firma_ID = FirmaID;
                        kullanici.DuzenleyenID = KullaniciID;
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
