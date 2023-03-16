using EasMe.Logging;
using EasMe.Result;
using Microsoft.Extensions.Logging;
using XSharp.Core.Lib;
using XSharp.Core.Manager;

namespace XSharp.App;

public class App
{
    private readonly static IEasLog logger = EasLogFactory.CreateLogger();

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
        InitLoggingSettings();
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
        LoopTillExit();
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
                    var createResult = WorkBookManager.Create(path);
                    if (createResult.IsFailure)
                    {
                        Console.WriteLine(createResult.ErrorCode);
                        continue;
                    }

                    var manager = createResult.Data!;
                    var exportResult = manager.ExportWorkSheets();
                    logger.LogResult(exportResult, "Exporting Excel To Csharp Models Completed");
                    break;
                }
                case "2":
                {
                    Console.WriteLine("Enter directory path");
                    var path = Console.ReadLine();
                    var multipleResult = WorkBookManager.CreateWithDirectory(path);
                    if (multipleResult.IsFailure) continue;
                    var manager = multipleResult.Data!;
                    var results = new List<Result>();
                    Parallel.ForEach(manager
                        , new ParallelOptions { MaxDegreeOfParallelism = 10 }
                        , sheet =>
                        {
                            var result = sheet.ExportWorkSheets();
                            lock (results)
                            {
                                results.Add(result);
                            }
                        });
                    var exportResult = results.Combine("ExportingExcelToCsharpModels");
                    logger.LogResult(exportResult, "Exporting Excel To Csharp Models Completed");
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