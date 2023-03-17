using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using XSharp.Shared.Abstract;
using XSharp.Shared.Models;

namespace XSharp.Shared;

public static class XFactory
{
    public static IXHeader CreateHeader(int index, string name, string fixedName, Type valueType)
    {
        var header = XKernel.This.GetInstance<IXHeader>();
        header.Index = index;
        header.Name = name;
        header.FixedName = fixedName;
        header.ValueType = valueType;
        return header;
    }
    public static IXHeader CreateHeader(int index)
    {
        var header = XKernel.This.GetInstance<IXHeader>();
        header.Index = index;
        return header;
    }
    public static IXCell CreateCell(object? value, Type? type, IXHeader header)
    {
        var cell = XKernel.This.GetInstance<IXCell>();
        cell.Value = value;
        cell.Type = type;
        cell.Header = header;
        return cell;
    }

    public static IXRow CreateRow(List<IXCell> cells, int index)
    {
        var row = XKernel.This.GetInstance<IXRow>();
        row.Cells = cells;
        row.Index = index;
        return row;
    }

    public static IXSheet CreateSheet(string name, string? fixedName, Type? sheetModelType, ExcelAddressBase dimension, List<IXRow> rows, List<IXHeader> headers)
    {
        var sheet = XKernel.This.GetInstance<IXSheet>();
        sheet.Name = name;
        sheet.FixedName = fixedName;
        sheet.SheetModelType = sheetModelType;
        sheet.Dimension = dimension;
        sheet.Rows = rows;
        sheet.Headers = headers;
        return sheet;
    }

    public static IXFile CreateFile(string name, List<IXSheet> sheets)
    {
        var file = XKernel.This.GetInstance<IXFile>();
        file.Name = name;
        file.Sheets = sheets;
        return file;
    }



    public static IXCell CreateCell(ExcelCell cell)
    {
        var xcell = XKernel.This.GetInstance<IXCell>();
        xcell.Value = cell.Value;
        xcell.Type = cell.Value?.GetType();
        xcell.Header = CreateHeader(cell.ColIndex);
        xcell.Formula = cell.Formula;
        return xcell;
    }
    
}