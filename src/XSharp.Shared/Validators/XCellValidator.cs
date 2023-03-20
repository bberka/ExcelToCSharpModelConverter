using EasMe.Extensions;
using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XCellValidator : IXCellValidator
{
    public bool IsIgnore(object? value)
    {

        return false;
    }

    public object? GetValidValue(object? value)
    {
        return value;
    }
}