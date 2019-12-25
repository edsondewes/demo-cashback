using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppCashback.Api.ViewModels;
using AppCashback.Core;
using AppCashback.Core.Comandos;
using AppCashback.Core.Consultas;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCashback.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<CompraModel>> Get()
        {
            var lista = await _mediator.Send(new ListaComprasPorCpfConsulta(
                cpf: User.Cpf()
                ));

            return CompraModel.Mapear(lista);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody]ExcluirCompraRequest request)
        {
            try
            {
                await _mediator.Send(new ExcluirCompraComando(
                    codigo: request.Codigo,
                    cpf: User.Cpf()
                    ));

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CadastrarCompraRequest request)
        {
            try
            {
                await _mediator.Send(new CriarCompraComando(
                    codigo: request.Codigo,
                    cpf: User.Cpf(),
                    data: request.Data.Value,
                    valor: request.Valor.Value
                    ));

                return Ok();
            }
            catch (ValidacaoCadastroException ex)
            {
                return BadRequest(ex.Erro);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Patch([FromBody]AtualizarCompraRequest request)
        {
            try
            {
                await _mediator.Send(new AtualizarCompraComando(
                    codigo: request.Codigo,
                    cpf: User.Cpf(),
                    data: request.Data,
                    valor: request.Valor
                    ));

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
