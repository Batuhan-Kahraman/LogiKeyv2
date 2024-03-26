using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class AjaxController : Controller
    {
        #region tanimlamalar
        SahiplikManager sahiplikManager = new SahiplikManager(new EFSahiplikRepository());
        GrupManager grupManager = new GrupManager(new EFGrupRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        MarkaManager markaManager = new MarkaManager(new EFMarkaRepository());
        ModelManager modelManager = new ModelManager(new EFModelRepository());
        YakitTipiManager yakitTipiManager = new YakitTipiManager(new EFYakitTipiRepository());
        AkuTipiManager akuTipiManager = new AkuTipiManager(new EFAkuTipiRepository());
        SurucuPozisyonManager surucuPozisyonManager = new SurucuPozisyonManager(new EFSurucuPozisyonRepository());
        EhliyetSinifiManager ehliyetSinifiManager = new EhliyetSinifiManager(new EFEhliyetSinifiRepository());
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        KazaTuruManager kazaTuruManager = new KazaTuruManager(new EFKazaTuruRepository());
        AracManager aracManager = new AracManager(new EFAracRepository());
        MasrafTipiManager masrafTipiManager = new MasrafTipiManager(new EFMasrafTipiRepository());

        LastikTipiManager lastikTipiManager = new LastikTipiManager(new EFLastikTipiRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        AkaryakitTasimaManager  akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());
        OgrenciTahsilatManager ogrenciTahsilatManager = new OgrenciTahsilatManager(new EFOgrenciTahsilatRepository());


        NormalTasimaManager  normalTasimaManager = new NormalTasimaManager(new EFNormalTasimaRepository());
        #endregion

        [HttpGet]
        public IActionResult SahiplikListe()
        {
            List<Sahiplik> liste = sahiplikManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
       
        [HttpGet]
        public IActionResult GrupListe()
        {
            List<Grup> liste = grupManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTurListe()
        {
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTipListe(int AracTurID)
        {
            List<AracTip> liste = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == AracTurID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MarkaListe()
        {
            List<Marka> liste = markaManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult ModelListe(int MarkaID)
        {
            List<Model> liste = modelManager.GetAllList(x => x.Durum == true && x.MarkaID == MarkaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult YakitTipiListe()
        {
            List<YakitTipi> liste = yakitTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AkuTipiListe()
        {
            List<AkuTipi> liste = akuTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuPozisyonListe()
        {
            List<SurucuPozisyon> liste = surucuPozisyonManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult EhliyetSinifiListe()
        {
            List<EhliyetSinifi> liste = ehliyetSinifiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult KazaTuruListe()
        {
            List<KazaTuru> liste = kazaTuruManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuListe()
        {
            List<Kullanicilar> liste = surucuManager.GetAllList(x => x.Kullanici_Durum == 1);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult PlakaListe()
        {
            List<Arac> liste = aracManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MasrafTipiListe()
        {
            List<MasrafTipi> liste = masrafTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult LastikTipListe()
        {
            List<LastikTipi> liste = lastikTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }


        [HttpGet]
        public IActionResult IlceListe(string IlKodu)
        {
            List<AdresOzellikTanimlama> liste = adresManager.GetAllList(x => x.IL_KODU == IlKodu);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult OgrenciListe(int OkulId)
        {
            var ogrenciOkul = from ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == 1)
                              join ogrenci in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 14) on ogrenciModul.CariOgrenci_ID equals ogrenci.Cari_ID
                              join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
                              where ogrenciModul.CariOkul_ID == OkulId
                              select new OkulOgrenciModel
                              {
                                  Surucu = null,
                                  Ogrenci = ogrenci,
                                  Okul = okul,
                                  OgrenciModulu = ogrenciModul,
                              };
                List<OkulOgrenciModel> ogrenciOkulListe = ogrenciOkul.ToList();
            return Json(ogrenciOkulListe);
        }
        [HttpGet]
        public IActionResult GozSayisiBul(int AracID)
        {
            Arac liste = aracManager.GetAllList(x => x.ID == AracID).SingleOrDefault();
            return Json(liste);
        }
        [HttpGet]
        public IActionResult DorseListe()
        {
            List<Arac> liste = aracManager.GetAllList(x => x.AracTurID == 4);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult UrunUnSec(int UrunID)
        {
            TasinacakUrun urun=tasinacakUrunManager.GetByID(UrunID);
            UnListesi liste = unListesiManager.GetAllList(x => x.Un_ID == urun.Un_ID).SingleOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult TasimaKontrol(int AracID)
        {
            AkaryakitTasima liste = akaryakitTasimaManager.GetAllList(x=>x.AracID==AracID && x.Durum==true).LastOrDefault();
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult NormalTasimaKontrol(int AracID)
        {
            NormalTasima liste = normalTasimaManager.GetAllList(x=>x.AracID==AracID && x.Durum==true).LastOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult OgrenciTahsilatDetay(int id)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var ogrenciTahsilatList = ogrenciTahsilatManager.GetAllList(x=>x.OgrenciId == id);
                    var cariList = cariManager.GetAllList(x=>x.Cari_ID==id);
                    var combinedQuery = (from ot in ogrenciTahsilatList
                                         join ogrenci in cariList on ot.OgrenciId equals ogrenci.Cari_ID
                                         join ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == 1) on ogrenci.Cari_ID equals ogrenciModul.CariOgrenci_ID
                                         join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
                                         join tb in context.OgrenciTahsilatBilgileri.Where(x=>x.Durum == true && x.OgrenciId == id).ToList() on ot.OgrenciId equals tb.OgrenciId
                                         where tb.OgrenciId == id
                                         select new OgrenciTahsilatViewModel()
                                         {
                                             ogrenciTahsilatBilgileri=tb,
                                             ogrenci = ogrenci,
                                             okul = okul,
                                             ogrenciTahsilat = ot,
                                         });
                    var ogrenciBilgi = (from ot in ogrenciTahsilatList
                                         join ogrenci in cariList on ot.OgrenciId equals ogrenci.Cari_ID
                                         join ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == 1) on ogrenci.Cari_ID equals ogrenciModul.CariOgrenci_ID
                                         join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
                                         where ot.OgrenciId == id
                                         select new OgrenciTahsilatViewModel()
                                         {
                                             ogrenci = ogrenci,
                                             okul = okul,
                                             ogrenciTahsilat = ot,
                                         }).FirstOrDefault();

                    List<OgrenciTahsilatViewModel> resultList = combinedQuery.ToList();

                    return Json(new { success = true, resultList = resultList, ogrenciBilgi = ogrenciBilgi });
                }
            }
        }

        [HttpPost]
        public IActionResult OdemeIslemi(int id,int odemeValue)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var odemeTahsilat = context.OgrenciTahsilatBilgileri.FirstOrDefault(x => x.Id == id);
                    var odeme =ogrenciTahsilatManager.GetByPropertyName("OgrenciId",odemeTahsilat.OgrenciId.ToString());
                  
                    
                        if (odemeTahsilat.OdemeDurumu == true && odemeValue == 3)
                        {
                            odemeTahsilat.OdemeDurumu = false;
                            odemeTahsilat.OdemeSekli = null;
                            var taksitSayi = context.OgrenciTahsilatBilgileri.Where(x => x.Id == id && x.OdemeDurumu == false).Count();
                            odeme.KalanBorcTutar += odemeTahsilat.Tutar ;
                          

                        }
                        else if (odemeTahsilat.OdemeDurumu == false && odemeValue != 3)
                        {
                        odemeTahsilat.OdemeDurumu = true;
                            odemeTahsilat.OdemeSekli = odemeValue;
                            var taksitSayi = context.OgrenciTahsilatBilgileri.Where(x => x.Id == id && x.OdemeDurumu == true).Count();

                            odeme.KalanBorcTutar -= odemeTahsilat.Tutar;
                        }
                    if (odemeValue == 3)
                    {
                        odemeTahsilat.OdemeDurumu = false;
                        odemeTahsilat.OdemeSekli = null;
                    }
                    else
                    {
                        odemeTahsilat.OdemeDurumu = true;
                        odemeTahsilat.OdemeSekli = odemeValue;
                    }

                    ogrenciTahsilatManager.TUpdate(odeme);
                    context.OgrenciTahsilatBilgileri.Update(odemeTahsilat);
                    context.SaveChanges();
                    transaction.Commit(); 
                    return Json(new { odemeTahsilat = odemeTahsilat, odeme = odeme });
                }
            }
        }
    }

}
