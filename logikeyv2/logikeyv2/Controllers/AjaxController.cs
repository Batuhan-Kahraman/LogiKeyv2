using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class AjaxController : Controller
    {
        #region tanimlamalar
        SahiplikManager sahiplikManager=new SahiplikManager(new EFSahiplikRepository());
        GrupManager grupManager = new GrupManager(new EFGrupRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        MarkaManager markaManager = new MarkaManager(new EFMarkaRepository());
        ModelManager modelManager = new ModelManager(new EFModelRepository());
        YakitTipiManager yakitTipiManager = new YakitTipiManager(new EFYakitTipiRepository());
        AkuTipiManager akuTipiManager = new AkuTipiManager(new EFAkuTipiRepository());
        #endregion
        [HttpGet]
        public IActionResult SahiplikListe()
        {
            List<Sahiplik> liste = sahiplikManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult GrupListe()
        {
            List<Grup> liste = grupManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTurListe()
        {
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTipListe(int AracTurID)
        {
            List<AracTip> liste = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID==AracTurID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MarkaListe()
        {
            List<Marka> liste = markaManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult ModelListe(int MarkaID)
        {
            List<Model> liste = modelManager.GetAllList(x => x.Durum == true && x.MarkaID == MarkaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult YakitTipiListe()
        {
            List<YakitTipi> liste = yakitTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AkuTipiListe()
        {
            List<AkuTipi> liste = akuTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
    }
}
