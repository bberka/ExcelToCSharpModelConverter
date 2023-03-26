using System.Reflection;

namespace XSharp.Core.Manager;

public class XFileReader
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();
    private readonly Assembly _assembly;
    private readonly string _path;
    private readonly XFile _xFile = new();
    private bool _isRead;

    private XFileReader(Assembly assembly, string path)
    {
        _assembly = assembly;
        _path = path;
        _xFile.Name = Path.GetFileName(path);
    }

    //Assembly must contain exported models for this to work
    public static ResultData<XFileReader> Create(Assembly assembly, string path)
    {
        try
        {
            var fileExists = File.Exists(path);
            if (!fileExists) return Result.Warn("File not found: " + path);

            return new XFileReader(assembly, path);
        }
        catch (Exception ex)
        {
            logger.Exception(ex, "Failed to create XFileReadManager");
            return Result.Exception(ex);
        }
    }

    private object? GetTypeInstanceFromAssemblyByName(string typeName)
    {
        return _assembly.CreateInstance(typeName);
    }

    public ResultData<XFile> Read(bool isReadAgain = false)
    {
        if (_isRead && !isReadAgain) return _xFile;
        try
        {
            var fileName = Path.GetFileName(_path);
            using var p = new ExcelPackage(_path);
            var sheets = p.Workbook.Worksheets;
            foreach (var sheet in sheets)
            {
                var fixedName = sheet.Name.FixXName();
                var sheetType = GetTypeInstanceFromAssemblyByName(fixedName);
                if (sheetType == null)
                {
                    logger.Warn("Sheet type not found: " + fixedName);
                    continue;
                }

                var res = new XSheetManager(sheet, fileName).Read(sheetType.GetType());
                if (res.IsFailure)
                {
                    logger.Error("Failed to create WorkSheetReader: " + sheet.Name + " : " + res.ErrorCode);
                    continue;
                }

                _xFile.AddSheet(res.Data!);
            }

            _isRead = true;
            return _xFile;
        }
        catch (Exception ex)
        {
            logger.Exception(ex, "Failed to read workbook: " + _xFile.Name);
            return Result.Exception(ex);
        }
    }
}