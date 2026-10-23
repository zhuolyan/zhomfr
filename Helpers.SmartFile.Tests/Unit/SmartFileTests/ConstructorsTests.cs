using System.Net.Mime;

using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class ConstructorTests
{
    [Test]
    public void Should_ThrowArgumentNullException_When_StreamIsNull()
    {
        Stream? nullStream = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            SmartFile _ = new(nullStream!, "null.txt");
        });
    }

    [Test]
    public void Should_DefaultNameLikeGuidString_When_NameIsEmpty()
    {
        SmartFile smartFile = new(FileFixtures.SAMPLE_TEXT, string.Empty);

        Assert.That(Guid.TryParse(smartFile.FileName, out Guid result), Is.True);
        Assert.That(result, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public void Should_SetCorrectMetadata_When_InitializedWithStream()
    {
        const string NAME = "image.jpg";

        using Stream    stream = new DisposeSpyStream(FileFixtures.JpegBytes);
        using SmartFile file   = new(stream, NAME);

        using (Assert.EnterMultipleScope()) {
            Assert.That(file.FileName, Is.EqualTo(NAME));
            Assert.That(file.Length, Is.EqualTo(FileFixtures.JpegBytes.Length));
            Assert.That(file.Extension, Is.EqualTo("jpeg"));

            Assert.That(file.ContentType, Is.EqualTo(MediaTypeNames.Image.Jpeg));
        }
    }

    [Test]
    public void Should_SetCorrectMetadata_When_InitializedWithBytes()
    {
        byte[]       bytes = FileFixtures.JpegBytes;
        const string NAME  = "image.jpg";

        using SmartFile file = new(bytes, NAME);

        using (Assert.EnterMultipleScope()) {
            Assert.That(file.FileName, Is.EqualTo(NAME));
            Assert.That(file.Length, Is.EqualTo(bytes.Length));
            Assert.That(file.Extension, Is.EqualTo("jpeg"));

            Assert.That(file.ContentType, Is.EqualTo(MediaTypeNames.Image.Jpeg));
        }
    }

    [Test]
    public void Should_SetTextType_When_InitializedWithString()
    {
        using SmartFile file = new(FileFixtures.SAMPLE_TEXT, "data.txt");

        Assert.That(file.ContentType, Is.EqualTo(MediaTypeNames.Text.Plain));
    }

    [Test]
    public void Should_SetJsonMimeType_When_InitializedWithJsonObject()
    {
        using SmartFile file = new(FileFixtures.SampleObject, "data.json");

        Assert.That(file.ContentType, Is.EqualTo(MediaTypeNames.Application.Json));
    }
}
