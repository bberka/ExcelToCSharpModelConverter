namespace XSharp.Shared.Abstract;

public interface IXSheetValidator: IXBaseValidator
{
    public bool IsIgnore(string name);
    
    public string GetValidSheetName(string name);
    
}