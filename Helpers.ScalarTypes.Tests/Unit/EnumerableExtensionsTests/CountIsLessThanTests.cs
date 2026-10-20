using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.EnumerableExtensionsTests;

[TestFixture]
public class CountIsLessThanTests
{
    [Test]
    public void Should_ReturnTrue_When_CountIsGreatThan()
    {
        Assert.That(EnumerableStub.GetValue().CountIsLessThan(4), Is.True);
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnFalse_When_CountIsLessThanOrEqual(int count)
    {
        Assert.That(EnumerableStub.GetValue().CountIsLessThan(count), Is.False);
    }
}
