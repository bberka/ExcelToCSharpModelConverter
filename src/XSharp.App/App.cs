using EasMe.Extensions;
using EasMe.Logging;
using Microsoft.Extensions.Logging;
using XSharp.Core.Export;
using XSharp.Core.Lib;
using XSharp.Core.Read;
using XSharp.Shared;
using XSharp.Test;

namespace XSharp.App;

public class App
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();

    private static App? _instance;

    private App()
    {
    }

    public static App This
    {
        get
        {
            _instance ??= new App();
            return _instance;
        }
    }

    public void Run()
    {
        XKernel.Init();
        InitLoggingSettings();
        RunReadOption();
        LoopTillExit();
    }

    private void RunReadOption()
    {
        var optionResult = OptionLib.This.ReadJson();
        if (optionResult.IsFailure)
        {
            logger.LogResult(optionResult, "Reading ExportOption.json Failed");
            Console.WriteLine("Reading ExportOption.json Failed. Error: " + optionResult.ErrorCode);
            Console.WriteLine("Press C key to create example option .json file. Press any other key to exit");
            var key = Console.ReadKey();
            if (key.Key != ConsoleKey.C) return;
            OptionLib.This.SetTestOption();
            OptionLib.This.WriteJson();
            return;
        }

        if (OptionLib.This.Option.ExtendValidatorDllFilePath.IsNullOrEmpty())
        {
            Console.WriteLine("Extend Validator is not specified in ExportOption.json");
            return;
        }

        var dllResult = XKernel.This.LoadDll(OptionLib.This.Option.ExtendValidatorDllFilePath!);
        if (!dllResult.IsFailure) return;
        logger.LogResult(dllResult, "Loading Extend Validator Failed");
        Console.WriteLine("Loading Extend Validator Failed. Error: " + dllResult.ErrorCode);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    private void LoopTillExit()
    {
        while (true)
        {
            PrintUsage();
            var input = Console.ReadLine();
            switch (input)
            {
                case null:
                    continue;
                case "1":
                {
                    Console.WriteLine("Enter file path");
                    var path = Console.ReadLine();
                    var exportResult = XFileExportManager.ExportExcelFile(path);
                    logger.LogResult(exportResult, "Exporting Excel To Csharp Models Completed");
                    break;
                }
                case "2":
                {
                    Console.WriteLine("Enter directory path");
                    var path = Console.ReadLine();
                    var exportResult = XFileExportManager.ExportExcelFilesInDirectory(path);
                    logger.LogResult(exportResult, "Exporting Excel To Csharp Models Completed");
                    break;
                }
                case "3":
                {
                    var path =
                        @"F:\bdo\data-sheet\DataSheet-Corsair-Base\DataSheet\DropDataSheet\DataSheet_ItemGroupDataTable_MonsterFromDrop_1.xlsm";
                    var sheet = XLib.GetExcelSheetsFromWorkBookPath(path)
                        .FirstOrDefault(x => x.Name == "ItemSubGroup_Table");
                    var readerResult = XSheetReadManager.Read<ItemSubGroup_Table>(sheet);
                    break;
                }
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void PrintUsage()
    {
        Console.WriteLine("1. Export Excel To Csharp Models by single file path");
        Console.WriteLine("2. Export Excel To Csharp Models by directory path");
        Console.WriteLine("3. Test read excel file");
        Console.WriteLine("0. Exit");
    }

    private void InitLoggingSettings()
    {
        EasLogFactory.Configure(x =>
        {
            x.ExceptionHideSensitiveInfo = false;
            x.MinimumLogLevel = LogLevel.Debug;
            x.ConsoleAppender = false;
        });
    }
}