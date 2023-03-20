using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using XSharp.Shared.Abstract;
using XSharp.Shared.Obsolete;

namespace XSharp.Shared;

public static class XFactory
{
    public static IXHeader CreateHeader(int index, string name, string fixedName, Type valueType)
    {
        var header = XKernel.This.GetInstance<IXHeader>();
        header.SetIndex(index);
        header.SetName(name);
        header.SetFixedName(fixedName);
        header.SetValueType(valueType);
        return header;
    }

    public static IXHeader CreateHeader(int index)
    {
        var header = XKernel.This.GetInstance<IXHeader>();
        header.SetIndex(index);
        return header;
    }

    public static IXCell CreateCell(object? value, Type? type, IXHeader header)
    {
        var cell = XKernel.This.GetInstance<IXCell>();
        cell.SetValue(value);
        cell.SetType(type);
        cell.SetHeader(header);
        return cell;
    }

    public static IXRow CreateRow(List<IXCell> cells, int index)
    {
        var row = XKernel.This.GetInstance<IXRow>();

        row.SetCells(cells);
        row.SetIndex(index);
        return row;
    }

    public static IXSheet CreateSheet(string name, string? fixedName, Type? sheetModelType, ExcelAddressBase dimension,
        List<object> rows, List<IXHeader> headers)
    {
        var sheet = XKernel.This.GetInstance<IXSheet>();
        sheet.SetName(name);
        sheet.SetFixedName(fixedName);
        sheet.SetType(sheetModelType);
        sheet.SetDimension(dimension);
        sheet.SetRows(rows);
        sheet.SetHeaders(headers);
        return sheet;
    }


    public static IXCell CreateCell(ExcelCell cell)
    {
        var xcell = XKernel.This.GetInstance<IXCell>();
        xcell.SetValue(cell.Value);
        xcell.SetType(cell.Value?.GetType());
        xcell.SetHeader(CreateHeader(cell.ColIndex));
        xcell.SetFormula(cell.Formula);
        return xcell;
    }
}