using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class AkaryakitAracTurController : BaseController
    {
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AkaryakitAracTurManager akaryakitAracTurManager = new AkaryakitAracTurManager(new EFAkaryakitAracTurRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return View(liste);
        }
        [HttpPost]
        public IActionResult Kaydet(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                var allItems = context.AkaryakitAracTur.ToList();
                context.AkaryakitAracTur.RemoveRange(allItems);
                context.SaveChanges();
                var check = form["check"];

                foreach (var id in check)
                {
                    
                AkaryakitAracTur item = new AkaryakitAracTur();
                item.TurID = int.Parse(id);
                    item.Durum = true;
                    item.FirmaID = FirmaID;
                    item.DuzenlemeTarihi = DateTime.Now;
                    item.OlusturmaTarihi = DateTime.Now;
                    item.DuzenleyenID = KullaniciID;
                    item.OlusturanId = KullaniciID;

                    akaryakitAracTurManager.TAdd(item);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
