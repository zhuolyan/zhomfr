using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Zhomfr.Hash.Abstractions.Tests.Stubs;

namespace Zhomfr.Hash.Abstractions.Tests.Unit.ServiceCollectionExtensionsTests;

[TestFixture]
public class AddDefaultHashTests
{
    [Test]
    public void Should_ReturnDefaultHash_When_CorrectConfig()
    {
        Dictionary<string, string?> config = new() { { "Zhomfr:Hash:Default:Secret", "default" }, { "Zhomfr:Hash:Another:Secret", "Another" } };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        services.AddDefaultHash<StubHash, StubConfig>(configuration);

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        StubHash? hashService = serviceProvider.GetService<IHash>() as StubHash;

        Assert.That(hashService, Is.Not.Null);
        Assert.That(hashService.Config.Secret, Is.EqualTo("default"));
    }

    [TestCase("test")]
    [TestCase("test1234")]
    public void Should_Throw_When_InvalidConfig(string input)
    {
        Dictionary<string, string?> config = new() { { "Zhomfr:Hash:Default:Secret", input }, { "Zhomfr:Hash:Another:Secret", "Another" } };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        services.AddDefaultHash<StubHash, StubConfig>(configuration);

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(() =>
        {
            IHash _ = serviceProvider.GetRequiredService<IHash>();
        });
    }
}
