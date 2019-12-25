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
    public class CriarRevendedorTestes
    {
        [Fact]
        public async Task Nao_deve_permitir_cadastrar_email_ja_existente()
        {
            var email = "email";

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<RevendedorPorEmailConsulta>(c => c.Email == email), default))
                .ReturnsAsync(new Revendedor());

            var comando = new CriarRevendedorComando("cpf", email, "nome", "senha");
            var handler = new SalvarRevendedorHandler(mediator.Object);

            await Assert.ThrowsAsync<ValidacaoCadastroException>(() => handler.Handle(comando, default));
        }

        [Fact]
        public async Task Nao_deve_permitir_cadastrar_cpf_ja_existente()
        {
            var cpf = "cpf";

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<RevendedorPorCpfConsulta>(c => c.Cpf == cpf), default))
                .ReturnsAsync(new Revendedor());

            var comando = new CriarRevendedorComando(cpf, "email", "nome", "senha");
            var handler = new SalvarRevendedorHandler(mediator.Object);

            await Assert.ThrowsAsync<ValidacaoCadastroException>(() => handler.Handle(comando, default));
        }
    }
}
