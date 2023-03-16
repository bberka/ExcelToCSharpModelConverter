namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("Instance_Reward")]
public Instance_Reward : BaseSheetModel
{
    public String InstanceDundeonNo { get; set; }
    [HeaderName("#FieldName")]
    public String FieldName { get; set; }
    [InvalidValueType]
    public String LearnSkill { get; set; }
    [InvalidValueType]
    public String RegionNo { get; set; }
    [HeaderName("PushItem1")]
    [InvalidValueType]
    public String PushItem_1 { get; set; }
    [HeaderName("PushItem2")]
    [InvalidValueType]
    public String PushItem_2 { get; set; }
    [HeaderName("PushItem3")]
    [InvalidValueType]
    public String PushItem_3 { get; set; }
    [HeaderName("Grade1")]
    [InvalidValueType]
    public String Grade_1 { get; set; }
    [HeaderName("Grade2")]
    [InvalidValueType]
    public String Grade_2 { get; set; }
    [HeaderName("Grade3")]
    [InvalidValueType]
    public String Grade_3 { get; set; }
    [HeaderName("Grade4")]
    [InvalidValueType]
    public String Grade_4 { get; set; }
    [HeaderName("Grade5")]
    [InvalidValueType]
    public String Grade_5 { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
