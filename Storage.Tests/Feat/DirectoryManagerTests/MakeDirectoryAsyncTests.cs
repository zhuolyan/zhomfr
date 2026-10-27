using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class MakeDirectoryAsyncTests(TestTypes type)
{
    [TestCase("not")]
    [TestCase("not/not")]
    [TestCase("dir1/not")]
    [TestCase("/not")]
    [TestCase("/not/not")]
    [TestCase("/dir1/not")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_DirectoryNotExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
            Assert.That(await tested.MakeDirectoryAsync(path, ct: ct), Is.True);
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }

    [TestCase("not")]
    [TestCase("/not")]
    [TestCase("/dir1/not")]
    [TestCase("dir1/not")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_DirectoryNotExistsAndRecursiveFalse(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
            Assert.That(await tested.MakeDirectoryAsync(path, false, ct), Is.True);
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }

    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_DirectoryExists(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
            Assert.That(await tested.MakeDirectoryAsync(path, ct: ct), Is.False);
            Assert.That(await tested.ExistsAsync(path, ct), Is.True);
        }
    }

    [TestCase("not/not")]
    [TestCase("dir1/not/not")]
    [TestCase("/not/not")]
    [TestCase("/dir1/not/not")]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_DirectoryNotExistsAndRecursiveFalse(string path, CancellationToken ct)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
            Assert.That(await tested.MakeDirectoryAsync(path, false, ct), Is.False);
            Assert.That(await tested.MissingAsync(path, ct), Is.True);
        }
    }
}
