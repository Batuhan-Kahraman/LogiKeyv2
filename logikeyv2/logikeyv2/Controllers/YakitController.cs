using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class YakitController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
