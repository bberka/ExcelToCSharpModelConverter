using System;

namespace XSharp.Test.ExportedModels;

[SheetName("Sheet_1")]
public class Sheet_1 : XSheetBase,BaseSheetModel
{
    [HeaderName("#비고1")]
    [InvalidValueType]
    public String _1 { get; set; }
    [HeaderName("#비고2")]
    [InvalidValueType]
    public String _2 { get; set; }
    [HeaderName("#비고3")]
    [InvalidValueType]
    public String _3 { get; set; }
    [HeaderName("#비고4")]
    [InvalidValueType]
    public String _4 { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
