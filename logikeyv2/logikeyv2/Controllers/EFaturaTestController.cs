using iTextSharp.text.pdf;
using logikeyv2.ApiServices;
using logikeyv2.Helpers;
using logikeyv2.Models;
using logikeyv2.Models.GaiEFaturaModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace logikeyv2.Controllers
{
	public class EFaturaTestController : BaseController
    {

        public readonly EFaturaApiService _eFaturaApiService;
        public EFaturaTestController(EFaturaApiService eFaturaApiService)
        {
            _eFaturaApiService = eFaturaApiService;
        }

        public IActionResult Index()
		{
			return View();
		}

        public async Task<IActionResult> CheckUser()
        {
            GaiCheckUserModel gaiCheckUser = new GaiCheckUserModel();
            GaiCheckUserResponse gaiCheckUserResponse;
            gaiCheckUser.Identifier = "3941069153";
            gaiCheckUser.DocumentType = "Invoice";
            var response = await _eFaturaApiService.CheckUser(gaiCheckUser, HttpContext.Session.GetString("BearerToken"));
            if(response.IsSuccessStatusCode)
            {
                gaiCheckUserResponse = await response.Content.ReadFromJsonAsync<GaiCheckUserResponse>();
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized) // Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.CheckUser(gaiCheckUser, HttpContext.Session.GetString("BearerToken"));
                gaiCheckUserResponse = await response.Content.ReadFromJsonAsync<GaiCheckUserResponse>();
            }

            return RedirectToAction("Index");
        }
        //
        public async Task<IActionResult> CreateInvoice()
        {
            List<GaiInvoiceCreateModel> listInvoice = new List<GaiInvoiceCreateModel>();
            GaiInvoiceCreateModel invoice = new GaiInvoiceCreateModel();
            GaiCreateInvoiceResponse responseModel;
            //Invoice modelinin içi burada doldurulup listeye eklenmeli. Tek seferde birden fazla fatura kesilebilir.
            listInvoice.Add(invoice);
            var response = await _eFaturaApiService.InvoiceCreate(listInvoice, HttpContext.Session.GetString("BearerToken"));
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GaiCreateInvoiceResponse>();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)// Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.InvoiceCreate(listInvoice, HttpContext.Session.GetString("BearerToken"));
                responseModel = await response.Content.ReadFromJsonAsync<GaiCreateInvoiceResponse>();
            }
            return View();
        }
        public async Task<IActionResult> PreviewInvoice()
        {
            GaiPreviewInvoiceModel invoiceModel = new GaiPreviewInvoiceModel();
            GaiPreviewInvoiceResponse responseModel;
            //invoiceModel burada doldurulmalıdır
            var response = await _eFaturaApiService.PreviewInvoice(invoiceModel, HttpContext.Session.GetString("BearerToken"));
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GaiPreviewInvoiceResponse>();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)// Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.PreviewInvoice(invoiceModel, HttpContext.Session.GetString("BearerToken"));
                responseModel = await response.Content.ReadFromJsonAsync<GaiPreviewInvoiceResponse>();
            }
            return View();
        }

        public async Task<IActionResult> DownloadOutboxInvoice()
        {
            GaiDownloadInvoiceModel downloadModel = new GaiDownloadInvoiceModel();
            GaiDownloadInvoiceResponse responseModel;
            //invoiceModel burada doldurulmalıdır
            var response = await _eFaturaApiService.DownloadOutboxInvoice(downloadModel, HttpContext.Session.GetString("BearerToken"));
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GaiDownloadInvoiceResponse>();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)// Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.DownloadOutboxInvoice(downloadModel, HttpContext.Session.GetString("BearerToken"));
                responseModel = await response.Content.ReadFromJsonAsync<GaiDownloadInvoiceResponse>();
            }
            return View();
        }
        public async Task<IActionResult> GetInvoiceOutbox()
        {
            GaiGetInvoiceModel invoiceModel = new GaiGetInvoiceModel();
            GaiGetInvoiceResponse responseModel;
            //invoiceModel burada doldurulmalıdır
            var response = await _eFaturaApiService.GetInvoiceOutbox(invoiceModel, HttpContext.Session.GetString("BearerToken"));
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GaiGetInvoiceResponse>();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)// Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.GetInvoiceOutbox(invoiceModel, HttpContext.Session.GetString("BearerToken"));
                responseModel = await response.Content.ReadFromJsonAsync<GaiGetInvoiceResponse>();
            }
            return View();
        }
        public async Task<IActionResult> GetOutboxInvoiceFilter()
        {
            GaiGetOutboxInvoiceFilterModel filterModel = new GaiGetOutboxInvoiceFilterModel();
            GaiGetOutboxInvoiceResponse responseModel;
            //invoiceModel burada doldurulmalıdır
            var response = await _eFaturaApiService.GetOutboxInvoiceFilter(filterModel, HttpContext.Session.GetString("BearerToken"));
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GaiGetOutboxInvoiceResponse>();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)// Yetkisiz işlem yapılmışsa güncel token alınıyor ve tekrar talep gönderiliyor
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.GetOutboxInvoiceFilter(filterModel, HttpContext.Session.GetString("BearerToken"));
                responseModel = await response.Content.ReadFromJsonAsync<GaiGetOutboxInvoiceResponse>();
            }
            return View();
        }
    }
}
