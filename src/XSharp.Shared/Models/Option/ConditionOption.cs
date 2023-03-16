
using XSharp.Shared.Constants;

namespace XSharp.Shared.Models.Option;

public class ConditionOption
{
    [JsonConverter(typeof(StringEnumConverter))]
    public Condition Condition { get; set; } = Condition.NotSet;
    [JsonConverter(typeof(StringEnumConverter))]
    public Types TypeToValidateCondition { get; set; } = Types.NotSet;
    public object? Value { get; set; }
    public ConditionOption? InnerCondition { get; set; }

    public static ConditionOption Default() => new ConditionOption();
}