using Curso.api.Business.Etities;
using Curso.api.Business.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Curso.api.Infraestruture.Data.Mappings.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _contexto;

        public CursoRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Cursos curso)
        {
            _contexto.Cursos.Add(curso);
        }

        public void commit()
        {
            _contexto.SaveChanges();
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public IList<Cursos> ObterPorUsuario(int codigoUsuario)
        {
            return _contexto.Cursos.Include(i => i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
