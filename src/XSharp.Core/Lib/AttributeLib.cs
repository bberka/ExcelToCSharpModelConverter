using System.Reflection;
using XSharp.Shared.Attributes;

namespace XSharp.Core.Lib;

public static class AttributeLib
{
    public static string? GetTableName<T>()
    {
        var type = typeof(T);
        var attribute = type.GetCustomAttribute<SheetNameAttribute>(false);
        return attribute?.Name ?? type.Name;
    }
    public static string GetHeaderName(PropertyInfo propInfo)
    {
        var attribute = propInfo?.GetCustomAttribute<SheetNameAttribute>(false);
        return attribute?.Name ?? propInfo?.Name ?? string.Empty;
    }

    public static string GetHeaderName<T>(string propertyName)
    {
        var type = typeof(T);
        var property = type.GetProperty(propertyName);
        var attribute = property?.GetCustomAttribute<HeaderNameAttribute>(false);
        return attribute?.Name ?? propertyName;
    }

    public static string GetHeaderName(PropertyInfo propInfo, string propertyName)
    {
        var attribute = propInfo?.GetCustomAttribute<HeaderNameAttribute>(false);
        return attribute?.Name ?? propertyName;
    }

    
}