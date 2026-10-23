using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using HeyRed.Mime;

using Microsoft.AspNetCore.WebUtilities;

namespace Zhomfr.Helpers.SmartFile;

/// <summary>
///     Represents a self-contained file object that manages its own data access. Operates on top of a
///     <see cref="System.IO.Stream"/> to provide high-level content manipulation.
/// </summary>
/// <remarks>
///     <para>
///         <b>Lifecycle Management:</b> When this object is provided via dependency injection or a Model Binder in
///         ASP.NET Core, it is typically registered for automatic disposal at the end of the HTTP request. In such cases,
///         you do not need to call <see cref="Dispose"/> manually.
///     </para>
///     <para>
///         <b>Manual Usage:</b> If you instantiate this class manually outside a managed scope (e.g., in a background
///         task or a console application), you <b>must</b> ensure it is disposed of to release underlying resources and
///         delete any temporary buffering files.
///     </para>
/// </remarks>
public class SmartFile : IDisposable, IAsyncDisposable
{
#region Fields

    private bool    _disposed;
    private string? _contentType;
    private string? _extension;

    private Stream _stream;

#endregion

#region Properties

    /// <summary>Gets the name of the file, including the extension.</summary>
    public string FileName { get; }

    /// <summary>Gets the file extension (e.g., "png").</summary>
    /// <remarks>Which may be different from name extension.</remarks>
    public string Extension
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this._extension)) {
                this.SetFileType();
            }

            return this._extension!;
        }
    }

    /// <summary>Gets the MIME type as declared by the source.</summary>
    public string ContentType
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this._contentType)) {
                this.SetFileType();
            }

            return this._contentType!;
        }
    }

    /// <summary>Gets the file size in bytes.</summary>
    /// <remarks>Accessing this property on a non-seekable stream will trigger full buffering to determine the length.</remarks>
    public long Length => this.GetStream().Length;

#endregion

#region Constructors

    /// <summary>Initializes a new instance of the <see cref="SmartFile"/> class using a data stream.</summary>
    /// <param name="stream">The source stream containing file data.</param>
    /// <param name="fileName">The name of the file including extension.</param>
    /// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
    public SmartFile(Stream stream, string fileName = "")
    {
        this._stream  = stream ?? throw new ArgumentNullException(nameof(stream));
        this.FileName = string.IsNullOrWhiteSpace(fileName) ? Guid.NewGuid().ToString() : fileName;
    }

    /// <summary>Initializes a new instance of the <see cref="SmartFile"/> class using a byte array.</summary>
    /// <param name="bytes">The byte array containing file data.</param>
    /// <param name="fileName">The name of the file.</param>
    public SmartFile(byte[] bytes, string fileName = "") : this(new MemoryStream(bytes), fileName)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="SmartFile"/> class using a string content.</summary>
    /// <param name="content">The string content to be stored as a file (encoded in UTF-8).</param>
    /// <param name="fileName">The name of the file.</param>
    public SmartFile(string content, string fileName = "") : this(Encoding.UTF8.GetBytes(content), fileName)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="SmartFile"/> class by serializing an object to JSON.</summary>
    /// <param name="data">The object to serialize into the file content.</param>
    /// <param name="fileName">The name of the file (e.g., "data.json").</param>
    /// <param name="options">Optional JSON serializer settings.</param>
    public SmartFile(object data, string fileName, JsonSerializerOptions? options = null) : this(JsonSerializer.SerializeToUtf8Bytes(data, options), fileName)
    {
    }

#endregion

#region Cleanup

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (this._disposed) {
            return;
        }

        await this._stream.DisposeAsync();
        this._disposed = true;

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (this._disposed) {
            return;
        }

        this._stream.Dispose();
        this._disposed = true;

        GC.SuppressFinalize(this);
    }

#endregion

#region Content Access

    /// <summary>Returns the underlying data stream. Ensures the stream is seekable by transparently buffering it if necessary.</summary>
    /// <returns>A <see cref="Stream"/> positioned at the beginning (0).</returns>
    /// <exception cref="ObjectDisposedException">If object already disposed.</exception>
    public Stream GetStream()
    {
        ObjectDisposedException.ThrowIf(this._disposed, nameof(SmartFile));

        if (!this._stream.CanSeek) {
            this._stream = new FileBufferingReadStream(this._stream, 1024 * 1024);
        }

        if (this._stream.CanSeek) {
            this._stream.Position = 0;
        }

        return this._stream;
    }

    /// <summary>Reads the entire file content as a UTF-8 string, resetting the stream position to the start.</summary>
    /// <returns>The file content as a string.</returns>
    public string AsString()
    {
        using StreamReader reader = new(this.GetStream(), Encoding.UTF8, leaveOpen: true);

        return reader.ReadToEnd();
    }

    /// <summary>
    ///     Asynchronously reads the entire content of the file as a UTF-8 string, resetting the stream position to the
    ///     start.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the string content of the file.</returns>
    public async Task<string> AsStringAsync(CancellationToken ct = default)
    {
        using StreamReader reader = new(this.GetStream(), Encoding.UTF8, leaveOpen: true);

        return await reader.ReadToEndAsync(ct);
    }

    /// <summary>Reads the entire file content into a byte array, resetting the stream position to the start.</summary>
    /// <returns>A byte array containing the file data.</returns>
    public byte[] AsBytes()
    {
        Stream stream = this.GetStream();

        if (stream is MemoryStream ms) {
            return ms.ToArray();
        }

        using MemoryStream memoryStream = new();
        stream.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }

    /// <summary>
    ///     Asynchronously reads the entire content of the file into a byte array, resetting the stream position to the
    ///     start.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the byte array of the file data.</returns>
    public async Task<byte[]> AsBytesAsync(CancellationToken ct = default)
    {
        Stream stream = this.GetStream();

        if (stream is MemoryStream ms) {
            return ms.ToArray();
        }

        using MemoryStream memoryStream = new();
        await stream.CopyToAsync(memoryStream, ct);

        return memoryStream.ToArray();
    }

    /// <summary>Deserializes the file content from JSON into the specified type.</summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="options">Optional JSON serializer settings.</param>
    /// <returns>The deserialized object, or null if deserialization fails.</returns>
    public T? AsJson<T>(JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<T>(this.GetStream(), options);
    }

    /// <summary>Asynchronously deserializes the file content from JSON into the specified type.</summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="options">Optional JSON serializer settings.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the deserialized object.</returns>
    public async Task<T?> AsJsonAsync<T>(JsonSerializerOptions? options = null, CancellationToken ct = default)
    {
        return await JsonSerializer.DeserializeAsync<T>(this.GetStream(), options, ct);
    }

    /// <summary>Performs a deep binary inspection (magic numbers) to determine the actual MIME type.</summary>
    /// <remarks>This method will trigger stream buffering if the underlying stream is non-seekable.</remarks>
    [Obsolete("Use ContentType property instead")]
    public string GetRealMimeType()
    {
        return this.ContentType;
    }

    /// <summary>Asynchronously performs a deep inspection of the file's binary signature.</summary>
    [Obsolete("Use ContentType property instead")]
    public Task<string> GetRealMimeTypeAsync(CancellationToken ct = default)
    {
        return Task.Run(() => this.ContentType, ct);
    }

    private void SetFileType()
    {
        FileType info = MimeGuesser.GuessFileType(this._stream);

        this._contentType = info.MimeType;
        this._extension   = info.Extension.ToLower();
    }

#endregion

#region Validation

    /// <summary>Get hash of file.</summary>
    /// <param name="algorithm">The hashing algorithm (e.g., MD5, SHA256).</param>
    /// <returns>The hexadecimal string representation of the file hash.</returns>
    public string Hash(HashAlgorithmName algorithm)
    {
        byte[] hashBytes = algorithm switch
                           {
                               var _ when algorithm == HashAlgorithmName.SHA256 => SHA256.HashData(this.GetStream()),
                               var _ when algorithm == HashAlgorithmName.SHA512 => SHA512.HashData(this.GetStream()),
                               var _ when algorithm == HashAlgorithmName.MD5    => MD5.HashData(this.GetStream()),
                               var _ when algorithm == HashAlgorithmName.SHA1   => SHA1.HashData(this.GetStream()),
                               var _                                            => throw new NotSupportedException($"Algorithm {algorithm.Name} is not supported"),
                           };

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    /// <summary>Asynchronously get hash of a file.</summary>
    /// <param name="algorithm">The hashing algorithm.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the hexadecimal file hash.</returns>
    public async Task<string> HashAsync(HashAlgorithmName algorithm, CancellationToken ct = default)
    {
        byte[] hashBytes = algorithm switch
                           {
                               var _ when algorithm == HashAlgorithmName.SHA256 => await SHA256.HashDataAsync(this.GetStream(), ct),
                               var _ when algorithm == HashAlgorithmName.SHA512 => await SHA512.HashDataAsync(this.GetStream(), ct),
                               var _ when algorithm == HashAlgorithmName.MD5    => await MD5.HashDataAsync(this.GetStream(), ct),
                               var _ when algorithm == HashAlgorithmName.SHA1   => await SHA1.HashDataAsync(this.GetStream(), ct),
                               var _                                            => throw new NotSupportedException($"Algorithm {algorithm.Name} is not supported"),
                           };

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

#endregion
}
