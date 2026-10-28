using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class MoveAsyncTests(TestTypes type)
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
    public async Task Should_ReturnTrue_When_OldFileExistsAndNewNotExists(string oldPath, CancellationToken ct)
    {
        const string NEW_PATH = "new.txt";

        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(oldPath, ct), Is.True);
            Assert.That(await tested.MissingAsync(NEW_PATH, ct), Is.True);

            Assert.That(await tested.MoveAsync(oldPath, NEW_PATH, ct), Is.True);

            Assert.That(await tested.MissingAsync(oldPath, ct), Is.True);
            Assert.That(await tested.ExistsAsync(NEW_PATH, ct), Is.True);
        }
    }

    [TestCase("no.txt")]
    [TestCase("/no.txt")]
    [TestCase("dir1/no.txt")]
    [TestCase("/dir1/no.txt")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_OldFileNotExistsAndNewNotExists(string oldPath, CancellationToken ct)
    {
        const string NEW_PATH = "new.txt";

        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(oldPath, ct), Is.True);
            Assert.That(await tested.MissingAsync(NEW_PATH, ct), Is.True);

            Assert.That(await tested.MoveAsync(oldPath, NEW_PATH, ct), Is.False);

            Assert.That(await tested.MissingAsync(oldPath, ct), Is.True);
            Assert.That(await tested.MissingAsync(NEW_PATH, ct), Is.True);
        }
    }

    [TestCase("file1.txt", "file2.txt")]
    [TestCase("/file1.txt", "file2.txt")]
    [TestCase("dir1/file1.txt", "/file2.txt")]
    [TestCase("/dir1/file1.txt", "/file2.txt")]
    [TestCase("dir1/dir1/file1.txt", "/file2.txt")]
    [TestCase("/dir1/dir1/file1.txt", "file2.txt")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_OldFileExistsAndNewExists(string oldPath, string newPath, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(oldPath, ct), Is.True);
            Assert.That(await tested.ExistsAsync(newPath, ct), Is.True);

            Assert.That(await tested.MoveAsync(oldPath, newPath, ct), Is.False);

            Assert.That(await tested.ExistsAsync(oldPath, ct), Is.True);
            Assert.That(await tested.ExistsAsync(newPath, ct), Is.True);

            SmartFile file1 = (await tested.GetAsync(oldPath, ct))!;
            SmartFile file2 = (await tested.GetAsync(newPath, ct))!;

            string value1 = await file1.AsStringAsync(ct);
            string value2 = await file2.AsStringAsync(ct);

            Assert.That(value1, Is.Not.EqualTo(value2));
        }
    }
}
