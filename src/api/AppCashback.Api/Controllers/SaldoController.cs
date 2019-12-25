using System.Threading.Tasks;
using AppCashback.Api.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCashback.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SaldoController : ControllerBase
    {
        private readonly ICashbackApi _api;

        public SaldoController(ICashbackApi api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<decimal> Get()
        {
            var cpf = User.Cpf();
            var valor = await _api.ObterValorAcumulado(cpf);
            return valor;
        }
    }
}
