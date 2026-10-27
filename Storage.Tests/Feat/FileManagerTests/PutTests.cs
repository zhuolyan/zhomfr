using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class PutTests(TestTypes type)
{
    private const string CONTENT = "test";

    [TestCase("new.txt")]
    [TestCase("new")]
    [TestCase("/new.txt")]
    [TestCase("/new")]
    [TestCase("dir1/new.txt")]
    [TestCase("dir1/new")]
    [TestCase("/dir1/new.txt")]
    [TestCase("/dir1/new")]
    public void Should_ReturnTrue_When_FileNotExists(string path)
    {
        IStorage        tested = GlobalSetup.GetTested(type);
        using SmartFile file   = new(PutTests.CONTENT);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);

            Assert.That(tested.Put(path, file), Is.True);

            Assert.That(tested.Exists(path), Is.True);

            using SmartFile file2 = tested.Get(path)!;

            Assert.That(file2.AsString(), Is.EqualTo(PutTests.CONTENT));
        }
    }

    [TestCase("file1.txt")]
    [TestCase("/file1.txt")]
    [TestCase("dir1/file1.txt")]
    [TestCase("/dir1/file1.txt")]
    [TestCase("dir1/dir1/file1.txt")]
    [TestCase("/dir1/dir1/file1.txt")]
    public void Should_ReturnTrue_When_FileExists(string path)
    {
        IStorage        tested = GlobalSetup.GetTested(type);
        using SmartFile file   = new(PutTests.CONTENT);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            using SmartFile file2 = tested.Get(path)!;

            Assert.That(file2.AsString(), Is.Not.EqualTo(PutTests.CONTENT));

            Assert.That(tested.Put(path, file), Is.True);

            Assert.That(tested.Exists(path), Is.True);

            using SmartFile file3 = tested.Get(path)!;

            Assert.That(file3.AsString(), Is.EqualTo(PutTests.CONTENT));
        }
    }

    [TestCase("dir1")]
    [TestCase("/dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnFalse_When_FileIsDir(string path)
    {
        IStorage        tested = GlobalSetup.GetTested(type);
        using SmartFile file   = new(PutTests.CONTENT);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);

            Assert.That(tested.Put(path, file), Is.False);

            Assert.That(tested.Exists(path), Is.True);
        }
    }
}
