using System.ComponentModel.DataAnnotations;
using static meuCuidado.Dominio.Extensions.EnumExtension;

namespace meuCuidado.Dominio.Models
{
    public class CadastroViewModel
    {
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public TipoUsuario TipoUsuario {  get; set; }
    }
}