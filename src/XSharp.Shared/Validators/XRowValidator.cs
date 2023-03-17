using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XRowValidator : IXRowValidator
{
    public bool IsIgnore(IXHeader header,int rowIndex, object? cellValue)
    {
        if (rowIndex == 1) return true;
        return false;
    }
}