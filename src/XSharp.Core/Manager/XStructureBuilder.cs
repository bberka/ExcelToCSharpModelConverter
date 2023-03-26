namespace XSharp.Core.Manager;

internal static class XStructureBuilder
{
    private static readonly XStructure _xStructure = new XStructure();

    internal static void AddXFile(string fileName, List<XSheetStructure>? sheetStructures = null)
    {
        if(fileName.IsNullOrEmpty()) return;
        if (sheetStructures is null)
        {
            _xStructure.XFileStructures.Add(new XFileStructure()
            {
                FileName = fileName,
                XSheetStructures = new List<XSheetStructure>()
            });
            return;
        }
        _xStructure.XFileStructures.Add(new XFileStructure()
        {
            FileName = fileName,
            XSheetStructures = sheetStructures
        });
    }

    internal static void AddXSheet(string fileName, string sheetName, string fixedName)
    {
        var xSheetStructure = new XSheetStructure()
        {
            Name = sheetName,
            FixedName = fixedName,
        };
        var xFileStructure = _xStructure.XFileStructures.Find(x => x.FileName == fileName);
        if (xFileStructure is null)
        {
            AddXFile(fileName, new List<XSheetStructure>() { xSheetStructure});
            return;
        }
        xFileStructure.XSheetStructures.Add(xSheetStructure);
    }
    internal static void PrintAsJson()
    {
        _xStructure.XFileStructures.RemoveAll(x => x.XSheetStructures.Count == 0);
        var json = XSerializer.SerializeJson(_xStructure);
        const string path = "XStructure.json";
        if(File.Exists(path)) File.Delete(path);
        File.WriteAllText(path, json);
        _xStructure.XFileStructures = new List<XFileStructure>();
    }
}