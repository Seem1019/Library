using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Cofigurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("autores");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("apellidos");

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        }
    }
}
