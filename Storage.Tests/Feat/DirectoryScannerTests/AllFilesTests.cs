using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryScannerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class AllFilesTests(TestTypes type)
{
    [TestCase("dir1", 6)]
    [TestCase("dir1/dir1", 4)]
    [TestCase("/dir1", 6)]
    [TestCase("/dir1/dir1", 4)]
    public void Should_ReturnList_When_DirectoryExists(string path, int count)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> files = [.. tested.AllFiles(path)];

            Assert.That(files, Has.Count.EqualTo(count));
            Assert.That(files, Has.All.Match("^.*file[1-2].txt$"));
        }
    }

    [TestCase("dir1", 28)]
    [TestCase("dir1/dir1", 12)]
    [TestCase("/dir1", 28)]
    [TestCase("/dir1/dir1", 12)]
    public void Should_ReturnList_When_DirectoryExistsAndHiddenIsTrue(string path, int count)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> files = [.. tested.AllFiles(path, true)];

            Assert.That(files, Has.Count.EqualTo(count));
            Assert.That(files, Has.All.Match("^.*file[1-4].txt$"));
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
            Assert.That(tested.AllFiles(path), Is.Empty);
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
            Assert.That(tested.AllFiles(path), Is.Empty);
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
            Assert.That(tested.AllFiles(path), Is.Empty);
        }
    }
}
