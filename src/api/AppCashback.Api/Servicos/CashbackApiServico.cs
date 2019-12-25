using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AppCashback.Api.Servicos
{
    public interface ICashbackApi
    {
        Task<decimal> ObterValorAcumulado(string cpf);
    }

    public class CashbackApiConfig
    {
        public string Token { get; set; }
        public string Url { get; set; }
    }

    public class CashbackApiResponse
    {
        public int StatusCode { get; set; }
        public CashbackApiResponseBody Body { get; set; }
    }

    public class CashbackApiResponseBody
    {
        public decimal Credit { get; set; }
    }

    public class CashbackApiServico : ICashbackApi
    {
        public HttpClient Client { get; }

        public CashbackApiServico(HttpClient client, IOptions<CashbackApiConfig> config)
        {
            client.BaseAddress = new Uri(config.Value.Url);
            client.DefaultRequestHeaders.Add("token", config.Value.Token);

            Client = client;
        }

        public async Task<decimal> ObterValorAcumulado(string cpf)
        {
            var response = await Client.GetAsync($"?cpf={cpf}");

            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsAsync<CashbackApiResponse>();
            return apiResponse.Body.Credit;
        }
    }
}
