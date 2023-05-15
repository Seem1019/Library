using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Cofigurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("editoriales");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("nombre");

            builder.Property(e => e.Headquarters)
                .HasMaxLength(45)
                .HasColumnName("sede");
        }
    }
}
