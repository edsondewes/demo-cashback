using System;
using MediatR;

namespace AppCashback.Core.Consultas
{
    public class RevendedorPorCpfConsulta : IRequest<Revendedor>
    {
        public string Cpf { get; set; }

        public RevendedorPorCpfConsulta(string cpf)
        {
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }
    }
}
