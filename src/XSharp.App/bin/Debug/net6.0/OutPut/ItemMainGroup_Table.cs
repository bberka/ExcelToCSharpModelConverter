namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("ItemMainGroup_Table")]
public ItemMainGroup_Table : BaseSheetModel
{
    public String ItemMainGroupKey { get; set; }
    [HeaderName("%DoSelectOnlyOne")]
    public String DoSelectOnlyOne { get; set; }
    [HeaderName("%RefreshStartHour")]
    [InvalidValueType]
    public String RefreshStartHour { get; set; }
    [HeaderName("%RefreshInterval")]
    [InvalidValueType]
    public String RefreshInterval { get; set; }
    [HeaderName("%PlantCraftResultCount")]
    public String PlantCraftResultCount { get; set; }
    [HeaderName("%SelectRate0")]
    public String SelectRate_0 { get; set; }
    [HeaderName("%Condition0")]
    public String Condition_0 { get; set; }
    [HeaderName("ItemSubGroupKey0")]
    public String ItemSubGroupKey_0 { get; set; }
    [HeaderName("%SelectRate1")]
    public String SelectRate_1 { get; set; }
    [HeaderName("%Condition1")]
    public String Condition_1 { get; set; }
    [HeaderName("ItemSubGroupKey1")]
    public String ItemSubGroupKey_1 { get; set; }
    [HeaderName("%SelectRate2")]
    public String SelectRate_2 { get; set; }
    [HeaderName("%Condition2")]
    public String Condition_2 { get; set; }
    [HeaderName("ItemSubGroupKey2")]
    public String ItemSubGroupKey_2 { get; set; }
    [HeaderName("%SelectRate3")]
    public String SelectRate_3 { get; set; }
    [HeaderName("%Condition3")]
    public String Condition_3 { get; set; }
    [HeaderName("ItemSubGroupKey3")]
    public String ItemSubGroupKey_3 { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
