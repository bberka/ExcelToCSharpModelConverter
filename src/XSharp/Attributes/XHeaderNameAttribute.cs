namespace XSharp.Attributes;

public class XHeaderNameAttribute : Attribute
{
  public XHeaderNameAttribute(string name) {
    Name = name;
  }

  public string Name { get; }
}