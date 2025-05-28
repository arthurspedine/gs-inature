using iNature.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iNature.Data.Mappings
{
    public class NoticiaMapping : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.ToTable("INATURE_NOTICIAS");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.DataPublicacao)
                .IsRequired();

            builder.Property(n => n.Resumo)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.Corpo)
                .IsRequired();

            // relacionamento com Usuario
            builder.HasOne(n => n.Usuario)
                .WithMany()
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}