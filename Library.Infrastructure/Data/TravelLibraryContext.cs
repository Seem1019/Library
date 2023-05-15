using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

#nullable disable

namespace Library.Infrastructure.Data
{
    public partial class TravelLibraryContext : DbContext
    {
        public TravelLibraryContext(){}

        public TravelLibraryContext(DbContextOptions<TravelLibraryContext> options):base(options){}

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorHasBook> AuthorsHasBooks { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
