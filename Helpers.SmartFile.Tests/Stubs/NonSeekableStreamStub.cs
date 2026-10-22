namespace Zhomfr.Helpers.SmartFile.Tests.Stubs;

public class NonSeekableStreamStub(byte[] bytes) : MemoryStream(bytes)
{
    public override bool CanSeek => false;

    public override long Position { get => base.Position; set => throw new NotSupportedException(); }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }
}
