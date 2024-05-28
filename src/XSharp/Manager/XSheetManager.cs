using System.Reflection;
using Microsoft.Extensions.Logging;
using XSharp.Models;

namespace XSharp.Manager;

public sealed class XSheetManager : IDisposable
{
  private readonly string _fileName;
  private readonly XSharpOptions _options;
  private readonly ExcelWorksheet _worksheet;

  public XSheetManager(ExcelWorksheet worksheet, string fileName, XSharpOptions options) {
    _worksheet = worksheet;
    _fileName = fileName;
    _options = options;
  }

  public void Dispose() {
    _worksheet.Dispose();
  }

  public void Export(string outPath, out XSheetStructure? structure) {
    structure = null;
    //if (worksheet is null) return Result.Warn("Worksheet is null");
    if (_worksheet.Dimension is null)
      throw new Exception("Worksheet dimension is null or empty");
    var validator = _options.XValidator;
    var fixedName = validator.GetValidSheetName(_worksheet.Name).FixXName();
    if (!fixedName.HasContent())
      throw new Exception("FixedSheetName is null or empty. SheetName: " + _worksheet.Name);
    var isIgnore = validator.IsIgnoreSheetByName(_worksheet.Name) || validator.IsIgnoreSheetByFixedName(fixedName);
    if (isIgnore)
      throw new Exception("Sheet is ignored: " + _worksheet.Name);
    var headers = GetHeaders().ToList();
    if (headers.Count == 0)
      throw new Exception("Worksheet has no valid headers");
    isIgnore = validator.IsIgnoreSheetByHeaders(headers);
    if (isIgnore)
      throw new Exception("Sheet is ignored by headers: " + _worksheet.Name + " FixedName: " + fixedName);
    structure = new XSheetStructure {
      FixedName = fixedName,
      Name = _worksheet.Name
    };
    XSheetModelBuilder.ExportSharpModel(headers, _worksheet.Name, fixedName, outPath, _options);
  }

  public static void ClearEmptyRowsAndColumns(ExcelPackage excelPackage) {
    foreach (var worksheet in excelPackage.Workbook.Worksheets) {
      var start = worksheet.Dimension.Start;
      var end = worksheet.Dimension.End;

      // Remove empty rows
      for (var row = end.Row; row >= start.Row; row--) {
        var isEmptyRow = true;
        for (var col = start.Column; col <= end.Column; col++) {
          var cellValue = worksheet.Cells[row, col].Value;
          if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString())) continue;
          isEmptyRow = false;
          break;
        }

        if (isEmptyRow) worksheet.DeleteRow(row, 1);
      }

      // Remove empty columns
      for (var col = end.Column; col >= start.Column; col--) {
        var isEmptyColumn = true;
        for (var row = start.Row; row <= end.Row; row++) {
          var cellValue = worksheet.Cells[row, col].Value;
          if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString())) {
            isEmptyColumn = false;
            break;
          }
        }

        if (isEmptyColumn) worksheet.DeleteColumn(col, 1);
      }
    }

    excelPackage.Save();
  }

  public XSheet Read(Type type) {
    //if (sheet is null) return Result.Warn("Worksheet is null");
    if (_worksheet.Dimension is null)
      throw new Exception("Worksheet dimension is null or empty");
    var headers = GetHeaders().ToList();
    if (headers.Count == 0)
      throw new Exception("Worksheet has no valid headers");
    if (!_worksheet.Name.HasContent())
      throw new Exception("Worksheet name is null or empty");
    var fixedName = _worksheet.Name.FixXName();
    if (!fixedName.HasContent())
      throw new Exception("FixedSheetName is null or empty. SheetName: " + _worksheet.Name);
    var xSheet = new XSheet {
      Name = _worksheet.Name,
      Dimension = _worksheet.Dimension,
      FixedName = fixedName,
      Headers = headers,
      FileName = _fileName,
      Rows = ReadRows(headers, type, _worksheet)
    };
    return xSheet;
  }

  public IEnumerable<XRow<object>> ReadRows(IReadOnlyCollection<XHeader> headers, Type type, ExcelWorksheet worksheet) {
    var start = worksheet.Dimension.Start;
    var end = worksheet.Dimension.End;
    //var rows = new List<XRow<object>>();
    var validator = _options.XValidator;
    for (var row = start.Row; row < end.Row + 1; row++) {
      if (row == _options.HeaderColumnNumber) continue;
      var item = Activator.CreateInstance(type)!;
      var isSetAnyValue = false;
      var isIgnoredRow = false;
      for (var col = start.Column; col <= end.Column; col++)
        try {
          var cell = worksheet.Cells[row, col];
          var value = cell.Value;
          if (value is null) continue;
          if (validator?.IsIgnoreCell(col, row, value) == true) continue;
          value = validator?.GetValidCellValue(value) ?? value;
          var currentHeader = headers.FirstOrDefault(x => x.Index == col - 1);
          if (currentHeader is null) continue;
          isIgnoredRow = validator?.IsIgnoreRow(currentHeader, row, value) == true;
          if (isIgnoredRow) break;
          var property = type.GetProperty(currentHeader.FixedName ?? "",
                                          BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
          if (property is null) continue;
          var isConvertSuccess =
            XValueConverter.TryConvert(value.ToString(), property.PropertyType, out var convertedVal);
          if (!isConvertSuccess || convertedVal is null) continue;
          property.SetValue(item, convertedVal);

          isSetAnyValue = true;
        }
        catch (Exception ex) {
          _options.Logger?.LogError(ex, "Error while reading table {SheetName}", worksheet.Name);
        }

      if (!isSetAnyValue || isIgnoredRow) continue;
      var xRow = new XRow<object> {
        Data = item,
        Index = row
      };
      yield return xRow;
    }
  }

  public XSheet<T> Read<T>() {
    var sheet = Read(typeof(T));
    return new XSheet<T> {
      Name = sheet.Name,
      Dimension = sheet.Dimension,
      FixedName = sheet.FixedName,
      Headers = sheet.Headers,
      FileName = sheet.FileName,
      Rows = sheet.Rows.Select(x => new XRow<T> {
        Index = x.Index,
        Data = (T)x.Data
      })
    };
  }

  public IEnumerable<XRow<T>> ReadRows<T>(IReadOnlyCollection<XHeader> headers, ExcelWorksheet worksheet, XSharpOptions options) {
    return ReadRows(headers, typeof(T), worksheet)
      .Select(x => new XRow<T> {
        Index = x.Index,
        Data = (T)x.Data
      });
  }

  public IEnumerable<XCell> ReadCells() {
    var startRow = _worksheet.Dimension?.Start.Row;
    var startCol = _worksheet.Dimension?.Start.Column;
    var endRow = _worksheet.Dimension?.End.Row;
    var endCol = _worksheet.Dimension?.End.Column;
    for (var row = startRow ?? 0; row <= (endRow ?? 0); row++)
    for (var col = startCol ?? 0; col <= (endCol ?? 0); col++) {
      var cell = _worksheet.Cells[row, col];
      yield return new XCell(cell, row, col);
    }
  }

  public IEnumerable<IGrouping<int, XCell>> ReadRows() {
    return ReadCells().GroupBy(x => x.RowIndex);
  }


  private IEnumerable<XRow<object>> ReadRows_New(IReadOnlyCollection<XHeader> headers, Type type) {
    var start = _worksheet.Dimension.Start;
    var end = _worksheet.Dimension.End;
    var rows = new List<XRow<object>>();
    var validator = _options.XValidator;
    var list = ReadRows();
    foreach (var item in list) {
      if (item.Key == _options.HeaderColumnNumber) continue;
      var obj = Activator.CreateInstance(type)!;
      var isSetAnyValue = false;
      var isIgnoredRow = false;
      foreach (var cell in item)
        try {
          var value = cell.Value;
          if (value is null) continue;
          if (validator?.IsIgnoreCell(cell.RowIndex, cell.ColumnIndex, value) == true) continue;
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
        catch (Exception ex) {
          _options.Logger.LogError(ex, "Error while reading table {SheetName}", _worksheet.Name);
        }

      if (!isSetAnyValue || isIgnoredRow) continue;
      yield return new XRow<object> {
        Data = obj,
        Index = item.Key
      };
    }
  }

  public IEnumerable<XHeader> GetHeaders() {
    var validator = _options.XValidator;
    var firstRow = _worksheet.Dimension?.Start.Row;
    var firstRowData = _worksheet.Cells[firstRow ?? 0, 1, firstRow ?? 0, _worksheet.Dimension?.End.Column ?? 0];
    var columns = firstRowData.Select((c, i) => {
                                if (c.Value is null) return null;
                                var xHeader = new XHeader {
                                  Index = i
                                };
                                var headerValue = c.Value?.ToString()?.RemoveLineEndings();
                                if (!headerValue.HasContent() || headerValue == null) {
                                  _options.Logger?.LogDebug("Header value is null or empty at index {Index} {SheetName}", i, _worksheet.Name);
                                  return null;
                                }

                                xHeader.Name = headerValue;
                                xHeader.FixedName = xHeader.Name.FixXName();
                                var isIgnore = validator.IsIgnoreHeader(xHeader);
                                if (isIgnore) return null;
                                if (!xHeader.FixedName.HasContent() || !xHeader.Name.HasContent()) return null;

                                xHeader.Comment = c?.Comment?.Text;
                                return xHeader;
                              })
                              .ToList();
    columns.RemoveAll(x => x is null);
    return columns.Count == 0
             ? new List<XHeader>()
             : columns.DistinctBy(x => x!.Name).DistinctBy(x => x!.FixedName).ToList()!;
  }

  //public IEnumerable<XHeader> GetHeaders() {
  //  var validator = XOptionLib.This.GetValidator();
  //  var valueTypeRow = XOptionLib.This.Option.SetValueTypesAtRowNumber;
  //  var firstRow = _worksheet.Dimension?.Start.Row;
  //  var firstRowData = _worksheet.Cells[firstRow ?? 0, 1, firstRow ?? 0, _worksheet.Dimension?.End.Column ?? 0];
  //  var exampleRowData = _worksheet.Cells[valueTypeRow, 1, valueTypeRow, _worksheet.Dimension?.End.Column ?? 0];

  //  var cols = new List<string>();

  //  for (var i = 0; i < firstRowData.Columns; i++) {
  //    var c = firstRowData[1, i];
  //    if (c.Value is null) {
  //      continue;
  //    }
  //    var cellToGetValue = exampleRowData[valueTypeRow, i + 1].Value;
  //    var xHeader = XKernel.This.GetInstance<XHeader>();
  //    xHeader.Index = i;
  //    var headerValue = c.Value?.ToString()?.RemoveLineEndings();
  //    if (headerValue.IsNullOrEmpty() || headerValue == null) {
  //      logger.Debug($"Header value is null or empty at index {i}. SheetName: " + _worksheet.Name);
  //      continue;
  //    }
  //    var tryType = XValueConverter.GetTryType(cellToGetValue?.ToString(), typeof(string));
  //    var type = validator.GetHeaderType(headerValue, cellToGetValue, tryType);
  //    xHeader.Name = headerValue;
  //    xHeader.ValueType = type;
  //    xHeader.FixedName = xHeader.Name.FixXName();
  //    var isIgnore = validator.IsIgnoreHeader(xHeader);
  //    if (isIgnore) {
  //      continue;
  //    }
  //    if (xHeader.FixedName.IsNullOrEmpty() || xHeader.Name.IsNullOrEmpty()) {
  //      continue;
  //    }
  //    xHeader.Comment = c.Comment?.Text;
  //    var isExist = cols.Contains(xHeader.FixedName);
  //    if (isExist) continue;
  //    cols.Add(xHeader.FixedName);
  //    yield return xHeader;
  //  }
  //}
}