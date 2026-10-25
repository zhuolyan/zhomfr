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
        /// <summary></summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        public IServiceCollection AddDefaultCache<TService, TConfig>(IConfiguration configuration) where TService : ICache where TConfig : AbstractConfig
        {
            services.AddOptions<TConfig>(ServiceCollectionExtensions.DEFAULT_SECTION)
                    .Bind(configuration.GetSection(ServiceCollectionExtensions.DEFAULT_SECTION))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            services.AddSingleton<ICache>(sp =>
            {
                TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(ServiceCollectionExtensions.DEFAULT_SECTION);

                return ActivatorUtilities.CreateInstance<TService>(sp, options);
            });

            return services;
        }

        /// <summary></summary>
        /// <param name="key"></param>
        /// <param name="configuration"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        public IServiceCollection AddKeyedCache<TService, TConfig>(string key, IConfiguration configuration) where TService : ICache where TConfig : AbstractConfig
        {
            string keyedSection = $"{ServiceCollectionExtensions.SECTION_PREFIX}{key}";

            services.AddOptions<TConfig>(keyedSection).Bind(configuration.GetSection(keyedSection)).ValidateDataAnnotations().ValidateOnStart();

            services.AddKeyedSingleton<ICache>(key,
                                               (sp, _) =>
                                               {
                                                   TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(keyedSection);

                                                   return ActivatorUtilities.CreateInstance<TService>(sp, options);
                                               });

            return services;
        }
    }
}
