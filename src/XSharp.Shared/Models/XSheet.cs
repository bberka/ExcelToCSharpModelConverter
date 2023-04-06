using OfficeOpenXml;

namespace XSharp.Shared.Models;

public class XSheet<T>
{
    public Type SheetModelType => typeof(T);
    public string FileName { get; set; }
    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileName);
    public string Name { get; set; }
    public string? FixedName { get; set; }
    public ExcelAddressBase Dimension { get; set; }
    public List<XRow<T>> Rows { get; set; } = new();
    public List<XHeader> Headers { get; set; } = new();
}

public class XSheet
{
    public string FileName { get; set; }
    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileName);

    public string Name { get; set; }
    public string? FixedName { get; set; }
    public ExcelAddressBase Dimension { get; set; }
    public List<XRow<object>> Rows { get; set; } = new();
    public List<XHeader> Headers { get; set; } = new();

    public List<XRow<T>> GetRowsAs<T>()
    {
        return Rows.Select(x => new XRow<T>
            {
                Index = x.Index,
                Data = (T)x.Data
            })
            .ToList();
    }

    public XSheet<T> GetAsXSheetT<T>()
    {
        return new XSheet<T>
        {
            Name = Name,
            FixedName = FixedName,
            Rows = GetRowsAs<T>(),
            Dimension = Dimension,
            FileName = FileName,
            Headers = Headers
        };
    }
}