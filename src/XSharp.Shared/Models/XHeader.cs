using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;

public class XHeader : IXHeader
{
    public int Index { get; set; }
    public string? Name { get; set; }
    public string? FixedName { get; set; }
    public Type? ValueType { get; set; }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetFixedName(string name)
    {
        FixedName = name;
    }

    public void SetValueType(Type type)
    {
        ValueType = type;
    }
}