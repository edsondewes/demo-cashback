using System;

namespace AppCashback.Core
{
    public class Compra
    {
        public string Codigo { get; set; }
        public string Cpf { get; set; }
        public DateTime Data { get; set; }
        public StatusCompra Status { get; set; }
        public decimal Valor { get; set; }

        public Cashback CalcularCashback() => new Cashback(Valor);
    }
}
