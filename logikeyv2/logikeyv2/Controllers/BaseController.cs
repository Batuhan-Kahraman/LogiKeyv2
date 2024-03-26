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

            // ViewBag aracılığıyla View'e aktar
            ViewBag.Eposta = eposta;
            ViewBag.Moduller = moduller;
        }
    }


}
