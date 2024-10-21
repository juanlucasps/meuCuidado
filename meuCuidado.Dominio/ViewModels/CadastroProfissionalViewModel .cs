using System.ComponentModel.DataAnnotations;
using static meuCuidado.Dominio.Extensions.EnumExtension;

namespace meuCuidado.Dominio.Models
{
    public class CadastroProfissionalViewModel
    {
        [Required(ErrorMessage = "O CPF de usuário é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O gênero de usuário é obrigatório.")]
        public string Genero { get; set; }

        //[Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        //public TipoUsuario TipoUsuario {  get; set; }
    }
}