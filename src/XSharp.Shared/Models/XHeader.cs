using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;

public class XHeader
{
    public int Index { get; set; }
    public string? Name { get; set; }
    public string? FixedName { get; set; }
    public string? Comment { get; set; }
    public Type? ValueType { get; set; }

}