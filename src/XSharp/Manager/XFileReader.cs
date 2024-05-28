using System.Reflection;
using Microsoft.Extensions.Logging;
using XSharp.Models;

namespace XSharp.Manager;

public sealed class XFileReader
{
  private readonly Assembly _assembly;
  private readonly XSharpOptions _options;
  private readonly XFile _xFile = new();
  private bool _isRead;

  private Task? _readTask;


  private XFileReader(Assembly assembly, string path, XSharpOptions options) {
    var fileExists = File.Exists(path);
    if (!fileExists) throw new FileNotFoundException("File not found: " + path);
    _assembly = assembly;
    _options = options;
    _xFile.FilePath = path;
  }

  private object? GetTypeInstanceFromAssemblyByName(string typeName) {
    return _assembly.CreateInstance(typeName);
  }

  public void StartReadingAsync() {
    _readTask ??= Task.Run(() => Read());
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
        _options.Logger?.LogWarning("Sheet type not found: {Name}", fixedName);
        continue;
      }

      var res = new XSheetManager(sheet, _xFile.NameWithoutExtension, _options).Read(sheetType.GetType());
      yield return res;
    }
  }

  public bool IsRead() {
    return _isRead;
  }
}