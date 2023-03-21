namespace XSharp.Core.Export;

public static class XFileExporter
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();


    public static Result ExportExcelFile(string? filePath)
    {
        try
        {
            var isValidFilePath = XPathLib.CheckFilePath(filePath);
            if (!isValidFilePath) return Result.Warn("Invalid file path: " + filePath);
            var isIgnoreFile = XOptionLib.This.GetValidator().IsIgnoreFileByPath(filePath!);
            if (isIgnoreFile) return Result.Warn("File ignored: " + filePath);
            using var p = new ExcelPackage(filePath);
            var sheets = p.Workbook.Worksheets;
            if (sheets.Count == 0) return Result.Warn("No WorkSheet found in Excel WorkBook");
            var outFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
            XPathLib.CheckDirectoryPath(outFolder);
            var fileName = Path.GetFileName(filePath);
            XStructureBuilder.AddXFile(fileName);
            var sheetExportResults = sheets.Select(x => XSheetExporter.Export(x, fileName, outFolder)).ToList();
            return sheetExportResults.CombineErrorArrays(nameof(ExportExcelFile));
           
        }
        catch (Exception ex)
        {
            logger.Exception(ex, "Failed to create Excel Exporter. FilePath: " + filePath);
            return Result.Exception(ex, "Failed to create Excel Exporter. FilePath: " + filePath);
        }

    }

    public static Result ExportExcelFilesInDirectory(string folderPath, bool isParallel = false)
    {
        var excelFiles = XPathLib.GetExcelFilesFromFolder(folderPath);
        if (excelFiles.Count == 0) return Result.Warn("No excel file found in directory: " + folderPath);
        var res =  isParallel
            ? excelFiles.AsParallel().Select(x => ExportExcelFile(x)).CombineAll(nameof(ExportExcelFilesInDirectory))
            : excelFiles.Select(x => ExportExcelFile(x)).CombineAll(nameof(ExportExcelFilesInDirectory));
        XStructureBuilder.PrintAsJson();
        return res;
    }
}