namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class AsBytesTests
{
    private const string FILENAME = "test.bin";

    [Test]
    public void Should_UseCopyTo_When_StreamIsRegular()
    {
        byte[]               data         = [1, 2, 3];
        using BufferedStream sourceStream = new(new MemoryStream(data));
        using SmartFile      file         = new(sourceStream, AsBytesTests.FILENAME);

        byte[] result = file.AsBytes();

        Assert.That(result, Is.EqualTo(data));
    }

    [Test]
    public void Should_ReturnBytesDirectly_When_StreamIsMemoryStream()
    {
        byte[]             data         = [1, 2, 3];
        using MemoryStream sourceStream = new(data);
        using SmartFile    file         = new(sourceStream, AsBytesTests.FILENAME);

        byte[] result = file.AsBytes();

        Assert.That(result, Is.EqualTo(data));
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Should_ReturnBytesDirectly_When_StreamIsMemoryStream(CancellationToken ct)
    {
        byte[]                expected = [10, 20, 30];
        await using SmartFile file     = new(expected, AsBytesTests.FILENAME);

        byte[] result = await file.AsBytesAsync(ct);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Should_UseCopyToAsync_When_StreamIsRegular(CancellationToken ct)
    {
        byte[] data = [1, 2, 3, 4, 5];

        await using BufferedStream sourceStream = new(new MemoryStream(data));
        await using SmartFile      file         = new(sourceStream, AsBytesTests.FILENAME);

        byte[] result = await file.AsBytesAsync(ct);

        Assert.That(result, Is.EqualTo(data));
    }
}
