using EasMe.Result;
using XSharp.Core.Extensions;
using XSharp.Shared;
using XSharp.Shared.Models;

namespace XSharp.Core.Manager;


public class XSheetManager
{
    public IXSheet Sheet { get; }

    private XSheetManager(IXSheet sheet)
    {
        Sheet = sheet;
    }    

    public Result ExportCsharpModel(string exportPath)
    {
        try
        {
            var start = Sheet.Dimension.Start;
            var end = Sheet.Dimension.End;
            var sheetName = Sheet.Name;
            var createResult = XModelManager.Create(Sheet);
            if (createResult.IsFailure) return createResult.ToResult();
            var creator = createResult.Data;
            return creator!.Write(exportPath);
        }
        catch (Exception ex)
        {
            return Result.Exception(ex);
        }
    }
    
    
    
    //Static
    public static ResultData<XSheetManager> Create(ExcelWorksheet? worksheet)
    {
        if (worksheet is null) return Result.Warn("Worksheet is null");
        if (worksheet.Dimension is null) return Result.Warn("Worksheet is empty");
        var sheetColumns = worksheet.GetHeaders();
        if(sheetColumns.Count == 0) return Result.Warn("Worksheet has no columns");
        var xsheet = XKernel.This.GetInstance<IXSheet>();
        xsheet.SetName(worksheet.Name);
        xsheet.SetHeaders(sheetColumns);
        xsheet.SetDimension(worksheet.Dimension);
        xsheet.SetFixedName(worksheet.Name.FixName());
        //xsheet.SetRows();
        return new XSheetManager(xsheet);
    }
    //Static
    public static ResultData<XSheetManager> Create(IXSheet? xSheet)
    {
        if (xSheet is null) return Result.Warn("xSheet is null");
        if (xSheet.Dimension is null) return Result.Warn("xSheet is empty");
        if(xSheet.Headers.Count == 0) return Result.Warn("xSheet has no columns");
        return new XSheetManager(xSheet);
    }
}

