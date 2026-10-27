using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class DeleteAsyncTests(TestTypes type)
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
    public async Task Should_ReturnTrue_When_FileExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            Assert.That(await tested.DeleteAsync(path, ct), Is.True);

            Assert.That(await tested.MissingAsync(path, ct), Is.True);
        }
    }

    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_FileIsDir(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            Assert.That(await tested.DeleteAsync(path, ct), Is.False);

            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }

    [TestCase("no")]
    [TestCase("/no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_FileNotExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);

            Assert.That(await tested.DeleteAsync(path, ct), Is.False);

            Assert.That(await tested.MissingAsync(path, ct), Is.True);
        }
    }
}
