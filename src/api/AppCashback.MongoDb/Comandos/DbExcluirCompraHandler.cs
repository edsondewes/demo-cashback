using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core;
using AppCashback.Core.Comandos.Internos;
using MediatR;
using MongoDB.Driver;

namespace AppCashback.MongoDb.Comandos
{
    internal class DbExcluirCompraHandler : IRequestHandler<DbExcluirCompraComando>
    {
        private readonly IMongoCollection<Compra> _collection;

        public DbExcluirCompraHandler(IMongoCollection<Compra> collection)
        {
            _collection = collection;
        }

        public async Task<Unit> Handle(DbExcluirCompraComando request, CancellationToken _)
        {
            await _collection.DeleteOneAsync(r => r.Codigo == request.Compra.Codigo && r.Cpf == request.Compra.Cpf);
            return Unit.Value;
        }
    }
}
