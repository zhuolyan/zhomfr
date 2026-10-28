using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileSystemStateTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class IsHiddenTests(TestTypes type)
{
    [TestCase("file3.txt")]
    [TestCase("file4.txt")]
    [TestCase("/file3.txt")]
    [TestCase("/file4.txt")]
    [TestCase("dir1/file3.txt")]
    [TestCase("dir1/file4.txt")]
    [TestCase("/dir1/file3.txt")]
    [TestCase("/dir1/file4.txt")]
    [TestCase("dir1/dir1/file3.txt")]
    [TestCase("dir1/dir1/file4.txt")]
    [TestCase("/dir1/dir1/file3.txt")]
    [TestCase("/dir1/dir1/file4.txt")]
    [TestCase("dir2")]
    [TestCase("/dir2")]
    [TestCase("dir1/dir2")]
    [TestCase("/dir1/dir2")]
    public void Should_ReturnTrue_When_ExistsAndIsHidden(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.IsHidden(path), Is.True);
        }
    }

    [TestCase("file1.txt")]
    [TestCase("file2.txt")]
    [TestCase("/file1.txt")]
    [TestCase("/file2.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("dir1/file2.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("/dir1/file2.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("dir1/dir1/file2.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file2.txt")]
    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnFalse_When_ExistsAndIsVisible(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.IsHidden(path), Is.False);
        }
    }

    [TestCase("/no")]
    [TestCase("no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    public void Should_ReturnFalse_When_NotExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.False);
            Assert.That(tested.IsHidden(path), Is.False);
        }
    }
}
