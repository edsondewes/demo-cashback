using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core;
using AppCashback.Core.Comandos.Internos;
using MediatR;
using MongoDB.Driver;

namespace AppCashback.MongoDb.Comandos
{
    internal class DbSalvarRevendedorHandler : IRequestHandler<DbSalvarRevendedorComando>
    {
        private readonly IMongoCollection<Revendedor> _collection;

        public DbSalvarRevendedorHandler(IMongoCollection<Revendedor> collection)
        {
            _collection = collection;
        }

        public async Task<Unit> Handle(DbSalvarRevendedorComando request, CancellationToken _)
        {
            await _collection.InsertOneAsync(request.Revendedor);
            return Unit.Value;
        }
    }
}
