using System.Reflection;
using XSharp.Shared;

namespace XSharp.Core.Manager;

public class XSheetManager : IDisposable
{
    private readonly ExcelWorksheet _worksheet;
    private readonly string _fileName;
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();

    public XSheetManager(ExcelWorksheet worksheet, string fileName)
    {
        _worksheet = worksheet;
        _fileName = fileName;
    }
    public Result Export(string outPath,out XSheetStructure? structure)
    {
        structure = null;
        //if (worksheet is null) return Result.Warn("Worksheet is null");
        if (_worksheet.Dimension is null) return Result.Warn("Worksheet dimension is null or empty");
        var validator = XOptionLib.This.GetValidator();
        var fixedName = validator.GetValidSheetName(_worksheet.Name).FixXName();
        if (fixedName.IsNullOrEmpty()) return Result.Warn("FixedSheetName is null or empty. SheetName: " + _worksheet.Name);
        var isIgnore = validator.IsIgnoreSheetByName(_worksheet.Name) || validator.IsIgnoreSheetByFixedName(fixedName);
        if (isIgnore) return Result.Warn("Sheet is ignored: " + _worksheet.Name);
        var headers = GetHeaders();
        if (headers.Count == 0) return Result.Warn("Worksheet has no valid headers");
        isIgnore = validator.IsIgnoreSheetByHeaders(headers);
        if (isIgnore) return Result.Warn("Sheet is ignored by headers: " + _worksheet.Name + " FixedName: " + fixedName);
        structure = new XSheetStructure()
        {
            FixedName = fixedName,
            Name = _worksheet.Name,
        };
        return XSheetModelBuilder.ExportSharpModel(headers, _worksheet.Name, fixedName, outPath);
    }

    public ResultData<XSheet> Read(Type type)
    {
        //if (sheet is null) return Result.Warn("Worksheet is null");
        if (_worksheet.Dimension is null) return Result.Warn("Worksheet dimension is null or empty");
        var headers = GetHeaders();
        if (headers.Count == 0) return Result.Warn("Worksheet has no valid headers");
        if (_worksheet.Name.IsNullOrEmpty()) return Result.Warn("WorkSheet name is null or empty");
        var fixedName = _worksheet.Name.FixXName();
        if (fixedName.IsNullOrEmpty()) return Result.Warn("FixedSheetName is null or empty. SheetName: " + _worksheet.Name);
        var xSheet = new XSheet();
        xSheet.Name = _worksheet.Name;
        xSheet.Dimension = _worksheet.Dimension;
        xSheet.FixedName = fixedName;
        xSheet.Headers = headers;
        xSheet.FileName = _fileName;
        var rows = ReadRows(headers, type);
        xSheet.Rows = rows;
        return xSheet;
    }

    public List<XRow<object>> ReadRows(IReadOnlyCollection<XHeader> headers, Type type)
    {
        var start = _worksheet.Dimension.Start;
        var end = _worksheet.Dimension.End;
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
                    var cell = _worksheet.Cells[row, col];
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
                    logger.Exception(ex, "Error while reading table: " + _worksheet.Name);
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

    public ResultData<XSheet<T>> Read<T>()
    {
        return Read(typeof(T))
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

    public List<XRow<T>> ReadRows<T>(IReadOnlyCollection<XHeader> headers)
    {
        return ReadRows(headers, typeof(T)).Select(x => new XRow<T>
        {
            Index = x.Index,
            Data = (T)x.Data,
        }).ToList();
    }

    public IEnumerable<XCell> ReadCells()
    {
        var startRow = _worksheet.Dimension?.Start.Row;
        var startCol = _worksheet.Dimension?.Start.Column;
        var endRow = _worksheet.Dimension?.End.Row;
        var endCol = _worksheet.Dimension?.End.Column;
        for (var row = startRow ?? 0; row <= (endRow ?? 0); row++)
        {
            for (var col = startCol ?? 0; col <= (endCol ?? 0); col++)
            {
                var cell = _worksheet.Cells[row, col];
                yield return new XCell(cell, row, col);
            }
        }
    }
    public IEnumerable<IGrouping<int, XCell>> ReadRows()
    {
        return ReadCells().GroupBy(x => x.RowIndex);
    }



    private List<XRow<object>> ReadRows_New(IReadOnlyCollection<XHeader> headers, Type type)
    {
        var start = _worksheet.Dimension.Start;
        var end = _worksheet.Dimension.End;
        var rows = new List<XRow<object>>();
        var validator = XOptionLib.This.GetValidator();
        var list = ReadRows();
        foreach (var item in list)
        {
            if (item.Key == XOptionLib.This.Option.HeaderColumnNumber) continue;
            var obj = Activator.CreateInstance(type)!;
            var isSetAnyValue = false;
            var isIgnoredRow = false;
            foreach (var cell in item)
            {
                try
                {
                    var value = cell.Value;
                    if (value is null) continue;
                    if (validator?.IsIgnoreCell(value) == true) continue;
                    value = validator?.GetValidCellValue(value) ?? value;
                    var currentHeader = headers.FirstOrDefault(x => x.Index == cell.ColumnIndex - 1);
                    if (currentHeader is null) continue;
                    isIgnoredRow = validator?.IsIgnoreRow(currentHeader, cell.RowIndex, value) == true;
                    if (isIgnoredRow) break;

                    var property = type.GetProperty(currentHeader.FixedName ?? "",
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property is null) continue;
                    var isConvertSuccess =
                        XValueConverter.TryConvert(value?.ToString(), property.PropertyType, out var convertedVal);
                    if (!isConvertSuccess || convertedVal is null) continue;
                    property.SetValue(obj, convertedVal);
                    isSetAnyValue = true;
                }
                catch (Exception ex)
                {
                    logger.Exception(ex, "Error while reading table: " + _worksheet.Name);
                }
            }
            if (!isSetAnyValue || isIgnoredRow) continue;
            rows.Add(new XRow<object>()
            {
                Data = obj,
                Index = item.Key
            });
        }
        return rows;
    }

    public List<XHeader> GetHeaders()
    {
        var validator = XOptionLib.This.GetValidator();
        var valueTypeRow = XOptionLib.This.Option.SetValueTypesAtRowNumber;
        var firstRow = _worksheet.Dimension?.Start.Row;
        var firstRowData = _worksheet.Cells[firstRow ?? 0, 1, firstRow ?? 0, _worksheet.Dimension?.End.Column ?? 0];
        var exampleRowData = _worksheet.Cells[valueTypeRow, 1, valueTypeRow, _worksheet.Dimension?.End.Column ?? 0];
        var columns = firstRowData.Select((c, i) =>
        {
            if (c.Value is null) return null;
            var cellToGetValue = exampleRowData[valueTypeRow, i + 1].Value;
            var xHeader = XKernel.This.GetInstance<XHeader>();
            xHeader.Index = i;
            var headerValue = c.Value?.ToString()?.RemoveLineEndings();
            if (headerValue.IsNullOrEmpty() || headerValue == null)
            {
                logger.Debug($"Header value is null or empty at index {i}. SheetName: " + _worksheet.Name);
                return null;
            }
            var tryType = XValueConverter.GetTryType(cellToGetValue?.ToString(), typeof(string));//Todo: OptionLib.This.Option.DefaultValueType use this
            var type = validator.GetHeaderType(headerValue, cellToGetValue, tryType);
            xHeader.Name = headerValue;
            xHeader.ValueType = type;
            xHeader.FixedName = xHeader.Name.FixXName();
            var isIgnore = validator.IsIgnoreHeader(xHeader);
            if (isIgnore) return null;
            if (xHeader.FixedName.IsNullOrEmpty() || xHeader.Name.IsNullOrEmpty()) return null;

            xHeader.Comment = c?.Comment?.Text;
            return xHeader;
        }).ToList();
        columns.RemoveAll(x => x is null);
        return columns.Count == 0 ? new List<XHeader>() : columns.DistinctBy(x => x!.Name).DistinctBy(x => x!.FixedName).ToList()!;
    }

    public void Dispose()
    {
        _worksheet.Dispose();
    }

}