using Azure.Core;
using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class OgrenciTahsilatController : BaseController
    {
        OgrenciTahsilatManager ogrenciTahsilatManager = new OgrenciTahsilatManager(new EFOgrenciTahsilatRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());


        [HttpGet]
        public IActionResult Index()
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
                    int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
                    var ogrenciTahsilatList = ogrenciTahsilatManager.GetAllList(x => x.EkleyenKullaniciID == KullaniciID && x.Durum==true && x.FirmaID == FirmaID);
                        var cariList = cariManager.GetAllList(x => x.Cari_GrupID == 14);
                        var tahsilatBigileri = context.OgrenciTahsilatBilgileri.Where(x => x.Durum == true).ToList();
                        var combinedQuery = (from ot in ogrenciTahsilatList
                                             join ogrenci in cariList on ot.OgrenciId equals ogrenci.Cari_ID
                                             join ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID) on ogrenci.Cari_ID equals ogrenciModul.CariOgrenci_ID
                                             join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13 && x.Firma_ID == FirmaID) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
                                             select new OgrenciTahsilatViewModel()
                                             {
                                                 //ogrenciTahsilatBilgileri=tb,
                                                 ogrenci = ogrenci,
                                                 okul = okul,
                                                 ogrenciTahsilat = ot,
                                             });

                        List<OgrenciTahsilatViewModel> resultList = combinedQuery.ToList();

                        return View(resultList);
                    
                } 
            }
        }


        [HttpGet]
        public IActionResult OgrenciTahsilatEkle()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");

            ViewBag.Okul = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            return View();
        }
        [HttpPost]
        public IActionResult OgrenciTahsilatEkle(OgrenciTahsilat ogrenciTahsilat)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
                    int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");

                    //var ogrenci = ogrenciTahsilatManager.GetByPropertyName("OgrenciId", ogrenciTahsilat.OgrenciId.ToString());
                    var ogrenci = ogrenciTahsilatManager.GetAllList(x=> x.OgrenciId == ogrenciTahsilat.OgrenciId && x.EkleyenKullaniciID == KullaniciID && x.FirmaID == FirmaID);
                    if (ogrenci.Count != 0)
                    {
                        TempData["Msg"] = "Bu öğrencinin kaydı daha önce oluşturulmuştur";
                        TempData["Bgcolor"] = "red";

                        ViewBag.Okul = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13);
                        return View(ogrenciTahsilat);
                    }
                    var taksitliTutar = ogrenciTahsilat.AnlasilanTutar / ogrenciTahsilat.TaksitSayisi;
             
                    for (var i = 1; i <= ogrenciTahsilat.TaksitSayisi; i++)
                    {
                        var yeniOgrenciTahsilat = new OgrenciTahsilatBilgileri(); // Yeni bir OgrenciTahsilat nesnesi oluştur
                        yeniOgrenciTahsilat.OgrenciId = ogrenciTahsilat.OgrenciId;
                        yeniOgrenciTahsilat.TaksitSayisi = ogrenciTahsilat.TaksitSayisi;
                        yeniOgrenciTahsilat.Tutar = taksitliTutar;
                        yeniOgrenciTahsilat.OdemeDurumu = false;
                        yeniOgrenciTahsilat.Durum = true;

                        if (i == 1)
                        {
                            yeniOgrenciTahsilat.OdemeTarihi = ogrenciTahsilat.VadeBaslangicTarihi;
                        }
                        else
                        {
                            yeniOgrenciTahsilat.OdemeTarihi = ogrenciTahsilat.VadeBaslangicTarihi.AddMonths(i-1);
                        }
                        try
                        {
                            context.OgrenciTahsilatBilgileri.Add(yeniOgrenciTahsilat);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex){ 

                        // Hata mesajını loglama veya kullanıcıya gösterme
                        Console.WriteLine("Hata: " + ex.Message);
                        }
                }
                    ogrenciTahsilat.KalanBorcTutar = ogrenciTahsilat.AnlasilanTutar;
                    ogrenciTahsilat.Durum = true;
                    ogrenciTahsilat.EkleyenKullaniciID = KullaniciID;
                    ogrenciTahsilat.OlusturmaTarihi = DateTime.Now;
                    ogrenciTahsilat.DuzenlemeTarihi = DateTime.Now;
                    ogrenciTahsilatManager.TAdd(ogrenciTahsilat);

                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public IActionResult OgrenciTahsilatSil(int id)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var ogrenciTahsilat = ogrenciTahsilatManager.GetByID(id);

                    var ogrenci = cariManager.GetByPropertyName("Cari_ID", ogrenciTahsilat.OgrenciId.ToString());

                    if (ogrenci == null)
                    {
                        TempData["Msg"] = "Öğrenci bulunamadı.";
                        TempData["Bgcolor"] = "red";
                        return RedirectToAction("Index");
                    }


                    if (ogrenciTahsilat == null)
                    {
                        TempData["Msg"] = "İşlem başarısız.";
                        TempData["Bgcolor"] = "red";
                        return RedirectToAction("Index");
                    }


                    ogrenciTahsilatManager.TDelete(ogrenciTahsilat);
                    var ogrenciTahsilatlar = context.OgrenciTahsilatBilgileri.Where(ot => ot.OgrenciId == ogrenciTahsilat.OgrenciId);
                    context.OgrenciTahsilatBilgileri.RemoveRange(ogrenciTahsilatlar);
                    context.SaveChanges();
                    transaction.Commit();
                    TempData["Msg"] = "İşlem başarılı.";
                    TempData["Bgcolor"] = "green";
                    return RedirectToAction("Index");
                }
            }
           
        }
    }
}
