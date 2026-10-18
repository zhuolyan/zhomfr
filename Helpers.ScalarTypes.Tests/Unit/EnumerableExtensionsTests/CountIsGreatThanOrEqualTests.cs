using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.EnumerableExtensionsTests;

[TestFixture]
public class CountIsGreatThanOrEqualTests
{
    [TestCase(2)]
    [TestCase(3)]
    public void Should_ReturnTrue_When_CountIsGreatThanOrEqual(int count)
    {
        Assert.That(EnumerableStub.GetValue().CountIsGreatThanOrEqual(count), Is.True);
    }

    [Test]
    public void Should_ReturnFalse_When_CountIsLessThan()
    {
        Assert.That(EnumerableStub.GetValue().CountIsGreatThanOrEqual(4), Is.False);
    }
}
