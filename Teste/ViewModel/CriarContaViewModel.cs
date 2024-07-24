using System.ComponentModel.DataAnnotations;

namespace Teste.ViewModel
{
    public class CriarContaViewModel
    {
        [Required(ErrorMessage = "O campo Responsável é obrigatório.")]
        public string Responsavel { get; set; } 
    } 
}
