using System.Xml.Serialization;
using ExcelToCSharpModelConverter.Shared.Constants;
using ExcelToCSharpModelConverter.Shared.Models.Option;

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

    public void SetOption(ExportOption option)
    {
        if (option is null)
        {
            throw new ArgumentNullException(nameof(option));
        }
        Option = option;
    }
    public void SetOption(string path = "ExportOption.xml")
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (!File.Exists(path))
        {
            throw new Exception("File not found: " + path);
        }
        var opt = ReadXml(path);
        if (opt is null)
        {
            throw new Exception("Option is null");
        }
        Option = opt;
    }
    private ExportOption? ReadXml(string path = "ExportOption.xml")
    {
        var serializer = new XmlSerializer(typeof(ExportOption));
        using var reader = new StreamReader(path);
        var option = (ExportOption?) serializer.Deserialize(reader);
        return option;
    }

    public void WriteXml()
    {
        var path = "ExportOption.xml";
        Option ??= new();
        var serializer = new XmlSerializer(typeof(ExportOption));
        using var writer = new StreamWriter(path);
        serializer.Serialize(writer, Option);
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
        option.UsingList.Add("System.Collections.Generic");
        option.UsingList.Add("System.Linq");
        Option = option;
    }
}