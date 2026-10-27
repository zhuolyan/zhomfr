namespace Zhomfr.Storage.Abstractions;

/// <summary>
///     Provides methods for inspecting metadata of files and directories. Supports synchronous and asynchronous
///     execution for local and remote storage compatibility.
/// </summary>
public interface IFileSystemMetadata
{
    /// <summary>Gets the total logical size of the file(s) at the specified path in bytes.</summary>
    /// <remarks>
    ///     This returns the actual length of the file content (the sum of raw data bytes). It does NOT reflect the "Size
    ///     on disk," as it excludes file system metadata, directory structures (MFT/inodes), and physical allocation overhead
    ///     such as cluster padding, disk compression, or sparse file optimization.
    /// </remarks>
    /// <param name="path">The path to the file or directory.</param>
    /// <returns>The total size in bytes based on the content length.</returns>
    long Size(string path);

    /// <summary>Asynchronously gets the total logical size of the file(s) at the specified path in bytes.</summary>
    /// <remarks>
    ///     This returns the actual length of the file content (the sum of raw data bytes). It does NOT reflect the "Size
    ///     on disk," as it excludes file system metadata, directory structures (MFT/inodes), and physical allocation overhead
    ///     such as cluster padding, disk compression, or sparse file optimization.
    /// </remarks>
    /// <param name="path">The path to the file or directory.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The total size in bytes based on the content length.</returns>
    Task<long> SizeAsync(string path, CancellationToken ct = default);

    /// <summary>Get creation time of file or directory.</summary>
    /// <param name="path">The file or directory path.</param>
    /// <returns>A <see cref="DateTime"/> representing created time.</returns>
    /// <remarks>
    ///     <para>
    ///         <b>⚠️ WARNING:</b> File creation and modification timestamps depend entirely on the underlying storage driver
    ///         and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
    ///         support). Use dedicated metadata storage if strict timestamp consistency is required.
    ///     </para>
    /// </remarks>
    DateTimeOffset CreatedAt(string path);

    /// <summary>Asynchronously get creation time of file or directory.</summary>
    /// <param name="path">The file or directory path.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the created time.</returns>
    /// <remarks>
    ///     <para>
    ///         <b>⚠️ WARNING:</b> File creation and modification timestamps depend entirely on the underlying storage driver
    ///         and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
    ///         support). Use dedicated metadata storage if strict timestamp consistency is required.
    ///     </para>
    /// </remarks>
    Task<DateTimeOffset> CreatedAtAsync(string path, CancellationToken ct = default);

    /// <summary>Get last modification time of file or directory.</summary>
    /// <param name="path">The file or directory path.</param>
    /// <returns>A <see cref="DateTime"/> representing last modification time.</returns>
    /// <remarks>
    ///     <para>
    ///         <b>⚠️ WARNING:</b> File creation and modification timestamps depend entirely on the underlying storage driver
    ///         and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
    ///         support). Use dedicated metadata storage if strict timestamp consistency is required.
    ///     </para>
    /// </remarks>
    DateTimeOffset LastModified(string path);

    /// <summary>Asynchronously get last modification time of file or directory.</summary>
    /// <param name="path">The file or directory path.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the last modification time.</returns>
    /// <remarks>
    ///     <para>
    ///         <b>⚠️ WARNING:</b> File creation and modification timestamps depend entirely on the underlying storage driver
    ///         and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
    ///         support). Use dedicated metadata storage if strict timestamp consistency is required.
    ///     </para>
    /// </remarks>
    Task<DateTimeOffset> LastModifiedAsync(string path, CancellationToken ct = default);
}
