using XSharp.Shared.Abstract;

namespace XSharp.ExtendExample.Validators;

public class CellValidatorExtend : IXCellValidator
{
    public bool IsIgnore(object? value)
    {
        var str = value?.ToString() ;
        if (str is null) return true;
        if (string.IsNullOrEmpty(str)) return true;
        return false;
    }

    public object? GetValidValue(object? value)
    {
        throw new NotImplementedException();
    }
}