using System.Security.Claims;

namespace AppCashback.Api
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Cpf(this ClaimsPrincipal user) => user.FindFirstValue("cpf");
    }
}
