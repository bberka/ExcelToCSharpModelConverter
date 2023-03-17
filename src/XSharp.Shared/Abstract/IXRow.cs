using XSharp.Shared.Models;

namespace XSharp.Shared.Abstract;

public interface IXRow
{
    List<IXCell> Cells { get; internal set; }
    int Index { get; internal set; }
}
