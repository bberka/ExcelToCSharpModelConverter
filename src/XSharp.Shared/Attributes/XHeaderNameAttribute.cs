namespace XSharp.Shared.Attributes;

public class XHeaderNameAttribute : Attribute
{
    public XHeaderNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}