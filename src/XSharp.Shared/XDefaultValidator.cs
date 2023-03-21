using XSharp.Shared.Abstract;

namespace XSharp.Shared;

public class XDefaultValidator : IXValidator
{
    public bool IsIgnoreRow(IXHeader header, int rowIndex, object? cellValue)
    {
        return false;
    }

    public bool IsIgnoreSheetByName(string name)
    {
        return false;
    }

    public bool IsIgnoreHeader(IXHeader header)
    {
        return false;

    }

    public bool IsIgnoreFileByPath(string filePath)
    {
        return false;
    }

    public bool IsIgnoreCell(object? value)
    {
        return false;
    }

    public Type GetHeaderType(string headerName, object? cellValueToGetType, Type defaultType)
    {
        return defaultType;
    }

    public Type GetHeaderType(IXHeader header, Type defaultType)
    {
        return defaultType;
    }

    public string GetValidSheetName(string name)
    {
        return name;
    }

    public object? GetValidCellValue(object? value)
    {
        return value;
    }
}