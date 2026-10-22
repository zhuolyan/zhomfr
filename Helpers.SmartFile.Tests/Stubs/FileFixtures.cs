namespace Zhomfr.Helpers.SmartFile.Tests.Stubs;

public static class FileFixtures
{
    public const string SAMPLE_TEXT = "Hello, Zhomfr Storage!";

    public const string EMPTY_DIR  = "/emptyDir";
    public const string THROW_FILE = "/throw/file.txt";

    public static readonly byte[] JpegBytes = [0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46];

    public static readonly byte[] PngBytes = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];

    public static readonly List<byte[]> TextFiles = [[0xEF, 0xBB, 0xBF], [0xFF, 0xFE], [0xFF, 0xFE, 0x00, 0x00]];

    public static readonly TestObject SampleObject = new(1, "Test Item");

    public record TestObject(int Id, string Name);
}
