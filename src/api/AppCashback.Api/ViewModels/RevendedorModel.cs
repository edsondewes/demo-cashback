using System.ComponentModel.DataAnnotations;

namespace AppCashback.Api.ViewModels
{
    public class RevendedorModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Cpf é obrigatório")]
        public string Cpf { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Email é obrigatório")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
