using System;
using MediatR;

namespace AppCashback.Core.Comandos
{
    public class CriarRevendedorComando : IRequest<Revendedor>
    {
        public string Cpf { get; }
        public string Email { get; }
        public string Nome { get; }
        public string Senha { get; }

        public CriarRevendedorComando(string cpf, string email, string nome, string senha)
        {
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Senha = senha ?? throw new ArgumentNullException(nameof(senha));
        }
    }
}
