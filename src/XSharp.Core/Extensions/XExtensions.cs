using System.Diagnostics;
using XSharp.Core.Export;
using XSharp.Shared;

namespace XSharp.Core.Extensions;

internal static class XExtensions
{
    private const string _lowerAll = "abcdefghijklmnoprstuvwxyzq";
    private const string _upperAll = "ABCDEFGHIJKLMNOPRSTUVWXYZQ";
    private const string _digits = "0123456789";
    private const string _validChars = _lowerAll + _upperAll + _digits;
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();

    public static List<ExcelWorksheet> GetSheets(this ExcelPackage package)
    {
        var sheets = new List<ExcelWorksheet>();
        for (var i = 1; i <= package.Workbook.Worksheets.Count; i++)
        {
            var sheet = package.Workbook.Worksheets[i];
            sheets.Add(sheet);
        }

        return sheets;
    }

    public static bool IsFilePathExcel(this string filePath)
    {
        return filePath!.EndsWith(".xlsx") || filePath.EndsWith(".xlsm") || filePath.EndsWith(".xls");
    }

    public static bool IsFilePathCsv(this string filePath)
    {
        return filePath!.EndsWith(".csv");
    }

    public static List<XHeader> GetHeaders(this ExcelWorksheet sheet)
    {
        var validator = XOptionLib.This.GetValidator();
        var valueTypeRow = XOptionLib.This.Option.SetValueTypesAtRowNumber;
        var firstRow = sheet.Dimension?.Start.Row;
        var firstRowData = sheet.Cells[firstRow ?? 0, 1, firstRow ?? 0, sheet.Dimension?.End.Column ?? 0];
        var exampleRowData = sheet.Cells[valueTypeRow, 1, valueTypeRow, sheet.Dimension?.End.Column ?? 0];
        var columns = firstRowData.Select((c, i) =>
        {
            if (c.Value is null) return null;
            var cellToGetValue = exampleRowData[valueTypeRow, i + 1].Value;
            var xHeader = XKernel.This.GetInstance<XHeader>();
            xHeader.Index = i;
            var headerValue = c.Value?.ToString()?.RemoveLineEndings();
            if (headerValue.IsNullOrEmpty() || headerValue == null)
            {
                logger.Debug($"Header value is null or empty at index {i}. SheetName: " + sheet.Name);
                return null;
            }
            var tryType = XValueConverter.GetTryType(cellToGetValue?.ToString(), typeof(string));//Todo: OptionLib.This.Option.DefaultValueType use this
            var type = validator.GetHeaderType(headerValue, cellToGetValue, tryType);
            xHeader.Name = headerValue;
            xHeader.ValueType = type;
            xHeader.FixedName= xHeader.Name.FixName();
            var isIgnore = validator.IsIgnoreHeader(xHeader);
            if (isIgnore) return null;
            if (xHeader.FixedName.IsNullOrEmpty() || xHeader.Name.IsNullOrEmpty()) return null;

            xHeader.Comment = c?.Comment?.Text;
            return xHeader;
        }).ToList();
        columns.RemoveAll(x => x is null);
        return columns.Count == 0 ? new List<XHeader>() : columns.DistinctBy(x => x.Name).DistinctBy(x => x.FixedName).ToList();
    }

    public static string FixName(this string name)
    {
        var sb = new StringBuilder();
        name = name.TrimAbsolute().RemoveLineEndings().RemoveText("_");
        var charList = name.Where(c => _validChars.Contains(c)).ToList();
        for (var i = 0; i < charList.Count; i++)
        {
            var ch = charList[i];
            if (i == 0)
            {
                if (char.IsDigit(ch))
                    sb.Append('_');
                if (char.IsLower(ch))
                    ch = char.ToUpper(ch);
            }
            sb.Append(ch);
        }
        return sb.ToString();
    }
}