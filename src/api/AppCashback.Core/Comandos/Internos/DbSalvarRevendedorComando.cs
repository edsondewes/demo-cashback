using System;
using MediatR;

namespace AppCashback.Core.Comandos.Internos
{
    internal class DbSalvarRevendedorComando : IRequest
    {
        public Revendedor Revendedor { get; }

        public DbSalvarRevendedorComando(Revendedor revendedor)
        {
            Revendedor = revendedor ?? throw new ArgumentNullException(nameof(revendedor));
        }
    }
}
