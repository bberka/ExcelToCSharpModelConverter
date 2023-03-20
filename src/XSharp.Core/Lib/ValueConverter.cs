using System.Text.RegularExpressions;

namespace XSharp.Core.Lib;

public static class ValueConverter
{
    public static bool TryConvert(object? value, Type? toType, out object? convertedType)
    {
        convertedType = null;
        if (value is null || toType is null) return false;
        if (toType == typeof(string))
        {
            convertedType = value.ToString();
            return true;
        }

        if (toType == typeof(bool) || toType == typeof(bool?))
        {
            if (value is bool b)
            {
                convertedType = b;
                return true;
            }

            if (value is string s)
            {
                if (s.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    convertedType = true;
                    return true;
                }

                if (s.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    convertedType = false;
                    return true;
                }
            }
            return false;
        }

        if (toType.IsEnum)
        {
            if (value is string s)
            {
                if (Enum.TryParse(toType, s, true, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(long) || toType == typeof(long?))
        {
            if (value is string s)
            {
                if (long.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(float) || toType == typeof(float?))
        {
            if (value is string s)
            {
                if (float.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(double) || toType == typeof(double?))
        {
            if (value is string s)
            {
                if (double.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(decimal) || toType == typeof(decimal?))
        {
            if (value is string s)
            {
                if (decimal.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(int) || toType == typeof(int?))
        {
            if (value is string s)
            {
                if (int.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(short) || toType == typeof(short?))
        {
            if (value is string s)
            {
                if (short.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(byte) || toType == typeof(byte?))
        {
            if (value is string s)
            {
                if (byte.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(char) || toType == typeof(char?))
        {
            if (value is string s)
            {
                if (char.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(DateTimeOffset) || toType == typeof(DateTimeOffset?))
        {
            if (value is string s)
            {
                if (DateTimeOffset.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }


        if (toType == typeof(DateTime) || toType == typeof(DateTime?))
        {
            if (value is string s)
            {
                if (DateTime.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(TimeSpan) || toType == typeof(TimeSpan?))
        {
            if (value is string s)
            {
                if (TimeSpan.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(Guid) || toType == typeof(Guid?))
        {
            if (value is string s)
            {
                if (Guid.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(Uri))
        {
            if (value is string s)
            {
                if (Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }

        if (toType == typeof(Version))
        {
            if (value is string s)
            {
                if (Version.TryParse(s, out var result))
                {
                    convertedType = result;
                    return true;
                }
            }
            return false;
        }
        

        return false;
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