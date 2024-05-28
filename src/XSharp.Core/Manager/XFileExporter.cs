namespace XSharp.Core.Manager;

public class XFileExporter
{
  private readonly string _fileName;
  private readonly string _filePath;
  private readonly XFileStructureBuilder _structureBuilder;

  public XFileExporter(string filePath) {
    _filePath = filePath;
    XPathLib.CheckFilePath(_filePath);
    _fileName = Path.GetFileNameWithoutExtension(_filePath);
    var fileExtension = Path.GetExtension(_filePath);
    _structureBuilder = new XFileStructureBuilder(_fileName, fileExtension);
  }

  public void ExportExcelFile() {
    var isIgnoreFile = XOptionLib.This.GetValidator().IsIgnoreFileByPath(_filePath!);
    if (isIgnoreFile)
      return; //ignore file
    var p = new ExcelPackage(_filePath);
    var sheets = p.Workbook.Worksheets;
    if (sheets.Count == 0)
      throw new Exception("No sheet found in file: " + _filePath);
    var outFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output_SheetModels");
    XPathLib.CheckDirectoryPath(outFolder);
    //_structureBuilder.AddXFile(fileName);
    var sheetExportResults = sheets.Select(x => {
      try {
        new XSheetManager(x, _fileName).Export(outFolder, out var structure);
        return structure;
      }
      catch {
        return null;
      }
    }).ToList();
    var structures = sheetExportResults.Where(x => x is not null).ToList();
    _structureBuilder.SetXSheets(structures);
    _structureBuilder.ExportJson();
    _structureBuilder.ExportModels();
  }

  public static void ExportExcelFilesInDirectory(string folderPath, bool isParallel = false) {
    var excelFiles = XPathLib.GetExcelFilesFromFolder(folderPath);
    if (excelFiles.Count == 0)
      throw new Exception("No excel file found in folder: " + folderPath);
    if (isParallel)
      Parallel.ForEach(excelFiles, x => new XFileExporter(x).ExportExcelFile());
    else
      foreach (var file in excelFiles)
        new XFileExporter(file).ExportExcelFile();
  }
}