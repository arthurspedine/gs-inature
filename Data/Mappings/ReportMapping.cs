using iNature.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iNature.Data.Mappings
{
    public class ReportMapping : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("INATURE_REPORTS");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Titulo).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Corpo).IsRequired();
            builder.Property(r => r.Tipo).HasConversion<string>().IsRequired();
            builder.Property(r => r.Data).IsRequired();
            builder.Property(r => r.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Logradouro).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Numero);

            builder.HasOne(r => r.Usuario)
                   .WithMany(u => u.Reports)
                   .HasForeignKey(r => r.UsuarioId);

            builder.HasMany(r => r.Confirmacoes)
                   .WithOne(c => c.Report)
                   .HasForeignKey(c => c.ReportId);
        }
    }
}