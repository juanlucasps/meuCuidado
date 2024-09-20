using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace meuCuidado.Models
{
    public class CuidadorIdoso
    {
        public int CuidadorId { get; set; }
        public CuidadorDeIdoso Cuidador { get; set; }
        public int IdosoId { get; set; }
        public Idoso Idoso { get; set; }
    }
}