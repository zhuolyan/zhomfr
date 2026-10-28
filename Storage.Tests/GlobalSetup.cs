using System.Collections.Concurrent;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Storage.Abstractions;
using Zhomfr.Storage.Local;

[assembly: Parallelizable(ParallelScope.All)]

namespace Zhomfr.Storage.Tests;

[SetUpFixture]
public class GlobalSetup
{
    public static readonly  List<TestTypes>                      Tested     = [TestTypes.Local];
    private static readonly ConcurrentDictionary<TestTypes, int> _runCounts = [];

#region Seed Data

    private static async Task Seed(int count)
    {
        using IServiceScope scope = GlobalSetup.ServiceProvider!.CreateScope();

        List<Task> tasks = [];

        foreach (TestTypes type in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string key = GlobalSetup.GetServiceKey(type, i);

                GlobalSetup.AddLocalFiles(Path.Combine(Directory.GetCurrentDirectory(), "storage", key));
            }
        }

        await Task.WhenAll(tasks);
    }

    private static void AddLocalFiles(string root, int deep = 1)
    {
        if (!Directory.Exists(root)) {
            Directory.CreateDirectory(root);
        }

        for (int i = 1; i <= 4; i++) {
            string filePath = Path.Combine(root, $"file{i}.txt");

            using Stream file = File.Create(filePath);

            foreach (byte chunk in Encoding.UTF8.GetBytes($"file{i}.txt")) {
                file.WriteByte(chunk);
            }

            file.Flush();
            file.Close();

            FileInfo fileInfo = new(filePath);

            if (i > 2) {
                if (OperatingSystem.IsWindows()) {
                    fileInfo.Attributes |= FileAttributes.Hidden;
                } else {
                    fileInfo.UnixFileMode = UnixFileMode.UserRead | UnixFileMode.UserWrite;
                }
            } else {
                if (OperatingSystem.IsWindows()) {
                    fileInfo.Attributes &= ~FileAttributes.Hidden;
                } else {
                    fileInfo.UnixFileMode = UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;
                }
            }
        }

        if (deep > 3) {
            return;
        }

        DirectoryInfo dir1 = Directory.CreateDirectory($"{root}/dir1");
        DirectoryInfo dir2 = Directory.CreateDirectory($"{root}/dir2");
        DirectoryInfo dir3 = Directory.CreateDirectory($"{root}/empty");

        if (OperatingSystem.IsWindows()) {
            dir2.Attributes |= FileAttributes.Hidden;
        } else {
            dir2.UnixFileMode = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite;
        }

        if (OperatingSystem.IsWindows()) {
            dir1.Attributes &= ~FileAttributes.Hidden;
            dir3.Attributes &= ~FileAttributes.Hidden;
        } else {
            dir1.UnixFileMode = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;
            dir3.UnixFileMode = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;
        }

        deep++;

        GlobalSetup.AddLocalFiles(dir1.FullName, deep);
        GlobalSetup.AddLocalFiles(dir2.FullName, deep);
    }

#endregion

#region Setup and TearDown

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        int count = GlobalSetup.CalculateTestsCount();

        await GlobalSetup.CreateContainers();

        GlobalSetup.CreateServiceProvider(count);

        await GlobalSetup.Seed(count);

        Thread.Sleep(1000);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await GlobalSetup.DisposeContainers();

        if (GlobalSetup.ServiceProvider != null) {
            await GlobalSetup.ServiceProvider.DisposeAsync();
        }

        Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "storage"), true);
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

    private static void CreateServiceProvider(int count)
    {
        Dictionary<string, string?> config   = GlobalSetup.GenerateConfiguration(count);
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
                    case TestTypes.Local:
                        services.AddKeyedStorage<LocalStorage, LocalStorageConfig>(key, configuration);

                        break;
                    default:
                    #pragma warning disable CA2208
                        throw new ArgumentOutOfRangeException(nameof(item), "Unknown test type");
                #pragma warning restore CA2208
                }
            }
        }
    }

    private static Dictionary<string, string?> GenerateConfiguration(int count)
    {
        Dictionary<string, string?> config = new();

        foreach (TestTypes item in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string name   = GlobalSetup.GetServiceKey(item, i);
                string prefix = $"Zhomfr:Storage:{name}:";

                config[$"{prefix}BaseUrl"] = $"htts://localhost:12345/storage/{name}";
                config[$"{prefix}RootDir"] = Path.Combine(Directory.GetCurrentDirectory(), "storage", name);
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
