namespace ExcelToCSharpModelConverter.Core.Extensions;

public static class ConditionExtensions
{
    public static bool IsCellNull(this string? value)
    {
        if (value is null)
        {
            return true;
        } 
        var any = OptionLib.This.Option.NullValueStrings.Any(x => x == value);
        return any;
    }
    // public static bool IsCellSkip(this string? value)
    // {
    //     
    // }
}