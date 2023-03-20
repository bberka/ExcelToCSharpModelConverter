using XSharp.Shared.Models;

namespace XSharp.Shared.Obsolete;

[Obsolete]
public interface IXRow
{
    List<IXCell> Cells { get; }
    int Index { get; }
    void SetCells(List<IXCell> cells);
    void SetIndex(int index);
}
