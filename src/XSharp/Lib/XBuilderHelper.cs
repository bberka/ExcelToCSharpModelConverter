using XSharp.Attributes;

namespace XSharp.Lib;

internal static class XBuilderHelper
{
  internal static void AppendNamespace(StringBuilder sb, bool isSheet, XSharpOptions options) {
    if (isSheet) {
      sb.AppendLine($"namespace {options.SheetModelNameSpace};");
      sb.AppendLine();
      return;
    }

    sb.AppendLine($"namespace {options.FileModelNameSpace};");
    sb.AppendLine();
  }

  internal static void AppendEnd(StringBuilder sb) {
    sb.AppendLine("}");
  }

  internal static void AppendPropertySummaryIfExists(
    StringBuilder sb,
    string? comment
  ) {
    if (comment is null || !comment.HasContent()) return;
    sb.AppendLine("    /// <summary>");
    //remove < and > from comment to avoid xml error
    sb.AppendLine("    /// " + comment
                               .RemoveText("<")
                               .RemoveText(">")
                               .ReplaceLineEndings(Environment.NewLine + "  /// <br/>"));
    sb.AppendLine("    /// </summary>");
  }

  internal static void AppendComment(
    StringBuilder sb,
    string comment
  ) {
    if (!comment.HasContent()) return;
    sb.AppendLine("    // " + comment.ReplaceLineEndings(Environment.NewLine + " //"));
  }

  internal static void AppendClassStart(StringBuilder sb, string className, bool isSheet, XSharpOptions options) {
    if (isSheet) {
      sb.Append("public class " + className + " : XSheetBase");
      if (options.SheetModelInheritanceList.Count > 0) sb.Append("," + string.Join(",", options.SheetModelInheritanceList));
    }
    else {
      sb.Append("public class " + className + " : XFileBase");
      if (options.FileModelInheritanceList.Count > 0) sb.Append("," + string.Join(",", options.FileModelInheritanceList));
    }

    sb.AppendLine();
    sb.AppendLine("{");
  }

  internal static void AppendUsingList(StringBuilder sb, XSharpOptions options) {
    if (options.UsingNameSpaceList.Count == 0) return;
    foreach (var item in options.UsingNameSpaceList) sb.AppendLine("using " + item + ";");
    sb.AppendLine();
  }

  internal static void AppendProperty(StringBuilder stringBuilder,
                                      string valueTypeString,
                                      string className,
                                      string propertyName,
                                      XSharpOptions options) {
    if (className == propertyName) propertyName = "_" + propertyName;
    if (options.IsUseNullable)
      stringBuilder.Append("    public " + valueTypeString + "? " + propertyName);
    else
      stringBuilder.Append("    public " + valueTypeString + " " + propertyName);
    stringBuilder.AppendLine(" { get; set; }");
  }

  internal static void AppendHeaderIndexAttribute(StringBuilder sb, int index) {
    sb.AppendLine($"    [{nameof(XHeaderIndexAttribute).RemoveText("Attribute")}({index})]");
  }

  internal static void AppendXHeaderNameAttribute(StringBuilder sb, string colName, string fixedColumnName) {
    sb.AppendLine($"    [{nameof(XHeaderNameAttribute).RemoveText("Attribute")}(\"{colName}\")]");
  }

  internal static void AppendXSheetNameAttribute(StringBuilder sb, string sheetName, bool isOnProperty) {
    if (isOnProperty) {
      sb.AppendLine($"    [{nameof(XSheetNameAttribute).RemoveText("Attribute")}(\"{sheetName}\")]");
      return;
    }

    sb.AppendLine($"    [{nameof(XSheetNameAttribute).RemoveText("Attribute")}(\"{sheetName}\")]");
  }

  internal static void AppendXFileNameAttribute(StringBuilder sb, string name, string ext) {
    sb.AppendLine($"[{nameof(XFileNameAttribute).RemoveText("Attribute")}(\"{name}\",\"{ext}\")]");
  }
}