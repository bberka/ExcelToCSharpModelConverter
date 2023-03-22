namespace XSharp.Core.Export;

internal static class XSheetExporter
{
    internal static Result Export(ExcelWorksheet? worksheet, string fileName,string outPath)
    {
        if (worksheet is null) return Result.Warn("Worksheet is null");
        if (worksheet.Dimension is null) return Result.Warn("Worksheet dimension is null or empty");
        var validator = XOptionLib.This.GetValidator();
        var isIgnore = validator.IsIgnoreSheetByName(worksheet.Name);
        if (isIgnore) return Result.Warn("Sheet is ignored by name: " + worksheet.Name);
        var fixedName = validator.GetValidSheetName(worksheet.Name).FixName();
        if (fixedName.IsNullOrEmpty())
            return Result.Warn("FixedSheetName is null or empty. SheetName: " + worksheet.Name);
        isIgnore = validator.IsIgnoreSheetByFixedName(fixedName);
        if (isIgnore) return Result.Warn("Sheet is ignored by fixed name: " + worksheet.Name + " FixedName: " + fixedName);
        var headers = worksheet.GetHeaders();
        if (headers.Count == 0) return Result.Warn("Worksheet has no valid headers");
        isIgnore = validator.IsIgnoreSheetByHeaders(headers);
        if (isIgnore) return Result.Warn("Sheet is ignored by headers: " + worksheet.Name + " FixedName: " + fixedName);
        var res = XSharpModelBuilder.ExportSharpModel(headers,worksheet.Name, fixedName,outPath);
        if (res.IsSuccess)
        {
            XStructureBuilder.AddXSheet(fileName,worksheet.Name,fixedName);
        }
        return res;
    }
}