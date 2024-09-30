using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meuCuidado.Models
{
    [Table("meuCuidado_ProfissionalValidacao")]
    public class ProfissionalValidacao
    {
        [Key]
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        public int? CuidadorId { get; set; }

        public int? FisioterapeutaId { get; set; }

        public string CertificadoPM { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string Recomendacoes { get; set; }
    }
}