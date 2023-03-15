using System.Globalization;

namespace ExcelToCSharpModelConverter.Core.Extensions;

public static class EnumExtensions
{
    public static string ToLowerString(this object value)
    {
        return value.ToString()?.ToLower(new CultureInfo("en-US")) ?? string.Empty;
        
    }
    
}