namespace XSharp.Shared.Abstract;

public interface IXHeader
{
    int Index { get; internal set; }
    string? Name { get; internal set; }
    string? FixedName { get; internal set; }
    Type? ValueType { get; internal set; }
    
    public void SetIndex(int index);
    public void SetName(string name);
    public void SetFixedName(string name);
    public void SetValueType(Type type);
}