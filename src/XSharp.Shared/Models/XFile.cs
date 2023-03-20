using EasMe.Result;
using OfficeOpenXml;
using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;

public partial class XFile : IXFile
{
    public string Name { get; set; } = string.Empty;
    public List<XSheet<object>> Sheets { get; set; } = new List<XSheet<object>>();

    public void SetName(string name)
    {
        Name = name;
    }
    public void SetSheets(List<XSheet<object>> sheets)
    {
        Sheets = sheets;
    }

    public Result AddSheet(XSheet<object> sheet)
    {
        var any = Sheets.Any(x => x.Name == sheet.Name);
        if (any)
        {
            return Result.Error($"Sheet with name {sheet.Name} already exists");
        }
        Sheets.Add(sheet);
        return Result.Success();
    }
}
