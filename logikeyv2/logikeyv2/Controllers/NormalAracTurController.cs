using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class NormalAracTurController : BaseController
    { 
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        NormalAracTurManager NormalAracTurManager = new NormalAracTurManager(new EFNormalAracTurRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            return View(liste);
        }
        [HttpPost]
        public IActionResult Kaydet(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                var allItems = context.NormalAracTur.ToList();
                context.NormalAracTur.RemoveRange(allItems);
                context.SaveChanges();
                var check = form["check"];

                foreach (var id in check)
                {
                    
                NormalAracTur item = new NormalAracTur();
                item.TurID = int.Parse(id);

                    item.Durum = true;
                    item.FirmaID = FirmaID;
                    item.DuzenlemeTarihi = DateTime.Now;
                    item.OlusturmaTarihi = DateTime.Now;
                    item.DuzenleyenID = KullaniciID;
                    item.OlusturanId = KullaniciID;
                    NormalAracTurManager.TAdd(item);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
