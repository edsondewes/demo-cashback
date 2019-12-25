using System;
using MediatR;

namespace AppCashback.Core.Comandos
{
    public class AtualizarCompraComando : IRequest<Compra>
    {
        public string Codigo { get; }
        public string Cpf { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }

        public AtualizarCompraComando(string codigo, string cpf, DateTime? data, decimal? valor)
        {
            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            Data = data;
            Valor = valor;
        }
    }
}
