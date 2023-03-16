namespace ExcelToCSharpModelConverter.ExportedModels;

public Item_Table_2 : BaseSheetModel
{
    [HeaderName("^Index")]
    public String Index { get; set; }
    [HeaderName("~ItemName")]
    public String ItemName { get; set; }
    public String OriginalPrice { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
