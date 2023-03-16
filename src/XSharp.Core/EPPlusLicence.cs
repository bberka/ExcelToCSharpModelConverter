using System.Diagnostics;

namespace XSharp.Core;

public class EPPlusLicence
{
    public static void Import()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        Trace.WriteLine("EPPlus NonCommercial licence activated");
    }
}