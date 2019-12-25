using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core.Comandos.Internos;
using AppCashback.Core.Consultas;
using MediatR;

namespace AppCashback.Core.Comandos.Handlers
{
    public class SalvarRevendedorHandler : IRequestHandler<CriarRevendedorComando, Revendedor>
    {
        private readonly IMediator _mediator;

        public SalvarRevendedorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Revendedor> Handle(CriarRevendedorComando request, CancellationToken cancellationToken)
        {
            var revendedor = await _mediator.Send(new RevendedorPorCpfConsulta(request.Cpf), cancellationToken);
            if (revendedor != null)
            {
                throw new ValidacaoCadastroException(new { Cpf = "Cpf já está em uso no sistema" });
            }

            revendedor = await _mediator.Send(new RevendedorPorEmailConsulta(request.Email), cancellationToken);
            if (revendedor != null)
            {
                throw new ValidacaoCadastroException(new { Email = "Email já está em uso no sistema" });
            }

            revendedor = new Revendedor
            {
                Cpf = request.Cpf,
                Email = request.Email,
                Nome = request.Nome,
                Senha = request.Senha
            };

            await _mediator.Send(new DbSalvarRevendedorComando(revendedor));
            return revendedor;
        }
    }
}
