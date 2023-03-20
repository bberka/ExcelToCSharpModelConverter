using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XHeaderValidator : IXHeaderValidator
{
    public bool IsIgnore(IXHeader header)
    {
        return false;
    }
}