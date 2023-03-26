namespace XSharp.Shared.Models;

public class XCell
{
    public XCell()
    {
        
    }

    public XCell(object? value, int columnIndex, int rowIndex)
    {
        Value = value;
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }
    public int ColumnIndex { get; set; }
    public int RowIndex { get; set; }
    public object? Value { get; set; }
}