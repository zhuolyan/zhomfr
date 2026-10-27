using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Storage.Abstractions;

[assembly: Parallelizable(ParallelScope.All)]

namespace Zhomfr.Storage.Tests;

[SetUpFixture]
public class GlobalSetup
{
    public static readonly  List<TestTypes>                      Tested     = [];
    private static readonly ConcurrentDictionary<TestTypes, int> _runCounts = [];

#region Seed Data

    private static async Task Seed(int count)
    {
        using IServiceScope scope = GlobalSetup.ServiceProvider!.CreateScope();

        List<Task> tasks = [];

        foreach (TestTypes type in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string key = GlobalSetup.GetServiceKey(type, i);
            }
        }

        await Task.WhenAll(tasks);
    }

    private void AddDirsForTests(string key)
    {
    }

#endregion

#region Setup and TearDown

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        int count = GlobalSetup.CalculateTestsCount();

        await GlobalSetup.CreateContainers();

        this.CreateServiceProvider(count);

        await GlobalSetup.Seed(count);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await GlobalSetup.DisposeContainers();

        if (GlobalSetup.ServiceProvider != null) {
            await GlobalSetup.ServiceProvider.DisposeAsync();
        }
    }

#endregion

#region Create Service Provider

    private static ServiceProvider? ServiceProvider { get; set; }

#pragma warning disable NUnit1028
    public static IStorage GetTested(TestTypes testType)
#pragma warning restore NUnit1028
    {
        if (!GlobalSetup._runCounts.TryGetValue(testType, out int _)) {
            GlobalSetup._runCounts[testType] = 0;
        }

        string key = $"{testType.ToString()}{GlobalSetup._runCounts[testType]++}";

        return GlobalSetup.ServiceProvider!.GetRequiredKeyedService<IStorage>(key);
    }

    private void CreateServiceProvider(int count)
    {
        Dictionary<string, string?> config   = this.GenerateConfiguration(count);
        ServiceCollection           services = [];

        GlobalSetup.AddServices(services, new ConfigurationManager().AddInMemoryCollection(config).Build(), count);

        GlobalSetup.ServiceProvider = services.BuildServiceProvider();
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration, int count)
    {
        foreach (TestTypes item in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string key = GlobalSetup.GetServiceKey(item, i);

                switch (item) {
                    default:
                    #pragma warning disable CA2208
                        throw new ArgumentOutOfRangeException(nameof(item), "Unknown test type");
                #pragma warning restore CA2208
                }
            }
        }
    }

    private Dictionary<string, string?> GenerateConfiguration(int count)
    {
        Dictionary<string, string?> config = new();

        foreach (TestTypes item in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string name   = GlobalSetup.GetServiceKey(item, i);
                string prefix = $"Zhomfr:Storage:{name}:";

                config[$"{prefix}BaseUrl"] = $"{name}";
                config[$"{prefix}RootDir"] = name;
            }
        }

        return config;
    }

    private static string GetServiceKey(TestTypes testType, int index)
    {
        return $"{testType.ToString()}{index}";
    }

    private static int CalculateTestsCount()
    {
        List<Type> attributes = [typeof(TestCaseAttribute), typeof(TestAttribute)];

        return typeof(GlobalSetup).Assembly.GetTypes()
                                  .Where(t => t.Namespace?.Contains("Feat") ?? false)
                                  .Select(t => t.GetMethods().Select(m => m.GetCustomAttributes(true).Count(a => attributes.Contains(a.GetType()))).Sum())
                                  .Sum();
    }

#endregion

#region Test Containers

    private static async Task CreateContainers()
    {
        await Task.WhenAll();
    }

    private static async Task DisposeContainers()
    {
        await Task.WhenAll();
    }

#endregion
}
