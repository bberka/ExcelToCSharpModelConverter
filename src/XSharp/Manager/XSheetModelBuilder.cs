using Microsoft.Extensions.Logging;
using XSharp.Models;

namespace XSharp.Manager;

internal static class XSheetModelBuilder
{
  public static void ExportSharpModel(List<XHeader> headers, string realSheetName, string fixedName, string outPath, XSharpOptions options) {
    XPathLib.CreateDirectory(outPath);
    var fileOutPath = Path.Combine(outPath, $"{fixedName}.cs");
    if (File.Exists(fileOutPath)) throw new Exception("File already exists: " + fileOutPath);
    var fileContent = CreateSharpModel(headers, realSheetName, fixedName, options);
    WriteToFile(fileOutPath, fileContent);
  }

  internal static void WriteToFile(string outPath, string content) {
    XPathLib.CreateDirectory(outPath);
    File.WriteAllText(outPath, content);
  }

  private static string CreateSharpModel(List<XHeader> headers, string realSheetName, string fixedSheetName, XSharpOptions options) {
    var sb = new StringBuilder();
    XBuilderHelper.AppendUsingList(sb, options);
    XBuilderHelper.AppendNamespace(sb, true, options);
    XBuilderHelper.AppendXSheetNameAttribute(sb, realSheetName, false);
    XBuilderHelper.AppendClassStart(sb, fixedSheetName, true, options);
    foreach (var col in headers) {
      if (!col.FixedName.HasContent() || !col.Name.HasContent()) {
         options.Logger.LogWarning("Column name or fixed name is empty. Sheet: {SheetName}", realSheetName);
         continue;
      }
      XBuilderHelper.AppendPropertySummaryIfExists(sb, col.Comment);
      XBuilderHelper.AppendXHeaderAttribute(sb, col.Index, col.Name, col.FixedName);
      XBuilderHelper.AppendProperty(sb, "string", fixedSheetName, col.FixedName, options);
    }

    XBuilderHelper.AppendEnd(sb);
    return sb.ToString();
  }
}