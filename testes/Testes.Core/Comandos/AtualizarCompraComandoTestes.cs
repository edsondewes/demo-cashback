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
    public class AtualizarCompraComandoTestes
    {
        [Theory]
        [InlineData(StatusCompra.EmValidacao)]
        [InlineData(StatusCompra.Rejeitado)]
        public async Task Deve_permitir_atualizar_compra_por_status(StatusCompra status)
        {
            var codigo = "codigo";
            var cpf = "cpf";
            var data = DateTime.Now;
            var valor = 100m;

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CompraPorCodigoConsulta>(c => c.Codigo == codigo && c.Cpf == cpf), default))
                .ReturnsAsync(new Compra
                {
                    Codigo = codigo,
                    Cpf = cpf,
                    Data = new DateTime(2000, 1, 1),
                    Status = status,
                    Valor = 50,
                });

            var comando = new AtualizarCompraComando(codigo, cpf, data, valor);
            var handler = new AtualizarCompraHandler(mediator.Object);

            var compraAtualizada = await handler.Handle(comando, default);
            Assert.Equal(codigo, compraAtualizada.Codigo);
            Assert.Equal(cpf, compraAtualizada.Cpf);
            Assert.Equal(data, compraAtualizada.Data);
            Assert.Equal(status, compraAtualizada.Status);
            Assert.Equal(valor, compraAtualizada.Valor);
        }

        [Fact]
        public async Task Nao_deve_permitir_atualizar_compra_aprovada()
        {
            var codigo = "codigo";
            var cpf = "cpf";
            var data = DateTime.Now;
            var valor = 100m;

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CompraPorCodigoConsulta>(c => c.Codigo == codigo && c.Cpf == cpf), default))
                .ReturnsAsync(new Compra
                {
                    Status = StatusCompra.Aprovado
                });

            var comando = new AtualizarCompraComando(codigo, cpf, data, valor);
            var handler = new AtualizarCompraHandler(mediator.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(comando, default));
        }
    }
}
