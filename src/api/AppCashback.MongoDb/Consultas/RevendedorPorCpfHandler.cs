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
    public class RevendedorPorCpfHandler : IRequestHandler<RevendedorPorCpfConsulta, Revendedor>
    {
        private readonly IMongoCollection<Revendedor> _collection;

        public RevendedorPorCpfHandler(IMongoCollection<Revendedor> collection)
        {
            _collection = collection;
        }

        public Task<Revendedor> Handle(RevendedorPorCpfConsulta request, CancellationToken cancellationToken)
        {
            return _collection
                .AsQueryable()
                .Where(r => r.Cpf == request.Cpf)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}