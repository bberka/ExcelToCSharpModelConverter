using Ninject.Modules;
using XSharp.Shared.Abstract;

namespace XSharp.Shared;

public class XModelBindings : NinjectModule
{
    public override void Load()
    {
        Bind<XFile>().To<XFile>();
        Bind<XHeader>().To<XHeader>();
        Bind<XOption>().ToSelf().InSingletonScope();
    }
}