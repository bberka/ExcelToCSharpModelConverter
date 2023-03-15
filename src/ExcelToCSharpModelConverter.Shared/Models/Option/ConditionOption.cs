namespace ExcelToCSharpModelConverter.Shared.Models.Option;

public class ConditionOption
{
    public Condition Condition { get; set; } = Condition.NotSet;
    public Types TypeToValidateCondition { get; set; } = Types.NotSet;
    public object? Value { get; set; }
    public ConditionOption? InnerCondition { get; set; }

    public static ConditionOption Default() => new ConditionOption();
}