using System.Text;
using EasMe.Extensions;
using EasMe.Result;
using XSharp.Core.Lib;
using XSharp.Shared.Models;

namespace XSharp.Core.Manager;

public class ModelCreator
{
    public string SheetName { get; }
    public string FixedSheetName { get; }

    private bool IsAddRealSheetNameAttribute => SheetName == FixedSheetName;
    public List<HeaderModel> ColumnHeaders { get; }

    public static ResultData<ModelCreator> Create(string sheetName, List<HeaderModel> headers)
    {
        if (sheetName.IsNullOrEmpty()) return Result.Warn("SheetName is empty");
        var fixedSheetName = sheetName.FixName();
        if (fixedSheetName.IsNullOrEmpty()) return Result.Warn("FixedSheetName is empty. SheetName: " + sheetName);
        if (headers.Count == 0) return Result.Warn("Headers is empty. SheetName: " + sheetName);
        return new ModelCreator(sheetName, fixedSheetName, headers);
    }
    private ModelCreator(string sheetName, string fixedSheetName,List<HeaderModel> headers)
    {
        SheetName = sheetName;
        FixedSheetName = fixedSheetName;
        ColumnHeaders = headers;
    }

    
    private string BuildFileOutPutPath(string path)
    {
        return Path.Combine(path, $"{FixedSheetName}.cs");
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
        foreach (var col in ColumnHeaders)
        {
            var fixedColName = col.Name?.FixName();
            if (fixedColName.IsNullOrEmpty())
            {
                errors.Add("FixedColName is empty. ColName: " + col.Name + " SheetName: " + SheetName);
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
        if (propertyName == FixedSheetName) propertyName = "_" + propertyName;
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
        if (IsAddRealSheetNameAttribute) sb.AppendLine($"[SheetName(\"{SheetName}\")]");
        sb.Append(OptionLib.This.Option.ConstructorAccessModifier.ToString().ToLower());
        sb.Append(' ');
        sb.Append(FixedSheetName);
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