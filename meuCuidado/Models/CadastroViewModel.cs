using System.ComponentModel.DataAnnotations;
using static meuCuidado.Extensions.EnumExtension;

namespace meuCuidado.Models
{
    public class CadastroViewModel
    {
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public TipoUsuario TipoUsuario {  get; set; }
    }
}