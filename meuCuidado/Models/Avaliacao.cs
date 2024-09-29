using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_Avaliacao")]
    public class Avaliacao
    {
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        [Required]
        public int RelacionamentoIdosoProfissionalId { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; } 

        public virtual RelacionamentoIdosoProfissional RelacionamentoIdosoProfissional { get; set; }
    }
}