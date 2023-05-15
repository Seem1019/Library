using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Cofigurations
{
    public class AuthorHasBookConfiguration : IEntityTypeConfiguration<AuthorHasBook>
    {
        public void Configure(EntityTypeBuilder<AuthorHasBook> builder)
        {
            builder.HasKey(e => new { e.AuthorId, e.BookIsbn });
            
            builder.ToTable("autores_has_libros");

            builder.Property(e => e.AuthorId).HasColumnName("autores_id");

            builder.Property(e => e.BookIsbn).HasColumnName("libros_ISBN");

            builder.HasOne(d => d.Author)
                .WithMany(p => p.AuthorHasBooks)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_autores_id");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.AuthorHasBooks)
                .HasForeignKey(d => d.BookIsbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_libros_ISBN");
        }
    }

}
