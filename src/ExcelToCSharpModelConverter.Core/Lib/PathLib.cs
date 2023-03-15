namespace ExcelToCSharpModelConverter.Core.Lib;

public static class PathLib
{

    public static void CreateDirectory(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if(directory is null) return;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}