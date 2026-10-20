using Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Unit.EnumerableExtensionsTests;

[TestFixture]
public class CountIsLessThanOrEqualTests
{
    [TestCase(3)]
    [TestCase(4)]
    public void Should_ReturnTrue_When_CountIsLessThanOrEqual(int count)
    {
        Assert.That(EnumerableStub.GetValue().CountIsLessThanOrEqual(count), Is.True);
    }

    [Test]
    public void Should_ReturnFalse_When_CountIsGreatThan()
    {
        Assert.That(EnumerableStub.GetValue().CountIsLessThanOrEqual(2), Is.False);
    }
}
