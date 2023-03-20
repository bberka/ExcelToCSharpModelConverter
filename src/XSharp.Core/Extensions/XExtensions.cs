using XSharp.Shared;

namespace XSharp.Core.Extensions;

internal static class XExtensions
{
    private const string _lowerAll = "abcdefghijklmnoprstuvwxyzq";
    private const string _upperAll = "ABCDEFGHIJKLMNOPRSTUVWXYZQ";
    private const string _digits = "0123456789";
    private const string _validChars = _lowerAll + _upperAll + _digits + "_";
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

    public static List<IXHeader> GetHeaders(this ExcelWorksheet sheet)
    {
        var headerValidator = XKernel.This.GetValidator<IXHeaderValidator>();
        var valueTypeRow = OptionLib.This.Option.SetValueTypesAtRowNumber;
        var firstRow = sheet.Dimension?.Start.Row;
        var firstRowData = sheet.Cells[firstRow ?? 0, 1, firstRow ?? 0, sheet.Dimension?.End.Column ?? 0];
        var exampleRowData = sheet.Cells[valueTypeRow, 1, valueTypeRow, sheet.Dimension?.End.Column ?? 0];
        var columns = firstRowData.Select((c, i) =>
        {
            // var cell = sheet.Cells[1, i + 1];
            var cellToGetValueType = exampleRowData[valueTypeRow, i + 1];
            if (c.Value is null) return null;
            var cellValueType = cellToGetValueType.Value?.GetType();
            var xHeader = XKernel.This.GetInstance<IXHeader>();
            xHeader.SetIndex(i);
            var headerValue = c.Value?.ToString()?.RemoveLineEndings();
            if (headerValue.IsNullOrEmpty() || headerValue == null)
            {
                logger.Warn($"Header value is null or empty at index {i}. SheetName: " + sheet.Name);
                return null;
            }

            xHeader.SetName(headerValue);
            if (cellValueType == null) xHeader.SetValueType(typeof(string));
            else xHeader.SetValueType(cellValueType);
            xHeader.SetFixedName(xHeader.Name.FixName());
            if (headerValidator is not null)
            {
                var isIgnore = headerValidator.IsIgnore(xHeader);
                if (isIgnore) return null;
            }

            if (xHeader.FixedName.IsNullOrEmpty() || xHeader.Name.IsNullOrEmpty()) return null;
            return xHeader;
        }).ToList();
        columns.RemoveAll(x => x is null);
        if (columns.Count == 0) return new List<IXHeader>();
        return columns.DistinctBy(x => x.Name).ToList();
    }

    public static string FixName(this string name)
    {
        var sb = new StringBuilder();
        name = name.TrimAbsolute().RemoveLineEndings();
        var isFirstChar = true;
        foreach (var c in name.Where(c => _validChars.Contains(c)))
        {
            if (isFirstChar && char.IsDigit(c))
            {
                sb.Append('_');
                isFirstChar = false;
            }

            sb.Append(c);
        }

        return sb.ToString();
    }
}