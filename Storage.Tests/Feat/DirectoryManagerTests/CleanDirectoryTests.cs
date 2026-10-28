using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class CleanDirectoryTests(TestTypes type)
{
    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    [TestCase("/empty")]
    [TestCase("/dir1/empty")]
    public void Should_ReturnTrue_When_DirectoryExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.CleanDirectory(path), Is.True);
            Assert.That(tested.IsEmptyDirectory(path), Is.True);
        }
    }

    [TestCase("not")]
    [TestCase("dir1/not")]
    [TestCase("/not")]
    [TestCase("/dir1/not")]
    public void Should_ReturnFalse_When_DirectoryNotExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);
            Assert.That(tested.CleanDirectory(path), Is.False);
            Assert.That(tested.Missing(path), Is.True);
        }
    }

    [TestCase("file1.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    public void Should_ReturnFalse_When_DirectoryIsFile(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.CleanDirectory(path), Is.False);
            Assert.That(tested.Exists(path), Is.True);
        }
    }
}
