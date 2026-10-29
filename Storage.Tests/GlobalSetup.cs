using System.Collections.Concurrent;
using System.Text;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

using FluentFTP;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Storage.Abstractions;
using Zhomfr.Storage.Ftp;
using Zhomfr.Storage.Local;

// ReSharper disable UnreachableSwitchArmDueToIntegerAnalysis

[assembly: Parallelizable(ParallelScope.All)]

namespace Zhomfr.Storage.Tests;

[SetUpFixture]
public class GlobalSetup
{
    public static readonly  List<TestTypes>                      Tested     = [TestTypes.Local, TestTypes.Ftp];
    private static readonly ConcurrentDictionary<TestTypes, int> _runCounts = [];

#region Seed Data

    private static async Task Seed(int count)
    {
        using IServiceScope scope = GlobalSetup.ServiceProvider!.CreateScope();

        List<Task> tasks = [];

        for (int i = 0; i < count; i++) {
            tasks.Add(GlobalSetup.AddLocalFiles(i));
        }

        await Task.WhenAll(tasks);
    }

    private static async Task AddLocalFiles(int iteration, int deep = 1, string root = "")
    {
        AsyncFtpClient client = new(GlobalSetup._ftpHost,
                                    "tests",
                                    "tests",
                                    GlobalSetup._ftpPort,
                                    new()
                                    {
                                        RetryAttempts                = 3,
                                        EncryptionMode               = FtpEncryptionMode.Auto,
                                        ValidateAnyCertificate       = true,
                                        DataConnectionType           = FtpDataConnectionType.AutoPassive,
                                        DataConnectionReadTimeout    = 5_000,
                                        DataConnectionConnectTimeout = 5_000,
                                        ReadTimeout                  = 5_000,
                                        ConnectTimeout               = 5_000,
                                        ListingParser                = FtpParser.Auto,
                                        TimeConversion               = FtpDate.UTC,
                                    });

        await client.AutoConnect();
        string key       = GlobalSetup.GetServiceKey(TestTypes.Local, iteration);
        string localPath = Path.Combine(Directory.GetCurrentDirectory(), "tests", key, root);
        string ftpPath   = Path.Combine("/home/tests/", root, key);

        DirectoryInfo localRootInfo = Directory.CreateDirectory(localPath);

        if (root.EndsWith("dir2/")) {
            if (OperatingSystem.IsWindows()) {
                localRootInfo.Attributes |= FileAttributes.Hidden;
            } else {
                localRootInfo.UnixFileMode = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite;
            }

            await client.Chmod(ftpPath, FtpPermission.Read | FtpPermission.Write, FtpPermission.None, FtpPermission.None);
        } else {
            if (OperatingSystem.IsWindows()) {
                localRootInfo.Attributes &= ~FileAttributes.Hidden;
                localRootInfo.Attributes &= ~FileAttributes.Hidden;
            } else {
                localRootInfo.UnixFileMode
                    = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;

                localRootInfo.UnixFileMode
                    = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;
            }

            await client.Chmod(ftpPath, FtpPermission.Read | FtpPermission.Write, FtpPermission.Read, FtpPermission.Read);
        }

        if (root.EndsWith("empty/")) {
            return;
        }

        for (int i = 1; i <= 4; i++) {
            string fileName      = $"file{i}.txt";
            string localFilePath = Path.Combine(localPath, fileName);
            string ftpFilePath   = Path.Combine(ftpPath, fileName);

            await using Stream localFile = File.Create(localFilePath);
            await using Stream ftpFile   = await client.OpenWrite(ftpFilePath);

            foreach (byte chunk in Encoding.UTF8.GetBytes(fileName)) {
                localFile.WriteByte(chunk);
                ftpFile.WriteByte(chunk);

                FileInfo localFileInfo = new(localFilePath);

                if (i > 2) {
                    if (OperatingSystem.IsWindows()) {
                        localFileInfo.Attributes |= FileAttributes.Hidden;
                    } else {
                        localFileInfo.UnixFileMode = UnixFileMode.UserRead | UnixFileMode.UserWrite;
                    }

                    await client.Chmod(ftpFilePath, FtpPermission.Read | FtpPermission.Write, FtpPermission.None, FtpPermission.None);
                } else {
                    if (OperatingSystem.IsWindows()) {
                        localFileInfo.Attributes &= ~FileAttributes.Hidden;
                    } else {
                        localFileInfo.UnixFileMode = UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;
                    }

                    await client.Chmod(ftpFilePath, FtpPermission.Read | FtpPermission.Write, FtpPermission.Read, FtpPermission.Read);
                }
            }
        }

        if (deep > 3) {
            return;
        }

        deep++;

        await GlobalSetup.AddLocalFiles(iteration, deep, root.EndWith("dir1/"));
        await GlobalSetup.AddLocalFiles(iteration, deep, root.EndWith("dir2/"));
        await GlobalSetup.AddLocalFiles(iteration, deep, root.EndWith("empty/"));
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
                    case TestTypes.Ftp:
                        services.AddKeyedStorage<FtpStorage, FtpStorageConfig>(key, configuration);

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

                config[$"{prefix}RootDir"] = item switch
                                             {
                                                 TestTypes.Local => Path.Combine(Directory.GetCurrentDirectory(), "storage", name),
                                                 TestTypes.Ftp   => $"/home/tests/{name}",
                                                 var _           => string.Empty,
                                             };

                config[$"{prefix}Host"] = item switch
                                          {
                                              TestTypes.Ftp => GlobalSetup._ftpHost,
                                              var _         => string.Empty,
                                          };

                config[$"{prefix}Port"] = item switch
                                          {
                                              TestTypes.Ftp => $"{GlobalSetup._ftpPort}",
                                              var _         => string.Empty,
                                          };

                config[$"{prefix}Username"] = item switch
                                              {
                                                  TestTypes.Ftp => "tests",
                                                  var _         => string.Empty,
                                              };

                config[$"{prefix}Password"] = item switch
                                              {
                                                  TestTypes.Ftp => "tests",
                                                  var _         => string.Empty,
                                              };
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

    private static IContainer _ftpContainer;
    private static string     _ftpHost;
    private static int        _ftpPort;

    private static async Task CreateContainers()
    {
        await Task.WhenAll(GlobalSetup.CreateFtpContainers());
    }

    private static async Task DisposeContainers()
    {
        await Task.WhenAll(GlobalSetup._ftpContainer.DisposeAsync().AsTask());
    }

    private static async Task CreateFtpContainers()
    {
        // Піднімаємо легкий FTP-сервер через Testcontainers
        GlobalSetup._ftpContainer = new ContainerBuilder("stilliard/pure-ftpd").WithPortBinding(21, true)     
                                                                               .WithPortBinding(30000, 30009)
                                                                               .WithEnvironment("FTP_USER_PASS", "tests")
                                                                               .WithEnvironment("FTP_USER_NAME", "tests")
                                                                               .WithEnvironment("FTP_USER_HOME", "/home/tests")
                                                                               .Build();

        await GlobalSetup._ftpContainer.StartAsync();

        GlobalSetup._ftpHost = GlobalSetup._ftpContainer.Hostname;
        GlobalSetup._ftpPort = GlobalSetup._ftpContainer.GetMappedPublicPort(21);
    }

#endregion
}
