using XSharp.Models;

namespace XSharp.Manager;

internal class XFileStructureBuilder
{
  private readonly string _fileExt;
  private readonly XSharpOptions _options;
  private readonly XFileStructure _xStructure;

  public XFileStructureBuilder(string fileName, string fileExt, XSharpOptions options) {
    _fileExt = fileExt;
    _options = options;
    _xStructure = new XFileStructure {
      FileName = fileName
    };
  }


  internal void AddXSheet(string sheetName, string fixedName) {
    lock (_xStructure) {
      _xStructure.XSheetStructures.Add(new XSheetStructure {
        Name = sheetName,
        FixedName = fixedName
      });
    }
  }

  internal void SetXSheets(List<XSheetStructure> structures) {
    lock (_xStructure) {
      _xStructure.XSheetStructures = structures;
    }
  }


  internal void ExportJson(string outputDir) {
    var json = XSerializer.SerializeJson(_xStructure);
    var dir = Path.Combine(outputDir,"Json");
    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    var path = Path.Combine(dir, $"{_xStructure.FileName}.json");
    if (File.Exists(path)) File.Delete(path);
    File.WriteAllText(path, json);
  }


  internal void ExportModels(string outputDir) {
    var dir = Path.Combine(outputDir,"FileModels");
    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    var fixedXFileName = _xStructure.FileName.FixXName();
    var sb = new StringBuilder();
    XBuilderHelper.AppendUsingList(sb, _options);
    XBuilderHelper.AppendNamespace(sb, false, _options);
    XBuilderHelper.AppendXFileNameAttribute(sb, _xStructure.FileName, _fileExt);
    XBuilderHelper.AppendClassStart(sb, fixedXFileName, false, _options);
    foreach (var sheet in _xStructure.XSheetStructures) {
      XBuilderHelper.AppendXSheetNameAttribute(sb, sheet.Name, true);
      XBuilderHelper.AppendProperty(sb, $"XSheet<{sheet.FixedName}>", fixedXFileName, sheet.FixedName, _options);
    }

    XBuilderHelper.AppendEnd(sb);
    var fileOutPath = Path.Combine(dir, $"{fixedXFileName}.cs");
    if (File.Exists(fileOutPath)) File.Delete(fileOutPath);
    File.WriteAllText(fileOutPath, sb.ToString());
  }
}