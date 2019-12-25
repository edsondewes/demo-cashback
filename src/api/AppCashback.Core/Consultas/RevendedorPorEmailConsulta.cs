using System;
using MediatR;

namespace AppCashback.Core.Consultas
{
    public class RevendedorPorEmailConsulta : IRequest<Revendedor>
    {
        public string Email { get; }
        
        public RevendedorPorEmailConsulta(string email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
