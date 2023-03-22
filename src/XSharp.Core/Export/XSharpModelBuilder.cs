using Microsoft.Extensions.Primitives;
using System.Text;
using XSharp.Shared;
using XSharp.Shared.Attributes;

namespace XSharp.Core.Export;

internal static class XSharpModelBuilder
{
    
    public static Result ExportSharpModel(List<XHeader> headers, string realSheetName, string fixedName, string outPath)
    {
        XPathLib.CreateDirectory(outPath);
        var fileOutPath = BuildFileOutPutPath(outPath,fixedName);
        if (File.Exists(fileOutPath)) return Result.Warn("File already exists: " + fileOutPath);
        var fileContent = CreateSharpModel(headers,realSheetName,fixedName);
        File.WriteAllText(fileOutPath, fileContent);
        return Result.Success("File created: " + fileOutPath);
    }
    
    private static string CreateSharpModel(List<XHeader> headers, string realSheetName, string fixedSheetName)
    {
        var sb = new StringBuilder();
        AppendUsingList(sb);
        AppendNamespace(sb);
        AppendSheetNameAttribute(sb, realSheetName);
        AppendClass(sb, fixedSheetName);
        foreach (var col in headers)
        {
            AppendComment(sb, col.Comment);
            AppendHeaderName(sb, col.Name, col.FixedName);
            AppendHeaderIndexAttribute(sb, col.Index);
            var valueType = AppendValueTypeString(sb, col.ValueType?.Name);
            AppendProperty(sb, valueType, fixedSheetName,col.FixedName);
        }
        AppendEnd(sb);
        return sb.ToString();
    }


    private static string BuildFileOutPutPath(string path, string fileName)
    {
        return Path.Combine(path, $"{fileName}.cs");
    }


    #region AppendMethods

    private static void AppendHeaderIndexAttribute(StringBuilder sb, int index)
    {
        sb.AppendLine($"    [{nameof(XHeaderIndexAttribute).RemoveText("Attribute")}({index})]");

    }
    private static void AppendProperty(StringBuilder stringBuilder, string valueTypeString, string fixedSheetName, string fixedPropertyName)
    {
        if (fixedSheetName == fixedPropertyName) fixedPropertyName = "_" + fixedPropertyName;
        if (XOptionLib.This.Option.IsUseNullable)
        {
            stringBuilder.Append("    public " + valueTypeString + "? " + fixedPropertyName);
        }
        else
        {
            stringBuilder.Append("    public " + valueTypeString + " " + fixedPropertyName);
        }
        stringBuilder.AppendLine(" { get; set; }");
    }
    private static void AppendHeaderName(StringBuilder sb, string colName, string fixedColumnName)
    {
        if (fixedColumnName == colName) return;
        sb.AppendLine($"    [{nameof(XHeaderNameAttribute).RemoveText("Attribute")}(\"{colName}\")]");
    }
    private static void AppendClass(StringBuilder sb, string className)
    {
        sb.Append("public class " + className + " : XSheetBase");
        if (XOptionLib.This.Option.ModelInheritanceList.Count > 0)
        {
            sb.Append("," + string.Join(",", XOptionLib.This.Option.ModelInheritanceList));
        }
        sb.AppendLine();
        sb.AppendLine("{");
    }
    private static void AppendSheetNameAttribute(StringBuilder sb, string sheetName)
    {
        sb.AppendLine($"[{nameof(XSheetNameAttribute).RemoveText("Attribute")}(\"{sheetName}\")]");

    }
    private static void AppendUsingList(StringBuilder sb)
    {
        if (XOptionLib.This.Option.UsingNameSpaceList.Count == 0) return;
        foreach (var item in XOptionLib.This.Option.UsingNameSpaceList) sb.AppendLine("using " + item + ";");
        sb.AppendLine();
    }

    private static void AppendNamespace(StringBuilder sb)
    {
        sb.AppendLine($"namespace {XOptionLib.This.Option.NameSpace};");
        sb.AppendLine();
    }
    private static void AppendEnd(StringBuilder sb)
    {
        sb.AppendLine("}");
    }
    private static string AppendValueTypeString(
        StringBuilder sb,
        string? valueTypeName
        )
    {
        if (!valueTypeName.IsNullOrEmpty()) return valueTypeName!;
        sb.AppendLine($"    [{nameof(XCellValueTypeInvalidAttribute).RemoveText("Attribute")}]");
        return "String";
    }

    private static bool AppendComment(
        StringBuilder sb,
        string? comment
        )
    {
        if (comment is null || comment.IsNullOrEmpty()) return false;
        sb.AppendLine("    /// <summary>");
        sb.AppendLine("    /// " + comment!.RemoveLineEndings());
        sb.AppendLine("    /// </summary>");
        return true;
    }
    #endregion

}