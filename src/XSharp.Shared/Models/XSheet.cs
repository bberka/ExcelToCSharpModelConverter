using OfficeOpenXml;
using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;

//public partial class XSheet : IXSheet
//{
//    private List<OfficeOpenXml.ExcelRow> _rows;
//    public string Name { get; set; }
//    public string? FixedName { get; set; }
//    public Type SheetModelType { get; set; }
//    public ExcelAddressBase Dimension { get; set;}

//    public List<object> Rows { get; set; }
//    public List<IXHeader> Headers { get; set;}

//    public void SetRows(List<object> rows)
//    {
//        Rows = rows;
//    }

//    public void SetHeaders(List<IXHeader> headers)
//    {
//        Headers = headers;
//    }
//    public void SetType(Type type)
//    {
//        SheetModelType = type;
//    }
//    public void SetName(string workSheetName)
//    {
//        Name = workSheetName;
//    }

//    public void SetFixedName(string name)
//    {
//        FixedName = name;
//    }

//    public void SetDimension(ExcelAddressBase dimension)
//    {
//        Dimension = dimension;
//    }
//}

public class XSheet<T> : IXSheet<T>
{
    public Type SheetModelType => typeof(T);
    public string Name { get; set; }
    public string? FixedName { get; set; }
    public ExcelAddressBase Dimension { get; set; }
    public List<T> Rows { get; set; }
    public List<IXHeader> Headers { get; set; }

    public void SetRows(List<T> rows)
    {
        Rows = rows;
    }

    public void SetHeaders(List<IXHeader> headers)
    {
        Headers = headers;
    }

    public void SetName(string workSheetName)
    {
        Name = workSheetName;
    }

    public void SetFixedName(string name)
    {
        FixedName = name;
    }

    public void SetDimension(ExcelAddressBase dimension)
    {
        Dimension = dimension;
    }
}