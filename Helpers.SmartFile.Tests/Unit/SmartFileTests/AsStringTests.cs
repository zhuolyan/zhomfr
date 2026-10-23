using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class AsStringTests
{
    private const string FILENAME = "test.txt";

    [Test]
    public void Should_ReturnSameString_When_FileInitializeWithSting()
    {
        using SmartFile file = new(FileFixtures.SAMPLE_TEXT, AsStringTests.FILENAME);

        string result = file.AsString();

        Assert.That(result, Is.EqualTo(FileFixtures.SAMPLE_TEXT));
    }

    [Test]
    public void Should_ThrowObjectDisposedException_When_CalledAfterDispose()
    {
        SmartFile file = new(FileFixtures.SAMPLE_TEXT, AsStringTests.FILENAME);

        file.Dispose();

        Assert.Throws<ObjectDisposedException>(() => file.AsString());
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Should_BeCallableMultipleTimes_When_StreamIsLeftOpen(CancellationToken ct)
    {
        await using SmartFile file = new(FileFixtures.SAMPLE_TEXT, AsStringTests.FILENAME);

        string firstCall  = await file.AsStringAsync(ct);
        string secondCall = await file.AsStringAsync(ct);

        using (Assert.EnterMultipleScope()) {
            Assert.That(firstCall, Is.EqualTo(FileFixtures.SAMPLE_TEXT));
            Assert.That(secondCall, Is.EqualTo(FileFixtures.SAMPLE_TEXT));
        }
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Should_ThrowObjectDisposedException_When_CalledAfterDisposeAsync(CancellationToken ct)
    {
        SmartFile file = new(FileFixtures.SAMPLE_TEXT, AsStringTests.FILENAME);

        await file.DisposeAsync();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => file.AsStringAsync(ct));
    }
}
