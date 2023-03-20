using System;

namespace XSharp.Test.ExportedModels;

[SheetName("Item_Table")]
public class Item_Table : XSheetBase,BaseSheetModel
{
    [HeaderName("^Index")]
    public String Index { get; set; }
    [HeaderName("~ItemName")]
    [InvalidValueType]
    public String ItemName { get; set; }
    [InvalidValueType]
    public String OriginalPrice { get; set; }
    [InvalidValueType]
    public String SellPriceToNpc { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
