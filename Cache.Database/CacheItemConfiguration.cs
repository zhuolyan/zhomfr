using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zhomfr.Cache.Database;

/// <inheritdoc/>
public class CacheItemConfiguration(string tableName = "CacheItems", string? schema = null) : IEntityTypeConfiguration<CacheItem>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CacheItem> builder)
    {
        builder.ToTable(tableName, schema);

        builder.HasKey(x => x.Key);

        builder.Property(x => x.Key).IsRequired().HasMaxLength(512);
        builder.Property(x => x.Value).IsRequired();
        builder.Property(x => x.ExpiredAt);

        builder.HasIndex(x => x.ExpiredAt);
    }
}
