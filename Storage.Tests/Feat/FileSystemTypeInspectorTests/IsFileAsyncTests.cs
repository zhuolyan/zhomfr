using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileSystemTypeInspectorTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class IsFileAsyncTests(TestTypes type)
{
    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_IsDir(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.IsFileAsync(path, ct), Is.False);
        }
    }

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
    public async Task Should_ReturnTrue_When_IsFile(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.IsFileAsync(path, ct), Is.True);
        }
    }

    [TestCase("/no")]
    [TestCase("no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_NotExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.False);
            Assert.That(await tested.IsFileAsync(path, ct), Is.False);
        }
    }
}
