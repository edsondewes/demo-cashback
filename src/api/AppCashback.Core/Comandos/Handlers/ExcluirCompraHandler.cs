using System;
using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core.Comandos.Internos;
using AppCashback.Core.Consultas;
using MediatR;

namespace AppCashback.Core.Comandos.Handlers
{
    public class ExcluirCompraHandler : IRequestHandler<ExcluirCompraComando>
    {
        private readonly IMediator _mediator;

        public ExcluirCompraHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ExcluirCompraComando request, CancellationToken cancellationToken)
        {
            var compra = await _mediator.Send(new CompraPorCodigoConsulta(request.Codigo, request.Cpf), cancellationToken);
            if (compra.Status == StatusCompra.Aprovado)
            {
                throw new InvalidOperationException("Não é possível excluir uma compra aprovada");
            }

            await _mediator.Send(new DbExcluirCompraComando(compra));
            return Unit.Value;
        }
    }
}
