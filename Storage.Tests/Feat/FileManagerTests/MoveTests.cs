using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Feat.FileManagerTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class MoveTests(TestTypes type)
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
    public void Should_ReturnTrue_When_OldFileExistsAndNewNotExists(string oldPath)
    {
        const string NEW_PATH = "new.txt";

        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(oldPath), Is.True);
            Assert.That(tested.Missing(NEW_PATH), Is.True);

            using SmartFile file1  = tested.Get(oldPath)!;
            string          value1 = file1.AsString();

            Assert.That(tested.Move(oldPath, NEW_PATH), Is.True);

            Assert.That(tested.Missing(oldPath), Is.True);
            Assert.That(tested.Exists(NEW_PATH), Is.True);

            using SmartFile file2  = tested.Get(NEW_PATH)!;
            string          value2 = file2.AsString();

            Assert.That(value2, Is.EqualTo(value1));
        }
    }

    [TestCase("no.txt")]
    [TestCase("/no.txt")]
    [TestCase("dir1/no.txt")]
    [TestCase("/dir1/no.txt")]
    public void Should_ReturnFalse_When_OldFileNotExistsAndNewNotExists(string oldPath)
    {
        const string NEW_PATH = "new.txt";

        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Missing(oldPath), Is.True);
            Assert.That(tested.Missing(NEW_PATH), Is.True);

            Assert.That(tested.Move(oldPath, NEW_PATH), Is.False);

            Assert.That(tested.Missing(oldPath), Is.True);
            Assert.That(tested.Missing(NEW_PATH), Is.True);
        }
    }

    [TestCase("file1.txt", "file2.txt")]
    [TestCase("file3.txt", "dir1")]
    [TestCase("/file1.txt", "file2.txt")]
    [TestCase("/file3.txt", "dir1")]
    [TestCase("dir1/file1.txt", "/file2.txt")]
    [TestCase("dir1/file3.txt", "/empty")]
    [TestCase("/dir1/file1.txt", "/file2.txt")]
    [TestCase("/dir1/file3.txt", "/empty")]
    [TestCase("dir1/dir1/file1.txt", "/file2.txt")]
    [TestCase("dir1/dir1/file3.txt", "/empty")]
    [TestCase("/dir1/dir1/file1.txt", "file2.txt")]
    [TestCase("/dir1/dir1/file3.txt", "empty")]
    public void Should_ReturnFalse_When_OldFileExistsAndNewExists(string oldPath, string newPath)
    {
        IStorage tested = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(tested.Exists(oldPath), Is.True);
            Assert.That(tested.Exists(newPath), Is.True);

            using SmartFile file1 = tested.Get(oldPath)!;
            using SmartFile file2 = tested.Get(newPath)!;

            string value1 = file1.AsString();
            string value2 = file2.AsString();

            Assert.That(value2, Is.Not.EqualTo(value1));

            Assert.That(tested.Move(oldPath, newPath), Is.False);

            Assert.That(tested.Exists(oldPath), Is.True);
            Assert.That(tested.Exists(newPath), Is.True);

            using SmartFile file3 = tested.Get(oldPath)!;
            using SmartFile file4 = tested.Get(newPath)!;

            string value3 = file3.AsString();
            string value4 = file4.AsString();

            Assert.That(value3, Is.EqualTo(value1));
            Assert.That(value4, Is.EqualTo(value2));
        }
    }
}
