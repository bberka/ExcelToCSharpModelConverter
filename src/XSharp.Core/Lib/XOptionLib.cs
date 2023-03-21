using XSharp.Shared;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Core.Lib;

public class XOptionLib
{
    private const string JsonPath = "ExportOption.json";

    private static XOptionLib? _instance;

    private XOptionLib()
    {
        Option = XKernel.This.GetInstance<IXOption>();
        SetDefaults();
        ReadJson();
    }

    public static XOptionLib This
    {
        get
        {
            _instance ??= new XOptionLib();
            return _instance;
        }
    }


    public IXOption Option { get; private set; }

    private IXValidator _validator = new XDefaultValidator();

    public IXValidator GetValidator()
    {
        return _validator;
    }
    public void SetValidator(IXValidator validator)
    {
        _validator = validator;
    }

    public void WriteJson()
    {
        var json = XSerializer.SerializeJson(Option);
        File.WriteAllText(JsonPath, json);
    }

    public Result ReadJson()
    {
        var fileExists = File.Exists(JsonPath);
        if (!fileExists) return Result.Error("File not found: " + JsonPath);
        var read = File.ReadAllText(JsonPath);
        var option = XSerializer.DeserializeJson<XOption>(read);
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
        Option.ModelInheritanceList.Add("XSheetBase");
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


}