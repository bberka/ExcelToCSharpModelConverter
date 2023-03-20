namespace XSharp.Core.Lib;

public static class XLib
{
    public static List<ExcelWorksheet> GetExcelSheetsFromWorkBookPath(string? path)
    {
        if (path is null) return new List<ExcelWorksheet>();
        var package = new ExcelPackage(new FileInfo(path));
        var sheets = new List<ExcelWorksheet>();
        for (var i = 0; i <= package.Workbook.Worksheets.Count - 1; i++)
        {
            var sheet = package.Workbook.Worksheets[i];
            sheets.Add(sheet);
        }

        return sheets;
    }
}