namespace ExcelToCSharpModelConverter.ExportedModels;

public Collect_Table : BaseSheetModel
{
    public String Index { get; set; }
    [InvalidValueType]
    public String ProductDataType { get; set; }
    [InvalidValueType]
    public String NeedObjectSkillNo { get; set; }
    [InvalidValueType]
    public String ProductSkillLevel { get; set; }
    [InvalidValueType]
    public String ProductTime { get; set; }
    [InvalidValueType]
    public String Exp { get; set; }
    [InvalidValueType]
    public String ProductSkillPointExp { get; set; }
    [InvalidValueType]
    public String ProductAllHP { get; set; }
    [InvalidValueType]
    public String ProductDeceraseHP { get; set; }
    [InvalidValueType]
    public String ItemDropID { get; set; }
    [InvalidValueType]
    public String ActionPoint { get; set; }
    [InvalidValueType]
    public String SpawnDelayTime { get; set; }
    [InvalidValueType]
    public String SpawnVariableTime { get; set; }
    [HeaderName("#Character Name_")]
    [InvalidValueType]
    public String CharacterName_ { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
