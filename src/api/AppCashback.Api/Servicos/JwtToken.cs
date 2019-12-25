using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AppCashback.Core;
using Microsoft.IdentityModel.Tokens;

namespace AppCashback.Api.Servicos
{
    public static class JwtToken
    {
        public static string Criar(Revendedor revendedor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Criado token sem assinatura para facilitar demonstração
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("cpf", revendedor.Cpf),
                    new Claim(ClaimTypes.Name, revendedor.Nome),
                    new Claim(ClaimTypes.Email, revendedor.Email)
                })
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
