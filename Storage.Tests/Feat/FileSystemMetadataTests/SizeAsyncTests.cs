using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileSystemMetadataTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class SizeAsyncTests(TestTypes type)
{
    [TestCase("file1.txt")]
    [TestCase("file3.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/file3.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("dir1/file3.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("/dir1/file3.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("dir1/dir1/file3.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file3.txt")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnRealSize_When_IsFile(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.SizeAsync(path, ct), Is.EqualTo(9));
        }
    }

    [TestCase("/no")]
    [TestCase("no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnZero_When_FileNotExist(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.False);
            Assert.That(await tested.SizeAsync(path, ct), Is.EqualTo(0));
        }
    }

    [TestCase("dir1", 252)]
    [TestCase("/dir1", 252)]
    [TestCase("dir1/dir1", 108)]
    [TestCase("/dir1/dir1", 108)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnRealSize_When_IsDir(string path, int size, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.SizeAsync(path, ct), Is.EqualTo(size));
        }
    }
}
