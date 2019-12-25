using System;
using System.Threading.Tasks;
using AppCashback.Core;
using AppCashback.Core.Comandos;
using AppCashback.Core.Comandos.Handlers;
using AppCashback.Core.Consultas;
using MediatR;
using Moq;
using Xunit;

namespace Testes.Core.Comandos
{
    public class CriarCompraComandoTestes
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Nao_deve_permitir_valor_menor_ou_igual_a_zero(int valor)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CriarCompraComando(
               codigo: "12345",
               cpf: "999999",
               data: DateTime.Now,
               valor: valor
               ));
        }

        [Fact]
        public async Task Nao_deve_permitir_cadastrar_codigo_ja_existente()
        {
            var codigo = "codigo";
            var cpf = "cpf";

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CompraPorCodigoConsulta>(c => c.Codigo == codigo && c.Cpf == cpf), default))
                .ReturnsAsync(new Compra());

            var comando = new CriarCompraComando(codigo, cpf, DateTime.Now, 100m);
            var handler = new CriarCompraHandler(mediator.Object);

            await Assert.ThrowsAsync<ValidacaoCadastroException>(() => handler.Handle(comando, default));
        }

        [Theory]
        [InlineData("15350946056", StatusCompra.Aprovado)]
        [InlineData("01234567890", StatusCompra.EmValidacao)]
        [InlineData("99999999999", StatusCompra.EmValidacao)]
        public async Task Compras_devem_utilizar_status_de_acordo_com_cpf(string cpf, StatusCompra status)
        {
            var codigo = "codigo";
            var data = DateTime.Now;
            var valor = 100m;

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CompraPorCodigoConsulta>(c => c.Codigo == codigo && c.Cpf == cpf), default))
                .ReturnsAsync(() => null);

            var comando = new CriarCompraComando(codigo, cpf, data, valor);
            var handler = new CriarCompraHandler(mediator.Object);

            var novaCompra = await handler.Handle(comando, default);
            Assert.Equal(codigo, novaCompra.Codigo);
            Assert.Equal(cpf, novaCompra.Cpf);
            Assert.Equal(data, novaCompra.Data);
            Assert.Equal(status, novaCompra.Status);
            Assert.Equal(valor, novaCompra.Valor);
        }
    }
}
