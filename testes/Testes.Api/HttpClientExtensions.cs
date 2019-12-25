using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppCashback.Api.ViewModels;
using Newtonsoft.Json;

namespace Testes.Api
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> RequestComToken(this HttpClient client, HttpMethod method, string uri, object content = null)
        {
            var tokenResponse = await client.PostAsJsonAsync("/revendedores/login", new { email = "email@host.com", senha = "12345" });
            var tokenModel = await tokenResponse.Content.ReadAsAsync<LoginModel>();

            var request = new HttpRequestMessage(method, uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenModel.Token);

            if (content != null)
            {
                var json = JsonConvert.SerializeObject(content);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = httpContent;
            }

            var response = await client.SendAsync(request);
            return response;
        }
    }
}
