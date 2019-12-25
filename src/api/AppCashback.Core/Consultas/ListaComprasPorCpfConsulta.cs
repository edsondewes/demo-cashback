using System.Collections.Generic;
using MediatR;

namespace AppCashback.Core.Consultas
{
    public class ListaComprasPorCpfConsulta : IRequest<IEnumerable<Compra>>
    {
        public string Cpf { get; set; }

        public ListaComprasPorCpfConsulta(string cpf)
        {
            Cpf = cpf ?? throw new System.ArgumentNullException(nameof(cpf));
        }
    }
}
