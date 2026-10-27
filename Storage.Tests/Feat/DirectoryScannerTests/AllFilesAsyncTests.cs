using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryScannerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class AllFilesAsyncTests(TestTypes type)
{
    [TestCase("dir1", 6)]
    [TestCase("dir1/dir1", 4)]
    [TestCase("/dir1", 6)]
    [TestCase("/dir1/dir1", 4)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnList_When_DirectoryExists(string path, int count, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            List<string> files = [.. await tested.AllFilesAsync(path, ct: ct).ToListAsync(cancellationToken: ct)];

            Assert.That(files, Has.Count.EqualTo(count));
            Assert.That(files, Has.All.Match("^file[0-9].txt$"));
        }
    }

    [TestCase("dir1", 21)]
    [TestCase("dir1/dir1", 9)]
    [TestCase("/dir1", 21)]
    [TestCase("/dir1/dir1", 9)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnList_When_DirectoryExistsAndHiddenIsTrue(string path, int count, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            List<string> files = [.. await tested.AllFilesAsync(path, true, ct).ToListAsync(cancellationToken: ct)];

            Assert.That(files, Has.Count.EqualTo(4));
            Assert.That(files, Has.All.Match("^file[0-9].txt$"));
        }
    }

    [TestCase("file1.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/dir1/file1.txt")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnEmptyList_When_DirectoryIsFile(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.AllFilesAsync(path, ct: ct).ToListAsync(cancellationToken: ct), Is.Empty);
        }
    }

    [TestCase("not")]
    [TestCase("dir1/not")]
    [TestCase("/not")]
    [TestCase("/dir1/not")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnEmptyList_When_DirectoryNotExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
            Assert.That(await tested.AllFilesAsync(path, ct: ct).ToListAsync(cancellationToken: ct), Is.Empty);
        }
    }

    [TestCase("/empty")]
    [TestCase("/dir1/empty")]
    [TestCase("empty")]
    [TestCase("dir1/empty")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnEmptyList_When_DirectoryEmpty(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.AllFilesAsync(path, ct: ct).ToListAsync(cancellationToken: ct), Is.Empty);
        }
    }
}
