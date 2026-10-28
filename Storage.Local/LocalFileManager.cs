using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IFileManager"/>
public partial class LocalStorage : IFileManager
{
    /// <inheritdoc/>
    public SmartFile? Get(string path)
    {
        path = this.GetRealPath(path);

        FileInfo fileInfo = new(path);

        if (!fileInfo.Exists) {
            return null;
        }

        return new(fileInfo.Open(FileMode.Open, FileAccess.ReadWrite), fileInfo.Name);
    }

    /// <inheritdoc/>
    public Task<SmartFile?> GetAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.Get(path));
    }

    /// <inheritdoc/>
    public bool Put(string path, SmartFile storageFile)
    {
        path = this.GetRealPath(path);

        if (Directory.Exists(path)) {
            return false;
        }

        FileInfo fileInfo = new(path);

        using Stream file = fileInfo.Create();

        storageFile.GetStream().CopyTo(file);

        file.Close();

        return File.Exists(path);
    }

    /// <inheritdoc/>
    public async Task<bool> PutAsync(string path, SmartFile storageFile, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        if (Directory.Exists(path)) {
            return false;
        }

        FileInfo fileInfo = new(path);

        await using Stream file = fileInfo.Create();

        await storageFile.GetStream().CopyToAsync(file, ct);

        file.Close();

        return File.Exists(path);
    }

    /// <inheritdoc/>
    public bool Delete(string path)
    {
        path = this.GetRealPath(path);

        if (!File.Exists(path)) {
            return false;
        }

        File.Delete(path);

        return !File.Exists(path);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.Delete(path));
    }

    /// <inheritdoc/>
    public bool Move(string path, string target)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        FileInfo inputInfo  = new(path);
        FileInfo outputInfo = new(target);

        if (!inputInfo.Exists || outputInfo.Exists) {
            return false;
        }

        using Stream input  = inputInfo.OpenRead();
        using Stream output = outputInfo.Create();

        input.CopyTo(output);

        input.Close();
        output.Close();

        inputInfo.Delete();

        return !File.Exists(path) && File.Exists(target);
    }

    /// <inheritdoc/>
    public async Task<bool> MoveAsync(string path, string target, CancellationToken ct = default)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        FileInfo inputInfo  = new(path);
        FileInfo outputInfo = new(target);

        if (!inputInfo.Exists || outputInfo.Exists) {
            return false;
        }

        await using Stream input  = inputInfo.OpenRead();
        await using Stream output = outputInfo.Create();

        await input.CopyToAsync(output, ct);

        input.Close();
        output.Close();

        inputInfo.Delete();

        return !File.Exists(path) && File.Exists(target);
    }

    /// <inheritdoc/>
    public bool Copy(string path, string target)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        FileInfo inputInfo  = new(path);
        FileInfo outputInfo = new(target);

        if (!inputInfo.Exists || outputInfo.Exists) {
            return false;
        }

        using Stream input  = inputInfo.OpenRead();
        using Stream output = outputInfo.Create();

        input.CopyTo(output);

        input.Close();
        output.Close();

        return File.Exists(path) && File.Exists(target);
    }

    /// <inheritdoc/>
    public async Task<bool> CopyAsync(string path, string target, CancellationToken ct = default)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        FileInfo inputInfo  = new(path);
        FileInfo outputInfo = new(target);

        if (!inputInfo.Exists || outputInfo.Exists) {
            return false;
        }

        await using Stream input  = inputInfo.OpenRead();
        await using Stream output = outputInfo.Create();

        await input.CopyToAsync(output, ct);

        input.Close();
        output.Close();

        return File.Exists(path) && File.Exists(target);
    }
}
