using XSharp.Abstract;
using XSharp.Models;

namespace XSharp;

public class XDefaultValidator : IXValidator
{
  public bool IsIgnoreRow(XHeader header, int rowIndex, object? cellValue) {
    return false;
  }

  public bool IsIgnoreSheetByName(string name) {
    return false;
  }

  public bool IsIgnoreSheetByFixedName(string fixedName) {
    return false;
  }

  public bool IsIgnoreSheetByHeaders(List<XHeader> headers) {
    return false;
  }

  public bool IsIgnoreHeader(XHeader header) {
    return false;
  }

  public bool IsIgnoreFileByPath(string filePath) {
    return false;
  }

  public bool IsIgnoreCell(object? value) {
    return false;
  }

  public string GetValidSheetName(string name) {
    return name;
  }

  public object? GetValidCellValue(object? value) {
    return value;
  }

  public Type GetHeaderType(XHeader header, Type defaultType) {
    return defaultType;
  }
}