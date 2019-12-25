using AppCashback.Core;
using Xunit;

namespace Testes.Core
{
    public class CashbackTestes
    {
        [Theory]
        [InlineData("10", "0.1", "1")]
        [InlineData("500", "0.1", "50")]
        [InlineData("999.99", "0.1", "99.99")]
        [InlineData("1000", "0.15", "150")]
        [InlineData("1499.99", "0.15", "224.99")]
        [InlineData("1500", "0.15", "225")]
        [InlineData("1501", "0.2", "300.2")]
        [InlineData("2000", "0.2", "400")]
        [InlineData("10000", "0.2", "2000")]
        public void Deve_dar_valor_e_percentual_de_acordo_com_faixas(string valorCompraStr, string percentualEsperadoStr, string valorEsperadoStr)
        {
            var valorCompra = decimal.Parse(valorCompraStr);
            var percentualEsperado = decimal.Parse(percentualEsperadoStr);
            var valorEsperado = decimal.Parse(valorEsperadoStr);

            var cashback = new Cashback(valorCompra);
            Assert.Equal(percentualEsperado, cashback.Percentual);
            Assert.Equal(valorEsperado, cashback.Valor);
        }
    }
}
