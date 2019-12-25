using System;
using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core.Comandos.Internos;
using AppCashback.Core.Consultas;
using MediatR;

namespace AppCashback.Core.Comandos.Handlers
{
    public class CriarCompraHandler : IRequestHandler<CriarCompraComando, Compra>
    {
        private readonly IMediator _mediator;

        public CriarCompraHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Compra> Handle(CriarCompraComando request, CancellationToken cancellationToken)
        {
            var compra = await _mediator.Send(new CompraPorCodigoConsulta(request.Codigo, request.Cpf), cancellationToken);
            if (compra != null)
            {
                throw new ValidacaoCadastroException(new { Codigo = "Código já está em uso no sistema" });
            }

            compra = new Compra
            {
                Codigo = request.Codigo,
                Cpf = request.Cpf,
                Data = request.Data,
                Status = ObterStatus(request.Cpf),
                Valor = request.Valor
            };

            await _mediator.Send(new DbSalvarCompraComando(compra));
            return compra;
        }

        private StatusCompra ObterStatus(string cpf)
        {
            if (cpf == "15350946056")
            {
                return StatusCompra.Aprovado;
            }

            return StatusCompra.EmValidacao;
        }
    }
}
