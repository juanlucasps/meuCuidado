using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_Tutor")]
    public class Tutor : Usuario
    {
        [Required]
        public string RelacaoComIdoso { get; set; }

        [Required]
        public DateTime IdadeDoIdoso { get; set; }

        public virtual Idoso Idoso { get; set; }

        public bool NecessidadesEspeciais { get; set; }

        public virtual IList<Medico> Medicos { get; set; }
    }
}
