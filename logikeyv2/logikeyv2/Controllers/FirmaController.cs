using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class FirmaController : Controller
    {
        FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
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
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        
                        firma.Firma_Durum = 1;
                        firma.EkleyenKullanici_ID = 1;//değişçek
                        firma.OlusturmaTarihi = DateTime.UtcNow;
                        firma.DuzenlemeTarihi = DateTime.UtcNow;
                        firmaManager.TAdd(firma);
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
    }
}

   
