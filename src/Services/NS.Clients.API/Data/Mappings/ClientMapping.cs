using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Clients.API.Models;
using NS.Core.DomainObjects;

namespace NS.Clients.API.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(x => x.Cpf, tf =>
            {
                tf.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(x => x.Email, tf =>
            {
                tf.Property(x => x.AddressEmail)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.AddressEmailMaxLength})");
            });

            // 1:1
            builder.HasOne(x => x.Address)
                .WithOne(x => x.Client);

            builder.ToTable("Clients");
        }
    }
}
