using XSharp.Shared.Abstract;

namespace XSharp.ExtendExample.Validators;

public class SheetValidatorExtend : IXSheetValidator
{
    public bool IsIgnore(string name)
    {
        if (string.IsNullOrEmpty(name)) return true;
        if (name.StartsWith("#")) return true;
        return false;
    }

    public string GetValidSheetName(string name)
    {
        return name;
    }
}