namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("EnchantDrop_Table")]
public EnchantDrop_Table : BaseSheetModel
{
    public String ItemClassify { get; set; }
    public String GradeType { get; set; }
    [InvalidValueType]
    public String EnchantLevel { get; set; }
    [HeaderName("%SelectRate")]
    [InvalidValueType]
    public String SelectRate { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
