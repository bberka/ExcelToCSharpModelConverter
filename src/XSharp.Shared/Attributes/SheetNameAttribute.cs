namespace XSharp.Shared.Attributes;

public class SheetNameAttribute : Attribute
{
    public SheetNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}