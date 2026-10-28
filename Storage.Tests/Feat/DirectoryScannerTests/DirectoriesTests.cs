using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryScannerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class DirectoriesTests(TestTypes type)
{
    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnList_When_DirectoryExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> dirs = [.. tested.Directories(path)];

            Assert.That(dirs, Has.Count.EqualTo(2));
        }
    }

    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnList_When_DirectoryExistsAndHiddenIsTrue(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> dirs = [.. tested.Directories(path, true)];

            Assert.That(dirs, Has.Count.EqualTo(3));
        }
    }

    [TestCase("file1.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/dir1/file1.txt")]
    public void Should_ReturnEmptyList_When_DirectoryIsFile(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.Directories(path), Is.Empty);
        }
    }

    [TestCase("not")]
    [TestCase("dir1/not")]
    [TestCase("/not")]
    [TestCase("/dir1/not")]
    public void Should_ReturnEmptyList_When_DirectoryNotExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);
            Assert.That(tested.Directories(path), Is.Empty);
        }
    }

    [TestCase("/empty")]
    [TestCase("/dir1/empty")]
    [TestCase("empty")]
    [TestCase("dir1/empty")]
    public void Should_ReturnEmptyList_When_DirectoryEmpty(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.Directories(path), Is.Empty);
        }
    }
}
