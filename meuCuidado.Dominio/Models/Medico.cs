﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Dominio.Models
{
    [Table("meuCuidado_Medico")]
    public class Medico : Usuario
    {
        [Required]
        public int IdosoId { get; set; }
        public virtual Idoso Idoso { get; set; }
    }
}