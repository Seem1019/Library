using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Library.Infrastructure.Data
{
    public partial class TravelLibraryContext : DbContext
    {
        public TravelLibraryContext()
        {
        }

        public TravelLibraryContext(DbContextOptions<TravelLibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorHasBook> AuthorsHasBooks { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Book> Books { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("autores");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<AuthorHasBook>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookIsbn });

                entity.ToTable("autores_has_libros");

                entity.Property(e => e.AuthorId).HasColumnName("autores_id");

                entity.Property(e => e.BookIsbn).HasColumnName("libros_ISBN");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorHasBooks)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_autores_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorHasBooks)
                    .HasForeignKey(d => d.BookIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_libros_ISBN");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("editoriales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.Headquarters)
                    .HasMaxLength(45)
                    .HasColumnName("sede");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Isbn);

                entity.ToTable("libros");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.PublisherId).HasColumnName("editoriales_id");

                entity.Property(e => e.NumberOfPages)
                    .HasMaxLength(10)
                    .HasColumnName("n_paginas")
                    .IsFixedLength(true);

                entity.Property(e => e.Synopsis)
                    .HasColumnType("text")
                    .HasColumnName("sinopsis");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_editoriales_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
