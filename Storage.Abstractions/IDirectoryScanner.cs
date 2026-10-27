namespace Zhomfr.Storage.Abstractions;

/// <summary>Scan directories with support for synchronous and asynchronous operations.</summary>
public interface IDirectoryScanner
{
    /// <summary>Get files in the given directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <returns>A file paths enumerable.</returns>
    IEnumerable<string> Files(string directory, bool hidden = false);

    /// <summary>Asynchronously get files in the given directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of file paths.</returns>
    IAsyncEnumerable<string> FilesAsync(string directory, bool hidden = false, CancellationToken ct = default);

    /// <summary>Get all files from the given directory recursively.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <returns>All recursive file paths enumerable.</returns>
    IEnumerable<string> AllFiles(string directory, bool hidden = false);

    /// <summary>Asynchronously get all files from the given directory recursively.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of all recursive file paths.</returns>
    IAsyncEnumerable<string> AllFilesAsync(string directory, bool hidden = false, CancellationToken ct = default);

    /// <summary>Get directories within a given directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <returns>Directory paths enumerable.</returns>
    IEnumerable<string> Directories(string directory, bool hidden = false);

    /// <summary>Asynchronously iterate through directories in the given directory.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of directory paths.</returns>
    IAsyncEnumerable<string> DirectoriesAsync(string directory, bool hidden = false, CancellationToken ct = default);

    /// <summary>Get all directories from the given directory recursively.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <returns>Directory paths enumerable.</returns>
    IEnumerable<string> AllDirectories(string directory, bool hidden = false);

    /// <summary>Asynchronously get all directories from the given directory recursively.</summary>
    /// <param name="directory">The directory path.</param>
    /// <param name="hidden">Whether to include hidden files.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of directory paths.</returns>
    IAsyncEnumerable<string> AllDirectoriesAsync(string directory, bool hidden = false, CancellationToken ct = default);
}
