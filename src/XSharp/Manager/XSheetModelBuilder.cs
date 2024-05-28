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
      XBuilderHelper.AppendPropertySummaryIfExists(sb, col.Comment);
      XBuilderHelper.AppendXHeaderNameAttribute(sb, col.Name, col.FixedName);
      XBuilderHelper.AppendHeaderIndexAttribute(sb, col.Index);

      XBuilderHelper.AppendProperty(sb, "string", fixedSheetName, col.FixedName, options);
    }

    XBuilderHelper.AppendEnd(sb);
    return sb.ToString();
  }
}