using Zhomfr.Helpers.SmartFile;

namespace Zhomfr.Storage.Abstractions;

/// <summary>A unified filesystem contract that aggregates specific functional interfaces. Provides a Laravel-like API.</summary>
/// <summary>
///     Provides high-level management of files and directories within a specific storage driver. Works primarily with
///     <see cref="SmartFile"/> objects to abstract away raw data handling.
/// </summary>
public interface IStorage : IDirectoryManager, IDirectoryScanner, IFileManager, IFileSystemMetadata, IFileSystemState, IFileSystemTypeInspector
{
    /// <summary>
    ///     <b>Unified Path Separator:</b> The contract uses a standardized path separator format. The underlying driver
    ///     will automatically translate it to match the target filesystem. Using Windows-specific separators (<c>\</c>) may
    ///     lead to unpredictable behavior.
    /// </summary>
    public const string DIRECTORY_SEPARATOR = "/";

    /// <summary>Default datetime if object not exists.</summary>
    static readonly DateTimeOffset Default = new(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);

    /// <summary>Gets the full public URL for the file at the given path.</summary>
    /// <param name="path">The relative path to the file.</param>
    /// <returns>A string representing the public URL of the file.</returns>
    public string? GetUrl(string path);
}
