using XSharp.Shared.Abstract;

namespace XSharp.Shared.Models;


public class XRow : IXRow
{
    public List<IXCell> Cells { get; set; }
    public int Index { get; set; }
}