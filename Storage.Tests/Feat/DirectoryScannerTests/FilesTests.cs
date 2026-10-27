using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryScannerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class FilesTests(TestTypes type)
{
    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnFilesList_When_DirectoryExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> files = [.. tested.Files(path)];

            Assert.That(files, Has.Count.EqualTo(2));
            Assert.That(files, Has.All.Match("^file[0-9].txt$"));
        }
    }

    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnFilesList_When_DirectoryExistsAndHiddenIsTrue(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            List<string> files = [.. tested.Files(path)];

            Assert.That(files, Has.Count.EqualTo(4));
            Assert.That(files, Has.All.Match("^file[0-9].txt$"));
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
            Assert.That(tested.Files(path), Is.Empty);
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
            Assert.That(tested.Files(path), Is.Empty);
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
            Assert.That(tested.Files(path), Is.Empty);
        }
    }
}
