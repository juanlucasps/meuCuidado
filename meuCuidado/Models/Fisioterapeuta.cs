using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_Fisioterapeuta")]
    public class Fisioterapeuta : Usuario
    {
        public virtual RelacionamentoIdosoProfissional RelacionamentoIdosoProfissional { get; set; }
    }
}