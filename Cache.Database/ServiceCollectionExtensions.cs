using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Cache.Abstractions;

namespace Zhomfr.Cache.Database;

/// <summary>Provide extension method for register cache services in DI.</summary>
public static class ServiceCollectionExtensions
{
    private static void Prepare<TContext>(IServiceCollection services) where TContext : DbContext
    {
        if (services.All(x => x.ServiceType != typeof(IDbContextFactory<TContext>))) {
            services.AddDbContextFactory<TContext>();
        }

        if (services.All(x => x.ServiceType != typeof(CacheCleaner<TContext>))) {
            services.AddHostedService<CacheCleaner<TContext>>();
        }
    }

    extension(IServiceCollection services)
    {
        /// <summary>Add database cache service as default (not keyed).</summary>
        /// <param name="configuration">App configuration.</param>
        /// <typeparam name="TContext">Db context type.</typeparam>
        /// <returns>Service collection with cache service.</returns>
        public IServiceCollection AddDefaultCache<TContext>(IConfiguration configuration) where TContext : DbContext
        {
            ServiceCollectionExtensions.Prepare<TContext>(services);

            services.AddDefaultCache<DatabaseCache<TContext>, DatabaseCacheConfig>(configuration);

            return services;
        }

        /// <summary>Add cache service as keyed.</summary>
        /// <param name="key">Service key.</param>
        /// <param name="configuration">App configuration.</param>
        /// <typeparam name="TContext">Db context type.</typeparam>
        /// <returns>Service collection with cache service.</returns>
        public IServiceCollection AddKeyedCache<TContext>(string key, IConfiguration configuration) where TContext : DbContext
        {
            ServiceCollectionExtensions.Prepare<TContext>(services);

            services.AddKeyedCache<DatabaseCache<TContext>, DatabaseCacheConfig>(key, configuration);

            return services;
        }
    }
}
