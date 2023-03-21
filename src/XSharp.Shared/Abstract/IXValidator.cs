namespace XSharp.Shared.Abstract;

public interface IXValidator
{
    public bool IsIgnoreRow(IXHeader header, int rowIndex, object? cellValue);
    public bool IsIgnoreSheetByName(string name);
    public bool IsIgnoreHeader(IXHeader header);
    public bool IsIgnoreFileByPath(string filePath);
    public bool IsIgnoreCell(object? value);
    public Type GetHeaderType(string headerName, object? cellValueToGetType, Type defaultType);
    public string GetValidSheetName(string name);
    public object? GetValidCellValue(object? value);

}