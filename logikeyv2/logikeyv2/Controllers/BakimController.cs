using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class BakimController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
