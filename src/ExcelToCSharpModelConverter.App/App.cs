using ExcelToCSharpModelConverter.Core.Lib;

namespace ExcelToCSharpModelConverter.App;

public class App
{

    private App()
    {
    }

    public static App This
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    private static App? _instance;

    public void Run()
    {
        OptionLib.This.SetTestOption();
        OptionLib.This.WriteXml();
    }
  
}