using XSharp.Shared;

namespace XSharp.Core.Export;

internal class XSharpModelBuilder
{
    private readonly List<IXHeader> _headers;
    private readonly string _sheetName;
    private readonly string _fixedName;
    private readonly string _outPath;

    private XSharpModelBuilder(List<IXHeader> headers,string sheetName,string fixedName, string outPath)
    {
        _headers = headers;
        _sheetName = sheetName;
        _fixedName = fixedName;
        _outPath = outPath;
    }

    public static Result Export(ExcelWorksheet? worksheet, string outPath)
    {
        if (worksheet is null) return Result.Warn("Worksheet is null");
        if (worksheet.Dimension is null) return Result.Warn("Worksheet dimension is null or empty");
        var headers = worksheet.GetHeaders();
        if (headers.Count == 0) return Result.Warn("Worksheet has no valid headers");
        if (worksheet.Name.IsNullOrEmpty()) return Result.Warn("SheetName is null or empty");
        var fixedName = worksheet.Name.FixName();
        if (fixedName.IsNullOrEmpty()) return Result.Warn("FixedSheetName is null or empty. SheetName: " + worksheet.Name);
        var sheetName = worksheet.Name;
        var validator = XKernel.This.GetValidator<IXSheetValidator>();
        if (validator is not null)
        {
            var isValidName = validator.IsIgnore(worksheet.Name);
            if (!isValidName)
            {
                return Result.Warn("Invalid sheet name: " + worksheet.Name);
            }
            var validName = validator.GetValidSheetName(sheetName);
            if (validName != worksheet.Name)
            {
                sheetName = validName;
            }
        }
        var modelBuilder = new XSharpModelBuilder(headers, sheetName, fixedName , outPath);
        return modelBuilder.ExportSharpModel(outPath);
    }

    private string BuildFileOutPutPath(string path)
    {
        return Path.Combine(path, $"{_fixedName}.cs");
    }

    public Result ExportSharpModel(string outPath)
    {
        PathLib.CreateDirectory(outPath);
        var fileOutPath = BuildFileOutPutPath(outPath);
        if (File.Exists(fileOutPath))
        {
            return Result.Warn("File already exists: " + fileOutPath);
        }
        var fileContent = CreateSharpModel();
        File.WriteAllText(fileOutPath, fileContent);
        return Result.Success("File created: " + fileOutPath);
    }


    private string CreateSharpModel()
    {
        var sb = new StringBuilder();
        AppendStart(sb);
        foreach (var col in _headers)
        {
            AppendHeaderName(sb, col.Name, col.FixedName);
            AppendValueTypeString(sb, col.ValueType?.Name, out var valueTypeString);
            AppendProperty(sb, valueTypeString, col.FixedName!);
        }
        AppendEnd(sb);
        return sb.ToString();
    }

    private static StringBuilder AppendHeaderName(StringBuilder sb, string colName, string fixedColumnName)
    {
        if (fixedColumnName != colName)
        {
            sb.AppendLine($"    [HeaderName(\"{colName}\")]");
        }
        return sb;
    }

    private static StringBuilder AppendValueTypeString(StringBuilder sb, string? valueTypeName, out string valueTypeString)
    {
        if (valueTypeName.IsNullOrEmpty())
        {
            sb.AppendLine("    [InvalidValueType]");
            valueTypeName = OptionLib.This.Option.DefaultValueType.ToString();
        }
        valueTypeString = valueTypeName;
        return sb;
    }

    private StringBuilder AppendProperty(StringBuilder stringBuilder, string valueTypeString, string propertyName)
    {
        if (propertyName == _fixedName) propertyName = "_" + propertyName;
        stringBuilder.AppendLine($"    public {valueTypeString} {propertyName} {{ get; set; }}");
        return stringBuilder;
    }

    private StringBuilder AppendStart(StringBuilder sb)
    {
        if (OptionLib.This.Option.UsingList.Count > 0)
        {
            foreach (var item in OptionLib.This.Option.UsingList)
            {
                sb.AppendLine("using " + item + ";");
            }
            sb.AppendLine();
        }
        sb.AppendLine($"namespace {OptionLib.This.Option.NameSpace};");
        sb.AppendLine();
        sb.AppendLine($"[SheetName(\"{_fixedName}\")]");
        sb.Append("public class");
        sb.Append(' ');
        sb.Append(_fixedName);
        sb.Append(' ');
        sb.AppendLine(": XSheetBase," + OptionLib.This.Option.ModelInheritanceString);
        sb.AppendLine("{");
        return sb;
    }

    private static StringBuilder AppendEnd(StringBuilder sb)
    {
        sb.AppendLine("}");
        return sb;
    }
}