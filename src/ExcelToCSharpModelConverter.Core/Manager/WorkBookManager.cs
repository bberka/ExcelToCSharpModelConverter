using EasMe.Extensions;
using EasMe.Result;
using OfficeOpenXml;

namespace ExcelToCSharpModelConverter.Core.Manager;

public class WorkBookManager
{
    private readonly string _filePath;

    private WorkBookManager(string filePath)
    {
        _filePath = filePath;
    }

    public static ResultData<WorkBookManager> Create(string? filePath)
    {
        if (filePath.IsNullOrEmpty())
        {
            return Result.Warn(1, "Invalid path value : " + filePath);
        }

        if (!File.Exists(filePath))
        {
            return Result.Warn(2, "Path not exists : " + filePath);
        }

        return new WorkBookManager(filePath);
    }


}