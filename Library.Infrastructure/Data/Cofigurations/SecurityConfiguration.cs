using Library.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Library.Core.Enumerations;
using System;

namespace Library.Infrastructure.Data.Cofigurations
{
    public class SecurityConfiguration : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            // Set the table name
            builder.ToTable("seguridad");

            // Set the primary key
            builder.HasKey(s => s.Id).HasName("id");

            // Configure properties
            builder.Property(s => s.User)
                .HasColumnName("usuario")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.UserName)
                .HasColumnName("nombre_de_usuario")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Password)
                .HasColumnName("contraseña")
                .IsRequired()
                .HasMaxLength(255);

            // Configure the Role Conversion
            builder.Property(s => s.Role)
                .HasColumnName("rol")
                 .HasMaxLength(15)
                .IsRequired()
                .HasConversion(
                x => x.ToString(),
                x => (RoleType)Enum.Parse(typeof(RoleType), x)
                );
        }
    }
}
