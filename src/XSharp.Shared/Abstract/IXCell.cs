namespace XSharp.Shared.Abstract;

public interface IXCell
{
    string? Formula { get; internal set; }
    object? Value { get; internal set; }
    Type? Type { get; internal set; }
    IXHeader Header { get; internal set; }
}