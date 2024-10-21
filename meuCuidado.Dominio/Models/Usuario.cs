﻿using System;
using System.ComponentModel.DataAnnotations;

namespace meuCuidado.Dominio.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public Guid IdentificadorUnico { get; set; }

        [Required]
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Genero { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"^\+55\s\(\d{2}\)\s9\s\d{4}-\d{4}$", ErrorMessage = "O número de telefone deve estar no formato +55 (XX) 9 XXXX-XXXX.")] 
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCadasto { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public DateTime? UltimoLogin { get; set; }
    }
}
