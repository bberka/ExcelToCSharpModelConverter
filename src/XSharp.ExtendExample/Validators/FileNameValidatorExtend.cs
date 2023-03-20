using XSharp.Shared.Abstract;

namespace XSharp.ExtendExample.Validators;

public class FileNameValidatorExtend : IXFileNameValidator
{
    public bool IsIgnore(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return true;
        if (filePath.StartsWith("#")) return true;
        return false;
    }
}