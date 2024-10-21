using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Dominio.Models
{

    [Table("meuCuidado_CuidadorDeIdoso")]
    public class CuidadorDeIdoso : Usuario
    {
        public virtual RelacionamentoIdosoProfissional RelacionamentoIdosoProfissional { get; set; }
    }
}