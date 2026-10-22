using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class DisposeTests
{
    [Test]
    public void Should_ReturnImmediately_When_CalledMultipleTimes()
    {
        DisposeSpyStream spy  = new();
        SmartFile        file = new(spy, "test.txt");

        file.Dispose();
        file.Dispose();

        Assert.That(spy.DisposeCount, Is.EqualTo(1));
    }

    [Test]
    public void Should_CloseStreamCorrectly_When_Disposed()
    {
        MemoryStream stream = new([1, 2, 3]);
        SmartFile    file   = new(stream, "test.bin");

        file.Dispose();

        Assert.Throws<ObjectDisposedException>(() => _ = stream.Length);
    }

    [Test]
    public async Task Should_ReturnImmediatelyOnSecondCall_When_CalledTwice()
    {
        DisposeSpyStream spy  = new();
        SmartFile        file = new(spy, "test.txt");

        await file.DisposeAsync();
        await file.DisposeAsync();

        Assert.That(spy.DisposeCount, Is.EqualTo(1));
    }

    [Test]
    public async Task Should_CloseStreamCorrectly_When_DisposedAsync()
    {
        MemoryStream stream = new([1, 2, 3]);
        SmartFile    file   = new(stream, "test.bin");

        await file.DisposeAsync();

        Assert.Throws<ObjectDisposedException>(() => _ = stream.Length);
    }
}
