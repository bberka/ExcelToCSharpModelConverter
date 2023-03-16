namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("ItemSubGroup_Table")]
public ItemSubGroup_Table : BaseSheetModel
{
    public String ItemSubGroupKey { get; set; }
    [HeaderName("%ItemKey")]
    public String ItemKey { get; set; }
    [HeaderName("%EnchantLevel")]
    [InvalidValueType]
    public String EnchantLevel { get; set; }
    [InvalidValueType]
    public String DoPetAddDrop { get; set; }
    [InvalidValueType]
    public String DoSechiAddDrop { get; set; }
    [HeaderName("%SelectRate_0")]
    [InvalidValueType]
    public String SelectRate__0 { get; set; }
    [HeaderName("%MinCount_0")]
    [InvalidValueType]
    public String MinCount__0 { get; set; }
    [HeaderName("%MaxCount_0")]
    [InvalidValueType]
    public String MaxCount__0 { get; set; }
    [HeaderName("%SelectRate_1")]
    [InvalidValueType]
    public String SelectRate__1 { get; set; }
    [HeaderName("%MinCount_1")]
    [InvalidValueType]
    public String MinCount__1 { get; set; }
    [HeaderName("%MaxCount_1")]
    [InvalidValueType]
    public String MaxCount__1 { get; set; }
    [HeaderName("%SelectRate_2")]
    [InvalidValueType]
    public String SelectRate__2 { get; set; }
    [HeaderName("%MinCount_2")]
    [InvalidValueType]
    public String MinCount__2 { get; set; }
    [HeaderName("%MaxCount_2")]
    [InvalidValueType]
    public String MaxCount__2 { get; set; }
    [InvalidValueType]
    public String IntimacyVariation { get; set; }
    [InvalidValueType]
    public String ExplorationPoint { get; set; }
    [InvalidValueType]
    public String ApplyRandomPrice { get; set; }
    [InvalidValueType]
    public String RentTime { get; set; }
    [InvalidValueType]
    public String PriceOption { get; set; }
    [HeaderName("# DropItemName")]
    public String DropItemName { get; set; }
    [HeaderName("# MIDKey")]
    public String MIDKey { get; set; }
    [HeaderName("# Mon Name")]
    [InvalidValueType]
    public String MonName { get; set; }
    [HeaderName("# Title")]
    [InvalidValueType]
    public String Title { get; set; }
    [HeaderName("# Level")]
    [InvalidValueType]
    public String Level { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
