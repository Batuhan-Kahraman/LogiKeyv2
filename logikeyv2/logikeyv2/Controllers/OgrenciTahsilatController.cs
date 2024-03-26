using Azure.Core;
using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class OgrenciTahsilatController : Controller
    {
        OgrenciTahsilatManager ogrenciTahsilatManager = new OgrenciTahsilatManager(new EFOgrenciTahsilatRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());


        [HttpGet]
        public IActionResult Index(string ogrenciDeger)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (ogrenciDeger != null)
                    {
                        var ogrenciTahsilatList = ogrenciTahsilatManager.GetAllList(x => x.EkleyenKullanici_ID == 1 && x.Durum == true);
                        var cariList = cariManager.GetAllList(x => x.Cari_GrupID == 14 && (x.Cari_TCNO_VergiNo == ogrenciDeger || x.Cari_Unvan.ToLower() == ogrenciDeger.ToLower()));
                        var combinedQuery = (from ot in ogrenciTahsilatList
                                             join ogrenci in cariList on ot.OgrenciId equals ogrenci.Cari_ID
                                             join ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == 1) on ogrenci.Cari_ID equals ogrenciModul.CariOgrenci_ID
                                             join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
                                             select new OgrenciTahsilatViewModel()
                                             {
                                                 ogrenci = ogrenci,
                                                 okul = okul,
                                                 ogrenciTahsilat = ot,
                                             });

                        List<OgrenciTahsilatViewModel> resultList = combinedQuery.ToList();

                        return View(resultList);
                    }
                    else
                    {
                        var ogrenciTahsilatList = ogrenciTahsilatManager.GetAllList(x => x.EkleyenKullanici_ID == 1);
                        var cariList = cariManager.GetAllList(x => x.Cari_GrupID == 14);
                        var tahsilatBigileri = context.OgrenciTahsilatBilgileri.Where(x => x.Durum == true).ToList();
                        var combinedQuery = (from ot in ogrenciTahsilatList
                                             join ogrenci in cariList on ot.OgrenciId equals ogrenci.Cari_ID
                                             join ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == 1) on ogrenci.Cari_ID equals ogrenciModul.CariOgrenci_ID
                                             join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
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
        }


        [HttpGet]
        public IActionResult OgrenciTahsilatEkle()
        {
            ViewBag.Okul = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13);
            return View();
        }
        [HttpPost]
        public IActionResult OgrenciTahsilatEkle(OgrenciTahsilat ogrenciTahsilat)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var ogrenci = ogrenciTahsilatManager.GetByPropertyName("OgrenciId", ogrenciTahsilat.OgrenciId.ToString());
                    if (ogrenci != null)
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
                            yeniOgrenciTahsilat.OdemeTarihi = ogrenciTahsilat.VadeBaslangicTarihi.AddMonths(i);
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
                    ogrenciTahsilat.EkleyenKullanici_ID = 1;
                    ogrenciTahsilat.OlusturmaTarihi = DateTime.Now;
                    ogrenciTahsilat.DuzenlemeTarihi = DateTime.Now;
                    ogrenciTahsilatManager.TAdd(ogrenciTahsilat);

                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public IActionResult OgrenciTahsilatSil(string tcNo)
        {
            var ogrenci = cariManager.GetByPropertyName("Cari_TCNO_VergiNo", tcNo);

            if (ogrenci == null)
            {
                TempData["Msg"] = "Öğrenci bulunamadı.";
                TempData["Bgcolor"] = "red";
                return RedirectToAction("Index");
            }

            var ogrenciTahsilat = ogrenciTahsilatManager.GetAllList(x => x.OgrenciId == ogrenci.Cari_ID);

            if (ogrenciTahsilat == null)
            {
                TempData["Msg"] = "İşlem başarısız.";
                TempData["Bgcolor"] = "red";
                return RedirectToAction("Index");
            }

            foreach (var item in ogrenciTahsilat)
            {
                item.Durum = false;
                ogrenciTahsilatManager.TUpdate(item);
            }

            TempData["Msg"] = "İşlem başarılı.";
            TempData["Bgcolor"] = "green";
            return RedirectToAction("Index");
        }
    }
}
