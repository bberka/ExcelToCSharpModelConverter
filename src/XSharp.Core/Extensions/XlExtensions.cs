using System.Text;
using EasMe.Extensions;
using XSharp.Core.Lib;
using XSharp.Shared.Models;

namespace XSharp.Core.Extensions;

public static class XlExtensions
{
    public static List<HeaderModel> GetHeaders(this ExcelWorksheet sheet)
    {
        var firstRow = sheet.Dimension?.Start.Row;
        var firstRowData = sheet.Cells[firstRow ?? 0, 1, firstRow ?? 0, sheet.Dimension?.End.Column ?? 0];
        var exampleRowData = sheet.Cells[OptionLib.This.Option.SetValueTypesAtRowIndex, 1, OptionLib.This.Option.SetValueTypesAtRowIndex, sheet.Dimension?.End.Column ?? 0];
        var columns = firstRowData.Select((c, i) =>
        {
            var cell = sheet.Cells[1, i + 1];
            var cellToGetValueType = exampleRowData[OptionLib.This.Option.SetValueTypesAtRowIndex, i + 1];
            if (c.Value is null) return null;
            var cellValueType = cellToGetValueType.Value?.GetType();
            return new HeaderModel()
            {
                Index = i,
                Name = c.Value?.ToString()?.RemoveLineEndings(),
                ValueType = cellValueType
            };
        }).ToList();
        columns.RemoveAll(x => x is null);
        return columns.DistinctBy(x => x.Name).ToList() ?? new();
    }

 
    const string _lowerAll = "abcdefghijklmnoprstuvwxyzq";
    const string _upperAll = "ABCDEFGHIJKLMNOPRSTUVWXYZQ";
    const string _digits = "0123456789";
    const string _validChars = _lowerAll + _upperAll + _digits + "_";
    
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