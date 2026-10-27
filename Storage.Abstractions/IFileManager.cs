using Zhomfr.Helpers.SmartFile;

namespace Zhomfr.Storage.Abstractions;

/// <summary>
///     Provides high-level file management operations including moving, copying, and deleting files. All operations
///     support both synchronous and asynchronous execution for local and remote storage compatibility.
/// </summary>
public interface IFileManager
{
    /// <summary>Retrieve a file from the storage at the given path.</summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>An <see cref="SmartFile"/> instance for data access.</returns>
    SmartFile? Get(string path);

    /// <summary>Asynchronously retrieve a file from the storage.</summary>
    /// <param name="path">The path to the file.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the retrieved <see cref="SmartFile"/>.</returns>
    Task<SmartFile?> GetAsync(string path, CancellationToken ct = default);

    /// <summary>
    ///     Write an <see cref="SmartFile"/> object to the specified path. This method consumes the stream from the
    ///     provided file object.
    /// </summary>
    /// <param name="path">The destination path.</param>
    /// <param name="storageFile">The file object to persist.</param>
    /// <returns>True if the file was stored successfully.</returns>
    bool Put(string path, SmartFile storageFile);

    /// <summary>Asynchronously write an <see cref="SmartFile"/> object to the specified path.</summary>
    /// <param name="path">The destination path.</param>
    /// <param name="storageFile">The file object to persist.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> PutAsync(string path, SmartFile storageFile, CancellationToken ct = default);

    /// <summary>Delete the file at the given path.</summary>
    /// <param name="path">The path to the file to be deleted.</param>
    /// <returns>True if the file was successfully deleted, false otherwise.</returns>
    bool Delete(string path);

    /// <summary>Asynchronously delete the file at the given path.</summary>
    /// <param name="path">The path to the file to be deleted.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> DeleteAsync(string path, CancellationToken ct = default);

    /// <summary>Move a file to a new location.</summary>
    /// <param name="path">The current source path of the file.</param>
    /// <param name="target">The destination path where the file should be moved.</param>
    /// <returns>True if the file was successfully moved, false otherwise.</returns>
    bool Move(string path, string target);

    /// <summary>Asynchronously move a file to a new location.</summary>
    /// <param name="path">The current source path of the file.</param>
    /// <param name="target">The destination path where the file should be moved.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> MoveAsync(string path, string target, CancellationToken ct = default);

    /// <summary>Copy a file to a new location.</summary>
    /// <param name="path">The source path of the file.</param>
    /// <param name="target">The destination path where the file should be copied.</param>
    /// <returns>True if the file was successfully copied, false otherwise.</returns>
    bool Copy(string path, string target);

    /// <summary>Asynchronously copy a file to a new location.</summary>
    /// <param name="path">The source path of the file.</param>
    /// <param name="target">The destination path where the file should be copied.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> CopyAsync(string path, string target, CancellationToken ct = default);
}
