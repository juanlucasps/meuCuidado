using meuCuidado.Dominio.Models;
using System.Collections.Generic;

namespace meuCuidado.Dominio.ViewModels
{
    public class CadastroTutorEMedicoDoIdosoViewModel
    {
        public Idoso Idoso { get; set; }
        public Tutor Tutor { get; set; }
        public List<Medico> Medicos { get; set; }
    }
}
