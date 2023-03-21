namespace XSharp.Core.Lib;

internal static class XValueConverter
{
    public static bool TryConvert(string? s, Type? toType, out object? convertedType)
    {
        convertedType = null;
        if (s is null || toType is null) return false;
        if (toType == typeof(string))
        {
            convertedType = s.ToString();
            return true;
        }

        if (toType == typeof(bool) || toType == typeof(bool?))
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


            return false;
        }

        if (toType.IsEnum)
        {
            if (Enum.TryParse(toType, s, true, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(long) || toType == typeof(long?))
        {

            if (long.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(float) || toType == typeof(float?))
        {

            if (float.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(double) || toType == typeof(double?))
        {

            if (double.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(decimal) || toType == typeof(decimal?))
        {

            if (decimal.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(int) || toType == typeof(int?))
        {

            if (int.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(short) || toType == typeof(short?))
        {

            if (short.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(byte) || toType == typeof(byte?))
        {

            if (byte.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(char) || toType == typeof(char?))
        {

            if (char.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(DateTimeOffset) || toType == typeof(DateTimeOffset?))
        {

            if (DateTimeOffset.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }


        if (toType == typeof(DateTime) || toType == typeof(DateTime?))
        {

            if (DateTime.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(TimeSpan) || toType == typeof(TimeSpan?))
        {

            if (TimeSpan.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(Guid) || toType == typeof(Guid?))
        {

            if (Guid.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(Uri))
        {

            if (Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }

        if (toType == typeof(Version))
        {

            if (Version.TryParse(s, out var result))
            {
                convertedType = result;
                return true;
            }

            return false;
        }


        return false;
    }


    public static bool TryConvert(string? s, out object? convertedType)
    {
        convertedType = null;
        if (s is null) return false;
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
        if (long.TryParse(s, out var result1))
        {
            convertedType = result1;
            return true;
        }

        if (float.TryParse(s, out var result2))
        {
            convertedType = result2;
            return true;
        }

        if (double.TryParse(s, out var result3))
        {
            convertedType = result3;
            return true;
        }

        if (decimal.TryParse(s, out var result4))
        {
            convertedType = result4;
            return true;
        }

        if (int.TryParse(s, out var result5))
        {
            convertedType = result5;
            return true;
        }

        if (short.TryParse(s, out var result6))
        {
            convertedType = result6;
            return true;
        }

        if (byte.TryParse(s, out var result7))
        {
            convertedType = result7;
            return true;
        }

        if (char.TryParse(s, out var result8))
        {
            convertedType = result8;
            return true;
        }

        if (DateTimeOffset.TryParse(s, out var result9))
        {
            convertedType = result9;
            return true;
        }


        if (DateTime.TryParse(s, out var result10))
        {
            convertedType = result10;
            return true;
        }

        if (TimeSpan.TryParse(s, out var result11))
        {
            convertedType = result11;
            return true;
        }

        if (Guid.TryParse(s, out var result12))
        {
            convertedType = result12;
            return true;
        }

        if (Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out var result13))
        {
            convertedType = result13;
            return true;
        }

        if (Version.TryParse(s, out var result14))
        {
            convertedType = result14;
            return true;
        }
        convertedType = s;
        return false;
    }
    public static Type GetTryType(string? s, Type defaultType)
    {
        if (s is null) return defaultType;
        if (s.Equals("false", StringComparison.OrdinalIgnoreCase) || s.Equals("true", StringComparison.OrdinalIgnoreCase))
            return typeof(bool);
        if (long.TryParse(s, out _))
            return typeof(long);
        //if (float.TryParse(s, out _))
        //    return typeof(float);
        //if (double.TryParse(s, out _))
        //    return typeof(double);
        if (decimal.TryParse(s, out _))
            return typeof(decimal);
        //if (int.TryParse(s, out _))
        //    return typeof(int);
        //if (short.TryParse(s, out _))
        //    return typeof(short);
        //if (byte.TryParse(s, out _))
        //    return typeof(byte);
        //if (char.TryParse(s, out var result8))
        //    return typeof(char);
        if (DateTimeOffset.TryParse(s, out _))
            return typeof(DateTimeOffset);
        if (DateTime.TryParse(s, out _))
            return typeof(DateTime);
        if (TimeSpan.TryParse(s, out _))
            return typeof(TimeSpan);
        if (Guid.TryParse(s, out _))
            return typeof(Guid);
        //if (Version.TryParse(s, out _))
        //    return typeof(Version);
        return defaultType;
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