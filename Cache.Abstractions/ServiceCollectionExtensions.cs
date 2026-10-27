using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Zhomfr.Cache.Abstractions;

/// <summary>Provide extension method for register cache services in DI.</summary>
public static class ServiceCollectionExtensions
{
    private const string SECTION_PREFIX  = "Zhomfr:Cache:";
    private const string DEFAULT_SECTION = $"{ServiceCollectionExtensions.SECTION_PREFIX}Default";

    extension(IServiceCollection services)
    {
        /// <summary>Add cache service as default (not keyed).</summary>
        /// <param name="configuration">App configuration.</param>
        /// <typeparam name="TCache">Cache driver type.</typeparam>
        /// <typeparam name="TConfig">Cache config type.</typeparam>
        /// <returns>Service collection with cache service.</returns>
        public IServiceCollection AddDefaultCache<TCache, TConfig>(IConfiguration configuration) where TCache : ICache where TConfig : class, IConfig
        {
            services.AddOptions<TConfig>(ServiceCollectionExtensions.DEFAULT_SECTION)
                    .Bind(configuration.GetSection(ServiceCollectionExtensions.DEFAULT_SECTION))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            services.AddSingleton<ICache>(sp =>
            {
                TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(ServiceCollectionExtensions.DEFAULT_SECTION);

                return ActivatorUtilities.CreateInstance<TCache>(sp, options);
            });

            return services;
        }

        /// <summary>Add cache service as keyed.</summary>
        /// <param name="key">Service key.</param>
        /// <param name="configuration">App configuration.</param>
        /// <typeparam name="TCache">Cache driver type.</typeparam>
        /// <typeparam name="TConfig">Cache config type.</typeparam>
        /// <returns>Service collection with cache service.</returns>
        public IServiceCollection AddKeyedCache<TCache, TConfig>(string key, IConfiguration configuration) where TCache : ICache where TConfig : class, IConfig
        {
            string section = $"{ServiceCollectionExtensions.SECTION_PREFIX}{key}";

            services.AddOptions<TConfig>(section).Bind(configuration.GetSection(section)).ValidateDataAnnotations().ValidateOnStart();

            services.AddKeyedSingleton<ICache>(key,
                                               (sp, _) =>
                                               {
                                                   TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(section);

                                                   return ActivatorUtilities.CreateInstance<TCache>(sp, options);
                                               });

            return services;
        }
    }
}
