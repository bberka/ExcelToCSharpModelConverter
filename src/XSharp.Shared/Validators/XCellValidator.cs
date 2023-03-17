using EasMe.Extensions;
using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XCellValidator : IXCellValidator
{
    public bool IsIgnore(object? value)
    {
        if (value is null) return true;
        if (value is string s && s.IsNullOrEmpty()) return false;
        return false;
    }

    public object? GetValidValue(object? value)
    {
        return value;
    }
}