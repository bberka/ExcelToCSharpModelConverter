
namespace ExcelToCSharpModelConverter.Shared.Models.Option;

public class IgnoreWhen
{
    public ConditionOption ConditionOption { get; set; } = ConditionOption.Default();
    [JsonConverter(typeof(StringEnumConverter))]
    public Types TypeToIgnore { get; set; } = Types.NotSet;
}