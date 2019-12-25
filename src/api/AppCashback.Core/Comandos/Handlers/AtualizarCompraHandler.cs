using System;
using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core.Comandos.Internos;
using AppCashback.Core.Consultas;
using MediatR;

namespace AppCashback.Core.Comandos.Handlers
{
    public class AtualizarCompraHandler : IRequestHandler<AtualizarCompraComando, Compra>
    {
        private readonly IMediator _mediator;

        public AtualizarCompraHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Compra> Handle(AtualizarCompraComando request, CancellationToken cancellationToken)
        {
            var compra = await _mediator.Send(new CompraPorCodigoConsulta(request.Codigo, request.Cpf), cancellationToken);
            if (compra.Status == StatusCompra.Aprovado)
            {
                throw new InvalidOperationException("Não é possível alterar uma compra aprovada");
            }

            if (request.Data.HasValue)
                compra.Data = request.Data.Value;

            if (request.Valor.HasValue)
                compra.Valor = request.Valor.Value;

            await _mediator.Send(new DbSalvarCompraComando(compra));
            return compra;
        }
    }
}
