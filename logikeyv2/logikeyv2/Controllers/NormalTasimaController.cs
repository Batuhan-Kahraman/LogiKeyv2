using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class NormalTasimaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
