using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class CleanDirectoryAsyncTests(TestTypes type)
{
    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    [TestCase("/empty")]
    [TestCase("/dir1/empty")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_DirectoryExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.CleanDirectoryAsync(path, ct), Is.True);

            Assert.That(await tested.IsEmptyDirectoryAsync(path, ct), Is.True);
        }
    }

    [TestCase("not")]
    [TestCase("dir1/not")]
    [TestCase("/not")]
    [TestCase("/dir1/not")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_DirectoryNotExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
            Assert.That(await tested.CleanDirectoryAsync(path, ct), Is.False);
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
        }
    }

    [TestCase("file1.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_DirectoryIsFile(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.CleanDirectoryAsync(path, ct), Is.False);
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }
}
