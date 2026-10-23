using System.Net.Mime;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class GetRealMimeTypeTests
{
    [Test]
    [CancelAfter(5_000)]
    public async Task Should_ReturnDetectedType_When_MatchIsFound(CancellationToken ct)
    {
        HttpClient         request = new();
        await using Stream image   = await request.GetStreamAsync("https://mockimage.tw/photo/320x240/ffffff/000000/Hello%20World", ct);

        await using SmartFile file = new(image);

        Assert.That(file.ContentType, Is.EqualTo(MediaTypeNames.Image.Png));
    }

    [Test]
    [CancelAfter(5_000)]
    public async Task Should_ReturnFallback_When_NoMatchIsFound(CancellationToken ct)
    {
        byte[]                randomBytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];
        await using SmartFile file        = new(randomBytes);

        Assert.That(file.ContentType, Is.EqualTo(MediaTypeNames.Application.Octet));
    }
}
