namespace XSharp.Core.Lib;

public static class ValueConverter
{
    public static object? TryConvert(string? value, out Type? type)
    {
        type = null;
        if (value is null) return null;
        if (value == "true" || value == "false")
        {
            type = typeof(bool);
            return bool.Parse(value);
        }

        throw new NotImplementedException();
    }
    
    private static object ConvertNumber(Type type, string value)
    {
        if (type == typeof(long) || type == typeof(long?))
            return Convert.ToInt64(value.RemoveDecimal());
        if (type == typeof(int) || type == typeof(int?))
            return Convert.ToInt32(value.RemoveDecimal());
        if (type == typeof(short) || type == typeof(short?))
            return Convert.ToInt16(value.RemoveDecimal());
        if (type == typeof(ulong) || type == typeof(ulong?))
            return Convert.ToUInt64(value.RemoveDecimal());
        if (type == typeof(uint) || type == typeof(uint?))
            return Convert.ToUInt32(value.RemoveDecimal());
        if (type == typeof(ushort) || type == typeof(ushort?))
            return Convert.ToUInt16(value.RemoveDecimal());
        if (type == typeof(byte) || type == typeof(byte?))
            return Convert.ToByte(value.RemoveDecimal());
        if (type == typeof(sbyte) || type == typeof(sbyte?))
            return Convert.ToSByte(value.RemoveDecimal());
        if (type == typeof(float) || type == typeof(float?))
            return Convert.ToSingle(value);
        if (type == typeof(double) || type == typeof(double?))
            return Convert.ToDouble(value);
        if (type == typeof(decimal) || type == typeof(decimal?))
            return Convert.ToDecimal(value);
        throw new ArgumentException("Invalid type");
    }
    private static string RemoveDecimal(this string value)
    {
        if (value.Contains('.')) value = value.Split(".")[0];
        return value;
    }
}