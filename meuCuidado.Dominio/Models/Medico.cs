using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Dominio.Models
{
    [Table("meuCuidado_Medico")]
    public class Medico : Usuario
    {
        public virtual Idoso Idoso { get; set; }
    }
}