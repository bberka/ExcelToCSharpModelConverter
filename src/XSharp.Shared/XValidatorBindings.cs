using Ninject.Modules;
using XSharp.Shared.Abstract;
using XSharp.Shared.Models;
using XSharp.Shared.Validators;


namespace XSharp.Shared;

public partial class XBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IXFile>().To<XFile>();
        Bind<IXSheet>().To<XSheet>();
        Bind<IXRow>().To<XRow>();
        Bind<IXCell>().To<XCell>();
        Bind<IXHeader>().To<XHeader>();
        Bind<IXCellValidator>().To<XCellValidator>();
        Bind<IXRowValidator>().To<XRowValidator>();
        Bind<IXHeaderValidator>().To<XHeaderValidator>();
        Bind<IXFileNameValidator>().To<XFileNameValidator>();
        Bind<IXSheetValidator>().To<XSheetValidator>();
        
    }
}