using OfficeOpenXml;
using XSharp.Shared.Models;
using XSharp.Shared.Obsolete;

namespace XSharp.Shared.Abstract;

public interface IXSheet
{
    string Name { get; }
    string? FixedName { get;  }
    Type? SheetModelType { get; }
    ExcelAddressBase Dimension { get; }
    List<object> Rows { get; }
    List<IXHeader> Headers { get; }
    void SetRows(List<object> rows);
    void SetHeaders(List<IXHeader> headers);
    void SetType(Type type);
    void SetName(string name);
    void SetFixedName(string name);
    void SetDimension(ExcelAddressBase dimension);
}

public interface IXSheet<T>
{
    string Name { get; }
    string? FixedName { get; }
    ExcelAddressBase Dimension { get; }
    List<T> Rows { get; }
    List<IXHeader> Headers { get; }
    void SetRows(List<T> rows);
    void SetHeaders(List<IXHeader> headers);
    void SetName(string name);
    void SetFixedName(string name);
    void SetDimension(ExcelAddressBase dimension);
}
