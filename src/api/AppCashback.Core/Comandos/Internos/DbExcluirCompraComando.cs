using System;
using MediatR;

namespace AppCashback.Core.Comandos.Internos
{
    internal class DbExcluirCompraComando : IRequest
    {
        public Compra Compra { get; }

        public DbExcluirCompraComando(Compra compra)
        {
            Compra = compra ?? throw new ArgumentNullException(nameof(compra));
        }
    }
}
