using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace meuCuidado.Models
{
    public class Idoso : Pessoa
    {
        [Required]
        public DateTime DataNascimento { get; set; }
        public ICollection<CuidadorIdoso> Cuidadores { get; set; }
        public ICollection<FisioterapeutaIdoso> Fisioterapeutas { get; set; }
    }
}