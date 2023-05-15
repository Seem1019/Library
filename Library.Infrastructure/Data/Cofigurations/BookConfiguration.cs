using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Cofigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Isbn);

            builder.ToTable("libros");

            builder.Property(e => e.Isbn).HasColumnName("ISBN");

            builder.Property(e => e.PublisherId).HasColumnName("editoriales_id");

            builder.Property(e => e.NumberOfPages)
                .HasMaxLength(10)
                .HasColumnName("n_paginas")
                .IsFixedLength(true);

            builder.Property(e => e.Synopsis)
                .HasColumnType("text")
                .HasColumnName("sinopsis");

            builder.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            builder.HasOne(d => d.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_editoriales_id");
        }
    }

}
