namespace XSharp.Shared.Abstract;

public interface IXHeader
{
    int Index { get; set; }
    string Name { get; set; }
    string FixedName { get; set; }
    string? Comment { get; set; }
    Type? ValueType { get; set; }

}