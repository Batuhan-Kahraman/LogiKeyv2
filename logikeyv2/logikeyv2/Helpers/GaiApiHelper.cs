using logikeyv2.ApiServices;
using logikeyv2.Models.GaiEFaturaModels;

namespace logikeyv2.Helpers
{
    public class GaiApiHelper
    {
        public async static void CheckBearerToken(EFaturaApiService _eFaturaApiService, HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Session.GetString("BearerToken")) || string.IsNullOrEmpty(context.Session.GetString("RefreshToken")))
            {
                GaiLoginModel gaiLoginModel = new GaiLoginModel();
                gaiLoginModel.userName = "GaiWS";
                gaiLoginModel.password = "GaiWS";
                var response = _eFaturaApiService.LoginGai(gaiLoginModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseModel = await response.Content.ReadFromJsonAsync<GaiLoginResponseModel>();
                    context.Session.SetString("BearerToken", responseModel.JwtToken.AccessToken);
                    context.Session.SetString("RefreshToken", responseModel.RefreshToken);
                }else
                {
                    throw new Exception("Gai Api Servise Bağlanılamadı.");
                }           }
            else
            {
                RefreshTokenModel refreshToken = new RefreshTokenModel();
                refreshToken.RefreshToken = context.Session.GetString("RefreshToken");
                var response = await _eFaturaApiService.RefreshToken(refreshToken);
                if (response.IsSuccessStatusCode)
                {
                    var responseModel = await response.Content.ReadFromJsonAsync<GaiLoginResponseModel>();
                    context.Session.SetString("BearerToken", responseModel.JwtToken.AccessToken);
                    context.Session.SetString("RefreshToken", responseModel.RefreshToken);
                }
                else
                {
                    throw new Exception("Gai Api Servisten refresh token alınamadı.");
                }
            }
        }
    }
}
