using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.EnumerableExtensionsTests;

[TestFixture]
public class CountIsGreatThanTests
{
    [Test]
    public void Should_ReturnTrue_When_CountIsGreatThan()
    {
        Assert.That(EnumerableStub.GetValue().CountIsGreatThan(2), Is.True);
    }

    [TestCase(3)]
    [TestCase(4)]
    public void Should_ReturnFalse_When_CountIsLessThanOrEqual(int count)
    {
        Assert.That(EnumerableStub.GetValue().CountIsGreatThan(count), Is.False);
    }
}
