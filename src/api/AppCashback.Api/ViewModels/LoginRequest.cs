using System.ComponentModel.DataAnnotations;

namespace AppCashback.Api.ViewModels
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Email é obrigatório")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
