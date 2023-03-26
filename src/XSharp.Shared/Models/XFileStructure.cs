namespace XSharp.Shared.Models;

public class XFileStructure
{
    public string FileName { get; set; } = string.Empty;
    public List<XSheetStructure> XSheetStructures { get; set; } = new();
}