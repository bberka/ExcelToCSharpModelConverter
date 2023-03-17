using XSharp.Shared.Models;

namespace XSharp.Shared.Abstract;

public interface IXCellValidator
{

    public bool IsIgnore(object? value);
    
    public object? GetValidValue(object? value);
    
    
}