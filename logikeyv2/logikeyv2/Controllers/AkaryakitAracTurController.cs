using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class AkaryakitAracTurController : Controller
    {
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AkaryakitAracTurManager akaryakitAracTurManager = new AkaryakitAracTurManager(new EFAkaryakitAracTurRepository());
        public IActionResult Index()
        {
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true);
            return View(liste);
        }
        [HttpPost]
        public IActionResult Kaydet(IFormCollection form)
        {
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
                akaryakitAracTurManager.TAdd(item);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
