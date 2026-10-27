using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Zhomfr.Storage.Abstractions;

/// <summary>Extensions for register storage driver in DI.</summary>
public static class ServiceCollectionExtensions
{
    private const string SECTION_PREFIX  = "Zhomfr:Storage:";
    private const string DEFAULT_SECTION = $"{ServiceCollectionExtensions.SECTION_PREFIX}Default";

    extension(IServiceCollection services)
    {
        /// <summary>Add storage service as default (not keyed).</summary>
        /// <param name="configuration">App configuration.</param>
        /// <typeparam name="TStorage">Storage driver type.</typeparam>
        /// <typeparam name="TConfig">Storage config type.</typeparam>
        /// <returns>Service collection with storage service.</returns>
        public IServiceCollection AddDefaultStorage<TStorage, TConfig>(IConfiguration configuration)
            where TStorage : class, IStorage where TConfig : class, IConfig
        {
            services.AddOptions<TConfig>(ServiceCollectionExtensions.DEFAULT_SECTION)
                    .Bind(configuration.GetSection(ServiceCollectionExtensions.DEFAULT_SECTION))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            Func<IServiceProvider, TStorage> storageFactory = sp =>
            {
                TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(ServiceCollectionExtensions.DEFAULT_SECTION);

                return ActivatorUtilities.CreateInstance<TStorage>(sp, options);
            };

            services.AddSingleton<IDirectoryManager>(storageFactory);
            services.AddSingleton<IDirectoryScanner>(storageFactory);
            services.AddSingleton<IFileManager>(storageFactory);
            services.AddSingleton<IFileSystemMetadata>(storageFactory);
            services.AddSingleton<IFileSystemState>(storageFactory);
            services.AddSingleton<IFileSystemTypeInspector>(storageFactory);
            services.AddSingleton<IFileSystemMetadata>(storageFactory);
            services.AddSingleton<IStorage>(storageFactory);

            return services;
        }

        /// <summary>Add storage service as keyed.</summary>
        /// <param name="key">Service key.</param>
        /// <param name="configuration">App configuration.</param>
        /// <typeparam name="TStorage">Storage driver type.</typeparam>
        /// <typeparam name="TConfig">Storage config type.</typeparam>
        /// <returns>Service collection with storage service.</returns>
        public IServiceCollection AddKeyedStorage<TStorage, TConfig>(string key, IConfiguration configuration)
            where TStorage : class, IStorage where TConfig : class, IConfig
        {
            string section = $"{ServiceCollectionExtensions.SECTION_PREFIX}{key}";

            services.AddOptions<TConfig>(section).Bind(configuration.GetSection(section)).ValidateDataAnnotations().ValidateOnStart();

            Func<IServiceProvider, object?, TStorage> storageFactory = (sp, _) =>
            {
                TConfig options = sp.GetRequiredService<IOptionsMonitor<TConfig>>().Get(section);

                return ActivatorUtilities.CreateInstance<TStorage>(sp, options);
            };

            services.AddKeyedSingleton<IDirectoryManager>(key, storageFactory);
            services.AddKeyedSingleton<IDirectoryScanner>(key, storageFactory);
            services.AddKeyedSingleton<IFileManager>(key, storageFactory);
            services.AddKeyedSingleton<IFileSystemMetadata>(key, storageFactory);
            services.AddKeyedSingleton<IFileSystemState>(key, storageFactory);
            services.AddKeyedSingleton<IFileSystemTypeInspector>(key, storageFactory);
            services.AddKeyedSingleton<IFileSystemMetadata>(key, storageFactory);
            services.AddKeyedSingleton<IStorage>(key, storageFactory);

            return services;
        }
    }
}
