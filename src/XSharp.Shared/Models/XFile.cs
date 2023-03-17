using EasMe.Result;
using OfficeOpenXml;
using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;



public partial class XFile : IXFile
{
    public string Name { get; set; } = string.Empty;
    public List<IXSheet> Sheets { get; set; } = new List<IXSheet>();

    public void SetName(string name)
    {
        Name = name;
    }
    public void SetSheets(List<IXSheet> sheets)
    {
        Sheets = sheets;
    }

    public Result AddSheet(IXSheet sheet)
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