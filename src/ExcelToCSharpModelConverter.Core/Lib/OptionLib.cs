using System.Text.Json;
using System.Xml.Serialization;
using EasMe.Extensions;
using EasMe.Result;
using ExcelToCSharpModelConverter.Shared.Constants;
using ExcelToCSharpModelConverter.Shared.Models.Option;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ExcelToCSharpModelConverter.Core.Lib;

public class OptionLib
{
    
    private OptionLib()
    {
        Option = new();
    }

    public static OptionLib This
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private static OptionLib? _instance;

    
    public ExportOption Option
    {
        
        get; 
        private set; 
    }


    public void WriteJson()
    {
        var json = JsonConvert.SerializeObject(Option, GetJsonSerializerSettings());
        File.WriteAllText(JsonPath, json);
        
    }

   
    private const string JsonPath = "ExportOption.json";
    public Result ReadJson()
    {
        var fileExists = File.Exists(JsonPath);
        if (!fileExists)
        {
            return Result.Error("File not found: " + JsonPath);
        }
        var read = File.ReadAllText(JsonPath);
        var option = JsonConvert.DeserializeObject<ExportOption>(read, GetJsonSerializerSettings());
        if(option is null)
        {
            return Result.Error("Option is null");
        }
        Option = option;
        return Result.Success();
    }
    public void SetTestOption()
    {
        var option = new ExportOption();
        option.IgnoreWhenConditions.Add(new IgnoreWhen());
        option.NullValueStrings.Add("<null>");
        option.NullValueStrings.Add("<empty>");
        option.TrimWhenConditions.Add(new TrimWhen());
        option.ReplaceWhenConditions.Add(new ReplaceWhen()
        {
            ConditionOption = new ConditionOption()
            {
                InnerCondition = new ()
                {
                    InnerCondition = new ()
                }
            }
        });
        option.UsingList.Add("System");
        Option = option;
    }
    
    private static JsonSerializerSettings GetJsonSerializerSettings()
    {
        var serializerOption = new JsonSerializerSettings();
        serializerOption.Formatting = Formatting.Indented;
        serializerOption.Converters.Add(new StringEnumConverter());
        serializerOption.NullValueHandling = NullValueHandling.Include;
        serializerOption.DefaultValueHandling = DefaultValueHandling.Include;
        serializerOption.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        serializerOption.PreserveReferencesHandling = PreserveReferencesHandling.None;
        serializerOption.TypeNameHandling = TypeNameHandling.None;
        return serializerOption;
    }
}