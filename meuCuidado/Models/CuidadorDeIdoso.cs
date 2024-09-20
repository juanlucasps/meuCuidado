using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace meuCuidado.Models
{
    public class CuidadorDeIdoso : Pessoa
    {
        [Required]
        public string RegistroProfissional { get; set; }
        public ICollection<CuidadorIdoso> IdososCadastrados { get; set; }
    }

}