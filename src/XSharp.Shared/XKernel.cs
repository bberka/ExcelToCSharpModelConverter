using System.Reflection;
using Ninject;
using XSharp.Shared.Abstract;

namespace XSharp.Shared;

public class XKernel : IXKernel
{

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
    
    public static void Init()
    {
        _ = This;
    }
}