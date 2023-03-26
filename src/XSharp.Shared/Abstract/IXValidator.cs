namespace XSharp.Shared.Abstract;

public interface IXValidator
{
    public bool IsIgnoreRow(XHeader header, int rowIndex, object? cellValue);
    public bool IsIgnoreSheetByName(string name);
    public bool IsIgnoreSheetByFixedName(string fixedName);
    public bool IsIgnoreSheetByHeaders(List<XHeader> headers);
    public bool IsIgnoreHeader(XHeader header);
    public bool IsIgnoreFileByPath(string filePath);
    public bool IsIgnoreCell(object? value);
    public Type GetHeaderType(string headerName, object? cellValueToGetType, Type defaultType);
    public string GetValidSheetName(string name);
    public object? GetValidCellValue(object? value);
}