using XSharp.Shared;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Core.Lib;

public class XOptionLib
{

    private static XOptionLib? _instance;

    private IXValidator _validator = new XDefaultValidator();

    private XOptionLib()
    {
        Option = XKernel.This.GetInstance<XOption>();
        SetDefaults();
    }

    public static XOptionLib This
    {
        get
        {
            _instance ??= new XOptionLib();
            return _instance;
        }
    }


    public XOption Option { get; private set; }

    public IXValidator GetValidator()
    {
        return _validator;
    }

    public void SetValidator(IXValidator validator)
    {
        _validator = validator;
    }


    public void Configure(Action<XOption> action)
    {
        action(Option);
    }

    public void SetDefaults()
    {
        Option.DefaultValueType = ValueType.String;
        Option.SheetModelNameSpace = "XSharp.Test.ExportedModels";
        Option.FileModelNameSpace = "XSharp.Test.ExportedModels";
        Option.HeaderColumnNumber = 1;
        Option.SetValueTypesAtRowNumber = 2;
        Option.NullValueStrings = new List<string> { "<null>" };
        Option.UsingNameSpaceList = new List<string>
        {
            "System",
            "XSharp.Shared.Models"
        };
    }

    
}