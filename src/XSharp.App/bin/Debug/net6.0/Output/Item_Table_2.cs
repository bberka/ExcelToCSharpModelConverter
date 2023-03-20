using System;

namespace XSharp.Test.ExportedModels;

[SheetName("Item_Table_2")]
public class Item_Table_2 : XSheetBase,BaseSheetModel
{
    [HeaderName("^Index")]
    public String Index { get; set; }
    [HeaderName("~ItemName")]
    public String ItemName { get; set; }
    [InvalidValueType]
    public String OriginalPrice { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
