using System.Diagnostics;
using System.Reflection;
using Ninject;
using OfficeOpenXml;

namespace XSharp.Shared;

public class XKernel
{
  private static XKernel? Instance;

  private static IKernel _kernel;

  private XKernel() {
    _kernel = new StandardKernel();
    _kernel.Load(Assembly.GetExecutingAssembly());
  }

  public static XKernel This {
    get {
      Instance ??= new XKernel();
      return Instance;
    }
  }

  public T GetInstance<T>() {
    return _kernel.Get<T>();
  }

  public void LoadNinjectModules(Assembly assembly) {
    _kernel.Load(assembly);
  }

  public void LoadDll(string filePath) {
    if (!File.Exists(filePath))
      throw new Exception("File not found: " + filePath);
    var isAbsolutePath = Path.IsPathRooted(filePath);
    if (!isAbsolutePath) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
    // return Result.Warn("File path is not absolute: " + filePath);
    var assembly = Assembly.LoadFile(filePath);
    LoadNinjectModules(assembly);
  }

  public static void Init() {
    ExcelPackage.LicenseContext = LicenseContext.Commercial;
    Trace.WriteLine("EPPlus NonCommercial license activated!!!");
    _ = This;
    Trace.WriteLine("XKernel initialized!!!");
  }
}