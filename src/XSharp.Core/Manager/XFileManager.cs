using EasMe.Logging;
using EasMe.Result;
using XSharp.Core.Lib;
using XSharp.Shared;

namespace XSharp.Core.Manager;

public class XFileManager
{
    public IXFile XFile { get; }
    private readonly static IEasLog logger = EasLogFactory.CreateLogger();

    public const string OutputFolder = "Output";
    private XFileManager(IXFile xFile)
    {
        XFile = xFile;
    }


    public static ResultData<XFileManager> Create(string? filePath)
    {
        var isValidFilePath = PathLib.CheckFilePath(filePath);
        if (!isValidFilePath)
        {
            return Result.Warn("Invalid file path: " + filePath);
        }

        var isExcelFile = filePath!.EndsWith(".xlsx") || filePath.EndsWith(".xlsm") || filePath.EndsWith(".xls");
        if (!isExcelFile)
        {
            return Result.Warn("Path is not excel file: " + filePath);
        }

        using var p = new ExcelPackage(filePath);
        var xFile = XKernel.This.GetInstance<IXFile>();
        xFile.SetName(Path.GetFileName(filePath));
        var sheets = p.Workbook.Worksheets;
        foreach (var sheet in sheets)
        {
            var res = XSheetManager.Create(sheet);
            if (res.IsFailure)
            {
                logger.Error("Failed to create WorkSheetReader: " + sheet.Name + " : " + res.ErrorCode);
                continue;
            }
            xFile.AddSheet(res.Data!.Sheet);
        }
        return new XFileManager(xFile);
    }

    public static ResultData<List<XFileManager>> CreateWithDirectory(string folderPath)
    {
        var isValidDirectory = PathLib.CheckDirectoryPath(folderPath);
        if (!isValidDirectory)
        {
            return Result.Warn("Invalid directory path: " + folderPath);
        }

        var excelFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)
            .Where(x => x.EndsWith(".xls") || x.EndsWith(".xlsm") || x.EndsWith(".xlsx"))
            .ToArray();
        if (excelFiles.Length == 0)
        {
            return Result.Warn("No excel file found in directory: " + folderPath);
        }

        var list = new List<XFileManager>();

        foreach (var item in excelFiles)
        {
            var res = Create(item);
            if (res.IsFailure)
            {
                logger.Error("Failed to create WorkBookManager: " + res.ErrorCode);
            }
            else
            {
                list.Add(res.Data!);
            }
        }

        return list;
    }

    public Result ExportWorkSheets()
    {
        try
        {
            var results = new List<Result>();
            foreach (var sheet in XFile.Sheets)
            {
                var readerResult = XSheetManager.Create(sheet);
                if (readerResult.IsFailure)
                {
                    logger.Error("Failed to create XSheetReader: " + sheet.Name + " : " + readerResult.ErrorCode);
                    results.Add(readerResult.ToResult());
                    continue;
                }
                var reader = readerResult.Data;
                var outFolderPath = Path.Combine(Directory.GetCurrentDirectory(), OutputFolder);
                PathLib.CheckDirectoryPath(outFolderPath);
                var result = reader!.ExportCsharpModel(outFolderPath);
                logger.LogResult(result, "Exported: " + sheet.Name);
                results.Add(result);
            }
            return results.Combine("ExportWorkSheets");
        }
        catch (Exception ex)
        {
            logger.Exception(ex, "Failed to export workbook: " +  XFile.Name);
            return Result.Exception(ex);
        }
    }
}