using System.Globalization;

namespace XSharp.Extensions;

public static class XExtensions
{
  private const string _lowerAll = "abcdefghijklmnoprstuvwxyzq";
  private const string _upperAll = "ABCDEFGHIJKLMNOPRSTUVWXYZQ";
  private const string _digits = "0123456789";
  private const string _validChars = _lowerAll + _upperAll + _digits;

  public static bool IsFilePathExcel(this string filePath) {
    return filePath!.EndsWith(".xlsx") || filePath.EndsWith(".xlsm") || filePath.EndsWith(".xls");
  }

  public static bool IsFilePathCsv(this string filePath) {
    return filePath!.EndsWith(".csv");
  }

  public static string FixXName(this string name) {
    var sb = new StringBuilder();
    name = name.RemoveWhiteSpace().RemoveLineEndings().RemoveText("_");
    var charList = name.Where(c => _validChars.Contains(c)).ToList();
    for (var i = 0; i < charList.Count; i++) {
      var ch = charList[i];
      if (i == 0) {
        if (char.IsDigit(ch))
          sb.Append('_');
        if (char.IsLower(ch))
          ch = char.ToUpper(ch, CultureInfo.InvariantCulture);
      }

      sb.Append(ch);
    }

    return sb.ToString();
  }
}