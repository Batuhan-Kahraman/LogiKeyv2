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
        
        public async Task<HttpResponseMessage> InvoiceCreate(List<GaiInvoiceCreateModel> model, string token)
        {
			var authorizationValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			_httpClient.DefaultRequestHeaders.Authorization = authorizationValue;
			var response = await _httpClient.PostAsJsonAsync("IntegrationGidenFatura/Create", model);
			return response;
		}
        //Daha önce sisteme gönderilmiş taslak ya da GİB'e gönderilmiş durumda olan faturaların görüntüsüne ulaşmak için kullanılır.
        public async Task<HttpResponseMessage> PreviewInvoice(GaiPreviewInvoiceModel model, string token)
        {
            var authorizationValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = authorizationValue;
            var response = await _httpClient.PostAsJsonAsync("IntegrationGidenFatura/PreviewInvoice", model);
            return response;
        }

        public async Task<HttpResponseMessage> DownloadOutboxInvoice(GaiDownloadInvoiceModel model, string token)
        {
            var authorizationValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = authorizationValue;
            var response = await _httpClient.PostAsJsonAsync("IntegrationGidenFatura/DownloadOutboxInvoice", model);
            return response;
        }
        //Ettn veya RefNo ile daha önce gönderilmiş olan faturaların genel bilgilerini getirmek için kullanılır.
        //Gönderilen faturaların daha fazla detay içeren filteleme özellikli listesine ulaşmak için Giden Fatura Filtrele seçeneğini kullanabilirsiniz.
        public async Task<HttpResponseMessage> GetInvoiceOutbox(GaiGetInvoiceModel model, string token)
        {
            var authorizationValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = authorizationValue;
            var response = await _httpClient.PostAsJsonAsync("IntegrationGidenFatura/GetInvoiceOutbox", model);
            return response;
        }
        //Giden faturaların filtrelenerek getirilmesi için kullanılır.
        public async Task<HttpResponseMessage> GetOutboxInvoiceFilter(GaiGetOutboxInvoiceFilterModel model, string token)
        {
            var authorizationValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = authorizationValue;
            var response = await _httpClient.PostAsJsonAsync("IntegrationGidenFatura/GetOutboxInvoiceFilter", model);
            return response;
        }
    }
}
