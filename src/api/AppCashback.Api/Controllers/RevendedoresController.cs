using System.Threading.Tasks;
using AppCashback.Api.Servicos;
using AppCashback.Api.ViewModels;
using AppCashback.Core;
using AppCashback.Core.Comandos;
using AppCashback.Core.Consultas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppCashback.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevendedoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RevendedoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginModel>> Login([FromBody]LoginRequest login)
        {
            var revendedor = await _mediator.Send(new LoginRevendedorConsulta(
                email: login.Email,
                senha: login.Senha
                ));

            if (revendedor is null)
            {
                return BadRequest("Usuário ou senha inválidos");
            }

            var model = new LoginModel
            {
                Nome = revendedor.Nome,
                Token = JwtToken.Criar(revendedor)
            };

            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RevendedorModel model)
        {
            try
            {
                await _mediator.Send(new CriarRevendedorComando(
                    cpf: model.Cpf,
                    email: model.Email,
                    nome: model.Nome,
                    senha: model.Senha
                    ));

                return Ok();
            }
            catch (ValidacaoCadastroException ex)
            {
                return BadRequest(ex.Erro);
            }
        }
    }
}
