using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using XSharp.Shared;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Core.Lib;

public class OptionLib
{
    private const string JsonPath = "ExportOption.json";

    private static OptionLib? _instance;

    private OptionLib()
    {
        Option = XKernel.This.GetInstance<IXOption>();
        SetDefaults();
        ReadJson();
    }

    public static OptionLib This
    {
        get
        {
            _instance ??= new OptionLib();
            return _instance;
        }
    }


    public IXOption Option { get; private set; }


    public void WriteJson()
    {
        var json = JsonConvert.SerializeObject(Option, GetJsonSerializerSettings());
        File.WriteAllText(JsonPath, json);
    }

    public Result ReadJson()
    {
        var fileExists = File.Exists(JsonPath);
        if (!fileExists) return Result.Error("File not found: " + JsonPath);

        var read = File.ReadAllText(JsonPath);
        var option = JsonConvert.DeserializeObject<XOption>(read, GetJsonSerializerSettings());
        if (option is null) return Result.Error("Option is null");

        Option = option;
        return Result.Success();
    }

    public void Configure(Action<XOption> option)
    {
        var xOption = new XOption();
        option(xOption);
        Option = xOption;
    }

    public void SetDefaults()
    {
        Option.DefaultValueType = ValueType.String;
        Option.NameSpace = "XSharp.Test.ExportedModels";
        Option.HeaderColumnNumber = 1;
        Option.SetValueTypesAtRowNumber = 2;
        Option.ModelInheritanceString = "BaseSheetModel, IBaseSheetModel";
        Option.MinimumLogLevel = LogLevel.Information;
        Option.NullValueStrings = new List<string> { "<null>" };
        Option.UsingNameSpaceList = new List<string>
        {
            "System",
            "XSharp.Shared.Models"
        };
    }

    public void SetTestOption()
    {
        var option = new XOption();
        option.NullValueStrings.Add("<null>");
        option.NullValueStrings.Add("<empty>");
        option.UsingNameSpaceList.Add("System");
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