using System;
using MediatR;

namespace AppCashback.Core.Comandos.Internos
{
    internal class DbSalvarCompraComando : IRequest
    {
        public Compra Compra { get; }

        public DbSalvarCompraComando(Compra compra)
        {
            Compra = compra ?? throw new ArgumentNullException(nameof(compra));
        }
    }
}
