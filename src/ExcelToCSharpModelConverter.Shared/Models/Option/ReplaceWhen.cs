using ExcelToCSharpModelConverter.Shared.Constants;

namespace ExcelToCSharpModelConverter.Shared.Models.Option;

public class ReplaceWhen
{
    public ConditionOption ConditionOption { get; set; } = ConditionOption.Default();
    public Types TypeToReplace { get; set; } = Types.NotSet;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}