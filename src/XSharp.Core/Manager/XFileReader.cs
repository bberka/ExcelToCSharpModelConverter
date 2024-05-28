using System.Reflection;
using Serilog;

namespace XSharp.Core.Manager;

public class XFileReader
{
  private readonly Assembly _assembly;
  private readonly XFile _xFile = new();
  private bool _isRead;

  private Task? ReadTask;


  private XFileReader(Assembly assembly, string path) {
    _assembly = assembly;
    _xFile.FilePath = path;
  }

  //Assembly must contain exported models for this to work
  public static XFileReader Create(Assembly assembly, string path) {
    var fileExists = File.Exists(path);
    if (!fileExists) throw new FileNotFoundException("File not found: " + path);
    return new XFileReader(assembly, path);
  }

  private object? GetTypeInstanceFromAssemblyByName(string typeName) {
    return _assembly.CreateInstance(typeName);
  }

  public void StartReadingAsync() {
    ReadTask ??= Task.Run(() => Read());
  }

  public XFile Read(bool isReadAgain = false) {
    if (_isRead && !isReadAgain) return _xFile;
    foreach (var sheet in ReadSheets()) _xFile.Sheets.Add(sheet);
    _isRead = true;
    return _xFile;
  }

  private IEnumerable<XSheet> ReadSheets() {
    using var p = new ExcelPackage(_xFile.FilePath);
    var sheets = p.Workbook.Worksheets;
    foreach (var sheet in sheets) {
      var fixedName = sheet.Name.FixXName();
      var sheetType = GetTypeInstanceFromAssemblyByName(fixedName);
      if (sheetType == null) {
        Log.Warning("Sheet type not found: {Name}", fixedName);
        continue;
      }

      var res = new XSheetManager(sheet, _xFile.NameWithoutExtension).Read(sheetType.GetType());
      yield return res;
    }
  }

  public bool IsRead() {
    return _isRead;
  }
}