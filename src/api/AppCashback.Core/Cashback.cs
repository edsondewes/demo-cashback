using System;

namespace AppCashback.Core
{
    public class Cashback
    {
        public decimal Percentual { get; set; }
        public decimal Valor { get; set; }

        public Cashback(decimal valorCompra)
        {
            Percentual = valorCompra switch
            {
                var v when v < 1000 => 0.1m,
                var v when v >= 1000 && v <= 1500 => 0.15m,
                _ => 0.2m
            };

            Valor = Math.Truncate(100m * valorCompra * Percentual) / 100m;
        }
    }
}
