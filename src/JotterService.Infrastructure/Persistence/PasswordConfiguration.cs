using JotterService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotterService.Infrastructure.Persistence;

public class PasswordConfiguration : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> builder)
    {
        builder.ToTable(nameof(Password));

        builder.Property(p => p.Url).HasMaxLength(2000);

        builder.Property(p => p.Username).HasMaxLength(320);

        builder.HasIndex(p => p.Url);

        builder.HasIndex(p => p.Title);

    }
}
