namespace ExcelToCSharpModelConverter.ExportedModels;

public BackUp : BaseSheetModel
{
    public String Key { get; set; }
    [HeaderName("#MonsterKey")]
    [InvalidValueType]
    public String MonsterKey { get; set; }
    [HeaderName("#(캐릭터, 아이템, 몬스터) 등의 Index")]
    [InvalidValueType]
    public String Index { get; set; }
    [InvalidValueType]
    public String LearningType { get; set; }
    [HeaderName("#대상의 종류(0:캐릭터/몬스터, 1:아이템)")]
    [InvalidValueType]
    public String _01 { get; set; }
    [HeaderName("%SelectRate")]
    [InvalidValueType]
    public String SelectRate { get; set; }
    [InvalidValueType]
    public String KnowledgeIndex { get; set; }
    [HeaderName("#지식번호PC에게 넣어줄 지식 번호")]
    [InvalidValueType]
    public String PC { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
