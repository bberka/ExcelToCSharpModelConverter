using System.Text;
using EasMe.Extensions;
using EasMe.Result;
using ExcelToCSharpModelConverter.Core.Exceptions;
using ExcelToCSharpModelConverter.Core.Extensions;
using ExcelToCSharpModelConverter.Core.Lib;

namespace ExcelToCSharpModelConverter.Core.Manager;

public class ModelCreator
{
    public string SheetName { get; }
    public string FixedSheetName { get; }

    private bool IsAddRealSheetNameAttribute => SheetName == FixedSheetName;
    public List<HeaderModel> ColumnHeaders { get; }


    public ModelCreator(string sheetName, IEnumerable<HeaderModel> colsHeaderModels)
    {
        SheetName = sheetName;
        if (sheetName.IsNullOrEmpty()) throw new SheetNameIsEmptyException();
        FixedSheetName = sheetName.FixName();
        if (FixedSheetName.IsNullOrEmpty()) throw new FixedSheetNameIsEmptyException();
        ColumnHeaders = colsHeaderModels.DistinctBy(x => x.Name).ToList();
        if (ColumnHeaders.Count == 0) throw new NoColumnHeadersFoundException();
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

        var fileContent = CreateModel();
        if (fileContent.IsNullOrEmpty()) return Result.Error("FileContent is empty");
        File.WriteAllText(fileOutPath, fileContent);
        return Result.Success("File created: " + fileOutPath);
    }

   
    private string CreateModel()
    {
        var sb = new StringBuilder();
        AppendStart(sb);
        foreach (var col in ColumnHeaders)
        {
            AppendHeaderName(sb, col.Name, out var fixedColName);
            AppendValueTypeString(sb,col.ValueType?.Name,out var valueTypeString);
            AppendProperty(sb, valueTypeString, fixedColName);
        }
        AppendEnd(sb);
        return sb.ToString();
    }

    private StringBuilder AppendHeaderName(StringBuilder sb, string colName, out string fixedColumnName)
    {
        fixedColumnName = colName.FixName();
        if (fixedColumnName != colName)
        {
            sb.AppendLine($"    [HeaderName(\"{colName}\")]");
        }

        return sb;
    }
    private StringBuilder AppendValueTypeString(StringBuilder sb, string? valueTypeName,out string valueTypeString)
    {
        if (valueTypeName.IsNullOrEmpty())
        {
            sb.AppendLine("    [InvalidValueType]");
            valueTypeName = OptionLib.This.Option.DefaultValueType;
        }
        valueTypeString = valueTypeName;
        return sb;
        
    }
    private StringBuilder AppendProperty(StringBuilder stringBuilder,string valueTypeString,string fixedColName)
    {
        if (fixedColName == FixedSheetName) fixedColName = "_" + fixedColName;
        stringBuilder.AppendLine($"    public {valueTypeString} {fixedColName} {{ get; set; }}");
        return stringBuilder;
    }
    private StringBuilder AppendStart(StringBuilder sb)
    {
        if (OptionLib.This.Option.Usings.Count > 0)
        {
            foreach (var item in OptionLib.This.Option.Usings)
            {
                sb.AppendLine(item);
            }

            sb.AppendLine();
        }

        sb.AppendLine($"namespace {OptionLib.This.Option.NameSpace};");
        sb.AppendLine();
        if (IsAddRealSheetNameAttribute) sb.AppendLine($"[SheetName(\"{SheetName}\")]");
        sb.AppendLine($"public class {FixedSheetName} {OptionLib.This.Option.ModelInheritanceString}");
        sb.AppendLine("{");
        return sb;
    }
    private StringBuilder AppendEnd(StringBuilder sb)
    {
        sb.AppendLine("}");
        return sb;
    }
}