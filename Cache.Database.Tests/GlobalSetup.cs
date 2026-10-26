using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Testcontainers.MsSql;
using Testcontainers.MySql;
using Testcontainers.PostgreSql;

using Zhomfr.Cache.Database.Tests.Stubs;

[assembly: Parallelizable(ParallelScope.All)]

namespace Zhomfr.Cache.Database.Tests;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await GlobalSetup.CreateContainers();

        GlobalSetup.CreateServiceProvider();

        using IServiceScope scope = GlobalSetup.ServiceProvider!.CreateScope();

        DbContext context1 = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContext>();
        DbContext context2 = scope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
        DbContext context3 = scope.ServiceProvider.GetRequiredService<MySqlDbContext>();

        await Task.WhenAll(context1.Database.EnsureCreatedAsync(), context2.Database.EnsureCreatedAsync(), context3.Database.EnsureCreatedAsync());

        context1.Set<CacheItem>()
                .AddRange(new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddHours(-2) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddSeconds(-2) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddHours(1) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = null });

        context2.Set<CacheItem>()
                .AddRange(new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddHours(-2) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddSeconds(-2) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddHours(1) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = null });

        context3.Set<CacheItem>()
                .AddRange(new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddHours(-2) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddSeconds(-2) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = DateTimeOffset.UtcNow.AddHours(1) },
                          new() { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString(), ExpiredAt = null });

        await Task.WhenAll(context1.SaveChangesAsync(), context2.SaveChangesAsync(), context3.SaveChangesAsync());
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await GlobalSetup.DisposeContainers();

        if (GlobalSetup.ServiceProvider != null) {
            await GlobalSetup.ServiceProvider.DisposeAsync();
        }
    }

#region Create Service Provider

    public static ServiceProvider? ServiceProvider { get; private set; }

    private static void CreateServiceProvider()
    {
        ServiceCollection services = [];

        GlobalSetup.AddServices(services);

        GlobalSetup.ServiceProvider = services.BuildServiceProvider();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddDbContextFactory<PostgreSqlDbContext>(o => o.UseNpgsql(GlobalSetup.PgsqlConnectionString));
        services.AddDbContextFactory<MySqlDbContext>(o => o.UseMySQL(GlobalSetup.MySqlConnectionString));
        services.AddDbContextFactory<MsSqlDbContext>(o => o.UseSqlServer(GlobalSetup.MsSqlConnectionString));
    }

#endregion

#region Test Containers

    private const string ERROR_MESSAGE = "Redis container is not initialized.";

    private static string PgsqlConnectionString =>
        GlobalSetup._postgreSqlContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    private static string MySqlConnectionString =>
        GlobalSetup._mySqlContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    private static string MsSqlConnectionString =>
        GlobalSetup._msSqlContainer?.GetConnectionString() ?? throw new InvalidOperationException(GlobalSetup.ERROR_MESSAGE);

    private static PostgreSqlContainer? _postgreSqlContainer;
    private static MsSqlContainer?      _msSqlContainer;
    private static MySqlContainer?      _mySqlContainer;

    private static async Task CreateContainers()
    {
        GlobalSetup._postgreSqlContainer = new PostgreSqlBuilder("postgres:15.1").Build();
        GlobalSetup._msSqlContainer      = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04").Build();
        GlobalSetup._mySqlContainer      = new MySqlBuilder("mysql:8.0").Build();

        await Task.WhenAll(GlobalSetup._postgreSqlContainer.StartAsync(), GlobalSetup._msSqlContainer.StartAsync(), GlobalSetup._mySqlContainer.StartAsync());
    }

    private static async Task DisposeContainers()
    {
        await Task.WhenAll(GlobalSetup._postgreSqlContainer?.DisposeAsync().AsTask()!,
                           GlobalSetup._msSqlContainer?.DisposeAsync().AsTask()!,
                           GlobalSetup._mySqlContainer?.DisposeAsync().AsTask()!);
    }

#endregion
}
