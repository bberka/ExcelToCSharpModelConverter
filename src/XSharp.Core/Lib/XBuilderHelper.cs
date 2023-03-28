using XSharp.Shared.Attributes;

namespace XSharp.Core.Lib;

internal static class XBuilderHelper
{
    internal static void AppendNamespace(StringBuilder sb)
    {
        sb.AppendLine($"namespace {XOptionLib.This.Option.NameSpace};");
        sb.AppendLine();
    }

    internal static void AppendEnd(StringBuilder sb)
    {
        sb.AppendLine("}");
    }

    internal static void AppendPropertySummaryIfExists(
        StringBuilder sb,
        string? comment
    )
    {
        if (comment is null || comment.IsNullOrEmpty()) return;
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
    )
    {
        if (comment.IsNullOrEmpty()) return;
        sb.AppendLine("    // " + comment.ReplaceLineEndings(Environment.NewLine + " //"));
    }

    internal static void AppendClassStart(StringBuilder sb, string className, bool isSheet)
    {
        if (isSheet)
            sb.Append("public class " + className + " : XSheetBase");
        else
            sb.Append("public class " + className + " : XFileBase");
        if (XOptionLib.This.Option.ModelInheritanceList.Count > 0)
            sb.Append("," + string.Join(",", XOptionLib.This.Option.ModelInheritanceList));
        sb.AppendLine();
        sb.AppendLine("{");
    }

    internal static void AppendUsingList(StringBuilder sb)
    {
        if (XOptionLib.This.Option.UsingNameSpaceList.Count == 0) return;
        foreach (var item in XOptionLib.This.Option.UsingNameSpaceList) sb.AppendLine("using " + item + ";");
        sb.AppendLine();
    }

    internal static void AppendProperty(StringBuilder stringBuilder, string valueTypeString, string className,
        string propertyName)
    {
        if (className == propertyName) propertyName = "_" + propertyName;
        if (XOptionLib.This.Option.IsUseNullable)
            stringBuilder.Append("    public " + valueTypeString + "? " + propertyName);
        else
            stringBuilder.Append("    public " + valueTypeString + " " + propertyName);
        stringBuilder.AppendLine(" { get; set; }");
    }

    internal static void AppendHeaderIndexAttribute(StringBuilder sb, int index)
    {
        sb.AppendLine($"    [{nameof(XHeaderIndexAttribute).RemoveText("Attribute")}({index})]");
    }

    internal static void AppendXHeaderNameAttribute(StringBuilder sb, string colName, string fixedColumnName)
    {
        sb.AppendLine($"    [{nameof(XHeaderNameAttribute).RemoveText("Attribute")}(\"{colName}\")]");
    }

    internal static void AppendXSheetNameAttribute(StringBuilder sb, string sheetName)
    {
        sb.AppendLine($"    [{nameof(XSheetNameAttribute).RemoveText("Attribute")}(\"{sheetName}\")]");
    }

    internal static void AppendXFileNameAttribute(StringBuilder sb, string name,string ext)
    {
        sb.AppendLine($"[{nameof(XFileNameAttribute).RemoveText("Attribute")}(\"{name}\",\"{ext}\")]");
    }


    internal static void AppendXCellValueTypeInvalidAttribute(
        StringBuilder sb,
        string valueTypeName
    )
    {
        sb.AppendLine($"    [{nameof(XCellValueTypeInvalidAttribute).RemoveText("Attribute")}]");
    }
}