using Curso.api.Business.Etities;
using Curso.api.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Curso.api.Infraestruture.Data
{
    public class CursoDbContext : DbContext
    {
        private DbContextOptionsBuilder<CursoDbContext> options;

        public CursoDbContext(DbContextOptions<CursoDbContext> options) : base(options)
        {

        }

        public CursoDbContext(DbContextOptionsBuilder<CursoDbContext> options)
        {
            this.options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cursos> Cursos { get; set; }

    }
}
