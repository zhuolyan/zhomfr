using Testcontainers.Redis;

namespace Zhomfr.Cache.Tests;

[SetUpFixture]
public class GlobalSetup
{
    private static RedisContainer? _container;

    // Статичні властивості, через які тести зможуть звертатися до Redis
    public static string RedisConnection =>
        GlobalSetup._container?.GetConnectionString() ?? throw new InvalidOperationException("Redis container is not initialized.");

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        // Запускається один раз перед усіма тестами в проєкті
        GlobalSetup._container = new RedisBuilder("redis:7.0").Build();
        await GlobalSetup._container.StartAsync();
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        if (GlobalSetup._container != null) {
            await GlobalSetup._container.DisposeAsync();
        }
    }
}
