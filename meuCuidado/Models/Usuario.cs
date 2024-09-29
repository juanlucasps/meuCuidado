using System;
using System.ComponentModel.DataAnnotations;

namespace meuCuidado.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        [Required]
        public string Nome { get; set; }

        public string CPF { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCadasto { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public DateTime? UltimoLogin { get; set; }
    }
}
