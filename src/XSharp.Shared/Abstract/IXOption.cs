using Microsoft.Extensions.Logging;
using XSharp.Shared.Constants;
using ValueType = System.ValueType;

namespace XSharp.Shared.Abstract;

public interface IXOption
{
    string? InFolderPath { get; set; }
    string? OutFolderPath { get; set; }
    string NameSpace { get; set; }
    LogLevel MinimumLogLevel { get; set; }
    Constants.ValueType DefaultValueType { get; set; }
    string ModelInheritanceString { get; set; }
    string ExtendValidatorDllFilePath { get; set; }
    long HeaderColumnIndex { get; set; }
    int SetValueTypesAtRowIndex { get; set; }
    List<string> UsingList { get; set; }
    List<string> NullValueStrings { get; set; }
}