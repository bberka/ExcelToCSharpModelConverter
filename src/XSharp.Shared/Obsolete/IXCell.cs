using XSharp.Shared.Abstract;

namespace XSharp.Shared.Obsolete;

[Obsolete]
public interface IXCell
{
    string? Formula { get; }
    object? Value { get; }
    string Address { get; }
    Type? Type { get; }
    IXHeader Header { get; }

    public void SetFormula(string formula);
    public void SetValue(object value);
    public void SetType(Type type);
    public void SetHeader(IXHeader header);
    public void SetAddress(string address);
}