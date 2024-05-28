namespace XSharp.Attributes;

public class XSheetNameAttribute : Attribute
{
  public XSheetNameAttribute(string name) {
    Name = name;
  }

  public string Name { get; }
}