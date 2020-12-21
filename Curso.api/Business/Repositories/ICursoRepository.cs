using Curso.api.Business.Etities;
using System.Collections.Generic;

namespace Curso.api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Cursos cursos);

        void Commit();
        IList<Cursos> ObterPorUsuario(int CodigoUsuario);
        
    }
}
