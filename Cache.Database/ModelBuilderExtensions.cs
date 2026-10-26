using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Cache.Database;

/// <summary>Extensions for add entity configuration for store data in DB.</summary>
public static class ModelBuilderExtensions
{
    extension(ModelBuilder modelBuilder)
    {
        /// <summary>Add entity for store cache data in DB to context.</summary>
        /// <param name="tableName">Table name for store cache data.</param>
        /// <param name="schema">DB schema if db supports.</param>
        /// <returns>Model builder with configuration for entity for store cache data.</returns>
        public ModelBuilder ApplyCacheConfiguration(string tableName = "CacheItems", string? schema = null)
        {
            modelBuilder.ApplyConfiguration(new CacheItemConfiguration(tableName, schema));

            return modelBuilder;
        }
    }
}
