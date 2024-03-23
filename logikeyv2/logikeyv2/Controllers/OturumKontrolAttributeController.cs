using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace logikeyv2.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class OturumKontrolAttributeController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Oturumun belirli bir anahtarı içerip içermediğini kontrol et (Anahtar yerine "SizinAnahtarınız" yazınız.)
            if (context.HttpContext.Session.GetString("KullaniciID") == null )
            {
                // Oturum yok veya beklenen veriyi içermiyor
                // Yönlendirme yapabilir, hata gösterebilir veya başka bir işlem yapabilirsiniz
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
          

                base.OnActionExecuting(context);
        }
    }
}
