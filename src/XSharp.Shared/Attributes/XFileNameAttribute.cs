namespace XSharp.Shared.Attributes;

public class XFileNameAttribute : Attribute
{
  public XFileNameAttribute(string name, string extension) {
    FileName = name;
    Extension = extension;
  }

  public string FileName { get; }
  public string Extension { get; }
}