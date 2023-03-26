using System.Reflection;
using XSharp.Shared.Attributes;

namespace XSharp.Core.Lib;

public static class XAttributeLib
{
    public static string? GetSheetName<T>()
    {
        var type = typeof(T);
        var attribute = type.GetCustomAttribute<XSheetNameAttribute>(false);
        return attribute?.Name ?? type.Name;
    }

    public static string? GetSheetName(Type type)
    {
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


    public static int GetHeaderIndex(PropertyInfo propInfo)
    {
        var attribute = propInfo?.GetCustomAttribute<XHeaderIndexAttribute>(false);
        return attribute?.Index ?? -1;
    }

    public static int GetHeaderIndex<T>(string propertyName)
    {
        var type = typeof(T);
        var property = type.GetProperty(propertyName);
        var attribute = property?.GetCustomAttribute<XHeaderIndexAttribute>(false);
        return attribute?.Index ?? -1;
    }
}