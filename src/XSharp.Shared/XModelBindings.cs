using Ninject.Modules;
using XSharp.Shared.Abstract;
using XSharp.Shared.Models;
using XSharp.Shared.Obsolete;

namespace XSharp.Shared;

public class XModelBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IXRow>().To<XRow>();
        Bind<IXCell>().To<XCell>();
        Bind<IXHeader>().To<XHeader>();
        Bind<IXOption>().To<XOption>().InSingletonScope();
    }
}