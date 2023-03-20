using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using XSharp.Shared.Abstract;
using XSharp.Shared.Constants;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Shared.Models;

[Serializable]
[XmlRoot("ExportOption")]
public partial class XOption : IXOption
{
    public string? InFolderPath { get; set; }
    public string? OutFolderPath { get; set; }
    public string NameSpace { get; set; }
    public LogLevel MinimumLogLevel { get; set; }
    public Constants.ValueType DefaultValueType { get; set; }
    public string ModelInheritanceString { get; set; }
    public string ExtendValidatorDllFilePath { get; set; }
    public long HeaderColumnIndex { get; set; }
    public int SetValueTypesAtRowIndex { get; set; }
    public List<string> UsingList { get; set; }
    public List<string> NullValueStrings { get; set; }
}