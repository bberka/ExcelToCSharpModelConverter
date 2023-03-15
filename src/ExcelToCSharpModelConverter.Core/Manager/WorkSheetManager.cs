using EasMe.Logging;
using EasMe.Result;
using ExcelToCSharpModelConverter.Core.Extensions;
using OfficeOpenXml;

namespace ExcelToCSharpModelConverter.Core.Manager;


public class WorkSheetReader
{
    private readonly static IEasLog logger = EasLogFactory.CreateLogger();

    private readonly ExcelWorksheet _worksheet;
    public WorkSheetReader(ExcelWorksheet worksheet)
    {
        _worksheet = worksheet;
    }

    public Result ExportCsharpModel(string exportPath)
    {
        if (_worksheet.Dimension is null) return Result.Warn("Worksheet is empty");
        var start = _worksheet.Dimension.Start;
        var end = _worksheet.Dimension.End;
        var sheetName = _worksheet.Name;
        var sheetColumns = _worksheet.GetHeaders();
        if(sheetColumns.Count == 0) return Result.Warn("Worksheet has no columns");
        var creator = new ModelCreator(sheetName, sheetColumns);
        return creator.Write(exportPath);
    }
}

