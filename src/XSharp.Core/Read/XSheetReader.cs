using System.Reflection;
using XSharp.Shared;

namespace XSharp.Core.Read;

public static class XSheetReader
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();

    public static ResultData<XSheet> Read(ExcelWorksheet? sheet, Type type, string fileName)
    {
        if (sheet is null) return Result.Warn("Worksheet is null");
        if (sheet.Dimension is null) return Result.Warn("Worksheet dimension is null or empty");
        var headers = sheet.GetHeaders();
        if (headers.Count == 0) return Result.Warn("Worksheet has no valid headers");
        if (sheet.Name.IsNullOrEmpty()) return Result.Warn("WorkSheet name is null or empty");
        var fixedName = sheet.Name.FixName();
        if (fixedName.IsNullOrEmpty()) return Result.Warn("FixedSheetName is null or empty. SheetName: " + sheet.Name);
        var xSheet = new XSheet();
        xSheet.Name = sheet.Name;
        xSheet.Dimension = sheet.Dimension;
        xSheet.FixedName = fixedName;
        xSheet.Headers = headers;
        xSheet.FileName = fileName;
        var rows = ReadRows(headers, sheet, type);
        xSheet.Rows = rows;
        return xSheet;
    }

    

    private static List<XRow<object>> ReadRows(IReadOnlyCollection<XHeader> headers, ExcelWorksheet sheet, Type type)
    {
        var start = sheet.Dimension.Start;
        var end = sheet.Dimension.End;
        var rows = new List<XRow<object>>();
        var validator = XOptionLib.This.GetValidator();
        for (var row = start.Row; row <= end.Row; row++)
        {
            if (row == XOptionLib.This.Option.HeaderColumnNumber) continue;
            var item = Activator.CreateInstance(type)!;
            var isSetAnyValue = false;
            var isIgnoredRow = false;
            for (var col = start.Column; col <= end.Column; col++)
            {
                try
                {
                    var cell = sheet.Cells[row, col];
                    var value = cell.Value;
                    if (value is null) continue;
                    if (validator?.IsIgnoreCell(value) == true) continue;
                    value = validator?.GetValidCellValue(value) ?? value;
                    var currentHeader = headers.FirstOrDefault(x => x.Index == col - 1);
                    if (currentHeader is null) continue;
                    isIgnoredRow = validator?.IsIgnoreRow(currentHeader, row, value) == true;
                    if (isIgnoredRow) break;
                    var property = type.GetProperty(currentHeader.FixedName ?? "",
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property is null) continue;
                    var isConvertSuccess =
                        XValueConverter.TryConvert(value?.ToString(), property.PropertyType, out var convertedVal);
                    if (!isConvertSuccess || convertedVal is null) continue;
                    property.SetValue(item, convertedVal);

                    isSetAnyValue = true;
                }
                catch (Exception ex)
                {
                    logger.Exception(ex, "Error while reading table: " + sheet.Name);
                }
            }

            if (!isSetAnyValue || isIgnoredRow) continue;
            rows.Add(new XRow<object>()
            {
                Data = item,
                Index = row
            });
        }
        return rows;
    }

    public static ResultData<XSheet<T>> Read<T>(ExcelWorksheet? sheet, string fileName)
    {
        return Read(sheet, typeof(T), fileName)
            .SelectAs(x => new XSheet<T>()
            {
                Name = x.Data.Name,
                Dimension = x.Data.Dimension,
                FixedName = x.Data.FixedName,
                Headers = x.Data.Headers,
                Rows = x.Data.GetRowsAs<T>(),
                FileName = x.Data.FileName
            });
    }

    private static List<XRow<T>> ReadRows<T>(IReadOnlyCollection<XHeader> headers, ExcelWorksheet sheet)
    {
        return ReadRows(headers, sheet, typeof(T)).Select(x => new XRow<T>
        {
            Index = x.Index,
            Data = (T)x.Data,
        }).ToList();
    }
}
