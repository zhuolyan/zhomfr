namespace Zhomfr.Storage.Abstractions;

/// <summary>
///     Provides methods for inspecting type of filesystem objects. Supports synchronous and asynchronous execution
///     for local and remote storage compatibility.
/// </summary>
public interface IFileSystemTypeInspector
{
    /// <summary>Determine if the given path is a directory.</summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if path is a directory, false otherwise.</returns>
    bool IsDirectory(string path);

    /// <summary>Asynchronously determine if the given path is a directory.</summary>
    /// <param name="path">The path to check.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing whether path is a directory.</returns>
    Task<bool> IsDirectoryAsync(string path, CancellationToken ct = default);

    /// <summary>Determine if the given path is a file.</summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if path is a file, false otherwise.</returns>
    bool IsFile(string path);

    /// <summary>Asynchronously determine if the given path is a file.</summary>
    /// <param name="path">The path to check.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing whether path is a file.</returns>
    Task<bool> IsFileAsync(string path, CancellationToken ct = default);
}
