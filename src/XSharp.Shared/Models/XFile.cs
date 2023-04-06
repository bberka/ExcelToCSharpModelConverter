﻿namespace XSharp.Shared.Models;

public class XFile
{
    public string FilePath { get; set; } = string.Empty;
    public string NameWithoutExtension => Path.GetFileNameWithoutExtension(FilePath);
    public string Extension => Path.GetExtension(FilePath);
    public List<XSheet> Sheets { get; set; } = new();

    public Result AddSheet(XSheet sheet)
    {
        var any = Sheets.Any(x => x.Name == sheet.Name);
        if (any) return Result.Error($"Sheet with name {sheet.Name} already exists");
        Sheets.Add(sheet);
        return Result.Success();
    }
}
