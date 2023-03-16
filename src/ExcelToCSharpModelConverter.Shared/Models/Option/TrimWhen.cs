
namespace ExcelToCSharpModelConverter.Shared.Models.Option;

public class TrimWhen
{
    public ConditionOption ConditionOption { get; set; } = ConditionOption.Default();
    [JsonConverter(typeof(StringEnumConverter))]
    public Types TypeToTrim { get; set; } = Types.NotSet;
}