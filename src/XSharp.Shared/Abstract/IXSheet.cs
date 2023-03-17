using OfficeOpenXml;
using XSharp.Shared.Models;

namespace XSharp.Shared.Abstract;

public interface IXSheet
{
    string Name { get; internal set;}
    string? FixedName { get; internal set; }
    Type? SheetModelType { get; internal set;}
    ExcelAddressBase Dimension { get; internal set;}
    List<IXRow> Rows { get; internal set;}
    List<IXHeader> Headers { get; internal set;}
    void SetRows(List<IXRow> rows);
    void SetHeaders(List<IXHeader> headers);
    void SetType(Type type);
    void SetName(string name);
    void SetFixedName(string name);
    void SetDimension(ExcelAddressBase dimension);
}
