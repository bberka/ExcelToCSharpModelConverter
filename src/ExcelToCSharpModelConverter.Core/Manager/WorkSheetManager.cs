using EasMe.Logging;
using EasMe.Result;
using ExcelToCSharpModelConverter.Core.Extensions;
using OfficeOpenXml;

namespace ExcelToCSharpModelConverter.Core.Manager;


public class WorkSheetManager
{

    private readonly ExcelWorksheet _worksheet;
    private readonly List<HeaderModel> _headers;

    private WorkSheetManager(ExcelWorksheet worksheet,List<HeaderModel> headers)
    {
        _worksheet = worksheet;
        _headers = headers;
    }
    
    public static ResultData<WorkSheetManager> Create(ExcelWorksheet worksheet)
    {
        if (worksheet is null) return Result.Warn("Worksheet is null");
        if (worksheet.Dimension is null) return Result.Warn("Worksheet is empty");
        var sheetColumns = worksheet.GetHeaders();
        if(sheetColumns.Count == 0) return Result.Warn("Worksheet has no columns");
        return new WorkSheetManager(worksheet ,sheetColumns);
    }
    public Result ExportCsharpModel(string exportPath)
    {
        try
        {
            var start = _worksheet.Dimension.Start;
            var end = _worksheet.Dimension.End;
            var sheetName = _worksheet.Name;
            var createResult = ModelCreator.Create(sheetName , _headers);
            if (createResult.IsFailure) return createResult.ToResult();
            var creator = createResult.Data;
            return creator!.Write(exportPath);
        }
        catch (Exception ex)
        {
            return Result.Exception(ex);
        }
    }
}

