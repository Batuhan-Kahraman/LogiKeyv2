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
            else if(response.StatusCode == HttpStatusCode.Unauthorized) 
            {
                GaiApiHelper.CheckBearerToken(_eFaturaApiService, HttpContext);
                response = await _eFaturaApiService.CheckUser(gaiCheckUser, HttpContext.Session.GetString("BearerToken"));
                gaiCheckUserResponse = await response.Content.ReadFromJsonAsync<GaiCheckUserResponse>();
            }

            return RedirectToAction("Index");
        }
	}
}
