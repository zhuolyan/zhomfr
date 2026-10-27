using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryScannerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class AllDirectoriesTests(TestTypes type)
{
    [TestCase("dir1", 4)]
    [TestCase("dir1/dir1", 2)]
    [TestCase("/dir1", 4)]
    [TestCase("/dir1/dir1", 2)]
    public void Should_ReturnList_When_DirectoryExists(string path, int count)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> dirs = [.. tested.AllDirectories(path)];

            Assert.That(dirs, Has.Count.EqualTo(count));
            Assert.That(dirs, Has.All.Match("^dir[0-9].txt$"));
        }
    }

    [TestCase("dir1", 9)]
    [TestCase("dir1/dir1", 3)]
    [TestCase("/dir1", 9)]
    [TestCase("/dir1/dir1", 3)]
    public void Should_ReturnList_When_DirectoryExistsAndHiddenIsTrue(string path, int count)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> dirs = [.. tested.AllDirectories(path)];

            Assert.That(dirs, Has.Count.EqualTo(count));
            Assert.That(dirs, Has.All.Match("^dir[0-9].txt$"));
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
            Assert.That(tested.AllDirectories(path), Is.Empty);
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
            Assert.That(tested.AllDirectories(path), Is.Empty);
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
            Assert.That(tested.AllDirectories(path), Is.Empty);
        }
    }
}
