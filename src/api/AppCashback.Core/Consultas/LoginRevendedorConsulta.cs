using MediatR;

namespace AppCashback.Core.Consultas
{
    public class LoginRevendedorConsulta : IRequest<Revendedor>
    {
        public string Email { get; }
        public string Senha { get; }

        public LoginRevendedorConsulta(string email, string senha)
        {
            Email = email ?? throw new System.ArgumentNullException(nameof(email));
            Senha = senha ?? throw new System.ArgumentNullException(nameof(senha));
        }
    }
}
