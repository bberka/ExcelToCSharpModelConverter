using EasMe.Extensions;
using EasMe.Logging;
using EasMe.Result;
using OfficeOpenXml;

namespace ExcelToCSharpModelConverter.Core.Manager;

public class WorkBookManager
{
    private readonly string _filePath;
    private readonly static IEasLog logger = EasLogFactory.CreateLogger();

    private WorkBookManager(string filePath)
    {
        _filePath = filePath;
    }


    public static ResultData<WorkBookManager> Create(string? filePath)
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
        return new WorkBookManager(filePath);
    }

    public static ResultData<List<WorkBookManager>> CreateWithDirectory(string folderPath)
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
        var list = new List<WorkBookManager>();
        
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
            using var p = new ExcelPackage(_filePath);
            var sheets = p.Workbook.Worksheets;
            var results = new List<Result>();
            foreach (var sheet in sheets)
            {
                var readerResult = WorkSheetManager.Create(sheet);
                if (readerResult.IsFailure)
                {
                    logger.Error("Failed to create WorkSheetReader: " + sheet.Name + " : " + readerResult.ErrorCode);
                    results.Add(readerResult.ToResult());
                    continue;
                }
                var reader = readerResult.Data;
                var outFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "OutPut");
                PathLib.CheckDirectoryPath(outFolderPath);
                var result = reader!.ExportCsharpModel(outFolderPath);
                logger.LogResult(result, "Exported: " + sheet.Name);
                results.Add(result);
            }
            return results.Combine("ExportWorkSheets");
        }
        catch (Exception ex)
        {
            logger.Exception(ex, "Failed to export workbook: " + _filePath);
            return Result.Exception(ex);
        }
    }
}