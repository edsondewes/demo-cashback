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
    public class ExcluirCompraComandoTestes
    {
        [Theory]
        [InlineData(StatusCompra.EmValidacao)]
        [InlineData(StatusCompra.Rejeitado)]
        public async Task Deve_permitir_excluir_compra_por_status(StatusCompra status)
        {
            var codigo = "codigo";
            var cpf = "cpf";

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CompraPorCodigoConsulta>(c => c.Codigo == codigo && c.Cpf == cpf), default))
                .ReturnsAsync(new Compra
                {
                    Status = status
                });

            var comando = new ExcluirCompraComando(codigo, cpf);
            var handler = new ExcluirCompraHandler(mediator.Object);

            await handler.Handle(comando, default);
        }

        [Fact]
        public async Task Nao_deve_permitir_excluir_compra_aprovada()
        {
            var codigo = "codigo";
            var cpf = "cpf";

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CompraPorCodigoConsulta>(c => c.Codigo == codigo && c.Cpf == cpf), default))
                .ReturnsAsync(new Compra
                {
                    Status = StatusCompra.Aprovado
                });

            var comando = new ExcluirCompraComando(codigo, cpf);
            var handler = new ExcluirCompraHandler(mediator.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(comando, default));
        }
    }
}
