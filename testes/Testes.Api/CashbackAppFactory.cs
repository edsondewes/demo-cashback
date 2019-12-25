using AppCashback.Api;
using AppCashback.Api.Servicos;
using AppCashback.Core;
using AppCashback.Core.Comandos.Internos;
using AppCashback.Core.Consultas;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Testes.Api
{
    public class CashbackAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var loginHandler = new Mock<IRequestHandler<LoginRevendedorConsulta, Revendedor>>();
                loginHandler
                    .Setup(h => h.Handle(It.Is<LoginRevendedorConsulta>(l => l.Email == "email@host.com" && l.Senha == "12345"), default))
                    .ReturnsAsync(new Revendedor
                    {
                        Cpf = "1234567890",
                        Email = "email@host.com",
                        Nome = "Revendedor Teste",
                        Senha = "12345"
                    });

                services.AddTransient(_ => loginHandler.Object);

                var revendedorPorCpfHandler = new Mock<IRequestHandler<RevendedorPorCpfConsulta, Revendedor>>();
                revendedorPorCpfHandler
                    .Setup(h => h.Handle(It.IsAny<RevendedorPorCpfConsulta>(), default))
                    .ReturnsAsync(() => null);

                services.AddTransient(_ => revendedorPorCpfHandler.Object);

                var revendedorPorEmailHandler = new Mock<IRequestHandler<RevendedorPorEmailConsulta, Revendedor>>();
                revendedorPorEmailHandler
                    .Setup(h => h.Handle(It.IsAny<RevendedorPorEmailConsulta>(), default))
                    .ReturnsAsync(() => null);

                services.AddTransient(_ => revendedorPorEmailHandler.Object);

                var dbSalvarRevendedorHandler = new Mock<IRequestHandler<DbSalvarRevendedorComando, Unit>>();
                dbSalvarRevendedorHandler
                    .Setup(h => h.Handle(It.IsAny<DbSalvarRevendedorComando>(), default))
                    .ReturnsAsync(Unit.Value);

                services.AddTransient(_ => dbSalvarRevendedorHandler.Object);

                var cashbackApi = new Mock<ICashbackApi>();
                cashbackApi
                    .Setup(c => c.ObterValorAcumulado("1234567890"))
                    .ReturnsAsync(100);

                services.AddSingleton(cashbackApi.Object);
            });
        }
    }

    [CollectionDefinition("Http server")]
    public class CashbackAppFactoryCollection : ICollectionFixture<CashbackAppFactory>
    {
    }
}
