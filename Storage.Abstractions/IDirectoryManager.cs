namespace Zhomfr.Storage.Abstractions;

/// <summary>Manages directories with support for synchronous and asynchronous operations.</summary>
public interface IDirectoryManager
{
    /// <summary>Create a directory.</summary>
    /// <param name="path">The directory path.</param>
    /// <param name="recursive">Whether to create parent directories.</param>
    /// <returns>True on success, false on failure.</returns>
    bool MakeDirectory(string path, bool recursive = true);

    /// <summary>Asynchronously create a directory.</summary>
    /// <param name="path">The directory path.</param>
    /// <param name="recursive">Whether to create parent directories.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> MakeDirectoryAsync(string path, bool recursive = true, CancellationToken ct = default);

    /// <summary>Recursively delete a directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <returns>True on success, false on failure.</returns>
    bool DeleteDirectory(string directory);

    /// <summary>Asynchronously delete a directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> DeleteDirectoryAsync(string directory, CancellationToken ct = default);

    /// <summary>Empty the specified directory of all files and folders.</summary>
    /// <param name="directory">The directory path.</param>
    /// <returns>True on success, false on failure.</returns>
    bool CleanDirectory(string directory);

    /// <summary>Asynchronously empty the specified directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the operation success.</returns>
    Task<bool> CleanDirectoryAsync(string directory, CancellationToken ct = default);

    /// <summary>Determine if the given path is an empty directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <returns>True if the directory is empty, false otherwise.</returns>
    bool IsEmptyDirectory(string directory);

    /// <summary>Asynchronously determine if the given path is an empty directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing whether the directory is empty.</returns>
    Task<bool> IsEmptyDirectoryAsync(string directory, CancellationToken ct = default);
}
