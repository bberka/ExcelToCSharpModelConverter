using OfficeOpenXml;

namespace XSharp.Shared.Abstract;

public interface IXSheet
{
    string Name { get; set; }
    string? FixedName { get; set; }
    Type? SheetModelType { get; set; }
    ExcelAddressBase Dimension { get; set; }
    List<object> Rows { get; set; }
    List<IXHeader> Headers { get; set; }

}

public interface IXSheet<T>
{
    string Name { get; set; }
    string? FixedName { get; set; }
    ExcelAddressBase Dimension { get; set; }
    List<T> Rows { get; set; }
    List<IXHeader> Headers { get; set; }

}
