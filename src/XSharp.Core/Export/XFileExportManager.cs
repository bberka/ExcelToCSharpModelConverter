using XSharp.Shared;

namespace XSharp.Core.Export;

public static class XFileExportManager
{
    //Todo: Move to config
    public const string OutputFolder = "Output";
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();

    public static Result ExportExcelFile(string? filePath)
    {
        var isValidFilePath = PathLib.CheckFilePath(filePath);
        if (!isValidFilePath) return Result.Warn("Invalid file path: " + filePath);
        var isValidName = XKernel.This.GetValidator<IXFileNameValidator>()?.IsIgnore(filePath ?? "");
        if (isValidName == false) return Result.Warn("Invalid file name: " + filePath);
        using var p = new ExcelPackage(filePath);
        var sheets = p.Workbook.Worksheets;
        if (sheets.Count == 0) return Result.Warn("No WorkSheet found in Excel WorkBook");
        var outFolderPath = Path.Combine(Directory.GetCurrentDirectory(), OutputFolder);
        PathLib.CreateDirectory(outFolderPath);
        var sheetExportResults = sheets.Select(x => XSharpModelBuilder.Export(x, outFolderPath)).ToList();
        return sheetExportResults.Combine(nameof(ExportExcelFile));
    }

    public static Result ExportExcelFilesInDirectory(string folderPath, bool isParallel = false)
    {
        var excelFiles = PathLib.GetExcelFilesFromFolder(folderPath);
        if (excelFiles.Count == 0) return Result.Warn("No excel file found in directory: " + folderPath);
        return isParallel
            ? excelFiles.AsParallel().Select(ExportExcelFile).Combine(nameof(ExportExcelFilesInDirectory))
            : excelFiles.Select(ExportExcelFile).Combine(nameof(ExportExcelFilesInDirectory));
    }
}