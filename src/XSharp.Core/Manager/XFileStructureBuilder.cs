using XSharp.Shared.Models;

namespace XSharp.Core.Manager;

internal class XFileStructureBuilder
{
    private readonly XFileStructure _xStructure;

    public XFileStructureBuilder(string fileName)
    {
        _xStructure = new XFileStructure();
        _xStructure.FileName = fileName;
    }
    //internal void AddXFile(string fileName, List<XSheetStructure>? sheetStructures = null)
    //{
    //    if(fileName.IsNullOrEmpty()) return;
    //    if (sheetStructures is null)
    //    {
    //        _xStructure.XFileStructures.Add(new XFileStructure()
    //        {
    //            FileName = fileName,
    //            XSheetStructures = new List<XSheetStructure>()
    //        });
    //        return;
    //    }
    //    _xStructure.XFileStructures.Add(new XFileStructure()
    //    {
    //        FileName = fileName,
    //        XSheetStructures = sheetStructures
    //    });
    //}
    internal void AddXSheet(string sheetName, string fixedName)
    {
        lock (_xStructure)
        {
            _xStructure.XSheetStructures.Add(new XSheetStructure()
            {
                Name = sheetName,
                FixedName = fixedName,
            });
        }
       
    }
    internal void SetXSheets(List<XSheetStructure> structures)
    {
        lock (_xStructure)
        {
            _xStructure.XSheetStructures = structures;
        }

    }
    //internal void AddXSheet(string fileName, string sheetName, string fixedName)
    //{
    //    var xSheetStructure = new XSheetStructure()
    //    {
    //        Name = sheetName,
    //        FixedName = fixedName,
    //    };
    //    var xFileStructure = _xStructure.XSheetStructures.Find(x => x.FileName == fileName);
    //    if (xFileStructure is null)
    //    {
    //        AddXFile(fileName, new List<XSheetStructure>() { xSheetStructure});
    //        return;
    //    }
    //    xFileStructure.XSheetStructures.Add(xSheetStructure);
    //}

    internal void ExportJson()
    {
        var json = XSerializer.SerializeJson(_xStructure);
        var dir = Path.Combine("StructureOutput");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, $"{_xStructure.FileName}.json");
        if (File.Exists(path)) File.Delete(path);
        File.WriteAllText(path, json);
    }

    //internal void ExportAsJson()
    //{
    //    _xStructure.XFileStructures.RemoveAll(x => x.XSheetStructures.Count == 0);
    //    var json = XSerializer.SerializeJson(_xStructure);
    //    var dir = Path.Combine("StructureOutputJson");
    //    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    //    string path = Path.Combine(dir, $"XStructure_{}.json");
    //    if(File.Exists(path)) File.Delete(path);
    //    File.WriteAllText(path, json);
    //    _xStructure.XFileStructures = new List<XFileStructure>();
    //}

    internal void ExportModels()
    {
        var dir = Path.Combine("StructureOutputModel");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var fixedXFileName = _xStructure.FileName.FixXName();
        var sb = new StringBuilder();
        XBuilderHelper.AppendUsingList(sb);
        XBuilderHelper.AppendNamespace(sb);
        XBuilderHelper.AppendXFileNameAttribute(sb, _xStructure.FileName);
        XBuilderHelper.AppendClassStart(sb, fixedXFileName);
        foreach (var sheet in _xStructure.XSheetStructures)
        {

            XBuilderHelper.AppendXSheetNameAttribute(sb, sheet.Name);
            XBuilderHelper.AppendProperty(sb, $"XSheet<{sheet.FixedName}>", fixedXFileName, sheet.FixedName);

        }
        XBuilderHelper.AppendEnd(sb);
        var fileOutPath = Path.Combine(dir, $"{fixedXFileName}.cs");
        if (File.Exists(fileOutPath)) File.Delete(fileOutPath);
        File.WriteAllText(fileOutPath, sb.ToString());

    }
}