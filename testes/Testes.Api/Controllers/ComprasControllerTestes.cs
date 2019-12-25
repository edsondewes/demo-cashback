using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Testes.Api.Controllers
{
    [Collection("Http server")]
    public class ComprasControllerTestes
    {
        private readonly HttpClient _client;

        public ComprasControllerTestes(CashbackAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("DELETE")]
        [InlineData("POST")]
        [InlineData("PATCH")]
        public async Task Endpoint_nao_deve_permitir_acesso_sem_token(string metodo)
        {
            var request = new HttpRequestMessage(new HttpMethod(metodo), "/compras");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_nao_deve_permitir_codigo_vazio()
        {
            var response = await _client.RequestComToken(HttpMethod.Delete, "/compras", new { });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Codigo é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_codigo_vazio()
        {
            var response = await _client.RequestComToken(HttpMethod.Post, "/compras", new
            {
                data = DateTime.Now,
                valor = 100
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Codigo é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_data_vazia()
        {
            var response = await _client.RequestComToken(HttpMethod.Post, "/compras", new
            {
                codigo = "xxxx",
                valor = 100
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Data é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_valor_vazio()
        {
            var response = await _client.RequestComToken(HttpMethod.Post, "/compras", new
            {
                codigo = "xxxx",
                data = DateTime.Now
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Valor é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_valor_negativo()
        {
            var response = await _client.RequestComToken(HttpMethod.Post, "/compras", new
            {
                codigo = "xxxx",
                data = DateTime.Now,
                valor = -100
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Valor deve ser maior que zero\"", mensagem);
        }
    }
}
