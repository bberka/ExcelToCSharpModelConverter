using System.Diagnostics;

namespace XSharp.Core;

public static class EPPlusLicence
{
    public static void Import()
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
        Trace.WriteLine("EPPlus NonCommercial licence activated");
    }
}