namespace ExcelToCSharpModelConverter.ExportedModels;

public MonsterCharacter_Table : BaseSheetModel
{
    [HeaderName("^Index")]
    [InvalidValueType]
    public String Index { get; set; }
    [InvalidValueType]
    public String CharName { get; set; }
    [HeaderName("~DisplayName")]
    [InvalidValueType]
    public String DisplayName { get; set; }
    [HeaderName("#배치 여부2014년 07월 12일 기준")]
    [InvalidValueType]
    public String _20140712 { get; set; }
    [HeaderName("~CharacterTitle")]
    [InvalidValueType]
    public String CharacterTitle { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
