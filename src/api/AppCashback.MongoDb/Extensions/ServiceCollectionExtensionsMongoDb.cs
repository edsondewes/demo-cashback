using AppCashback.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AppCashback.MongoDb
{
    public static class ServiceCollectionExtensionsMongoDb
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            Mapeamentos.Registrar();

            services.AddSingleton<ConexaoMongoDb>();
            services.AddSingleton(provider => provider.GetRequiredService<ConexaoMongoDb>().DataBase.GetCollection<Compra>("compra"));
            services.AddSingleton(provider => provider.GetRequiredService<ConexaoMongoDb>().DataBase.GetCollection<Revendedor>("revendedor"));

            return services;
        }
    }
}
