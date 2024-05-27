using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var firma = firmaManager.GetByID(FirmaID);
            var giris = Task.Run(async () => await EFaturaHelper.Login(firma.Firma_EFatura_KullaniciAdi, firma.Firma_EFatura_Sifre)).Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
