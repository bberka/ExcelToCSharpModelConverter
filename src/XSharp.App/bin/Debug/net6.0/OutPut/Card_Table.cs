namespace ExcelToCSharpModelConverter.ExportedModels;

public Card_Table : BaseSheetModel
{
    [HeaderName("~Name")]
    public String Name { get; set; }
    [HeaderName("^Key")]
    [InvalidValueType]
    public String Key { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
