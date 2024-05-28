using Microsoft.Extensions.Logging;

namespace XSharp.Manager;

public sealed class XFileExporter
{
  private readonly string _fileName;
  private readonly string _filePath;
  private readonly XSharpOptions _options;
  private readonly XFileStructureBuilder _structureBuilder;

  public XFileExporter(string filePath, XSharpOptions options) {
    _filePath = filePath;
    _options = options;
    XPathLib.CheckFilePath(_filePath);
    _fileName = Path.GetFileNameWithoutExtension(_filePath);
    var fileExtension = Path.GetExtension(_filePath);
    _structureBuilder = new XFileStructureBuilder(_fileName, fileExtension, options);
  }

  public void ExportExcelFile() {
    var isIgnoreFile = _options.XValidator.IsIgnoreFileByPath(_filePath!);
    if (isIgnoreFile)
      return; //ignore file
    var p = new ExcelPackage(_filePath);
    var sheets = p.Workbook.Worksheets;
    if (sheets.Count == 0) {
      _options.Logger.LogWarning("No sheet found in file: {FileName}", _fileName);
      return;
    }

    var outFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output_SheetModels");
    XPathLib.CheckDirectoryPath(outFolder);
    var sheetExportResults = sheets.Select(x => {
                                     try {
                                       new XSheetManager(x, _fileName, _options).Export(outFolder, out var structure);
                                       return structure;
                                     }
                                     catch {
                                       return null;
                                     }
                                   })
                                   .ToList();
    var structures = sheetExportResults.Where(x => x is not null).ToList();
    _structureBuilder.SetXSheets(structures!);
    _structureBuilder.ExportJson();
    _structureBuilder.ExportModels();
  }

  public static void ExportExcelFilesInDirectory(string folderPath, XSharpOptions options, bool isParallel = false) {
    var excelFiles = XPathLib.GetExcelFilesFromFolder(folderPath);
    if (excelFiles.Count == 0)
      throw new Exception("No excel file found in folder: " + folderPath);
    if (isParallel)
      Parallel.ForEach(excelFiles, x => new XFileExporter(x, options).ExportExcelFile());
    else
      foreach (var file in excelFiles)
        new XFileExporter(file, options).ExportExcelFile();
  }
}