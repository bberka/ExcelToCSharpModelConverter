namespace XSharp.Shared.Attributes;

public class XHeaderIndexAttribute : Attribute
{
    public XHeaderIndexAttribute(int index)
    {
        Index = index;
    }
    public int Index { get; set; }
}