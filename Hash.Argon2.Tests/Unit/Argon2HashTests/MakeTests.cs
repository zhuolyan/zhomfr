namespace Zhomfr.Hash.Argon2.Tests.Unit.Argon2HashTests;

public class MakeTests
{
    private const string     PASSWORD = "test";
    private       Argon2Hash _tested;

    [OneTimeSetUp]
    public void Setup()
    {
        this._tested = new(new() { Secret = "test", Iterations = 2, MemorySize = 8, DegreeOfParallelism = 2, SaltSize = 4 });
    }

    [Test]
    public void Should_ReturnUniqueHash_When_MakeIsCalled()
    {
        string hash1 = this._tested.Make(MakeTests.PASSWORD);
        string hash2 = this._tested.Make(MakeTests.PASSWORD);

        using (Assert.EnterMultipleScope()) {
            Assert.That(hash1, Does.StartWith("v1$"));
            Assert.That(hash1.Split('$'), Has.Length.EqualTo(6));
            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }
    }
}
