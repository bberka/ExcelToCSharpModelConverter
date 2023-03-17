using Microsoft.Extensions.Logging;
using XSharp.Shared.Constants;
using ValueType = System.ValueType;

namespace XSharp.Shared.Abstract;

public interface IExportOption
{
    string? InFolderPath { get; set; }
    string? OutFolderPath { get; set; }
    string NameSpace { get; set; }
    LogLevel MinimumLogLevel { get; set; }
    Constants.ValueType DefaultValueType { get; set; }
    string ModelInheritanceString { get; set; }
    SetterType SetterTypeString { get; set; }
    AccessModifierType SetterAccessModifier { get; set; }
    AccessModifierType GetterAccessModifier { get; set; }
    AccessModifierType ClassAccessModifier { get; set; }
    AccessModifierType ConstructorAccessModifier { get; set; }
    ModelType CreateModelAs { get; set; }
    long HeaderColumnIndex { get; set; }
    int SetValueTypesAtRowIndex { get; set; }
    bool IgnoreExceptions { get; set; }
    bool IgnoreFormulas { get; set; }
    List<string> UsingList { get; set; }
    List<string> NullValueStrings { get; set; }
}