using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;



public class XCell : IXCell
{
    public string? Formula { get; set; }
    public object? Value { get; set; }
    public Type? Type { get; set; }
    public IXHeader Header { get; set; } = null!;
}