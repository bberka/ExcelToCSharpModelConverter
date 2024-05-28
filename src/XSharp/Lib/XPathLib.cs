namespace XSharp.Lib;

public static class XPathLib
{
  public static void CreateDirectory(string path) {
    var directory = Path.GetDirectoryName(path);
    if (directory is null) return;
    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
  }

  public static void CheckFilePath(string? path) {
    ArgumentNullException.ThrowIfNull(path);
    if (!File.Exists(path)) throw new FileNotFoundException("File not found: " + path);
  }

  public static bool CheckDirectoryPath(string? path) {
    if (!path.HasContent()) return false;
    var exists = Directory.Exists(path);
    if (!exists) Directory.CreateDirectory(path);
    return true;
  }

  public static List<string> GetExcelFilesFromFolder(string folder) {
    var isDirectoryExists = Directory.Exists(folder);
    if (!isDirectoryExists) return new List<string>();
    var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
                         .Where(x => x.IsFilePathExcel())
                         .ToList();
    return files;
  }
}