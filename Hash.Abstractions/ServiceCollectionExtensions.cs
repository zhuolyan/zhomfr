using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Zhomfr.Hash.Abstractions;

/// <summary>Provide extension method for register hash services in DI.</summary>
public static class ServiceCollectionExtensions
{
    private const string SECTION_PREFIX  = "Zhomfr:Hash:";
    private const string DEFAULT_SECTION = $"{ServiceCollectionExtensions.SECTION_PREFIX}Default";

    extension(IServiceCollection services)
    {
        /// <summary>Add service as default hash service.</summary>
        /// <param name="configuration">Configuration object.</param>
        /// <typeparam name="TService">Hash service type.</typeparam>
        /// <typeparam name="TConfig">Configuration type for current service type.</typeparam>
        /// <returns>Collection of services with default hash service.</returns>
        public IServiceCollection AddDefaultHash<TService, TConfig>(IConfiguration configuration) where TService : IHash where TConfig : AbstractConfig
        {
            services.AddOptions<TConfig>(ServiceCollectionExtensions.DEFAULT_SECTION)
                    .Bind(configuration.GetSection(ServiceCollectionExtensions.DEFAULT_SECTION))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            services.AddSingleton<IHash>(sp =>
            {
                TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(ServiceCollectionExtensions.DEFAULT_SECTION);

                return ActivatorUtilities.CreateInstance<TService>(sp, options);
            });

            return services;
        }

        /// <summary>Add service as keyed hash service.</summary>
        /// <param name="key">Service key.</param>
        /// <param name="configuration">Configuration object.</param>
        /// <typeparam name="TService">Hash service type.</typeparam>
        /// <typeparam name="TConfig">Configuration type for current service type.</typeparam>
        /// <returns>Collection of services with default hash service.</returns>
        /// <returns></returns>
        public IServiceCollection AddKeyedHash<TService, TConfig>(string key, IConfiguration configuration) where TService : IHash where TConfig : AbstractConfig
        {
            string keyedSection = $"{ServiceCollectionExtensions.SECTION_PREFIX}{key}";

            services.AddOptions<TConfig>(keyedSection).Bind(configuration.GetSection(keyedSection)).ValidateDataAnnotations().ValidateOnStart();

            services.AddKeyedSingleton<IHash>(key,
                                              (sp, _) =>
                                              {
                                                  TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(keyedSection);

                                                  return ActivatorUtilities.CreateInstance<TService>(sp, options);
                                              });

            return services;
        }
    }
}
