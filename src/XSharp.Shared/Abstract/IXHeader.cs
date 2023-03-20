namespace XSharp.Shared.Abstract;

public interface IXHeader
{
    int Index { get; }
    string Name { get; }
    string FixedName { get; }
    Type ValueType { get; }

    public void SetIndex(int index);
    public void SetName(string name);
    public void SetFixedName(string name);
    public void SetValueType(Type type);
}