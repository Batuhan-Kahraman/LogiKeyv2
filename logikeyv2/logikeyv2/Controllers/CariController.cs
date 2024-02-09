using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class CariController : Controller
    {
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariGrupManager cariGrupManager = new CariGrupManager(new EFCariGrupRepository());

        public IActionResult Index()
        {
            List<Cari> liste = cariManager.GetAllList(x => x.Durum == true);
           
            return View(liste);

        }
        public IActionResult Ekle()
        {
            var CariGrupList = cariGrupManager.GetAllList(x => x.Durum == true);
            ViewBag.CariGrup = CariGrupList;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Cari cari)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        cari.Durum = true;
                        cari.EkleyenKullanici_ID = 1;//değişçek
                        cari.DuzenleyenKullanici_ID = 1;//değişçek
                        cari.Firma_ID = 1;//değişçek
                        cari.Olusturma_Tarihi = DateTime.UtcNow;
                        cari.Duzenleme_Tarihi = DateTime.UtcNow;
                        cariManager.TAdd(cari);
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
            return View(cari);
        }
    }
}
