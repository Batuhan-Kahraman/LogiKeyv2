using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace logikeyv2.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Session'dan gerekli verileri al
            var eposta = HttpContext.Session.GetString("Eposta");
            var moduller = HttpContext.Session.GetString("Moduller");
            var KullaniciGrup_ID = HttpContext.Session.GetString("KullaniciGrup_ID");
            var MenuModulID = HttpContext.Session.GetString("MenuModulID");


            var firmaAdi = HttpContext.Session.GetString("Firma");

            // ViewBag aracılığıyla View'e aktar
            ViewBag.Eposta = eposta;
            ViewBag.Moduller = moduller;
            ViewBag.KullaniciGrup_ID = KullaniciGrup_ID;

            ViewBag.MenuModulID = MenuModulID;

            VersiyonManager v = new VersiyonManager(new EFVersiyonRepository());
            var versiyon=v.GetAllList(x=>x.Durum==true).OrderByDescending(v => v.OlusturmaTarihi).FirstOrDefault();
            if (versiyon != null) { ViewBag.Versiyon = "1."+versiyon.ID; }
            else { ViewBag.Versiyon = "1"; }

            ViewBag.FirmaAdi = firmaAdi;

        }
    }


}
