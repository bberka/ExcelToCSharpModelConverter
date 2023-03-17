namespace XSharp.Shared.Attributes;


public partial class HeaderNameAttribute : Attribute
{
    public HeaderNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}