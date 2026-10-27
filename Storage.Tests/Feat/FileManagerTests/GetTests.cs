using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class GetTests(TestTypes type)
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
    public void Should_ReturnFile_When_FileExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            using SmartFile? file = tested.Get(path);

            Assert.That(file, Is.Not.Null);
            Assert.That(tested.Exists(path), Is.True);
        }
    }

    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnNull_When_FileIsDir(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            using SmartFile? file = tested.Get(path);

            Assert.That(file, Is.Null);
            Assert.That(tested.Exists(path), Is.True);
        }
    }

    [TestCase("no")]
    [TestCase("/no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    public void Should_ReturnNull_When_FileNotExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);

            using SmartFile? file = tested.Get(path);

            Assert.That(file, Is.Null);
            Assert.That(tested.Missing(path), Is.True);
        }
    }
}
