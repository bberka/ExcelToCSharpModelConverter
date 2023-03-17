namespace XSharp.Shared.Abstract;

public interface IXRowValidator
{
    public bool IsIgnore(IXHeader header,int rowIndex,object? cellValue);
    
    
}