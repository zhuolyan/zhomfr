using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileSystemMetadataTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class SizeTests(TestTypes type)
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
    public void Should_ReturnRealSize_When_IsFile(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.Size(path), Is.EqualTo(9));
        }
    }

    [TestCase("/no")]
    [TestCase("no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    public void Should_ReturnZero_When_FileNotExist(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.False);
            Assert.That(tested.Size(path), Is.EqualTo(0));
        }
    }

    [TestCase("dir1", 252)]
    [TestCase("/dir1", 252)]
    [TestCase("dir1/dir1", 108)]
    [TestCase("/dir1/dir1", 108)]
    public void Should_ReturnRealSize_When_IsDir(string path, int size)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.Size(path), Is.EqualTo(size));
        }
    }
}
