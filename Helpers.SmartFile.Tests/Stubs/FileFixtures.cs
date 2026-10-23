namespace Zhomfr.Helpers.SmartFile.Tests.Stubs;

public static class FileFixtures
{
    public const string SAMPLE_TEXT = "Hello, Zhomfr Storage!";

    public static readonly byte[] JpegBytes = [0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46];

    public static readonly TestObject SampleObject = new(1, "Test Item");

    public record TestObject(int Id, string Name);
}
