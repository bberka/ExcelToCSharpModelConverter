using System;

namespace XSharp.Test.ExportedModels;

[SheetName("Dummy_for_Comparison_5")]
public class Dummy_for_Comparison_5 : XSheetBase,BaseSheetModel
{
    [HeaderName("캐릭터 DB")]
    public String DB { get; set; }
}
