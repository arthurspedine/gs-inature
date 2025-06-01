using iNature.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iNature.Data.Mappings
{
    public class ReportConfirmacaoMapping : IEntityTypeConfiguration<ReportConfirmacao>
    {
        public void Configure(EntityTypeBuilder<ReportConfirmacao> builder)
        {
            builder.ToTable("INATURE_REPORT_CONFIRMACOES");
            builder.HasKey(rc => rc.Id);

            builder.Property(rc => rc.DataConfirmacao).IsRequired();

            builder.HasOne(rc => rc.Report)
                   .WithMany(r => r.Confirmacoes)
                   .HasForeignKey(rc => rc.ReportId);

            builder.HasOne(rc => rc.Usuario)
                   .WithMany()
                   .HasForeignKey(rc => rc.UsuarioId);
        }
    }
}