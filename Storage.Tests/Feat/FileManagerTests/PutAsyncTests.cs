using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class PutAsyncTests(TestTypes type)
{
    private const string CONTENT = "test";

    [TestCase("new.txt")]
    [TestCase("new")]
    [TestCase("/new.txt")]
    [TestCase("/new")]
    [TestCase("dir1/new.txt")]
    [TestCase("dir1/new")]
    [TestCase("/dir1/new.txt")]
    [TestCase("/dir1/new")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_FileNotExists(string path, CancellationToken ct)
    {
        IStorage              tested = GlobalSetup.GetTested(type);
        await using SmartFile file   = new(PutAsyncTests.CONTENT);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);

            Assert.That(await tested.PutAsync(path, file, ct), Is.True);

            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            await using SmartFile file2 = (await tested.GetAsync(path, ct))!;

            Assert.That(await file2.AsStringAsync(ct), Is.EqualTo(PutAsyncTests.CONTENT));
        }
    }

    [TestCase("file1.txt")]
    [TestCase("/file1.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_FileExists(string path, CancellationToken ct)
    {
        IStorage              tested = GlobalSetup.GetTested(type);
        await using SmartFile file   = new(PutAsyncTests.CONTENT);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            await using SmartFile file2 = (await tested.GetAsync(path, ct))!;

            Assert.That(await file2.AsStringAsync(ct), Is.Not.EqualTo(PutAsyncTests.CONTENT));

            Assert.That(await tested.PutAsync(path, file, ct), Is.True);

            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            await using SmartFile file3 = (await tested.GetAsync(path, ct))!;

            Assert.That(await file3.AsStringAsync(ct), Is.EqualTo(PutAsyncTests.CONTENT));
        }
    }

    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_FileIsDir(string path, CancellationToken ct)
    {
        IStorage              tested = GlobalSetup.GetTested(type);
        await using SmartFile file   = new(PutAsyncTests.CONTENT);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);

            Assert.That(await tested.PutAsync(path, file, ct), Is.False);

            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }
}
