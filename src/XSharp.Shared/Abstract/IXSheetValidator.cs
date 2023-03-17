namespace XSharp.Shared.Abstract;

public interface IXSheetValidator
{
    public bool IsIgnore(string name);
    
    public string GetValidSheetName(string name);
    
}