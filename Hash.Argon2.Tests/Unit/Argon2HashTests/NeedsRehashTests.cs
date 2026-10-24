using System.Security.Cryptography;
using System.Text;

using Konscious.Security.Cryptography;

namespace Zhomfr.Hash.Argon2.Tests.Unit.Argon2HashTests;

public class NeedsRehashTests
{
    private const string PASSWORD = "test";
    private       string _hashString;

    [OneTimeSetUp]
    public void Setup()
    {
        byte[] salt = new byte[4];
        RandomNumberGenerator.Fill(salt);

        Argon2id argon2 = new(Encoding.UTF8.GetBytes(NeedsRehashTests.PASSWORD))
        {
            Salt = salt, DegreeOfParallelism = 2, Iterations = 2, MemorySize = 1024, KnownSecret = [.. "test"u8],
        };

        this._hashString = $"v1${2}${1024}${2}${Convert.ToBase64String(salt)}${Convert.ToBase64String(argon2.GetBytes(1024))}";
    }

    [Test]
    public void Should_ReturnFalse_When_NotNeedsRehash()
    {
        Argon2Hash tested = new(new() { Secret = "test", Iterations = 2, MemorySize = 1024, DegreeOfParallelism = 2, SaltSize = 4 });

        Assert.That(tested.NeedsRehash(this._hashString), Is.False);
    }

    [TestCase(1, 2, 1024, 4)]
    [TestCase(3, 2, 1024, 4)]
    [TestCase(2, 1, 1024, 4)]
    [TestCase(2, 3, 1024, 4)]
    [TestCase(2, 1, 1023, 4)]
    [TestCase(2, 3, 1025, 4)]
    [TestCase(2, 2, 1024, 3)]
    [TestCase(2, 2, 1024, 5)]
    public void Should_ReturnTrue_When_ChangesDegreeOfParallelism(int iterations, int dop, int mem, int saltSize)
    {
        Argon2Hash tested = new(new() { Secret = "test", Iterations = iterations, MemorySize = mem, DegreeOfParallelism = dop, SaltSize = saltSize });

        Assert.That(tested.NeedsRehash(this._hashString), Is.True);
    }

    [TestCase("v1$2$4$2$")]
    [TestCase("v1$2$4$2$soem$")]
    [TestCase("$2$4$2$soem$hesh")]
    [TestCase("v1$24$2$soem$hesh")]
    public void Should_ReturnThrow_When_InvalidHashFormat(string hash)
    {
        Argon2Hash tested = new(new() { Secret = "test", Iterations = 2, MemorySize = 1024, DegreeOfParallelism = 2, SaltSize = 4 });

        Assert.Throws<ArgumentException>(() => tested.NeedsRehash(hash));
    }
}
