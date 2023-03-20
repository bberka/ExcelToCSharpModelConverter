using System.Reflection;
using EasMe.Logging;
using EasMe.Result;
using Ninject;
using OfficeOpenXml.Interfaces.Drawing.Text;
using XSharp.Shared.Abstract;

namespace XSharp.Shared;

public class XKernel : IXKernel
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();
    private XKernel()
    {
        _kernel = new StandardKernel();
        _kernel.Load(Assembly.GetExecutingAssembly());

    }
    public static XKernel This
    {
        get
        {
            Instance ??= new();
            return Instance;
        }
    }
    private static XKernel? Instance;

    private static IKernel _kernel;
    public T GetInstance<T>() 
    {
        return _kernel.Get<T>();
    }
    public void Load(Assembly assembly)
    {
        _kernel.Load(assembly);
    }
    public Result LoadDll(string filePath)
    {
        if(!File.Exists(filePath)) return Result.Warn("File not found: " + filePath);
        var isAbsolutePath = Path.IsPathRooted(filePath);
        if (!isAbsolutePath)
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            // return Result.Warn("File path is not absolute: " + filePath);
        }
        var assembly = Assembly.LoadFile(filePath);
        Load(assembly);
        return Result.Success();
    }
    
    public T? GetValidator<T>() where T : IXBaseValidator
    {
        try
        {
            return GetInstance<T>();
        }
        catch (Exception ex)
        {
            logger.Debug(ex, "Failed to get instance: " + typeof(T).Name);
            return default;
        }
    }
    
    public static void Init()
    {
        _ = This;
    }

   
}