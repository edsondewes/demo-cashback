using MediatR;

namespace AppCashback.Core.Comandos
{
    public class ExcluirCompraComando : IRequest
    {
        public string Codigo { get; }
        public string Cpf { get; }

        public ExcluirCompraComando(string codigo, string cpf)
        {
            Codigo = codigo ?? throw new System.ArgumentNullException(nameof(codigo));
            Cpf = cpf ?? throw new System.ArgumentNullException(nameof(cpf));
        }
    }
}
