namespace XSharp.Core.Manager;

internal class XFileStructureBuilder
{
    private readonly string _fileExt;
    private readonly XFileStructure _xStructure;

    public XFileStructureBuilder(string fileName,string fileExt)
    {
        _fileExt = fileExt;
        _xStructure = new XFileStructure();
        _xStructure.FileName = fileName;
    }


    internal void AddXSheet(string sheetName, string fixedName)
    {
        lock (_xStructure)
        {
            _xStructure.XSheetStructures.Add(new XSheetStructure
            {
                Name = sheetName,
                FixedName = fixedName
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
   

    internal void ExportJson()
    {
        var json = XSerializer.SerializeJson(_xStructure);
        var dir = Path.Combine("StructureOutput");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, $"{_xStructure.FileName}.json");
        if (File.Exists(path)) File.Delete(path);
        File.WriteAllText(path, json);
    }

 

    internal void ExportModels()
    {
        var dir = Path.Combine("StructureOutputModel");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var fixedXFileName = _xStructure.FileName.FixXName();
        var sb = new StringBuilder();
        XBuilderHelper.AppendUsingList(sb);
        XBuilderHelper.AppendNamespace(sb);
        XBuilderHelper.AppendXFileNameAttribute(sb, _xStructure.FileName,_fileExt);
        XBuilderHelper.AppendClassStart(sb, fixedXFileName, false);
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