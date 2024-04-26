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
    public class AjaxController : BaseController
    {
        #region tanimlamalar
        SahiplikManager sahiplikManager = new SahiplikManager(new EFSahiplikRepository());
        GrupManager grupManager = new GrupManager(new EFGrupRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AkaryakitAracTurManager akaryakitAracTurManager = new AkaryakitAracTurManager(new EFAkaryakitAracTurRepository());
        NormalAracTurManager normalAracTurManager = new NormalAracTurManager(new EFNormalAracTurRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        MarkaManager markaManager = new MarkaManager(new EFMarkaRepository());
        ModelManager modelManager = new ModelManager(new EFModelRepository());
        YakitTipiManager yakitTipiManager = new YakitTipiManager(new EFYakitTipiRepository());
        YakitAltTipiManager yakitAltTipiManager = new YakitAltTipiManager(new EFYakitAltTipiRepository());
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
        AkaryakitTasimaManager akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());
        OgrenciTahsilatManager ogrenciTahsilatManager = new OgrenciTahsilatManager(new EFOgrenciTahsilatRepository());


        NormalTasimaManager normalTasimaManager = new NormalTasimaManager(new EFNormalTasimaRepository());
        UyariTipManager uyariTipManager = new UyariTipManager(new EFUyariTipRepository());

        ServisBakimDurumManager servisBakimDurumManager =new ServisBakimDurumManager(new EFServisBakimDurumRepository());
        ServisBakimTuruManager servisBakimTuruManager =new ServisBakimTuruManager(new EFServisBakimTuruRepository());
        BakimYeriManager bakimYeriManager =new BakimYeriManager(new EFBakimYeriRepository());
        StokManager stokManager =new StokManager(new EFStokRepository());
        GiderTipManager giderTipManager =new GiderTipManager(new EFGiderTipRepository());
        GiderAltTipManager giderAltTipManager =new GiderAltTipManager(new EFGiderAltTipRepository());
        IstasyonManager istasyonManager =new IstasyonManager(new EFIstasyonRepository());
        TankManager tankManager =new TankManager(new EFTankRepository());

        #endregion

        [HttpGet]
        public IActionResult TankListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Tank> liste = tankManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult IstasyonListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Istasyon> liste = istasyonManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SahiplikListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Sahiplik> liste = sahiplikManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult ServisBakimDurumListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<ServisBakimDurum> liste = servisBakimDurumManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult ServisBakimTuruListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<ServisBakimTuru> liste = servisBakimTuruManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult ServisBakimYeriListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<BakimYeri> liste = bakimYeriManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult StokListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Stok> liste = stokManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }


        [HttpGet]
        public IActionResult TedarikciListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Kullanicilar> liste = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID == 8);
            return Json(liste);
        }

        [HttpGet]
        public IActionResult GiderTipListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<GiderTip> liste = giderTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult GiderAltTipListe(int GiderTipID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<GiderAltTip> liste = giderAltTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID && x.GiderTipID==GiderTipID);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult StokSec(int StokID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            Stok liste = stokManager.GetByID(StokID);
            return Json(liste);
        }


        [HttpGet]
        public IActionResult UyariTipListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<UyariTip> liste = uyariTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }

        [HttpGet]
        public IActionResult GrupListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Grup> liste = grupManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTurListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");

            int? MenuModulID = HttpContext.Session.GetInt32("MenuModulID");

            if (MenuModulID.HasValue)
            {
                if (MenuModulID == 3)
                {
                    var liste = from akaryakitAracTur in akaryakitAracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                                join aracTur in aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID) on akaryakitAracTur.TurID equals aracTur.ID
                                select new { AracTur = aracTur };

                    return Json(liste);

                }
                else if (MenuModulID == 2)
                {
                    var liste = from normalAkaryakitTur in normalAracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                                join aracTur in aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID) on normalAkaryakitTur.TurID equals aracTur.ID
                                select new { AracTur = aracTur };
                    return Json(liste);
                }
                else
                {

                    List<AracTur> liste = (from aracTur in aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                                           select aracTur).ToList();
                    
                    return Json(liste);
                }
            }
            else
            {
                List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
                return Json(liste);

            }
        }
        [HttpGet]
        public IActionResult AracTipListe(int AracTurID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AracTip> liste = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == AracTurID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MarkaListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Marka> liste = markaManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult ModelListe(int MarkaID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Model> liste = modelManager.GetAllList(x => x.Durum == true && x.MarkaID == MarkaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult YakitTipiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<YakitTipi> liste = yakitTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult YakitAltTipiListe(int ID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<YakitAltTipi> liste = yakitAltTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.YakitTipiID==ID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AkuTipiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AkuTipi> liste = akuTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuPozisyonListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<SurucuPozisyon> liste = surucuPozisyonManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult EhliyetSinifiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<EhliyetSinifi> liste = ehliyetSinifiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult KazaTuruListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<KazaTuru> liste = kazaTuruManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Kullanicilar> liste = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID == 2);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult TedarikciCariListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Cari> liste = cariManager.GetAllList(x => x.Durum == 1 &&  x.Cari_GrupID == 3);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult BakimPersonelListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Kullanicilar> liste = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID == 7);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult PlakaListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> liste = aracManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MasrafTipiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<MasrafTipi> liste = masrafTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult LastikTipListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<LastikTipi> liste = lastikTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }


        [HttpGet]
        public IActionResult IlListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var adres = adresManager.List();
            var liste =adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();
            return Json(liste);
        }
        [HttpGet]
        public IActionResult IlceListe(string IlKodu)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
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
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            Arac liste = aracManager.GetAllList(x => x.ID == AracID && x.FirmaID == FirmaID).SingleOrDefault();
            return Json(liste);
        }
        [HttpGet]
        public IActionResult DorseListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> liste = aracManager.GetAllList(x => x.AracTurID == 4 && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult CekiciListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> liste = aracManager.GetAllList(x => x.AracTurID == 1 && x.FirmaID == FirmaID);
            return Json(liste);
        }

        [HttpGet]
        public IActionResult UrunUnSec(int UrunID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            TasinacakUrun urun = tasinacakUrunManager.GetByID(UrunID);
            UnListesi liste = unListesiManager.GetAllList(x => x.Un_ID == urun.Un_ID && x.Firma_ID == FirmaID).SingleOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult AkaryakitTasimaKontrol(int AracID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            AkaryakitTasima liste = akaryakitTasimaManager.GetAllList(x => x.AracID == AracID && x.Durum == true && x.FirmaID == FirmaID).LastOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult NormalTasimaKontrol(int AracID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            NormalTasima liste = normalTasimaManager.GetAllList(x => x.AracID == AracID && x.Durum == true && x.FirmaID == FirmaID).LastOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult OgrenciTahsilatDetay(int id)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var ogrenciTahsilatList = ogrenciTahsilatManager.GetAllList(x => x.OgrenciId == id);
                    var cariList = cariManager.GetAllList(x => x.Cari_ID == id);
                    var combinedQuery = (from ot in ogrenciTahsilatList
                                         join ogrenci in cariList on ot.OgrenciId equals ogrenci.Cari_ID
                                         join ogrenciModul in ogrenciModuluManager.GetAllList(x => x.Durum == 1) on ogrenci.Cari_ID equals ogrenciModul.CariOgrenci_ID
                                         join okul in cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 13) on ogrenciModul.CariOkul_ID equals okul.Cari_ID
                                         join tb in context.OgrenciTahsilatBilgileri.Where(x => x.Durum == true && x.OgrenciId == id).ToList() on ot.OgrenciId equals tb.OgrenciId
                                         where tb.OgrenciId == id
                                         select new OgrenciTahsilatViewModel()
                                         {
                                             ogrenciTahsilatBilgileri = tb,
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
        public IActionResult OdemeIslemi(int id, int odemeValue)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var odemeTahsilat = context.OgrenciTahsilatBilgileri.FirstOrDefault(x => x.Id == id);
                    var odeme = ogrenciTahsilatManager.GetByPropertyName("OgrenciId", odemeTahsilat.OgrenciId.ToString());


                    if (odemeTahsilat.OdemeDurumu == true && odemeValue == 3)
                    {
                        odemeTahsilat.OdemeDurumu = false;
                        odemeTahsilat.OdemeSekli = null;
                        var taksitSayi = context.OgrenciTahsilatBilgileri.Where(x => x.Id == id && x.OdemeDurumu == false).Count();
                        odeme.KalanBorcTutar += odemeTahsilat.Tutar;


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
        [HttpGet]
        public IActionResult OgrenciTaksitBilgileri(int id)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var odemeTahsilat = context.OgrenciTahsilatBilgileri.FirstOrDefault(x => x.Id == id);

                    return Json(odemeTahsilat);
                }
            }
        }
    }

}
