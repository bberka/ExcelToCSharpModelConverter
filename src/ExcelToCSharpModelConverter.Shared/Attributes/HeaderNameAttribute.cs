namespace ExcelToCSharpModelConverter.Shared.Attributes;

public class HeaderNameAttribute : Attribute
{
    public HeaderNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}