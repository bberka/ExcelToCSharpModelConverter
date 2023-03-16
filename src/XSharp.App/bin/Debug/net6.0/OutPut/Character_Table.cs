namespace ExcelToCSharpModelConverter.ExportedModels;

public Character_Table : BaseSheetModel
{
    [HeaderName("^Index")]
    public String Index { get; set; }
    [InvalidValueType]
    public String CharName { get; set; }
    [HeaderName("~DisplayName")]
    [InvalidValueType]
    public String DisplayName { get; set; }
    [HeaderName("~CharacterTitle")]
    [InvalidValueType]
    public String CharacterTitle { get; set; }
    [InvalidValueType]
    public String Level { get; set; }
    [InvalidValueType]
    public String ExpectedPrice { get; set; }
    [InvalidValueType]
    public String Count { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
