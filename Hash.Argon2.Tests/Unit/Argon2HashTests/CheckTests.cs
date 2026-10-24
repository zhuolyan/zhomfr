using System.Security.Cryptography;
using System.Text;

using Konscious.Security.Cryptography;

namespace Zhomfr.Hash.Argon2.Tests.Unit.Argon2HashTests;

public class CheckTests
{
    private const string     PASSWORD = "test";
    private       string     _hashString;
    private       Argon2Hash _tested;

    [OneTimeSetUp]
    public void Setup()
    {
        this._tested = new(new() { Secret = "test", Iterations = 2, MemorySize = 8, DegreeOfParallelism = 2, SaltSize = 4 });

        byte[] salt = new byte[4];
        RandomNumberGenerator.Fill(salt);

        Argon2id argon2 = new(Encoding.UTF8.GetBytes(CheckTests.PASSWORD))
        {
            Salt = salt, DegreeOfParallelism = 2, Iterations = 2, MemorySize = 8, KnownSecret = [.. "test"u8],
        };

        this._hashString = $"v1${2}${8}${2}${Convert.ToBase64String(salt)}${Convert.ToBase64String(argon2.GetBytes(8))}";
    }

    [Test]
    public void Should_ReturnTrue_When_PasswordIsCorrect()
    {
        Assert.That(this._tested.Check(CheckTests.PASSWORD, this._hashString), Is.True);
    }

    [TestCase("Test")]
    [TestCase("TEst")]
    [TestCase("test ")]
    [TestCase(" test")]
    public void Should_ReturnFalse_When_PasswordIsInvalid(string password)
    {
        Assert.That(this._tested.Check(password, this._hashString), Is.False);
    }

    [TestCase("v1$2$4$2$")]
    [TestCase("v1$2$4$2$soem$")]
    [TestCase("$2$4$2$soem$hesh")]
    [TestCase("v1$24$2$soem$hesh")]
    public void Should_ReturnThrow_When_InvalidHashFormat(string hash)
    {
        Assert.Throws<ArgumentException>(() => this._tested.Check(CheckTests.PASSWORD, hash));
    }
}
