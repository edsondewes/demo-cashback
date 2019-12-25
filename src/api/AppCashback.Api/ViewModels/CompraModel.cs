using System;
using System.Collections.Generic;
using System.Linq;
using AppCashback.Core;

namespace AppCashback.Api.ViewModels
{
    public class CompraModel
    {
        public string Codigo { get; set; }
        public DateTime Data { get; set; }
        public StatusCompra Status { get; set; }
        public decimal Valor { get; set; }

        public decimal PercentualCashback { get; set; }
        public decimal ValorCashback { get; set; }

        public static IEnumerable<CompraModel> Mapear(IEnumerable<Compra> compras)
            => compras.Select(Mapear);

        public static CompraModel Mapear(Compra compra)
        {
            var cashback = compra.CalcularCashback();
            return new CompraModel
            {
                Codigo = compra.Codigo,
                Data = compra.Data,
                Status = compra.Status,
                Valor = compra.Valor,

                PercentualCashback = cashback.Percentual,
                ValorCashback = cashback.Valor,
            };
        }
    }
}
