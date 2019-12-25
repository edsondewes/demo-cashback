using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppCashback.Core;
using AppCashback.Core.Consultas;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppCashback.MongoDb.Consultas
{
    public class CompraPorCodigoHandler : IRequestHandler<CompraPorCodigoConsulta, Compra>
    {
        private readonly IMongoCollection<Compra> _collection;

        public CompraPorCodigoHandler(IMongoCollection<Compra> collection)
        {
            _collection = collection;
        }

        public Task<Compra> Handle(CompraPorCodigoConsulta request, CancellationToken cancellationToken)
        {
            return _collection
                .AsQueryable()
                .Where(c => c.Codigo == request.Codigo && c.Cpf == request.Cpf)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
