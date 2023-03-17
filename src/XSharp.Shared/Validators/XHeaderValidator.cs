using EasMe.Extensions;
using XSharp.Shared.Abstract;

namespace XSharp.Shared.Validators;

public class XHeaderValidator : IXHeaderValidator
{
    public bool IsIgnore(IXHeader header)
    {
        if(header.Name.IsNullOrEmpty()) return true;
        if(header.FixedName.IsNullOrEmpty()) return true;
        return false;
    }
    //
    // public bool IsIgnore(object? firstRowHeaderCellValue)
    // {
    //     if(firstRowHeaderCellValue is null) return true;
    //     return false;
    // }
}