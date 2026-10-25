using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.ServiceCollectionExtensionsTests;

[TestFixture]
public class AddDefaultCacheTests
{
    [Test]
    public void Should_ReturnDefaultCache_When_CorrectConfig()
    {
        Dictionary<string, string?> config = new() { { "Zhomfr:Cache:Default:Prefix", "default" }, { "Zhomfr:Cache:Another:Prefix", "Another" } };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        Assert.That(services.AddDefaultCache<StubCache, StubConfig>(configuration), Is.InstanceOf<IServiceCollection>());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        StubCache? hashService = serviceProvider.GetService<ICache>() as StubCache;

        Assert.That(hashService, Is.Not.Null);
        Assert.That(hashService.Config.Prefix, Is.EqualTo("default"));
    }

    [TestCase("test")]
    [TestCase("test1234")]
    public void Should_Throw_When_InvalidConfig(string input)
    {
        Dictionary<string, string?> config = new() { { "Zhomfr:Cache:Default:Prefix", input }, { "Zhomfr:Cache:Another:Prefix", "Another" } };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        Assert.That(services.AddDefaultCache<StubCache, StubConfig>(configuration), Is.InstanceOf<IServiceCollection>());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(() =>
        {
            ICache _ = serviceProvider.GetRequiredService<ICache>();
        });
    }
}
