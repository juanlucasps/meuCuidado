using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace meuCuidado.Models
{
    public class FisioterapeutaIdoso
    {
        public int FisioterapeutaId { get; set; }
        public Fisioterapeuta Fisioterapeuta { get; set; }
        public int IdosoId { get; set; }
        public Idoso Idoso { get; set; }
    }
}