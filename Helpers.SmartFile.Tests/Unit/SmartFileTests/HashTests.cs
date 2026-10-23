using System.Security.Cryptography;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class HashTests
{
    public const string FILE_CONTENT = "exists";

    [Test]
    [TestCase(nameof(HashAlgorithmName.SHA256), "ad5aabc97d88f6e56deaffc34c7a6e292e9e12db04a56799d773cfb64ca9898d")]
    [TestCase(nameof(HashAlgorithmName.SHA512),
              "f7699b90a6a16924a3e3b26ce3ace114487ba5fe89505669af2e94291a4275ac7ab86e497a729ecee6c7ea9e41ba256d97bfa1eb494fd7f025f94cb6933d889d")]
    [TestCase(nameof(HashAlgorithmName.MD5), "e087923eb5dd1310f5f25ddd5ae5b580")]
    [TestCase(nameof(HashAlgorithmName.SHA1), "4d68c8f13459c0edb40504de5003ec2a6b74e613")]
    public void Should_ReturnCorrectHash_When_Called(string stringAlgorithmName, string expectedHash)
    {
        HashAlgorithmName algorithmName = new(stringAlgorithmName);

        using SmartFile tested = new(HashTests.FILE_CONTENT, "test.txt");

        Assert.That(tested.Hash(algorithmName), Is.EqualTo(expectedHash));
    }

    [Test]
    public void Should_ReturnCorrectHash_When_Called()
    {
        using SmartFile tested = new(HashTests.FILE_CONTENT, "test.txt");

        Assert.Throws<NotSupportedException>(() => tested.Hash(HashAlgorithmName.SHA384));
    }

    [Test]
    [CancelAfter(1_000)]
    [TestCase(nameof(HashAlgorithmName.SHA256), "ad5aabc97d88f6e56deaffc34c7a6e292e9e12db04a56799d773cfb64ca9898d")]
    [TestCase(nameof(HashAlgorithmName.SHA512),
              "f7699b90a6a16924a3e3b26ce3ace114487ba5fe89505669af2e94291a4275ac7ab86e497a729ecee6c7ea9e41ba256d97bfa1eb494fd7f025f94cb6933d889d")]
    [TestCase(nameof(HashAlgorithmName.MD5), "e087923eb5dd1310f5f25ddd5ae5b580")]
    [TestCase(nameof(HashAlgorithmName.SHA1), "4d68c8f13459c0edb40504de5003ec2a6b74e613")]
    public async Task Should_ReturnCorrectHash_When_Called(string stringAlgorithmName, string expectedHash, CancellationToken ct)
    {
        HashAlgorithmName algorithmName = new(stringAlgorithmName);

        await using SmartFile tested = new(HashTests.FILE_CONTENT, "test.txt");

        Assert.That(await tested.HashAsync(algorithmName, ct), Is.EqualTo(expectedHash));
    }

    [Test]
    [CancelAfter(1_000)]
    public async Task Should_ReturnCorrectHash_When_Called(CancellationToken ct)
    {
        await using SmartFile tested = new(HashTests.FILE_CONTENT, "test.txt");

        await Assert.ThrowsAsync<NotSupportedException>(() => _ = tested.HashAsync(HashAlgorithmName.SHA384, ct));
    }
}
