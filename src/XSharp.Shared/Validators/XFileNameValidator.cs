using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XFileNameValidator : IXFileNameValidator
{
    public bool IsIgnore(string filePath)
    {
        return false;
    }
}