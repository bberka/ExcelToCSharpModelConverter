using XSharp.Shared.Abstract;

namespace XSharp.Shared.Obsolete;

[Obsolete]
public class XCell : IXCell
{
    public string? Formula { get; private set; }
    public object? Value { get; private set; }
    public string Address { get; private set; }
    public Type? Type { get; private set; }
    public IXHeader Header { get; private set; } = null!;

    public void SetFormula(string formula)
    {
        Formula = formula;
    }

    public void SetValue(object value)
    {
        Value = value;
    }

    public void SetType(Type type)
    {
        Type = type;
    }

    public void SetHeader(IXHeader header)
    {
        Header = header;
    }

    public void SetAddress(string address)
    {
        Address = address;
    }
}