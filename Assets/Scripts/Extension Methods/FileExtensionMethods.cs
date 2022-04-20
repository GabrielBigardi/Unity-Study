using System.IO;

public static class FileExtensionMethods
{
    /// <summary>
    /// Creates a directory at <paramref name="folder"/> if it doesn't exist
    /// </summary>
    /// <param name="folder"></param>
    public static void CreateDirectoryIfNotExists(this string folder)
    {
        if (folder == "")
            return;

        string path = Path.GetDirectoryName(folder);

        if (path == "")
            return;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
