using Ninject.Modules;
using XSharp.ExtendExample.Validators;
using XSharp.Shared.Abstract;

namespace XSharp.ExtendExample;

public class XExtendValidatorBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IXCellValidator>().To<CellValidatorExtend>();
        Bind<IXFileNameValidator>().To<FileNameValidatorExtend>();
        Bind<IXSheetValidator>().To<SheetValidatorExtend>();
    }
}