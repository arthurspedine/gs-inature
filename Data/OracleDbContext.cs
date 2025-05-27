using Microsoft.EntityFrameworkCore;

namespace iNature.Data 
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OracleDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
