using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Zhomfr.Storage.Abstractions;
using Zhomfr.Storage.Tests.Stubs;

namespace Zhomfr.Storage.Tests.Unit.ServiceCollectionExtensionsTests;

[TestFixture]
public class AddDefaultStorageTests
{
    [Test]
    public void Should_ReturnDefaultStorage_When_CorrectConfig()
    {
        const string BASE_URL = "https://localhost/storage";
        const string ROOT_DIR = "/root/dir/to/storage";

        Dictionary<string, string?> config = new()
        {
            { "Zhomfr:Storage:Default:BaseUrl", BASE_URL },
            { "Zhomfr:Storage:Default:RootDir", ROOT_DIR },
            { "Zhomfr:Storage:Another:BaseUrl", "https://localhost/storage/another" },
            { "Zhomfr:Storage:Another:RootDir", "/root/dir/to/storage/another" },
        };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        Assert.That(services.AddDefaultStorage<StubStorage, StubConfig>(configuration), Is.InstanceOf<IServiceCollection>());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        StubStorage? tested1 = serviceProvider.GetService<IDirectoryManager>() as StubStorage;
        StubStorage? tested2 = serviceProvider.GetService<IDirectoryScanner>() as StubStorage;
        StubStorage? tested3 = serviceProvider.GetService<IFileManager>() as StubStorage;
        StubStorage? tested4 = serviceProvider.GetService<IFileSystemMetadata>() as StubStorage;
        StubStorage? tested5 = serviceProvider.GetService<IFileSystemState>() as StubStorage;
        StubStorage? tested6 = serviceProvider.GetService<IFileSystemTypeInspector>() as StubStorage;
        StubStorage? tested7 = serviceProvider.GetService<IFileSystemMetadata>() as StubStorage;
        StubStorage? tested8 = serviceProvider.GetService<IStorage>() as StubStorage;

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
        Dictionary<string, string?> config = new()
        {
            { "Zhomfr:Storage:Default:BaseUrl", input },
            { "Zhomfr:Storage:Default:RootDir", "/root/dir/to/storage" },
            { "Zhomfr:Storage:Another:BaseUrl", "https://localhost/storage/another" },
            { "Zhomfr:Storage:Another:RootDir", "/root/dir/to/storage/another" },
        };

        ServiceCollection  services      = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        Assert.That(services.AddDefaultStorage<StubStorage, StubConfig>(configuration), Is.InstanceOf<IServiceCollection>());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(() =>
        {
            IStorage _ = serviceProvider.GetRequiredService<IStorage>();
        });
    }
}
