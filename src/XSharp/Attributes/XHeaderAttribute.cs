namespace XSharp.Attributes;

public class XHeaderAttribute : Attribute
{
  public XHeaderAttribute(int index,string name) {
    Name = name;
  }

  public string Name { get; }
  public int Index { get; }
}