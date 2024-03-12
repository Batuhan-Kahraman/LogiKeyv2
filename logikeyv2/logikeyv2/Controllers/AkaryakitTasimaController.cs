using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logikeyv2.Controllers
{
    public class AkaryakitTasimaController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        TasimaManager tasimaManager = new TasimaManager(new EFTasimaRepository());
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariUcretlendirmeManager ucretlendirme = new CariUcretlendirmeManager(new EFCariUcretlendirmeRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        BirimlerManager birimlerManager = new BirimlerManager(new EFBirimlerRepository());
        TasimaTipiManager tasimaTipiManager = new TasimaTipiManager(new EFTasimaTipiRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        AkaryakitTasimaManager akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        AkaryakitTasimaDetayManager akaryakitTasimaDetayManager = new AkaryakitTasimaDetayManager(new EFAkaryakitTasimaDetayRepository());
        AkaryakitTasimaDetayUrunManager akaryakitTasimaDetayUrunManager = new AkaryakitTasimaDetayUrunManager(new EFAkaryakitTasimaDetayUrunRepository());
        AkaryakitFaturaManager akaryakitFaturaManager = new AkaryakitFaturaManager(new EFAkaryakitFaturaRepository());




        public IActionResult Index()
        {


            var combinedQuery = from tasima in akaryakitTasimaManager.GetAllList(x => x.Durum == true)
                                join arac in aracManager.GetAllList((y => y.Durum == true)) on tasima.AracID equals arac.ID
                                join surucu1 in surucuManager.GetAllList((y => y.Kullanici_Durum == 1)) on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                join tasimaTipi in tasimaTipiManager.GetAllList((y => y.Durum == true)) on tasima.TasimaTipiID equals tasimaTipi.ID
                                join aracTur in aracTurManager.GetAllList((y => y.Durum == true)) on tasima.AracTurID equals aracTur.ID
                                select new TasimaModel { Tasima = tasima, Arac = arac, Surucu = surucu1, TasimaTip = tasimaTipi, AracTur = aracTur };

            List<TasimaModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }
        public IActionResult TasimaEkle()
        {
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true);
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true);

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1);
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8);
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6);

            ViewBag.Araclar = aracListesi;
            ViewBag.CekiciPlaka = cekiciPlaka;
            ViewBag.DorsePlaka = dorsePlaka;
            ViewBag.DorseListe = dorseListesi;
            ViewBag.Suruculer = surucuListesi;
            ViewBag.TasinacakUrun = tasinacakUrun;
            ViewBag.UnListesi = UnListesi;
            ViewBag.CariListesi = CariListesi;
            ViewBag.aracTip = aracTip;
            ViewBag.aracTur = aracTur;
            ViewBag.birimler = birimler;
            ViewBag.tasimaTipi = tasimaTipi;
            ViewBag.AliciListesi = AliciListesi;
            ViewBag.GondericiListesi = GondericiListesi;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;


            return View();
        }

        [HttpPost]
        public IActionResult TasimaEkle(AkaryakitTasima akaryakitTasima, IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        akaryakitTasima.Goz1UrunID = string.IsNullOrWhiteSpace(form["Goz1UrunID"]) ? 0 : int.Parse(form["Goz1UrunID"]);
                        akaryakitTasima.Goz2UrunID = string.IsNullOrWhiteSpace(form["Goz2UrunID"]) ? 0 : int.Parse(form["Goz2UrunID"]);
                        akaryakitTasima.Goz3UrunID = string.IsNullOrWhiteSpace(form["Goz3UrunID"]) ? 0 : int.Parse(form["Goz3UrunID"]);
                        akaryakitTasima.Goz4UrunID = string.IsNullOrWhiteSpace(form["Goz4UrunID"]) ? 0 : int.Parse(form["Goz4UrunID"]);
                        akaryakitTasima.Goz5UrunID = string.IsNullOrWhiteSpace(form["Goz5UrunID"]) ? 0 : int.Parse(form["Goz5UrunID"]);
                        akaryakitTasima.Goz6UrunID = string.IsNullOrWhiteSpace(form["Goz6UrunID"]) ? 0 : int.Parse(form["Goz6UrunID"]);

                        akaryakitTasima.Durum = true;
                        akaryakitTasima.FirmaID = 1;//değişçek
                        akaryakitTasima.OlusturmaTarihi = DateTime.Now;
                        akaryakitTasima.DuzenlemeTarihi = DateTime.Now;
                        akaryakitTasima.OlusturanId = 1;//değişcek
                        akaryakitTasima.DuzenleyenID = 1;//değişcek
                        akaryakitTasimaManager.TAdd(akaryakitTasima);

                       


                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";

                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult TasimaDuzenle(int ID)
        {
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true);
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true);

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1);
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8);
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6);

            ViewBag.Araclar = aracListesi;
            ViewBag.CekiciPlaka = cekiciPlaka;
            ViewBag.DorsePlaka = dorsePlaka;
            ViewBag.DorseListe = dorseListesi;
            ViewBag.Suruculer = surucuListesi;
            ViewBag.TasinacakUrun = tasinacakUrun;
            ViewBag.UnListesi = UnListesi;
            ViewBag.CariListesi = CariListesi;
            ViewBag.aracTip = aracTip;
            ViewBag.aracTur = aracTur;
            ViewBag.birimler = birimler;
            ViewBag.tasimaTipi = tasimaTipi;
            ViewBag.AliciListesi = AliciListesi;
            ViewBag.GondericiListesi = GondericiListesi;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;


            AkaryakitTasima tasima = akaryakitTasimaManager.GetByID(ID);

            return View(tasima);
        }

        [HttpPost]
        public IActionResult TasimaDuzenle(AkaryakitTasima kayit, IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        AkaryakitTasima akaryakitTasima = akaryakitTasimaManager.GetByID(kayit.ID);
                        akaryakitTasima.ToplamYuklenenMiktar = kayit.ToplamYuklenenMiktar;
                        akaryakitTasima.AracTurID = kayit.AracTurID;
                        akaryakitTasima.Kullanici1ID = kayit.Kullanici1ID;
                        akaryakitTasima.Kullanici2ID = kayit.Kullanici2ID;
                        akaryakitTasima.Kullanici3ID = kayit.Kullanici3ID;
                        akaryakitTasima.TasimaTipiID = kayit.TasimaTipiID;
                        akaryakitTasima.AracID = kayit.AracID;
                        akaryakitTasima.CekiciPlakaID = kayit.CekiciPlakaID;
                        akaryakitTasima.DorseID = kayit.DorseID;
                        akaryakitTasima.DorsePlakaID = kayit.DorsePlakaID;
                        akaryakitTasima.DuzenlemeTarihi = DateTime.Now;
                        akaryakitTasima.DuzenleyenID = 1;

                        akaryakitTasima.Goz1UrunID = string.IsNullOrWhiteSpace(form["Goz1UrunID"]) ? 0 : int.Parse(form["Goz1UrunID"]);
                        akaryakitTasima.Goz2UrunID = string.IsNullOrWhiteSpace(form["Goz2UrunID"]) ? 0 : int.Parse(form["Goz2UrunID"]);
                        akaryakitTasima.Goz3UrunID = string.IsNullOrWhiteSpace(form["Goz3UrunID"]) ? 0 : int.Parse(form["Goz3UrunID"]);
                        akaryakitTasima.Goz4UrunID = string.IsNullOrWhiteSpace(form["Goz4UrunID"]) ? 0 : int.Parse(form["Goz4UrunID"]);
                        akaryakitTasima.Goz5UrunID = string.IsNullOrWhiteSpace(form["Goz5UrunID"]) ? 0 : int.Parse(form["Goz5UrunID"]);
                        akaryakitTasima.Goz6UrunID = string.IsNullOrWhiteSpace(form["Goz6UrunID"]) ? 0 : int.Parse(form["Goz6UrunID"]);

                        akaryakitTasimaManager.TUpdate(akaryakitTasima);

                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";

                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Sil(IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        AkaryakitTasima item = akaryakitTasimaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        akaryakitTasimaManager.TUpdate(item);

                        List<AkaryakitTasimaDetay> itemDetay = akaryakitTasimaDetayManager.GetAllList(x => x.AkaryakitTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetay)
                        {
                            x.Durum = false;
                            akaryakitTasimaDetayManager.TUpdate(x);

                        }
                        List<AkaryakitTasimaDetayUrun> itemDetayUrun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.AkaryakitTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetayUrun)
                        {
                            x.Durum = false;
                            akaryakitTasimaDetayUrunManager.TUpdate(x);

                        }
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }

        }

        public IActionResult FaturaGoster(int ID)
        {
            AkaryakitTasima tasima = akaryakitTasimaManager.GetByID(ID);
            List<AkaryakitTasimaDetay> tasimaDetay = akaryakitTasimaDetayManager.GetAllList(x => x.AkaryakitTasimaID == ID);
            List<AkaryakitTasimaDetayUrun> tasimaDetayUrun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.AkaryakitTasimaID == ID);

            List<AkaryakitFatura> faturaList = akaryakitFaturaManager.GetAllList(x => x.AkaryakitTasimaID == ID);

            ViewBag.Tasima = tasima;
            ViewBag.TasimaDetay = tasimaDetay;
            ViewBag.TasimaDetayUrun = tasimaDetayUrun;
            ViewBag.faturaList = faturaList;


            return View();

            /*










            AkaryakitFaturaViewModel viewModel = new AkaryakitFaturaViewModel
            {
                Tasima = tasima,
                FaturaList = faturaList,
                DetayList = new List<AkaryakitTasimaDetay>(),
                UrunList = new List<AkaryakitTasimaDetayUrun>(),
                FaturaKesenList = new List<Cari>(),
                FaturaKesilenList = new List<Cari>()
            };

            foreach (var fatura in faturaList)
            {
                AkaryakitTasimaDetay detay = akaryakitTasimaDetayManager.GetAllList(x => x.ID == fatura.AkaryakitTasimaDetayID).SingleOrDefault();
                viewModel.DetayList.Add(detay);

                var urunListe = fatura.AkaryakitTasimaDetayUrunID.Split(';');
                foreach (var urunBilgi in urunListe)
                {
                    AkaryakitTasimaDetayUrun urun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.ID == int.Parse(urunBilgi)).SingleOrDefault();
                    viewModel.UrunList.Add(urun);
                }

                var faturaKesenListe = fatura.FaturaKesenID.Split(';');
                foreach (var faturaKesenBilgi in faturaKesenListe)
                {
                    Cari faturaKesen = cariManager.GetAllList(x => x.Cari_ID == int.Parse(faturaKesenBilgi)).SingleOrDefault();
                    viewModel.FaturaKesenList.Add(faturaKesen);
                }

                var faturaKesilenListe = fatura.FaturaKesilenID.Split(';');
                foreach (var faturaKesilenBilgi in faturaKesilenListe)
                {
                    Cari faturaKesilen = cariManager.GetAllList(x => x.Cari_ID == int.Parse(faturaKesilenBilgi)).SingleOrDefault();
                    viewModel.FaturaKesilenList.Add(faturaKesilen);
                }
            }

            return View(viewModel);



@foreach (var fatura in Model.FaturaList)
{

    <div class="d-flex justify-content-between align-items-end mb-4">
        <h2 class="mb-0">@sayac .Fatura</h2>
    </div>
    <div class="bg-body dark__bg-gray-1100 p-4 mb-4 rounded-2">
        <div class="row g-4">
            <div class="col-12 col-lg-3">
                <div class="row g-4 g-lg-2">
                    <div class="col-12 col-sm-6 col-lg-12">
                        <div class="row align-items-center g-0">
                            <div class="col-auto col-lg-6 col-xl-5">
                                <h6 class="mb-0 me-3">Fatura No :</h6>
                            </div>
                            <div class="col-auto col-lg-6 col-xl-7">
                                <p class="fs-9 text-body-secondary fw-semibold mb-0">@fatura.FaturaNo</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 col-lg-12">
                        <div class="row align-items-center g-0">
                            <div class="col-auto col-lg-6 col-xl-5">
                                <h6 class="me-3">Fatura Tarihi :</h6>
                            </div>
                            <div class="col-auto col-lg-6 col-xl-7">
                                <p class="fs-9 text-body-secondary fw-semibold mb-0">@fatura.DuzenlemeTarihi</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="px-0">
        <div class="table-responsive scrollbar">
            <table class="table fs-9 text-body mb-0">
                <thead class="bg-body-secondary">
                    <tr>
                        <th scope="col" style="min-width: 60px;">Gönderici</th>
                        <th scope="col" style="min-width: 60px;">Alıcı</th>
                        <th scope="col" style="min-width: 60px;">Fatura Kesen</th>
                        <th scope="col" style="min-width: 60px;">Fatura Kesilen</th>
                        <th scope="col" style="min-width: 60px;">Ürün Adı</th>
                        <th scope="col" style="min-width: 60px;">Yüükleme Miktarı</th>
                    </tr>
                </thead>
                <tbody>
                    @{
                        var detayFatura = Model.DetayList.SingleOrDefault(d => d.ID == fatura.AkaryakitTasimaDetayID);
                        var urunFatura = Model.UrunList
                        .Where(u =>
                        fatura.AkaryakitTasimaDetayUrunID.Split(';').Select(int.Parse).Contains(u.ID)
                        )
                        .ToList();
                        var faturaKesen = Model.FaturaKesenList
                        .Where(u => fatura.FaturaKesenID.Split(';').Contains(u.Cari_Unvan));

                        <tr>
                            <td colspan="6">
                                @detayFatura
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                @urunFatura
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                @faturaKesen
                            </td>
                        </tr>

                        foreach (var detay in Model.DetayList)
                        {
                            <tr>
                                <td>@detay.GondericiID</td>
                                <td>@detay.AliciID</td>
                                <td>@Model.FaturaKesenList[0].Cari_Unvan</td>
                                <td>@Model.FaturaKesilenList[0].Cari_Unvan</td>
                                <td>@Model.UrunList[0].UrunID</td>
                                <td>@Model.UrunList[0].YuklemeMiktari</td>
                            </tr>
                        }
                    }
                </tbody>
            </table>
        </div>
    </div>
}


             */

        }
    }
}
