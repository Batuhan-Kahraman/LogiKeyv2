using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using logikeyv2.ApiServices;
using logikeyv2.Helpers;
using logikeyv2.Models.GaiEFaturaModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace logikeyv2.Controllers
{
    //[OturumKontrolAttributeController]
    public class EFaturaController : BaseController
    {
        public readonly EFaturaApiService _eFaturaApiService;
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        public EFaturaController(EFaturaApiService eFaturaApiService)
        {
            _eFaturaApiService = eFaturaApiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EFaturaKes()
        {
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EFaturaKes(CreateInvoiceDto dto)
        {
            GaiInvoiceCreateModel model = new GaiInvoiceCreateModel();
            model.Ettn = Guid.NewGuid().ToString();
            model.IsDraft = false;
            model.IsCalculateByApi = false;
            model.Profile = dto.Profile;
            model.InvoiceType = dto.InvoiceType;
            model.DocNo = dto.DocNo;
            model.CurrencyCode = dto.CurrencyCode;
            model.InvoiceType = dto.InvoiceType;
            model.InvoiceAccount = new GaiInvoiceAccount();
            model.InvoiceAccount.AccountName = dto.AccountName;
            model.InvoiceAccount.TaxOfficeName = dto.TaxOfficeName;
            model.InvoiceAccount.CountryName = dto.CountryName;
            model.InvoiceAccount.CityName = dto.CityName;
            model.InvoiceAccount.District = dto.District;
            model.InvoiceAccount.StreetName = dto.StreetName;
            model.InvoiceAccount.BlockName = dto.BlockName;
            model.InvoiceAccount.BuildingName = dto.BuildingName;
            model.InvoiceAccount.Room = dto.Room;
            model.InvoiceAccount.Telephone = dto.Telephone;
            model.InvoiceAccount.Email = dto.Email;
            InvoiceDetail detail = new InvoiceDetail();
            detail.ProductName = dto.ProductName;
            detail.Quantity = dto.Quantity;
            detail.UnitCode = dto.UnitCode;
            detail.UnitPrice = dto.UnitPrice;
            detail.productCode = dto.productCode;
            detail.Amount = dto.Amount;
            detail.KdvDahilTutar = dto.KdvDahilTutar;
            model.InvoiceDetail.Add(detail);
            try
            {
                List<GaiInvoiceCreateModel> listInvoice = new List<GaiInvoiceCreateModel>();
                GaiCreateInvoiceResponse responseModel;
                //Invoice modelinin içi burada doldurulup listeye eklenmeli. Tek seferde birden fazla fatura kesilebilir.
                listInvoice.Add(model);
                var response = await _eFaturaApiService.InvoiceCreate(listInvoice, HttpContext.Session.GetString("BearerToken"));
                if (response.IsSuccessStatusCode)
                {
                    responseModel = await response.Content.ReadFromJsonAsync<GaiCreateInvoiceResponse>();
                    TempData["Msg"] = "İşlem başarılı. E-Fatura başarıyla kesildi.";
                    TempData["Bgcolor"] = "green";
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)// Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
                {
                    
                    GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                    response = await _eFaturaApiService.InvoiceCreate(listInvoice, HttpContext.Session.GetString("BearerToken"));
                    responseModel = await response.Content.ReadFromJsonAsync<GaiCreateInvoiceResponse>();
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Msg"] = "İşlem başarılı. E-Fatura başarıyla kesildi.";
                        TempData["Bgcolor"] = "green";
                    }
                    else
                    {
                        TempData["Msg"] = "İşlem başarısız. Hata: " + responseModel.Message;
                        TempData["Bgcolor"] = "red";
                    }
                    
                }
                else
                {
                    responseModel = await response.Content.ReadFromJsonAsync<GaiCreateInvoiceResponse>();
                    throw new Exception(responseModel.Message);
                }

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "İşlem başarısız. Hata: " + ex.Message;
                TempData["Bgcolor"] = "red";
            }
            //if (ModelState.IsValid)
            //{

            //}
            //else
            //{
            //    TempData["Msg"] = "İşlem başarısız. Lütfen zorunlu alanların tamamını doldurunuz.";
            //    TempData["Bgcolor"] = "red";
            //}
            return View();
        }
        //FirmaManager firmaManager = new FirmaManager(new EFFirmaRepository());
        //public IActionResult FaturaGonder(int FaturaID)
        //{
        //    int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");


        //    //efatura gönder

        //   var sonucFatura = Task.Run(async () => await EFaturaHelper.Create(FaturaID, FirmaID)).Result;


        //    ViewBag.mesaj = sonucFatura;
        //    return View();
        //}
    }
}