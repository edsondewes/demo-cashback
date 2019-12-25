using MediatR;

namespace AppCashback.Core.Consultas
{
    public class CompraPorCodigoConsulta : IRequest<Compra>
    {
        public string Codigo { get; }
        public string Cpf { get; }

        public CompraPorCodigoConsulta(string codigo, string cpf)
        {
            Codigo = codigo ?? throw new System.ArgumentNullException(nameof(codigo));
            Cpf = cpf ?? throw new System.ArgumentNullException(nameof(cpf));
        }
    }
}
