using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.api.Model.Usuarios
{
    public class ValdaCampoViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValdaCampoViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
