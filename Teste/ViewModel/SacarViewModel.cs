using System.ComponentModel.DataAnnotations;

namespace Teste.ViewModel
{
    public class SacarViewModel
    {
        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Número da conta deve ser um valor positivo.")]
        public int NumeroConta { get; set; }
        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }
    }
}
