using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_RelacionamentoIdosoProfissional")]
    public class RelacionamentoIdosoProfissional
    {
        [Key]
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        public int? CuidadorId { get; set; }

        public int? FisioterapeutaId { get; set; }

        public int? IdosoId { get; set; }

        public int? TutorId { get; set; }

        public virtual IList<Avaliacao> Avaliacoes { get; set; }

        public virtual CuidadorDeIdoso Cuidador { get; set; }

        public virtual Fisioterapeuta Fisioterapeuta { get; set; }

        public virtual Idoso Idoso { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}