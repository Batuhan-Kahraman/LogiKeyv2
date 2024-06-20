using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class EslestirmeController : BaseController
    {
        #region tanimlamalar
        AracManager aracManager = new AracManager(new EFAracRepository());
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AkaryakitTasimaManager akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        NormalTasimaManager normalTasimaManager = new NormalTasimaManager(new EFNormalTasimaRepository());
        #endregion

        public IActionResult Index(int ModulID=0)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            ViewBag.ModulID = ModulID;
            if (ModulID == 2)
            {

                var combinedQuery = from tasima in normalTasimaManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2))
                                    join arac in aracManager.List() on tasima.AracID equals arac.ID
                                    join aracTur in aracTurManager.List() on arac.AracTurID equals aracTur.ID
                                    join surucu1 in surucuManager.List() on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                    join dorse in aracManager.List() on tasima.DorsePlakaID equals dorse.ID into dorseJoin
                                    from dorse in dorseJoin.DefaultIfEmpty()
                                    select new TasimaModel
                                    {
                                        NormalTasima = tasima,
                                        AracTur = aracTur,
                                        Arac = arac,
                                        Dorse = dorse,
                                        Surucu = surucu1
                                    };
                List<TasimaModel> combinedList = combinedQuery.ToList();


                return View(combinedList);
            }
            else if (ModulID == 3)
            {

                var combinedQuery = from tasima in akaryakitTasimaManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2))
                                    join arac in aracManager.List() on tasima.AracID equals arac.ID
                                    join aracTur in aracTurManager.List() on arac.AracTurID equals aracTur.ID
                                    join dorse in aracManager.List() on tasima.DorsePlakaID equals dorse.ID
                                    join surucu1 in surucuManager.List() on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                    select new TasimaModel
                                    {
                                        Tasima = tasima,
                                        AracTur = aracTur,
                                        Arac = arac,
                                        Dorse = dorse,
                                        Surucu = surucu1
                                    };



                List<TasimaModel> combinedList = combinedQuery.ToList();


                return View(combinedList);
            }
            else
                return RedirectToAction("Index", "Home");
        }


    }
}
