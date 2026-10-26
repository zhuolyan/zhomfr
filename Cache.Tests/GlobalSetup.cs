using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Testcontainers.MsSql;
using Testcontainers.MySql;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Database;
using Zhomfr.Cache.InMemory;
using Zhomfr.Cache.Redis;
using Zhomfr.Cache.Tests.Stubs;

[assembly: Parallelizable(ParallelScope.All)]

namespace Zhomfr.Cache.Tests;

[SetUpFixture]
public class GlobalSetup
{
    public static readonly  List<TestTypes>         Tested     = [.. Enum.GetValues<TestTypes>()];
    private static readonly Dictionary<string, int> _runCounts = [];

    private static readonly List<StubSeedItem> _seedItems =
    [
        new() { Key = StubKeys.EXISTS, Value         = StubValues.Exists, Ttl        = new TimeSpan(10, 0, 0) },
        new() { Key = StubKeys.EXISTS_ANOTHER, Value = StubValues.ExistsAnother, Ttl = new TimeSpan(10, 0, 0) },
        new() { Key = StubKeys.EXISTS_EXPIRED, Value = StubValues.ExistsExpired, Ttl = new TimeSpan(0, 0, 1) },
    ];

#region Seed Data

    private static async Task Seed(int count)
    {
        using IServiceScope scope = GlobalSetup.ServiceProvider!.CreateScope();

        DbContext context1 = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContext>();
        DbContext context2 = scope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
        DbContext context3 = scope.ServiceProvider.GetRequiredService<MySqlDbContext>();

        await Task.WhenAll(context1.Database.EnsureCreatedAsync(), context2.Database.EnsureCreatedAsync(), context3.Database.EnsureCreatedAsync());

        List<Task> tasks = [];

        foreach (TestTypes type in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string key   = GlobalSetup.GetServiceKey(type, i);
                ICache cache = GlobalSetup.ServiceProvider!.GetRequiredKeyedService<ICache>(key);

                tasks.AddRange(GlobalSetup._seedItems.Select(item => cache.AddAsync(item.Key, item.Value, item.Ttl)));
            }
        }

        await Task.WhenAll(tasks);
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

        Thread.Sleep(2_000);
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
    public static ICache GetTested(TestTypes testType)
#pragma warning restore NUnit1028
    {
        string name = testType.ToString();

        if (!GlobalSetup._runCounts.TryGetValue(name, out int _)) {
            GlobalSetup._runCounts[name] = 0;
        }

        string key = $"{name}{GlobalSetup._runCounts[name]++}";

        return GlobalSetup.ServiceProvider!.GetRequiredKeyedService<ICache>(key);
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
        services.AddMemoryCache();
        services.AddDbContextPool<PostgreSqlDbContext>(o => o.UseNpgsql(GlobalSetup.PgsqlConnectionString));
        services.AddDbContextPool<MySqlDbContext>(o => o.UseMySQL(GlobalSetup.MySqlConnectionString));
        services.AddDbContextPool<MsSqlDbContext>(o => o.UseSqlServer(GlobalSetup.MsSqlConnectionString));

        foreach (TestTypes item in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string key = GlobalSetup.GetServiceKey(item, i);

                switch (item) {
                    case TestTypes.InMemory:
                        services.AddKeyedCache<InMemoryCache, InMemoryConfig>(key, configuration);

                        break;
                    case TestTypes.Redis:
                        services.AddKeyedCache<RedisCache, RedisConfig>(key, configuration);

                        break;
                    case TestTypes.PostgreSql:
                        services.AddKeyedCache<PostgreSqlDbContext>(key, configuration);

                        break;
                    case TestTypes.SqlServer:
                        services.AddKeyedCache<MsSqlDbContext>(key, configuration);

                        break;
                    case TestTypes.MySql:
                        services.AddKeyedCache<MySqlDbContext>(key, configuration);

                        break;
                    default:
                    #pragma warning disable CA2208
                        throw new ArgumentOutOfRangeException(nameof(item), "Unknown test type");
                #pragma warning restore CA2208
                }
            }
        }
    }

    private static string GetServiceKey(TestTypes testType, int index)
    {
        return $"{testType.ToString()}{index}";
    }

    private Dictionary<string, string?> GenerateConfiguration(int count)
    {
        Dictionary<string, string?> config = new();

        foreach (TestTypes item in GlobalSetup.Tested) {
            for (int i = 0; i < count; i++) {
                string name   = GlobalSetup.GetServiceKey(item, i);
                string prefix = $"Zhomfr:Cache:{name}:";

                config[$"{prefix}Prefix"] = $"{name}";
                config[$"{prefix}Ttl"]    = "1";

                if (item == TestTypes.Redis) {
                    config[$"{prefix}Connection"] = GlobalSetup.RedisConnectionString;
                }
            }
        }

        return config;
    }

    private static int CalculateTestsCount()
    {
        List<Type> attributes = [typeof(TestCaseAttribute), typeof(TestAttribute)];

        return typeof(GlobalSetup).Assembly.GetTypes()
                                  .Where(t => t.Namespace?.Contains("CacheTests") ?? false)
                                  .Select(t => t.GetMethods().Select(m => m.GetCustomAttributes(true).Count(a => attributes.Contains(a.GetType()))).Sum())
                                  .Sum();
    }

#endregion

#region Test Containers

    private const string ERROR_MESSAGE = "Redis container is not initialized.";

    private static string RedisConnectionString =>
        GlobalSetup._redisContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    private static string PgsqlConnectionString =>
        GlobalSetup._postgreSqlContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    private static string MySqlConnectionString =>
        GlobalSetup._mySqlContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    public static string MsSqlConnectionString =>
        GlobalSetup._msSqlContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    private static PostgreSqlContainer? _postgreSqlContainer;
    private static MsSqlContainer?      _msSqlContainer;
    private static MySqlContainer?      _mySqlContainer;
    private static RedisContainer?      _redisContainer;

    private static async Task CreateContainers()
    {
        GlobalSetup._postgreSqlContainer = new PostgreSqlBuilder("postgres:15.1").Build();
        GlobalSetup._msSqlContainer      = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04").Build();
        GlobalSetup._mySqlContainer      = new MySqlBuilder("mysql:8.0").Build();
        GlobalSetup._redisContainer      = new RedisBuilder("redis:7.0").Build();

        await Task.WhenAll(GlobalSetup._postgreSqlContainer.StartAsync(),
                           GlobalSetup._msSqlContainer.StartAsync(),
                           GlobalSetup._mySqlContainer.StartAsync(),
                           GlobalSetup._redisContainer.StartAsync());
    }

    private static async Task DisposeContainers()
    {
        await Task.WhenAll(GlobalSetup._postgreSqlContainer?.DisposeAsync().AsTask()!,
                           GlobalSetup._msSqlContainer?.DisposeAsync().AsTask()!,
                           GlobalSetup._mySqlContainer?.DisposeAsync().AsTask()!,
                           GlobalSetup._redisContainer?.DisposeAsync().AsTask()!);
    }

#endregion
}
