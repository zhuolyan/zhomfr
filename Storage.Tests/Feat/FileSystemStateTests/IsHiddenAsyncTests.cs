using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileSystemStateTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class IsHiddenAsyncTests(TestTypes type)
{
    [TestCase("file3.txt")]
    [TestCase("file4.txt")]
    [TestCase("/file3.txt")]
    [TestCase("/file4.txt")]
    [TestCase("dir1/file3.txt")]
    [TestCase("dir1/file4.txt")]
    [TestCase("/dir1/file3.txt")]
    [TestCase("/dir1/file4.txt")]
    [TestCase("dir1/dir1/file3.txt")]
    [TestCase("dir1/dir1/file4.txt")]
    [TestCase("/dir1/dir1/file3.txt")]
    [TestCase("/dir1/dir1/file4.txt")]
    [TestCase("dir2")]
    [TestCase("/dir2")]
    [TestCase("dir1/dir2")]
    [TestCase("/dir1/dir2")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_ExistsAndIsHidden(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.IsHiddenAsync(path, ct), Is.True);
        }
    }

    [TestCase("file1.txt")]
    [TestCase("file2.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/file2.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("dir1/file2.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("/dir1/file2.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("dir1/dir1/file2.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file2.txt")]
    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_ExistsAndIsVisible(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.IsHiddenAsync(path, ct), Is.False);
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
            Assert.That(await tested.IsHiddenAsync(path, ct), Is.False);
        }
    }
}
