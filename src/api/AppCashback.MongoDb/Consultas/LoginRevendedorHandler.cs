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
    public class LoginRevendedorHandler : IRequestHandler<LoginRevendedorConsulta, Revendedor>
    {
        private readonly IMongoCollection<Revendedor> _collection;

        public LoginRevendedorHandler(IMongoCollection<Revendedor> collection)
        {
            _collection = collection;
        }

        public Task<Revendedor> Handle(LoginRevendedorConsulta request, CancellationToken cancellationToken)
        {
            return _collection
                .AsQueryable()
                .Where(r => r.Email == request.Email && r.Senha == request.Senha)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
