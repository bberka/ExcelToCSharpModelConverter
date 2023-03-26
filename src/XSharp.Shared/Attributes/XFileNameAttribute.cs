namespace XSharp.Shared.Attributes;

public class XFileNameAttribute : Attribute
{
    public XFileNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}