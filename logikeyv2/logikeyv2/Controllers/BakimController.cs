using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DataAccessLayer.Concrate;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class BakimController : BaseController
    {
        BakimManager bakimManager = new BakimManager(new EFBakimRepository());
        BakimStokManager bakimStokManager = new BakimStokManager(new EFBakimStokRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Bakim> liste = bakimManager.GetAllList(x => x.Durum == true &&( x.FirmaID == FirmaID || x.FirmaID == -2));
            return View(liste);
        }
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Bakim bakim, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");

            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        bakim.Durum = true;
                        bakim.FirmaID = FirmaID;
                        bakim.OlusturmaTarihi = DateTime.Now;
                        bakim.DuzenlemeTarihi = DateTime.Now;
                        bakim.OlusturanId = KullaniciID;
                        bakim.DuzenleyenID = KullaniciID;
                        bakimManager.TAdd(bakim);

                        int kayitSayisi = int.Parse(form["StokSayisi"]);
                        for (var i = 1; i <= kayitSayisi; i++)
                        {
                            BakimStok bakimStok = new BakimStok();
                            bakimStok.BakimID = bakim.ID;
                            bakimStok.TedarikciID = int.Parse(form["TedarikciID" + i + "[]"]);
                            bakimStok.Miktar = int.Parse(form["Miktar" + i + "[]"]);
                            bakimStok.BirimFiyat = int.Parse(form["BirimFiyat" + i + "[]"]);
                            bakimStok.StokID = int.Parse(form["StokID" + i + "[]"]);
                            bakimStok.FisNo = form["FisNo" + i + "[]"];
                            bakimStok.GiderTipID = int.Parse(form["GiderTipID" + i + "[]"]);
                            bakimStok.GiderAltTipID = int.Parse(form["GiderAltTipID" + i + "[]"]);


                            bakimStok.Durum = true;
                            bakimStok.FirmaID = FirmaID;
                            bakimStok.OlusturmaTarihi = DateTime.Now;
                            bakimStok.DuzenlemeTarihi = DateTime.Now;
                            bakimStok.OlusturanId = KullaniciID;
                            bakimStok.DuzenleyenID = KullaniciID;
                            bakimStokManager.TAdd(bakimStok);
                        }

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

        public IActionResult Duzenle(int ID)
        {
            Bakim bakim = bakimManager.GetByID(ID);
            List<BakimStok> bakimStok = bakimStokManager.GetAllList(x => x.Durum == true && x.BakimID == ID);
            ViewBag.BakimStok = bakimStok;
            return View(bakim);
        }

        [HttpPost]
        public IActionResult Duzenle(Bakim bakim, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Bakim item = bakimManager.GetByID(bakim.ID);
                        item.CekiciID = bakim.CekiciID;
                        item.DorseID = bakim.DorseID;
                        item.Aciklama = bakim.Aciklama;
                        item.AracKm = bakim.AracKm;
                        item.ServisBakimDurumID = bakim.ServisBakimDurumID;
                        item.ServisBakimTurID = bakim.ServisBakimTurID;
                        item.TarihSaat = bakim.TarihSaat;
                        item.ArizaNedeni = bakim.ArizaNedeni;
                        item.BakimYeri = bakim.BakimYeri;
                        item.PersonelID = bakim.PersonelID;
                        item.YapilmasiGereken = bakim.YapilmasiGereken;
                        item.SurucuID = bakim.SurucuID;

                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        bakimManager.TUpdate(item);

                        int kayitSayisi = int.Parse(form["StokSayisi"]);
                        for (var i = 1; i <= kayitSayisi; i++)
                        {
                            BakimStok bakimStok;
                            try
                            {
                                int BakimStokID = int.Parse(form["BakimStokID" + i + "[]"]);
                                bakimStok = bakimStokManager.GetByID(BakimStokID);
                                bakimStok.BakimID = bakim.ID;
                                bakimStok.TedarikciID = int.Parse(form["TedarikciID" + i + "[]"]);
                                bakimStok.Miktar = int.Parse(form["Miktar" + i + "[]"]);
                                bakimStok.BirimFiyat = int.Parse(form["BirimFiyat" + i + "[]"]);
                                bakimStok.StokID = int.Parse(form["StokID" + i + "[]"]);
                                bakimStok.FisNo = form["FisNo" + i + "[]"];
                                bakimStok.GiderTipID = int.Parse(form["GiderTipID" + i + "[]"]);
                                bakimStok.GiderAltTipID = int.Parse(form["GiderAltTipID" + i + "[]"]);


                                bakimStok.Durum = true;
                                bakimStok.FirmaID = FirmaID;
                                bakimStok.DuzenlemeTarihi = DateTime.Now;
                                bakimStok.DuzenleyenID = KullaniciID;
                                bakimStokManager.TUpdate(bakimStok);
                            }
                            catch
                            {
                                bakimStok = new BakimStok();
                                bakimStok.BakimID = bakim.ID;
                                bakimStok.TedarikciID = int.Parse(form["TedarikciID" + i + "[]"]);
                                bakimStok.Miktar = int.Parse(form["Miktar" + i + "[]"]);
                                bakimStok.BirimFiyat = int.Parse(form["BirimFiyat" + i + "[]"]);
                                bakimStok.StokID = int.Parse(form["StokID" + i + "[]"]);
                                bakimStok.FisNo = form["FisNo" + i + "[]"];
                                bakimStok.GiderTipID = int.Parse(form["GiderTipID" + i + "[]"]);
                                bakimStok.GiderAltTipID = int.Parse(form["GiderAltTipID" + i + "[]"]);


                                bakimStok.Durum = true;
                                bakimStok.FirmaID = FirmaID;
                                bakimStok.OlusturmaTarihi = DateTime.Now;
                                bakimStok.DuzenlemeTarihi = DateTime.Now;
                                bakimStok.OlusturanId = KullaniciID;
                                bakimStok.DuzenleyenID = KullaniciID;
                                bakimStokManager.TAdd(bakimStok);
                            }

                        }

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

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Bakim item = bakimManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        bakimManager.TUpdate(item);
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
    }
}
