namespace ExcelToCSharpModelConverter.ExportedModels;

public Item_Table : BaseSheetModel
{
    [HeaderName("#비고1")]
    [InvalidValueType]
    public String _1 { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
