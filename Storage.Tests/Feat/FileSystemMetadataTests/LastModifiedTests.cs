using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileSystemMetadataTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class LastModifiedTests(TestTypes type)
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
    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnNotMinValue_When_Exists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.LastModified(path), Is.Not.EqualTo(IStorage.DefaultDateTime));
        }
    }

    [TestCase("/no")]
    [TestCase("no")]
    [TestCase("dir1/no")]
    [TestCase("/dir1/no")]
    public void Should_ReturnMinValue_When_NotExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.False);
            Assert.That(tested.LastModified(path), Is.EqualTo(IStorage.DefaultDateTime));
        }
    }
}
