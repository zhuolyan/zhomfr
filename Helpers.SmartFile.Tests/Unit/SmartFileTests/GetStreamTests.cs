using System.Text;

using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class GetStreamTests
{
    [Test]
    public void Should_ReturnStream_When_Called()
    {
        byte[] buffer = new byte[FileFixtures.SAMPLE_TEXT.Length];

        using SmartFile file = new(FileFixtures.SAMPLE_TEXT, "test.bin");

        using Stream stream = file.GetStream();

        Assert.That(stream, Is.Not.Null);

        stream.ReadExactly(buffer);

        Assert.That(Encoding.UTF8.GetString(buffer), Is.EqualTo(FileFixtures.SAMPLE_TEXT));
    }

    [Test]
    public void Should_ThrowObjectDisposedException_When_CalledAfterDispose()
    {
        SmartFile file = new(FileFixtures.SAMPLE_TEXT, "test.txt");
        file.Dispose();

        Assert.Throws<ObjectDisposedException>(() => file.GetStream());
    }

    [Test]
    public void Should_EnableSeekingViaBuffering_When_StreamCannotSeek()
    {
        const string          CONTENT           = "Hello, Zhomfr Storage!";
        byte[]                bytes             = Encoding.UTF8.GetBytes(CONTENT);
        NonSeekableStreamStub nonSeekableStream = new(bytes);

        SmartFile storageFile  = new(nonSeekableStream, "test.txt");
        Stream    resultStream = storageFile.GetStream();

        Assert.Multiple(() =>
        {
            Assert.That(resultStream.CanSeek, Is.True, "Потік має стати Seekable після буферизації");

            using StreamReader reader        = new(resultStream, Encoding.UTF8, leaveOpen: true);
            string             actualContent = reader.ReadToEnd();

            Assert.That(actualContent, Is.EqualTo(CONTENT));
            Assert.DoesNotThrow(() => resultStream.Position = 0);
        });

        storageFile.Dispose();
    }
}
