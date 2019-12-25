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
    public class RevendedorPorEmailHandler : IRequestHandler<RevendedorPorEmailConsulta, Revendedor>
    {
        private readonly IMongoCollection<Revendedor> _collection;

        public RevendedorPorEmailHandler(IMongoCollection<Revendedor> collection)
        {
            _collection = collection;
        }

        public Task<Revendedor> Handle(RevendedorPorEmailConsulta request, CancellationToken cancellationToken)
        {
            return _collection
                .AsQueryable()
                .Where(r => r.Email == request.Email)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
