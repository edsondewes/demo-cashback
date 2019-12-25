using System.Collections.Generic;
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
    public class ListaComprasPorCpfHandler : IRequestHandler<ListaComprasPorCpfConsulta, IEnumerable<Compra>>
    {
        private readonly IMongoCollection<Compra> _collection;

        public ListaComprasPorCpfHandler(IMongoCollection<Compra> collection)
        {
            _collection = collection;
        }

        public async Task<IEnumerable<Compra>> Handle(ListaComprasPorCpfConsulta request, CancellationToken cancellationToken)
        {
            var lista = await _collection
                .AsQueryable()
                .Where(c => c.Cpf == request.Cpf)
                .ToListAsync(cancellationToken);

            return lista;
        }
    }
}