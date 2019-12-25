using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Testes.Api.Controllers
{
    [Collection("Http server")]
    public class SaldoControllerTestes
    {
        private readonly HttpClient _client;

        public SaldoControllerTestes(CashbackAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_nao_deve_permitir_acesso_sem_token()
        {
            var response = await _client.GetAsync("/saldo");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Get_deve_retornar_valor()
        {
            var response = await _client.RequestComToken(HttpMethod.Get, "/saldo");
            response.EnsureSuccessStatusCode();

            var valor = await response.Content.ReadAsAsync<decimal>();
            Assert.Equal(100, valor);
        }
    }
}