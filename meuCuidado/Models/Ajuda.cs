﻿using System.ComponentModel.DataAnnotations;

namespace meuCuidado.Models
{
    public class Ajuda
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"^\+55\s\(\d{2}\)\s9\s\d{4}-\d{4}$", ErrorMessage = "O número de telefone deve estar no formato +55 (XX) 9 XXXX-XXXX.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A forma de retorno é obrigatória.")]
        public string FormaDeRetorno { get; set; }
    }
}
