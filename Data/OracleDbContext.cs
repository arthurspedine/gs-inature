using iNature.Models;
using Microsoft.EntityFrameworkCore;

namespace iNature.Data 
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Noticia> Noticias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OracleDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
