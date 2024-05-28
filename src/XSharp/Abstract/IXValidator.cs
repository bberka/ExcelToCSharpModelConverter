using XSharp.Models;

namespace XSharp.Abstract;

public interface IXValidator
{
  public bool IsIgnoreRow(XHeader header, int rowIndex, object? cellValue);
  public bool IsIgnoreSheetByName(string name);
  public bool IsIgnoreSheetByFixedName(string fixedName);
  public bool IsIgnoreSheetByHeaders(List<XHeader> headers);
  public bool IsIgnoreHeader(XHeader header);
  public bool IsIgnoreFileByPath(string filePath);
  public bool IsIgnoreCell(int rowIndex, int colIndex, object? cellValue);
  public string GetValidSheetName(string name);
  public object? GetValidCellValue(object? value);
}