namespace Zhomfr.Storage.Local;

public partial class LocalStorage
{
    private const UnixFileMode VISIBLE_FILE = UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.OtherRead;

    private const UnixFileMode HIDDEN_FILE = UnixFileMode.UserRead | UnixFileMode.UserWrite;

    private const UnixFileMode VISIBLE_DIR = UnixFileMode.UserExecute | LocalStorage.VISIBLE_FILE;

    private const UnixFileMode HIDDEN_DIR = UnixFileMode.UserExecute | LocalStorage.HIDDEN_FILE;

    private static IEnumerable<FileSystemInfo> GetAllItemsForRealPath(string path, bool hidden)
    {
        DirectoryInfo info = new(path);

        if (!info.Exists) {
            yield break;
        }

        FileSystemInfo[] items = info.GetFileSystemInfos();

        foreach (FileSystemInfo item in items) {
            if (!hidden && !LocalStorage.IsVisibleRealPath(item.FullName)) {
                continue;
            }

            if (item is DirectoryInfo) {
                foreach (FileSystemInfo inner in LocalStorage.GetAllItemsForRealPath(item.FullName, hidden)) {
                    yield return inner;
                }
            }

            yield return item;
        }
    }

    private static bool IsVisibleRealPath(string path)
    {
        FileSystemInfo info;

        if (Directory.Exists(path)) {
            info = new DirectoryInfo(path);
        } else if (File.Exists(path)) {
            info = new FileInfo(path);
        } else {
            return false;
        }

        if (OperatingSystem.IsWindows()) {
            return !info.Attributes.HasFlag(FileAttributes.Hidden);
        }

        return info.UnixFileMode.HasFlag(UnixFileMode.GroupRead) || info.UnixFileMode.HasFlag(UnixFileMode.OtherRead);
    }

    private static bool IsHiddenRealPath(string path)
    {
        FileSystemInfo info;

        if (Directory.Exists(path)) {
            info = new DirectoryInfo(path);
        } else if (File.Exists(path)) {
            info = new FileInfo(path);
        } else {
            return false;
        }

        if (OperatingSystem.IsWindows()) {
            return info.Attributes.HasFlag(FileAttributes.Hidden);
        }

        return !info.UnixFileMode.HasFlag(UnixFileMode.GroupRead) && !info.UnixFileMode.HasFlag(UnixFileMode.OtherRead);
    }

    private static bool MakeVisibleRealPath(string path)
    {
        FileSystemInfo info;
        UnixFileMode   currentFileMode;

        if (Directory.Exists(path)) {
            info            = new DirectoryInfo(path);
            currentFileMode = LocalStorage.VISIBLE_DIR;
        } else if (File.Exists(path)) {
            info            = new FileInfo(path);
            currentFileMode = LocalStorage.VISIBLE_FILE;
        } else {
            return false;
        }

        if (OperatingSystem.IsWindows()) {
            info.Attributes &= ~FileAttributes.Hidden;
        } else {
            info.UnixFileMode = currentFileMode;
        }

        return LocalStorage.IsVisibleRealPath(path);
    }

    private static bool MakeHiddenRealPath(string path)
    {
        FileSystemInfo info;
        UnixFileMode   currentFileMode;

        if (Directory.Exists(path)) {
            info            = new DirectoryInfo(path);
            currentFileMode = LocalStorage.HIDDEN_DIR;
        } else if (File.Exists(path)) {
            info            = new FileInfo(path);
            currentFileMode = LocalStorage.HIDDEN_FILE;
        } else {
            return false;
        }

        if (OperatingSystem.IsWindows()) {
            info.Attributes |= FileAttributes.Hidden;
        } else {
            info.UnixFileMode = currentFileMode;
        }

        return !LocalStorage.IsVisibleRealPath(path);
    }
}
