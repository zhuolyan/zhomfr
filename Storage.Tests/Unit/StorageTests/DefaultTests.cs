using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Unit.StorageTests;

[TestFixture]
public class DefaultTests
{
    [Test]
    public void Should_ReturnDefault_When_Called()
    {
        Assert.That(IStorage.DefaultDateTime, Is.EqualTo(new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero)));
    }
}
