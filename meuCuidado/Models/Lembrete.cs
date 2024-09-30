using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_Lembrete")]
    public class Lembrete
    {
        [Key]
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        [Required]
        public int RelacionamentoIdosoProfissionalId { get; set; }

        public int? MedicamentoId { get; set; }

        public string Descricao { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public bool Repete { get; set; }

        public virtual RelacionamentoIdosoProfissional RelacionamentoIdosoProfissional { get; set; }

        // Propriedade de navegação
        public virtual Medicamento Medicamento { get; set; }
    }
}
