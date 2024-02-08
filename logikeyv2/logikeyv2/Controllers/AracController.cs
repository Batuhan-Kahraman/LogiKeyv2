using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class AracController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
            
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Arac arac)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        arac.Durum = true;
                        arac.FirmaID = 1;//değişçek
                        arac.OlusturmaTarihi = DateTime.Now;
                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.OlusturanId = 1;//değişcek
                        arac.DuzenleyenID = 1;//değişcek
                        aracManager.TAdd(arac);
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
            return View(arac);
        }
    }
}
