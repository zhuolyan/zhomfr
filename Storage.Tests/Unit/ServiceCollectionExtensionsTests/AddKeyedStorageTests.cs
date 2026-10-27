using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Zhomfr.Storage.Abstractions;
using Zhomfr.Storage.Tests.Stubs;

namespace Zhomfr.Storage.Tests.Unit.ServiceCollectionExtensionsTests;

[TestFixture]
public class AddKeyedStorageTests
{
    [Test]
    public void Should_ReturnDefaultStorage_When_CorrectConfig()
    {
        const string BASE_URL = "https://localhost/storage/another";
        const string ROOT_DIR = "/root/dir/to/storage/another";
        const string KEY      = "Another";

        Dictionary<string, string?> config = new()
        {
            { "Zhomfr:Storage:Default:BaseUrl", "https://localhost/storage" },
            { "Zhomfr:Storage:Default:RootDir", "/root/dir/to/storage" },
            { $"Zhomfr:Storage:{KEY}:BaseUrl", BASE_URL },
            { $"Zhomfr:Storage:{KEY}:RootDir", ROOT_DIR },
        };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        Assert.That(services.AddKeyedStorage<StubStorage, StubConfig>(KEY, configuration), Is.InstanceOf<IServiceCollection>());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        StubStorage? tested1 = serviceProvider.GetKeyedService<IDirectoryManager>(KEY) as StubStorage;
        StubStorage? tested2 = serviceProvider.GetKeyedService<IDirectoryScanner>(KEY) as StubStorage;
        StubStorage? tested3 = serviceProvider.GetKeyedService<IFileManager>(KEY) as StubStorage;
        StubStorage? tested4 = serviceProvider.GetKeyedService<IFileSystemMetadata>(KEY) as StubStorage;
        StubStorage? tested5 = serviceProvider.GetKeyedService<IFileSystemState>(KEY) as StubStorage;
        StubStorage? tested6 = serviceProvider.GetKeyedService<IFileSystemTypeInspector>(KEY) as StubStorage;
        StubStorage? tested7 = serviceProvider.GetKeyedService<IFileSystemMetadata>(KEY) as StubStorage;
        StubStorage? tested8 = serviceProvider.GetKeyedService<IStorage>(KEY) as StubStorage;

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested1, Is.Not.Null);
            Assert.That(tested1.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested1.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested2, Is.Not.Null);
            Assert.That(tested2.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested2.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested3, Is.Not.Null);
            Assert.That(tested3.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested3.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested4, Is.Not.Null);
            Assert.That(tested4.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested4.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested5, Is.Not.Null);
            Assert.That(tested5.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested5.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested6, Is.Not.Null);
            Assert.That(tested6.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested6.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested7, Is.Not.Null);
            Assert.That(tested7.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested7.Config.RootDir, Is.EqualTo(ROOT_DIR));

            Assert.That(tested8, Is.Not.Null);
            Assert.That(tested8.Config.BaseUrl, Is.EqualTo(BASE_URL));
            Assert.That(tested8.Config.RootDir, Is.EqualTo(ROOT_DIR));
        }
    }

    [TestCase("test")]
    [TestCase("test1234")]
    public void Should_Throw_When_InvalidConfig(string input)
    {
        const string KEY = "Another";

        Dictionary<string, string?> config = new()
        {
            { "Zhomfr:Storage:Default:BaseUrl", "https://localhost/storage" },
            { "Zhomfr:Storage:Default:RootDir", "/root/dir/to/storage" },
            { $"Zhomfr:Storage:{KEY}:BaseUrl", input },
            { $"Zhomfr:Storage:{KEY}:RootDir", "/root/dir/to/storage/another" },
        };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        Assert.That(services.AddKeyedStorage<StubStorage, StubConfig>(KEY, configuration), Is.InstanceOf<IServiceCollection>());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(() =>
        {
            IStorage _ = serviceProvider.GetRequiredKeyedService<IStorage>(KEY);
        });
    }
}
