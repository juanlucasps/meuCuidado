using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_Medico")]
    public class Medico : Usuario
    {
        public virtual Idoso Idoso { get; set; }
    }
}