namespace XSharp.Shared.Models;

public class WorkSheetModel
{
    public string WorkSheetName { get; private set; }
    public Type SheetModelType { get; private set; }
    public List<object> Rows { get; private set; }

    public void SetRows(List<object> rows)
    {
        Rows = rows;
    }
    public void SetType(Type type)
    {
        SheetModelType = type;
    }
    public void SetWorkSheetName(string workSheetName)
    {
        WorkSheetName = workSheetName;
    }
   
}