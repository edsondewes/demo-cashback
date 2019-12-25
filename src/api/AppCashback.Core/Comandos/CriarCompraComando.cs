using System;
using MediatR;

namespace AppCashback.Core.Comandos
{
    public class CriarCompraComando : IRequest<Compra>
    {
        public string Codigo { get; }
        public string Cpf { get; }
        public DateTime Data { get; }
        public decimal Valor { get; }

        public CriarCompraComando(string codigo, string cpf, DateTime data, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valor), "Valor da compra deve ser maior quer zero");
            }

            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            Data = data;
            Valor = valor;
        }
    }
}
