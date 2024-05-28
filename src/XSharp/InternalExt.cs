namespace XSharp;

internal static class InternalExt
{
  internal static string RemoveWhiteSpace(this string str) {
    return str.Replace(" ", "");
  }

  internal static string RemoveLineEndings(this string str) {
    return str.ReplaceLineEndings("");
  }

  internal static string RemoveText(this string str, string text) {
    return str.Replace(text, "");
  }

  internal static bool HasContent(this string? str) {
    return str != null && !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
  }
}