using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class TasimaController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        SurucuManager surucuManager = new SurucuManager(new EFSurucuRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        TasimaManager tasimaManager = new TasimaManager(new EFTasimaRepository());
        UnListesiManager unListesiManager= new UnListesiManager(new EFUnListesiRepository());
        public IActionResult Index()
        {
            List<Tasima> liste = tasimaManager.GetAllList(x => x.Durum == 1);
            return View();
        }
    }
}
