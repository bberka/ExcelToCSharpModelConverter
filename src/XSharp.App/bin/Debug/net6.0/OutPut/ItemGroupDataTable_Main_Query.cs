namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("ItemGroupDataTable_Main_Query")]
public ItemGroupDataTable_Main_Query : BaseSheetModel
{
    public Double ItemMainGroupKey { get; set; }
    [HeaderName("#Drop 구성")]
    public String Drop { get; set; }
    [HeaderName("#Drop 방식")]
    public String Drop { get; set; }
    [HeaderName("%DoSelectOnlyOne")]
    public Double DoSelectOnlyOne { get; set; }
    [HeaderName("%RefreshStartHour")]
    public String RefreshStartHour { get; set; }
    [HeaderName("%RefreshInterval")]
    public String RefreshInterval { get; set; }
    [HeaderName("%PlantCraftResultCount")]
    public Double PlantCraftResultCount { get; set; }
    [HeaderName("%SelectRate0")]
    public Double SelectRate_0 { get; set; }
    [HeaderName("%Condition0")]
    public String Condition_0 { get; set; }
    [HeaderName("ItemSubGroupKey0")]
    public Double ItemSubGroupKey_0 { get; set; }
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
    public Double E_MergeIndex { get; set; }
    [HeaderName("Column22")]
    [InvalidValueType]
    public String Column_22 { get; set; }
    [HeaderName("Column23")]
    [InvalidValueType]
    public String Column_23 { get; set; }
    [HeaderName("DataSheet_ItemGroupDataTable_AlchemyStone.xlsm")]
    public String DataSheet_ItemGroupDataTable_AlchemyStonexlsm { get; set; }
}
