using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AppCashback.Api.ViewModels;
using Xunit;

namespace Testes.Api.Controllers
{
    [Collection("Http server")]
    public class RevendedoresControllerTestes
    {
        private readonly HttpClient _client;

        public RevendedoresControllerTestes(CashbackAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_nao_deve_permitir_email_vazio()
        {
            var response = await _client.PostAsJsonAsync("/revendedores/login", new
            {
                senha = "12345"
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Email é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Login_nao_deve_permitir_senha_vazia()
        {
            var response = await _client.PostAsJsonAsync("/revendedores/login", new
            {
                email = "email@host.com"
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Senha é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Login_deve_retornar_token_em_caso_de_autenticacao_valida()
        {
            var response = await _client.PostAsJsonAsync("/revendedores/login", new
            {
                email = "email@host.com",
                senha = "12345"
            });

            response.EnsureSuccessStatusCode();
            var loginModel = await response.Content.ReadAsAsync<LoginModel>();
            Assert.NotEmpty(loginModel.Token);
        }

        [Fact]
        public async Task Login_deve_retornar_bad_request_em_caso_de_autenticacao_invalida()
        {
            var response = await _client.PostAsJsonAsync("/revendedores/login", new
            {
                email = "email@host.com",
                senha = "xxxxx"
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_deve_cadastrar_novo_revendedor()
        {
            var response = await _client.PostAsJsonAsync("/revendedores", new
            {
                cpf = "99999999999",
                email = "email@host.com",
                nome = "Nome Revendedor",
                senha = "12345"
            });

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_nao_deve_permitir_cpf_vazio()
        {
            var response = await _client.PostAsJsonAsync("/revendedores", new
            {
                email = "email@host.com",
                nome = "Nome Revendedor",
                senha = "12345"
            });
          
            Assert.False(response.IsSuccessStatusCode);
            
            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Cpf é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_email_vazio()
        {
            var response = await _client.PostAsJsonAsync("/revendedores", new
            {
                cpf = "99999999999",
                nome = "Nome Revendedor",
                senha = "12345"
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Email é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_nome_vazio()
        {
            var response = await _client.PostAsJsonAsync("/revendedores", new
            {
                cpf = "99999999999",
                email = "email@host.com",
                senha = "12345"
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Nome é obrigatório\"", mensagem);
        }

        [Fact]
        public async Task Post_nao_deve_permitir_senha_vazia()
        {
            var response = await _client.PostAsJsonAsync("/revendedores", new
            {
                cpf = "99999999999",
                email = "email@host.com",
                nome = "Nome Revendedor",
            });

            Assert.False(response.IsSuccessStatusCode);

            var mensagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"Campo Senha é obrigatório\"", mensagem);
        }
    }
}
