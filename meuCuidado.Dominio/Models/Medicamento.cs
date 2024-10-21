using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Dominio.Models
{
    [Table("meuCuidado_Medicamento")]
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Dosagem { get; set; }

        [Required]
        public DateTime DataHoraPrimeiroAlerta { get; set; }

        public DateTime? DataHoraSegundoAlerta { get; set; }

        public string FormaFarmaceutica { get; set; }

        public int DuracaoEmDias { get; set; }

        public string Observacoes { get; set; }

        public virtual Lembrete Lembrete { get; set; }
    }
}
