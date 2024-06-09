using logikeyv2.Helpers;
using logikeyv2.Models.GaiEFaturaModels;
using System.Net;

namespace logikeyv2.ApiServices
{
    public class EFaturaApiService
    {
        private readonly HttpClient _httpClient;
        public EFaturaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginGai(GaiLoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("IntegrationKullanici/Login", model);
            return response;
        }

        public async Task<HttpResponseMessage> RefreshToken(RefreshTokenModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("IntegrationKullanici/RefreshToken", model);
            return response;
        }

        public async Task<HttpResponseMessage> CheckUser(GaiCheckUserModel model, string token)
        {
            var authorizationValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = authorizationValue;
            var response = await _httpClient.PostAsJsonAsync("IntegrationGibKullaniciListe/CheckUser", model);
            return response;
        }
    }
}
