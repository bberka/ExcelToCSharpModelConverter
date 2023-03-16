namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("ItemGroupDataTable_Sub_All")]
public ItemGroupDataTable_Sub_All : BaseSheetModel
{
    public Double ItemSubGroupKey { get; set; }
    public Double ItemKey { get; set; }
    [HeaderName("%EnchantLevel")]
    public Double EnchantLevel { get; set; }
    public Double DoPetAddDrop { get; set; }
    [HeaderName("%SelectRate_0")]
    public Double SelectRate__0 { get; set; }
    [HeaderName("%MinCount_0")]
    public Double MinCount__0 { get; set; }
    [HeaderName("%MaxCount_0")]
    public Double MaxCount__0 { get; set; }
    [HeaderName("%SelectRate_1")]
    public Double SelectRate__1 { get; set; }
    [HeaderName("%MinCount_1")]
    public Double MinCount__1 { get; set; }
    [HeaderName("%MaxCount_1")]
    public Double MaxCount__1 { get; set; }
    [HeaderName("%SelectRate_2")]
    public Double SelectRate__2 { get; set; }
    [HeaderName("%MinCount_2")]
    public Double MinCount__2 { get; set; }
    [HeaderName("%MaxCount_2")]
    public Double MaxCount__2 { get; set; }
    public String IntimacyVariation { get; set; }
    public String ExplorationPoint { get; set; }
    public Double ApplyRandomPrice { get; set; }
    public String RentTime { get; set; }
    public Double PriceOption { get; set; }
    [HeaderName("# ItemName")]
    public String ItemName { get; set; }
    [HeaderName("# 재료 1")]
    [InvalidValueType]
    public String _1 { get; set; }
    [HeaderName("# 재료 2")]
    [InvalidValueType]
    public String _2 { get; set; }
    [HeaderName("#^E_Merge Index")]
    public Double E_MergeIndex { get; set; }
    [HeaderName("Column25")]
    [InvalidValueType]
    public String Column_25 { get; set; }
    [HeaderName("Column26")]
    public Double Column_26 { get; set; }
    [HeaderName("DataSheet_ItemGroupDataTable_AlchemyStone.xlsm")]
    public String DataSheet_ItemGroupDataTable_AlchemyStonexlsm { get; set; }
}
