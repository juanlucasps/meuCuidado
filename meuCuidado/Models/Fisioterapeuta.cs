using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace meuCuidado.Models
{
    public class Fisioterapeuta : Pessoa
    {
        [Required]
        public string RegistroProfissional { get; set; }
        public ICollection<FisioterapeutaIdoso> IdososCadastrados { get; set; }
    }
}