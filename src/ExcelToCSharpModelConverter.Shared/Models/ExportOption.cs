using System.Xml.Serialization;
using ExcelToCSharpModelConverter.Shared.Models.Option;
using Microsoft.Extensions.Logging;

namespace ExcelToCSharpModelConverter.Shared.Models;

[Serializable]
[XmlRoot("ExportOption")]
public class ExportOption
{

    public string? InFolderPath { get; set; } 
    public string? OutFolderPath { get; set; }
    
    public string NameSpace { get; set; } = "ExcelToCSharpModelConverter.ExportedModels";
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;

    
    public string DefaultValueType { get; set; } = "String";

    public string ModelInheritanceString { get; set; } = ": BaseSheetModel";
    
    
    public string SetterTypeString { get; set; } = "set";
    public string SetterAccessModifier { get; set; } = "";
    public string GetterAccessModifier { get; set; } = "";
    public string ClassAccessModifier { get; set; } = "public";
    public string ConstructorAccessModifier { get; set; } = "public";
    // public string CreateConstructorInitializer { get; set; }
    
    
    public long HeaderColumnIndex { get; set; } = 1;
    public int SetValueTypesAtRowIndex { get; set; } = 2;

    public bool IgnoreExceptions { get; set; } = false;
    public bool IgnoreInvalidFormulas { get; set; } = false;


    public List<string> Usings { get; set; } = new List<string>();
    public List<string> NullValueStrings { get; set; } = new List<string>();

    public List<IgnoreWhen> IgnoreWhenConditions { get; set; } = new List<IgnoreWhen>();
    public List<ReplaceWhen> ReplaceWhenConditions { get; set; } = new List<ReplaceWhen>();
    public List<TrimWhen> TrimWhenConditions { get; set; } = new List<TrimWhen>();
    

}