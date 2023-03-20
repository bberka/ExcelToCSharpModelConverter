using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XSheetValidator : IXSheetValidator
{
    public bool IsIgnore(string name)
    {
        return false;
    }

    public string GetValidSheetName(string name)
    {
        return name;
    }
}
