namespace Zhomfr.Helpers.SmartFile.Tests.Stubs;

public class DisposeSpyStream : MemoryStream
{
    private bool _isStreamDisposed;

    public DisposeSpyStream()
    {
    }

    public DisposeSpyStream(byte[] bytes) : base(bytes)
    {
    }

    public int DisposeCount { get; private set; }

    protected override void Dispose(bool disposing)
    {
        if (disposing && !this._isStreamDisposed) {
            this._isStreamDisposed = true;
            this.DisposeCount++;
        }

        base.Dispose(disposing);
    }

    public override async ValueTask DisposeAsync()
    {
        if (!this._isStreamDisposed) {
            GC.SuppressFinalize(this);
            await base.DisposeAsync();
        }
    }
}
