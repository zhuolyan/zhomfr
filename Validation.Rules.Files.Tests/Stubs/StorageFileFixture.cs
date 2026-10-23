using System.Text;

using Zhomfr.Helpers.SmartFile;

namespace Zhomfr.Validation.Rules.Files.Tests.Stubs;

public static class StorageFileFixture
{
    public static SmartFile CreateWithEncoding(string content, Encoding encoding)
    {
        byte[]       bytes  = encoding.GetBytes(content);
        MemoryStream stream = new(bytes);

        return new(stream, "test.txt");
    }
}
