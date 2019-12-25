using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core;
using AppCashback.Core.Comandos.Internos;
using MediatR;
using MongoDB.Driver;

namespace AppCashback.MongoDb.Comandos
{
    internal class DbSalvarCompraHandler : IRequestHandler<DbSalvarCompraComando>
    {
        private readonly IMongoCollection<Compra> _collection;

        public DbSalvarCompraHandler(IMongoCollection<Compra> collection)
        {
            _collection = collection;
        }

        public async Task<Unit> Handle(DbSalvarCompraComando request, CancellationToken _)
        {
            var filtro = Builders<Compra>.Filter.Eq(c => c.Codigo, request.Compra.Codigo)
                & Builders<Compra>.Filter.Eq(c => c.Cpf, request.Compra.Cpf);

            var atualizacao = Builders<Compra>.Update
                .Set(c => c.Data, request.Compra.Data)
                .Set(c => c.Status, request.Compra.Status)
                .Set(c => c.Valor, request.Compra.Valor);

            await _collection.UpdateOneAsync(filtro, atualizacao, new UpdateOptions { IsUpsert = true });
            return Unit.Value;
        }
    }
}
