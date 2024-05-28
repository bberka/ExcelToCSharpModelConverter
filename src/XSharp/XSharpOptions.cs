using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using XSharp.Abstract;
using XSharp.Models;

namespace XSharp;

public enum MapHeadersBehaviourType
{
  Index,
  Name
}

public sealed class XSharpOptions
{
  private XSharpOptions() { }

  public string SheetModelNameSpace { get; set; } = "XSharp.ExportedModels";
  public string FileModelNameSpace { get; set; } = "XSharp.ExportedModels";
  public List<string> SheetModelInheritanceList { get; set; } = new();
  public List<string> FileModelInheritanceList { get; set; } = new();
  public long HeaderColumnNumber { get; set; } = 1;
  public List<string> UsingNameSpaceList { get; set; } = new();
  public List<string> NullValueStrings { get; set; } = new();
  public bool IsUseNullable { get; set; } = false;
  public ILogger Logger { get; set; } = NullLogger.Instance;
  public IXValidator XValidator { get; set; } = new XDefaultValidator();

  public MapHeadersBehaviourType MapHeadersBehaviour { get; set; } = MapHeadersBehaviourType.Index;

  // public XHeader? XHeader { get; set; } 
  // public XCell? XCell { get; set; } 

  public static XSharpOptions Create(Action<XSharpOptions> configure) {
    var options = new XSharpOptions();
    configure(options);
    return options;
  }
}