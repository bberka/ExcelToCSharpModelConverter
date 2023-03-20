using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XRowValidator : IXRowValidator
{
    public bool IsIgnore(IXHeader header, int rowIndex, object? cellValue)
    {
        return false;
    }
}