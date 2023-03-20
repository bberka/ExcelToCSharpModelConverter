namespace XSharp.Shared.Obsolete;

[Obsolete]
public class XRow : IXRow
{
    public List<IXCell> Cells { get; private set; }
    public int Index { get; private set; }
    public void SetCells(List<IXCell> cells)
    {
        Cells = cells;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }
}