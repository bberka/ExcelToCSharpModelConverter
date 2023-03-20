﻿using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using XSharp.Shared.Abstract;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Shared.Models;

[Serializable]
[XmlRoot("ExportOption")]
public class XOption : IXOption
{
    //public string? InFolderPath { get; set; }
    //public string? OutFolderPath { get; set; }
    public string NameSpace { get; set; }
    public LogLevel MinimumLogLevel { get; set; }
    public ValueType DefaultValueType { get; set; }
    public string ModelInheritanceString { get; set; }
    public string ExtendValidatorDllFilePath { get; set; }
    public long HeaderColumnNumber { get; set; }
    public int SetValueTypesAtRowNumber { get; set; }
    public List<string> UsingNameSpaceList { get; set; } = new();
    public List<string> NullValueStrings { get; set; } = new();
}