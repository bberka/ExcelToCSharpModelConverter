namespace ExcelToCSharpModelConverter.Shared.Models.Option;

public class IgnoreWhen
{
    public ConditionOption ConditionOption { get; set; } = ConditionOption.Default();
    public Types TypeToIgnore { get; set; } = Types.NotSet;
}