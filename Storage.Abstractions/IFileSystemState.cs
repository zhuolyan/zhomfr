namespace Zhomfr.Storage.Abstractions;

/// <summary>
///     Provides methods for inspecting state of files and directories. Supports synchronous and asynchronous
///     execution for local and remote storage compatibility.
/// </summary>
public interface IFileSystemState
{
    /// <summary>Determine if file or directory exists at the given path.</summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if path exists, false otherwise.</returns>
    bool Exists(string path);

    /// <summary>Asynchronously determine if file or directory exists at the given path.</summary>
    /// <param name="path">The path to check.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing whether path exists.</returns>
    Task<bool> ExistsAsync(string path, CancellationToken ct = default);

    /// <summary>Determine if file or directory is missing at the given path.</summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if path does not exist, false otherwise.</returns>
    bool Missing(string path);

    /// <summary>Asynchronously determine if file or directory is missing at the given path.</summary>
    /// <param name="path">The path to check.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing whether path is missing.</returns>
    Task<bool> MissingAsync(string path, CancellationToken ct = default);

    /// <summary>Determine if file or directory is invisible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>True if the file or directory is hidden, false otherwise.</returns>
    bool IsHidden(string path);

    /// <summary>Determine if file or directory is invisible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>True if the file or directory is hidden, false otherwise.</returns>
    Task<bool> IsHiddenAsync(string path, CancellationToken ct = default);

    /// <summary>Determine if file or directory is visible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>True if the file or directory is visible, false otherwise.</returns>
    bool IsVisible(string path);

    /// <summary>Determine if file or directory is visible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>True if the file or directory is visible, false otherwise.</returns>
    Task<bool> IsVisibleAsync(string path, CancellationToken ct = default);

    /// <summary>Make file or directory invisible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>True if the file or directory was successfully make hidden, false otherwise.</returns>
    bool MakeHidden(string path);

    /// <summary>Make file or directory invisible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>True if the file or directory was successfully make hidden, false otherwise.</returns>
    Task<bool> MakeHiddenAsync(string path, CancellationToken ct = default);

    /// <summary>Make file or directory visible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>True if the file or directory was successfully make visible, false otherwise.</returns>
    bool MakeVisible(string path);

    /// <summary>Make file or directory visible for access from web.</summary>
    /// <param name="path">The path of the file.</param>
    /// <param name="ct">The cancellation token to cancel the operation.</param>
    /// <returns>True if the file or directory was successfully make visible, false otherwise.</returns>
    Task<bool> MakeVisibleAsync(string path, CancellationToken ct = default);
}
