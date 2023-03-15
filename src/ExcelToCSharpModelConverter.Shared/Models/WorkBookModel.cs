namespace ExcelToCSharpModelConverter.Shared.Models;

public class WorkBookModel
{
    public string WorkBookName { get; private set; }
    public List<WorkSheetModel> WorkSheets { get; private set; }

    public void SetWorkBookName(string workBookName)
    {
        WorkBookName = workBookName;
    }
    public void SetWorkSheets(List<WorkSheetModel> workSheets)
    {
        WorkSheets = workSheets;
    }

}