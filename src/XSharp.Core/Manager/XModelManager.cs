
namespace XSharp.Core.Manager;

public class XModelManager
{
    public IXSheet Sheet { get; }
    private bool IsAddRealSheetNameAttribute => Sheet.Name == Sheet.FixedName;

    public static ResultData<XModelManager> Create(IXSheet sheet)
    {
        if (sheet.Name.IsNullOrEmpty()) return Result.Warn("SheetName is empty");
        if (sheet.FixedName.IsNullOrEmpty()) return Result.Warn("FixedSheetName is empty. SheetName: " + sheet.Name);
        if (sheet.Headers.Count == 0) return Result.Warn("Headers list is empty. SheetName: " + sheet.Name);
        return new XModelManager(sheet);
    }

    private XModelManager(IXSheet sheet)
    {
        Sheet = sheet;
    }
    
    private string BuildFileOutPutPath(string path)
    {
        return Path.Combine(path, $"{Sheet.FixedName}.cs");
    }

    public Result Write(string outPath)
    {
        PathLib.CreateDirectory(outPath);
        var fileOutPath = BuildFileOutPutPath(outPath);
        if (File.Exists(fileOutPath))
        {
            return Result.Warn("File already exists: " + fileOutPath);
        }
        var fileContentResult = CreateModel();
        if (fileContentResult.IsFailure) return fileContentResult.ToResult();
        var fileContent = fileContentResult.Data;
        if (fileContent.IsNullOrEmpty()) return Result.Error("FileContent is empty");
        File.WriteAllText(fileOutPath, fileContent);
        return Result.Success("File created: " + fileOutPath);
    }


    private ResultData<string> CreateModel()
    {
        var errors = new List<string>();
        var sb = new StringBuilder();
        AppendStart(sb);
        foreach (var col in Sheet.Headers)
        {
            var fixedColName = col.Name?.FixName();
            if (fixedColName.IsNullOrEmpty())
            {
                errors.Add("FixedColName is empty. ColName: " + col.Name + " SheetName: " + Sheet.Name);
                continue;
            }
            AppendHeaderName(sb, col.Name!, fixedColName!);
            AppendValueTypeString(sb, col.ValueType?.Name, out var valueTypeString);
            AppendProperty(sb, valueTypeString, fixedColName!);
        }
        AppendEnd(sb);
        return Result.Success<string>(sb.ToString(),"CreateModel", errors.ToArray());
    }

    private StringBuilder AppendHeaderName(StringBuilder sb, string colName, string fixedColumnName)
    {
        if (fixedColumnName != colName)
        {
            sb.AppendLine($"    [HeaderName(\"{colName}\")]");
        }
        return sb;
    }

    private StringBuilder AppendValueTypeString(StringBuilder sb, string? valueTypeName, out string valueTypeString)
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
        if (propertyName == Sheet.FixedName) propertyName = "_" + propertyName;
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
        if (IsAddRealSheetNameAttribute) sb.AppendLine($"[SheetName(\"{Sheet.Name}\")]");
        sb.Append(OptionLib.This.Option.ConstructorAccessModifier.ToString().ToLower());
        sb.Append(' ');
        sb.Append(Sheet.FixedName);
        sb.Append(' ');
        sb.AppendLine(OptionLib.This.Option.ModelInheritanceString);
        sb.AppendLine("{");
        return sb;
    }

    private StringBuilder AppendEnd(StringBuilder sb)
    {
        sb.AppendLine("}");
        return sb;
    }
}