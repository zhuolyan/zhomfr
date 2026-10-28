using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IFileSystemState"/>
public partial class LocalStorage : IFileSystemState
{
    /// <inheritdoc/>
    public bool Exists(string path)
    {
        path = this.GetRealPath(path);

        return File.Exists(path) || Directory.Exists(path);
    }

    /// <inheritdoc/>
    public Task<bool> ExistsAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.Exists(path));
    }

    /// <inheritdoc/>
    public bool Missing(string path)
    {
        return !this.Exists(path);
    }

    /// <inheritdoc/>
    public Task<bool> MissingAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.Missing(path));
    }

    /// <inheritdoc/>
    public bool IsHidden(string path)
    {
        path = this.GetRealPath(path);

        return LocalStorage.IsHiddenRealPath(path);
    }

    /// <inheritdoc/>
    public Task<bool> IsHiddenAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.IsHidden(path));
    }

    /// <inheritdoc/>
    public bool IsVisible(string path)
    {
        path = this.GetRealPath(path);

        return LocalStorage.IsVisibleRealPath(path);
    }

    /// <inheritdoc/>
    public Task<bool> IsVisibleAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.IsVisible(path));
    }

    /// <inheritdoc/>
    public bool MakeHidden(string path)
    {
        path = this.GetRealPath(path);

        return LocalStorage.MakeHiddenRealPath(path);
    }

    /// <inheritdoc/>
    public Task<bool> MakeHiddenAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.MakeHidden(path));
    }

    /// <inheritdoc/>
    public bool MakeVisible(string path)
    {
        path = this.GetRealPath(path);

        return LocalStorage.MakeVisibleRealPath(path);
    }

    /// <inheritdoc/>
    public Task<bool> MakeVisibleAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.MakeVisible(path));
    }
}
