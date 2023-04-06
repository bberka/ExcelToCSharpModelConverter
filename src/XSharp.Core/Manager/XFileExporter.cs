namespace XSharp.Core.Manager;

public class XFileExporter
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();
    private readonly string _fileName;
    private readonly string _filePath;
    private readonly XFileStructureBuilder _structureBuilder;

    public XFileExporter(string filePath)
    {
        _filePath = filePath;
        _fileName = Path.GetFileNameWithoutExtension(_filePath);
        var fileExtension = Path.GetExtension(_filePath);
        _structureBuilder = new XFileStructureBuilder(_fileName, fileExtension);
    }

    public Result ExportExcelFile()
    {
        try
        {
            var isValidFilePath = XPathLib.CheckFilePath(_filePath);
            if (!isValidFilePath) return Result.Warn("Invalid file path: " + _filePath);
            var isIgnoreFile = XOptionLib.This.GetValidator().IsIgnoreFileByPath(_filePath!);
            if (isIgnoreFile) return Result.Warn("File ignored: " + _filePath);
            var p = new ExcelPackage(_filePath);
            var sheets = p.Workbook.Worksheets;
            if (sheets.Count == 0) return Result.Warn("No WorkSheet found in Excel WorkBook");
            var outFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output_SheetModels");
            XPathLib.CheckDirectoryPath(outFolder);
            //_structureBuilder.AddXFile(fileName);
            var sheetExportResults = sheets.Select(x => new
            {
                Result = new XSheetManager(x, _fileName).Export(outFolder, out var structure),
                Structure = structure
            }).ToList();
            var structures = sheetExportResults.Where(x => x.Structure is not null).Select(x => x.Structure!).ToList();
            _structureBuilder.SetXSheets(structures);
            _structureBuilder.ExportJson();
            _structureBuilder.ExportModels();
            return sheetExportResults.Select(x => x.Result).CombineErrorArrays(nameof(ExportExcelFile));
        }
        catch (Exception ex)
        {
            logger.Exception(ex, "Failed to create Excel Exporter. FilePath: " + _filePath);
            return Result.Exception(ex, "Failed to create Excel Exporter. FilePath: " + _filePath);
        }
    }

    public static Result ExportExcelFilesInDirectory(string folderPath, bool isParallel = false)
    {
        var excelFiles = XPathLib.GetExcelFilesFromFolder(folderPath);
        if (excelFiles.Count == 0) return Result.Warn("No excel file found in directory: " + folderPath);
        var res = isParallel
            ? excelFiles.AsParallel().Select(x => new XFileExporter(x).ExportExcelFile())
                .CombineAll(nameof(ExportExcelFilesInDirectory))
            : excelFiles.Select(x => new XFileExporter(x).ExportExcelFile())
                .CombineAll(nameof(ExportExcelFilesInDirectory));
        return res;
    }
}