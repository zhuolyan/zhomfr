using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.EnumerableExtensionsTests;

[TestFixture]
public class BetweenTests
{
    [Test]
    public void Should_ReturnTrue_When_CountIsBetweenInclusive()
    {
        Assert.That(EnumerableStub.GetValue().CountIsBetween(1, 3), Is.True);
    }

    [Test]
    public void Should_ReturnFalse_When_CountIsGreaterThan()
    {
        Assert.That(EnumerableStub.GetValue().CountIsBetween(1, 2), Is.False);
    }

    [Test]
    public void Should_ReturnFalse_When_CountIsLessThan()
    {
        Assert.That(EnumerableStub.GetValue().CountIsBetween(4, 5), Is.False);
    }

    [Test]
    public void Should_Throw_When_MinGreaterThanMax()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => EnumerableStub.GetValue().CountIsBetween(5, 4));
    }
}
