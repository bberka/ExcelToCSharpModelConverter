using System.Reflection;
using XSharp.Shared.Attributes;

namespace XSharp.Core.Lib;

public static class XAttributeLib
{
    public static string? GetTableName<T>()
    {
        var type = typeof(T);
        var attribute = type.GetCustomAttribute<XSheetNameAttribute>(false);
        return attribute?.Name ?? type.Name;
    }

    public static string GetHeaderName(PropertyInfo propInfo)
    {
        var attribute = propInfo?.GetCustomAttribute<XSheetNameAttribute>(false);
        return attribute?.Name ?? propInfo?.Name ?? string.Empty;
    }

    public static string GetHeaderName<T>(string propertyName)
    {
        var type = typeof(T);
        var property = type.GetProperty(propertyName);
        var attribute = property?.GetCustomAttribute<XHeaderNameAttribute>(false);
        return attribute?.Name ?? propertyName;
    }

    public static string GetHeaderName(PropertyInfo propInfo, string propertyName)
    {
        var attribute = propInfo?.GetCustomAttribute<XHeaderNameAttribute>(false);
        return attribute?.Name ?? propertyName;
    }
}