using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.DirectoryManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class MakeDirectoryTests(TestTypes type)
{
    [TestCase("not")]
    [TestCase("not/not")]
    [TestCase("dir1/not")]
    [TestCase("/not")]
    [TestCase("/not/not")]
    [TestCase("/dir1/not")]
    public void Should_ReturnTrue_When_DirectoryNotExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);
            Assert.That(tested.MakeDirectory(path), Is.True);
            Assert.That(tested.Exists(path), Is.True);
        }
    }

    [TestCase("not")]
    [TestCase("/not")]
    [TestCase("/dir1/not")]
    [TestCase("dir1/not")]
    public void Should_ReturnTrue_When_DirectoryNotExistsAndRecursiveFalse(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);
            Assert.That(tested.MakeDirectory(path, false), Is.True);
            Assert.That(tested.Exists(path), Is.True);
        }
    }

    [TestCase("dir1")]
    [TestCase("dir1/dir1")]
    [TestCase("/dir1")]
    [TestCase("/dir1/dir1")]
    public void Should_ReturnFalse_When_DirectoryExists(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(path), Is.True);
            Assert.That(tested.MakeDirectory(path), Is.False);
            Assert.That(tested.Exists(path), Is.True);
        }
    }

    [TestCase("not/not")]
    [TestCase("dir1/not/not")]
    [TestCase("/not/not")]
    [TestCase("/dir1/not/not")]
    public void Should_ReturnFalse_When_DirectoryNotExistsAndRecursiveFalse(string path)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(path), Is.True);
            Assert.That(tested.MakeDirectory(path, false), Is.False);
            Assert.That(tested.Missing(path), Is.True);
        }
    }
}
