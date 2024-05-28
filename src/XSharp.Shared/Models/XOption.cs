using System.Xml.Serialization;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Shared.Models;

[Serializable]
[XmlRoot("ExportOption")]
public class XOption
{
  //public string? InFolderPath { get; set; }
  //public string? OutFolderPath { get; set; }
  public string SheetModelNameSpace { get; set; }
  public string FileModelNameSpace { get; set; }
  public ValueType DefaultValueType { get; set; }
  public List<string> SheetModelInheritanceList { get; set; } = new();
  public List<string> FileModelInheritanceList { get; set; } = new();
  public long HeaderColumnNumber { get; set; }
  public int SetValueTypesAtRowNumber { get; set; }
  public List<string> UsingNameSpaceList { get; set; } = new();
  public List<string> NullValueStrings { get; set; } = new();
  public bool IsUseNullable { get; set; } = true;
}