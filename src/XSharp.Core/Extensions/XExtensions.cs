using System.Text;
using EasMe.Extensions;
using XSharp.Core.Lib;
using XSharp.Shared;
using XSharp.Shared.Models;

namespace XSharp.Core.Extensions;

internal static class XExtensions
{
    public static List<IXHeader> GetHeaders(this ExcelWorksheet sheet)
    {
        var headerValidator = XKernel.This.GetInstance<IXHeaderValidator>();
        var valueTypeRow = OptionLib.This.Option.SetValueTypesAtRowIndex;
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
            xHeader.SetName(c.Value?.ToString()?.RemoveLineEndings());
            xHeader.SetValueType(cellValueType);
            var isIgnore  = headerValidator.IsIgnore(xHeader);
            if (isIgnore) return null;
            return xHeader;
            
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