using System;
using System.ComponentModel.DataAnnotations;

namespace AppCashback.Api.ViewModels
{
    public class CadastrarCompraRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Codigo é obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Campo Data é obrigatório")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Campo Valor é obrigatório")]
        [Range(0.01d, double.MaxValue, ErrorMessage = "Campo Valor deve ser maior que zero")]
        public decimal? Valor { get; set; }
    }
}
