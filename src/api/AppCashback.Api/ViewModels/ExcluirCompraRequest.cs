using System.ComponentModel.DataAnnotations;

namespace AppCashback.Api.ViewModels
{
    public class ExcluirCompraRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Codigo é obrigatório")]
        public string Codigo { get; set; }
    }
}
