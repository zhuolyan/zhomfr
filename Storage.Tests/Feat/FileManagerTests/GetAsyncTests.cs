using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class GetAsyncTests(TestTypes type)
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
    public async Task Should_ReturnFile_When_FileExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            await using SmartFile? file = await tested.GetAsync(path, ct);

            Assert.That(file, Is.Not.Null);
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }

    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnNull_When_FileIsDir(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            await using SmartFile? file = await tested.GetAsync(path, ct);

            Assert.That(file, Is.Null);
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }

    [TestCase("no")]
    [TestCase("/no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnNull_When_FileNotExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);

            await using SmartFile? file = await tested.GetAsync(path, ct);

            Assert.That(file, Is.Null);
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
        }
    }
}
